param
(
    [Parameter(Mandatory=$True)]
    [string]
    $FilePath
)

# Turn off trace and stop on any error
Set-PSDebug -Off
$ErrorActionPreference = "Stop"

if (![System.IO.File]::Exists($FilePath))
{
    throw [System.IO.FileNotFoundException]::new("Cannot find file '$FilePath'")
}

[System.IO.File]::Delete($FilePath)
