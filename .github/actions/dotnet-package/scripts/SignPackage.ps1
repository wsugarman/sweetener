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
    $BuildConfiguration,

    [Parameter(Mandatory=$True)]
    [string]
    $PackageVersion,

    [Parameter(Mandatory=$False)]
    [string]
    $KeyVaultCertificateName = 'William-Sugarman-Code-Signing',

    [Parameter(Mandatory=$False)]
    [string]
    $KeyVaultUrl = 'https://sugarman-keyvault.vault.azure.net/',

    [Parameter(Mandatory=$False)]
    [string]
    $TimestampUrl = "http://timestamp.digicert.com",

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

# Sign Package using NuGetKeyVaultSignTool
& dotnet tool install "NuGetKeyVaultSignTool" --version "3.2.2" --tool-path $DotNetTools --configfile $NuGetConfig

$nuGetKeyVaultSignTool = [System.IO.Path]::Combine($DotNetTools, "NuGetKeyVaultSignTool.exe")
$package               = [System.IO.Path]::Combine("src", $ProjectType, $ProjectName, "bin", $BuildConfiguration, "$ProjectName.$PackageVersion.nupkg")

if (![System.IO.File]::Exists($package))
{
    throw [System.IO.FileNotFoundException]::new("Cannot find '$package'")
}

& $nuGetKeyVaultSignTool sign $package `
  -fd sha256 `
  -tr $TimestampUrl `
  -td sha256 `
  -kvu $KeyVaultUrl `
  -kvt $KeyVaultTenantId `
  -kvi $KeyVaultClientId `
  -kvs $KeyVaultClientSecret `
  -kvc $KeyVaultCertificateName

if (!$?)
{
    throw [Exception]::new("NuGetKeyVaultSignTool returned exit code '$LastExitCode'")
}
