export default async function createRelease({ github, context, release }) {
  const tag = await github.rest.git.getRef({
    owner: context.repo.owner,
    repo: context.repo.repo,
    ref: `tags/${release.tag}`
  });

  // If the tag already exists, exit prematurely
  if (tag) {
    console.log(`Tag ${release.tag} already exists`);
    return;
  }
  else
  {
    console.log(`Tag ${release.tag} does not exist`);
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
