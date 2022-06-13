const cp = await import('node:child_process');
const fs = await import('node:fs');
const path = await import('node:path');

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

  // Create the tag for the release
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

  // Create the release
  const name = path.basename(release.folder);
  const zipPath = path.join(release.folder, '..', `${name}.zip`);
  const zipOutput = cp.execSync(`zip -r ../${name}.zip .`, { cwd: release.folder, encoding: 'utf8' });
  process.stdout.write(zipOutput);

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
    release_id: newRelease.data.id,
    name: `${name}.zip`,
    data: fs.readFileSync(zipPath),
    headers: {
      'content-type': 'application/zip',
      'content-length': fs.statSync(zipPath).size,
    }
  });
}
