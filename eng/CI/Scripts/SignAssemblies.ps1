param
(
    [Parameter(Mandatory=$True)]
    [string]
    $ProjectName,

    [Parameter(Mandatory=$True)]
    [string]
    $BuildConfiguration,

    [Parameter(Mandatory=$True)]
    [string]
    $StrongNameKeyPath,

    [Parameter(Mandatory=$True)]
    [string]
    $KeyVaultUrl,

    [Parameter(Mandatory=$True)]
    [string]
    $KeyVaultCertificateName,

    [Parameter(Mandatory=$True)]
    [string]
    $KeyVaultClientId,

    [Parameter(Mandatory=$True)]
    [string]
    $KeyVaultClientSecret,

    [Parameter(Mandatory=$False)]
    [string]
    $ProjectUrl = "https://wsugarman.github.io/Sweetener/",

    [Parameter(Mandatory=$False)]
    [string]
    $TimestampUrl = "http://timestamp.digicert.com",

    [Parameter(Mandatory=$False)]
    [string]
    $SignManifestName = "SignManifest",

    [Parameter(Mandatory=$False)]
    [string]
    $NetFXTools = "C:/Program Files (x86)/Microsoft SDKs/Windows/v10.0A/bin/NETFX 4.8 Tools",

    [Parameter(Mandatory=$False)]
    [string]
    $DotNetTools = "tools",

    [Parameter(Mandatory=$False)]
    [string]
    $NuGetConfig = "NuGet.config"
)

# Turn off trace and stop on any error
Set-PSDebug -Off
$ErrorActionPreference = "Stop"

# Extract the description from the project file
$projectFile = [System.IO.Path]::Combine("src", $ProjectName, $ProjectName + ".csproj")
$description = (Select-Xml -Path $projectFile -Xpath "/Project/PropertyGroup/Description").Node.InnerText
if ([string]::IsNullOrWhiteSpace($description))
{
    throw [InvalidOperationException]::new("Cannot find 'Description' in $projectFile")
}

# Find all of the assemblies to sign
$buildDirectory = [System.IO.Path]::Combine("src", $ProjectName, "bin", $BuildConfiguration)
$assemblies = @(Get-ChildItem -Path $buildDirectory -Include ($ProjectName + ".dll") -Recurse)
if ($assemblies.Length -eq 0)
{
    throw [System.IO.FileNotFoundException]::new("Could not find any assemblies '$AssemblyName' in '$buildDirectory'")
}

# Sign with Enhanced Strong Name
$strongNameTool = [System.IO.Path]::Combine($NetFXTools, "sn.exe")
foreach ($AssemblyPath in $assemblies)
{
    & $strongNameTool -Ra $AssemblyPath $StrongNameKeyPath
}

# Sign Assemblies using AzureSignTool
& dotnet tool install "AzureSignTool" --version "2.0.17" --tool-path $DotNetTools --configfile $NuGetConfig

$azureSignTool = [System.IO.Path]::Combine($DotNetTools, "AzureSignTool.exe")
$manifest      = [System.IO.Path]::Combine($buildDirectory, $SignManifestName + ".txt")

# Write file manifest
$assemblies | ForEach-Object { $_.FullName } | Out-File -FilePath $manifest

# Sign assemblies
& $azureSignTool sign `
  -du $ProjectUrl `
  -d $description `
  -fd sha256 `
  -tr $TimestampUrl `
  -td sha256 `
  -kvu $KeyVaultUrl `
  -kvi $KeyVaultClientId `
  -kvs $KeyVaultClientSecret `
  -kvc $KeyVaultCertificateName `
  -q `
  -ifl $manifest

if (!$?)
{
    throw [Exception]::new("AzureSignTool returned exit code '$LastExitCode'")
}