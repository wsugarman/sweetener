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
  catch (httpEx) {
    // A missing tag will throw a 404
    if (httpEx.status && httpEx.status == 404) {
      console.log(`Tag ${release.tag} does not exist`);
    }
    else {
      throw httpEx;
    }
  }

  /*
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

  await github.rest.repos.createRelease({
    owner: context.repo.owner,
    repo: context.repo.repo,
    tag_name: release.tag,
    prerelease: release.prerelease,
    draft: true
  });
  */
}
