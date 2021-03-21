param
(
    [Parameter(Mandatory=$True)]
    [string]
    $Base64StringContent,

    [Parameter(Mandatory=$True)]
    [string]
    $OutputFilePath
)

# Turn off trace and stop on any error
Set-PSDebug -Off
$ErrorActionPreference = "Stop"

if ([string]::IsNullOrWhiteSpace($Base64StringContent))
{
    throw [InvalidOperationException]::new("No file content specified")
}

# Extract the bytes and write them to the file
$fileBytes = [System.Convert]::FromBase64String($Base64StringContent)
[System.IO.File]::WriteAllBytes($OutputFilePath, $fileBytes)
