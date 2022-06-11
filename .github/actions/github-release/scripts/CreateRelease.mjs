const fs = await import('node:fs')

export default async function createRelease({ github, context, release }) {
  try {
    // Try to get the tag
    await github.rest.git.getRef({
      owner: context.repo.owner,
      repo: context.repo.repo,
      ref: `tags/${release.tag}`
    });

    console.log(`Tag ${release.tag} already exists`);
    return;
  }
  catch (httpError) {
    // A missing tag will throw a 404
    if (httpError.status == 404) {
      console.log(`Tag ${release.tag} does not exist`);
    }
    else {
      throw httpError;
    }
  }

  const commit = await github.rest.git.getCommit({
    owner: context.repo.owner,
    repo: context.repo.repo,
    commit_sha: context.sha
  });

  await github.rest.git.createTag({
    owner: context.repo.owner,
    repo: context.repo.repo,
    tag: release.tag,
    message: `${release.project} Version ${release.version}`,
    object: context.sha,
    type: 'commit',
    tagger: commit.author
  });

  await github.rest.git.createRef({
    owner: context.repo.owner,
    repo: context.repo.repo,
    ref: `refs/tags/${release.tag}`,
    sha: context.sha
  });

  const newRelease = await github.rest.repos.createRelease({
    owner: context.repo.owner,
    repo: context.repo.repo,
    tag_name: release.tag,
    name: `${release.project} ${release.version}`,
    prerelease: release.prerelease,
    draft: true
  });

  await github.rest.repos.uploadReleaseAsset({
    owner: context.repo.owner,
    repo: context.repo.repo,
    release_id: newRelease.id,
    name: release.name,
    data: fs.readFileSync(release.fullName),
    headers: {
      'content-type': contentType,
      'content-length': fs.statSync(release.fullName).size,
    },
  });
}
