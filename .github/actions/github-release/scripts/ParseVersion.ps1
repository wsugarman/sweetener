param
(
    [Parameter(Mandatory=$True)]
    [string]
    $ProjectName,

    [Parameter(Mandatory=$True)]
    [string]
    $PackageDirectory
)

# Turn off trace and stop on any error
Set-PSDebug -Off
$ErrorActionPreference = "Stop"

# Validate the package
$packages = @(Get-ChildItem -Path $PackageDirectory -Include "$ProjectName.*.nupkg" -Recurse)
if ($packages.Length -eq 0)
{
    throw [InvalidOperationException]::new("Cannot find NuGet package for '$ProjectName'")
}

if ($packages.Length -gt 1)
{
    throw [InvalidOperationException]::new("Found multiple NuGet packages that contain the name '$ProjectName'")
}

# Use Regex to parse out version based on our own naming conventions
$packageName = $packages[0].Name
$isValid = $packageName -match $ProjectName.Replace(".", "\.") + "\.(?<Version>\d+\.\d+\.\d+(?<Suffix>-[a-zA-Z]+\.\d+)?)\.nupkg"
if (!$isValid)
{
    throw [InvalidOperationException]::new("Unexpected package name '$packageName'")
}

$packageVersion = $Matches.Version
if ($Matches.Suffix)
{
    $prerelease = '--prerelease'
}


Write-Host "::set-output name=name::$packageName"
Write-Host "::set-output name=version::$packageVersion"
Write-Host "::set-output name=prerelease::$prerelease"
Write-Host "::set-output name=tag::$($ProjectName)_$($packageVersion)"
