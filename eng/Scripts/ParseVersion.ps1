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

$VersionParts = Get-Content $VersionFilePath | Out-String | ConvertFrom-Json
$AssemblyVersion = [string]::Format("{0}.0.0.0", $VersionParts.major)

# A Continuous Integration (CI) build will use its optional suffix in the package version,
# while all other builds, presumably created through PRs, will instead append the incrementing
# build number to both the file and package version
if ($BuildId)
{
    $FileVersion    = [string]::Format("{0}.{1}.{2}.{3}"      , $VersionParts.major, $VersionParts.minor, $VersionParts.patch, $BuildId)
    $PackageVersion = [string]::Format("{0}.{1}.{2}-build.{3}", $VersionParts.major, $VersionParts.minor, $VersionParts.patch, $BuildId)
}
else
{
    $FileVersion    = [string]::Format("{0}.{1}.{2}.0", $VersionParts.major, $VersionParts.minor, $VersionParts.patch)
    $PackageVersion = [string]::Format("{0}.{1}.{2}"  , $VersionParts.major, $VersionParts.minor, $VersionParts.patch)
    if (![string]::IsNullOrWhiteSpace($VersionParts.suffix))
    {
        $PackageVersion += "-" + $VersionParts.suffix
    }
}

# Output variables to be used in the build
Write-Host "##vso[task.setvariable variable=Assembly;isOutput=true]$AssemblyVersion"
Write-Host "##vso[task.setvariable variable=File;isOutput=true]$FileVersion"
Write-Host "##vso[task.setvariable variable=Package;isOutput=true]$PackageVersion"
