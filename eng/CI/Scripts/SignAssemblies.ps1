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
$ProjectFile = [System.IO.Path]::Combine("src", $ProjectName, $ProjectName + ".csproj")
$Description = (Select-Xml -Path $ProjectFile -Xpath "/Project/PropertyGroup/Description").Node.InnerText
if ([string]::IsNullOrWhiteSpace($Description))
{
    throw [InvalidOperationException]::new("Cannot find 'Description' in $ProjectFile")
}

# Find all of the assemblies to sign
$BuildDirectory = [System.IO.Path]::Combine("src", $ProjectName, "bin", $BuildConfiguration)
$Assemblies = @(Get-ChildItem -Path $BuildDirectory -Include ($ProjectName + ".dll") -Recurse)
if ($Assemblies.Length -eq 0)
{
    throw [System.IO.FileNotFoundException]::new("Could not find any assemblies '$AssemblyName' in '$BuildDirectory'")
}

# Sign with Enhanced Strong Name
$StrongNameTool = [System.IO.Path]::Combine($NetFXTools, "sn.exe")
foreach ($AssemblyPath in $Assemblies)
{
    & $StrongNameTool -Ra $AssemblyPath $StrongNameKeyPath
}

# Sign Assemblies using AzureSignTool
& dotnet tool install "AzureSignTool" --tool-path $DotNetTools --configfile $NuGetConfig

$AzureSignTool = [System.IO.Path]::Combine($DotNetTools, "AzureSignTool.exe")
$Manifest      = [System.IO.Path]::Combine($BuildDirectory, $SignManifestName + ".txt")

# Write file manifest
$Assemblies | % { $_.FullName } | Out-File -FilePath $Manifest

# Sign assemblies
& $AzureSignTool sign `
  -du $ProjectUrl `
  -d $Description `
  -fd sha256 `
  -kvu $KeyVaultUrl `
  -kvi $KeyVaultClientId `
  -kvs $KeyVaultClientSecret `
  -kvc $KeyVaultCertificateName `
  -q `
  -ifl $Manifest

if (!$?)
{
    throw [Exception]::new("AzureSignTool returned exit code '$LastExitCode'")
}