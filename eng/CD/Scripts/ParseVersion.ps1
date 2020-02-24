param
(
    [Parameter(Mandatory=$True)]
    [string]
    $ProjectName,

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
$Packages = @(Get-ChildItem -Path $PackageDirectory -Include "$ProjectName.*.nupkg" -Recurse)
if ($Packages.Length -eq 0)
{
    throw [InvalidOperationException]::new("Cannot find NuGet package for '$ProjectName'")
}

if ($Packages.Length -gt 1)
{
    throw [InvalidOperationException]::new("Found multiple possible '$ProjectName' NuGet packages")
}

# Use Regex to parse out version based on our own naming conventions
$PackageName = $Packages[0].Name
$Valid = $PackageName -match $ProjectName.Replace(".", "\.") + "\.(?<Version>\d+\.\d+\.\d+(?:-[a-zA-Z]+\.\d+)?)\.nupkg"
if (!$Valid)
{
    throw [InvalidOperationException]::new("Unexpected package name '$PackageName'")
}

$PackageVersion = $Matches.Version

# Check to see if Version file has changed
$VersionChanged = @(Invoke-Expression "git diff-tree --no-commit-id --name-only -r $SourceVersion") -contains "src/${{ parameters.Project }}/Version.json"
if ($VersionChanged)
{
    Write-Host "##vso[build.updatebuildnumber]$PackageVersion"
}
else
{
    Write-Host "##vso[build.updatebuildnumber]$PackageVersion (Skipped)"
}

Write-Host "##vso[task.setvariable variable=Package;isOutput=true]$PackageVersion"
Write-Host "##vso[task.setvariable variable=Changed;isOutput=true]$VersionChanged"