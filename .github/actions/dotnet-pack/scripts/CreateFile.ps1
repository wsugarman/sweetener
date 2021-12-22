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

if ([string]::IsNullOrWhiteSpace($OutputFilePath))
{
    throw [InvalidOperationException]::new("No output file specified")
}

# Extract the bytes and write them to the file
$fileBytes = [System.Convert]::FromBase64String($Base64StringContent)
New-Item -ItemType 'file' -Path $OutputFilePath -Force | Out-Null
[System.IO.File]::WriteAllBytes($OutputFilePath, $fileBytes)
