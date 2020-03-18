param
(
    [Parameter(Mandatory=$True)]
    [string]
    $StrongNameKeyBase64String,

    [Parameter(Mandatory=$True)]
    [string]
    $StrongNameKeyOutputPath
)

# Turn off trace and stop on any error
Set-PSDebug -Off
$ErrorActionPreference = "Stop"

if ([string]::IsNullOrWhiteSpace($StrongNameKeyBase64String))
{
    throw [InvalidOperationException]::new("Cannot find the strong name key")
}

# Extract the bytes and write them to the file
$StrongNameKeyBytes = [System.Convert]::FromBase64String($StrongNameKeyBase64String)
[System.IO.File]::WriteAllBytes($StrongNameKeyOutputPath, $StrongNameKeyBytes)
