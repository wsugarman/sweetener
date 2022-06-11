param
(
    [Parameter(Mandatory=$True)]
    [string]
    $ProjectName,

    [Parameter(Mandatory=$True)]
    [string]
    $Version,

    [Parameter(Mandatory=$True)]
    [string]
    $Tag,

    [Parameter(Mandatory=$True)]
    [string]
    $CommitSha
)

# Turn off trace and stop on any error
Set-PSDebug -Off
$ErrorActionPreference = "Stop"

# Determine who committed this release
$username = git log -n 1 --pretty=format:%an $CommitSha
$email = git log -n 1 --pretty=format:%ae $CommitSha

# Update the config to reflect this user
& git config user.name "$username"
& git config user.email "$email"

# Create the tag
& git tag -s -m "$ProjectName Version $Version" $Tag $CommitSha
& git push origin $Tag
