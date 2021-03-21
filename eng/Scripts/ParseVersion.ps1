param
(
    [Parameter(Mandatory=$True)]
    [string]
    $VersionFilePath,

    [Parameter(Mandatory=$False)]
    [Nullable[int]]
    $BuildId = $null
)

# Turn off trace and stop on any error
Set-PSDebug -Off
$ErrorActionPreference = "Stop"

$versionParts = Get-Content $VersionFilePath | Out-String | ConvertFrom-Json
$assemblyVersion = [string]::Format("{0}.0.0.0", $versionParts.major)

# A Continuous Integration (CI) build will use its optional suffix in the package version,
# while all other builds, presumably created through PRs, will instead append the incrementing
# build number to both the file and package version
if ($BuildId)
{
    $fileVersion    = [string]::Format("{0}.{1}.{2}.{3}"      , $versionParts.major, $versionParts.minor, $versionParts.patch, $BuildId)
    $packageVersion = [string]::Format("{0}.{1}.{2}-build.{3}", $versionParts.major, $versionParts.minor, $versionParts.patch, $BuildId)
}
else
{
    $fileVersion    = [string]::Format("{0}.{1}.{2}.0", $versionParts.major, $versionParts.minor, $versionParts.patch)
    $packageVersion = [string]::Format("{0}.{1}.{2}"  , $versionParts.major, $versionParts.minor, $versionParts.patch)
    if (![string]::IsNullOrWhiteSpace($versionParts.suffix))
    {
        $packageVersion += "-" + $versionParts.suffix
    }
}

# Output variables to be used in the build
Write-Host "##vso[task.setvariable variable=Assembly;isOutput=true]$assemblyVersion"
Write-Host "##vso[task.setvariable variable=File;isOutput=true]$fileVersion"
Write-Host "##vso[task.setvariable variable=Package;isOutput=true]$packageVersion"
