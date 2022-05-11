param
(
    [Parameter(Mandatory=$True)]
    [string]
    $PackageName,

    [Parameter(Mandatory=$True)]
    [string]
    $PackDirectory,

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

$packagePath = [System.IO.Path]::Combine($PackDirectory, $PackageName + ".nupkg")
if (![System.IO.File]::Exists($packagePath))
{
    throw [System.IO.FileNotFoundException]::new("Cannot find '$packagePath'")
}

# Sign Package using NuGetKeyVaultSignTool
& dotnet tool install "NuGetKeyVaultSignTool" --version "3.2.2" --tool-path $DotNetTools --configfile $NuGetConfig

$nuGetKeyVaultSignTool = [System.IO.Path]::Combine($DotNetTools, "NuGetKeyVaultSignTool.exe")
& $nuGetKeyVaultSignTool sign $packagePath `
  -fd sha256 `
  -tr $TimestampUrl `
  -td sha256 `
  -kvu $KeyVaultUrl `
  -kvc $KeyVaultCertificateName `
  -kvm

if (!$?)
{
    throw [Exception]::new("NuGetKeyVaultSignTool returned exit code '$LastExitCode'")
}

# Sign the Symbol Package too (if present)
$symbolPackagePath = [System.IO.Path]::Combine($PackDirectory, $PackageName + ".snupkg")
if ([System.IO.File]::Exists($symbolPackagePath))
{
    & $nuGetKeyVaultSignTool sign $symbolPackagePath `
      -fd sha256 `
      -tr $TimestampUrl `
      -td sha256 `
      -kvu $KeyVaultUrl `
      -kvc $KeyVaultCertificateName `
      -kvm

    if (!$?)
    {
        throw [Exception]::new("NuGetKeyVaultSignTool returned exit code '$LastExitCode'")
    }
}
