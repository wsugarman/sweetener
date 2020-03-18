param
(
    [Parameter(Mandatory=$True)]
    [string]
    $StrongNameKeyOutputPath,
)

# Turn off trace and stop on any error
Set-PSDebug -Off
$ErrorActionPreference = "Stop"

# Assume the bytes have already been stored in a variable called "Sweetener-Strong-Name-Key"
if ([string]::IsNullOrWhiteSpace($(Sweetener-Strong-Name-Key)))
{
    throw [InvalidOperationException]::new("Cannot find the strong name key")
}

# Extract the bytes and write them to the file
$StrongNameKeyBytes = [System.Convert]::FromBase64String($(Sweetener-Strong-Name-Key))
[System.IO.File].WriteAllBytes($StrongNameKeyOutputPath, $StrongNameKeyBytes)
