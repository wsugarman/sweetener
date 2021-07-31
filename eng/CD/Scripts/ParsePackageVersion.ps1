param
(
    [Parameter(Mandatory=$True)]
    [string]
    $ProjectName,

    [Parameter(Mandatory=$True)]
    [string]
    $ProjectType,

    [Parameter(Mandatory=$True)]
    [string]
    $PackageDirectory,

    [Parameter(Mandatory=$True)]
    [string]
    $SourceVersion
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
    throw [InvalidOperationException]::new("Found multiple possible '$ProjectName' NuGet packages")
}

# Use Regex to parse out version based on our own naming conventions
$packageName = $packages[0].Name
$valid = $packageName -match $ProjectName.Replace(".", "\.") + "\.(?<Version>\d+\.\d+\.\d+(?:-[a-zA-Z]+\.\d+)?)\.nupkg"
if (!$valid)
{
    throw [InvalidOperationException]::new("Unexpected package name '$packageName'")
}

$packageVersion = $Matches.Version

# Check to see if Version file has changed
$versionChanged = @(& git diff-tree --no-commit-id --name-only -r $SourceVersion) -contains "src/$ProjectType/$ProjectName/Version.json"
if ($versionChanged)
{
    Write-Host "##vso[build.updatebuildnumber]$packageVersion"
}
else
{
    Write-Host "##vso[build.updatebuildnumber]$packageVersion (Skipped)"
}

Write-Host "##vso[task.setvariable variable=Package;isOutput=true]$packageVersion"
Write-Host "##vso[task.setvariable variable=Changed;isOutput=true]$versionChanged"