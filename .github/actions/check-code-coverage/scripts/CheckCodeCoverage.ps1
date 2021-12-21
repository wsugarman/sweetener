param
(
    [Parameter(Mandatory=$True)]
    [string]
    $CodeCoverageFile,

    [Parameter(Mandatory=$False)]
    [Nullable[double]]
    $LineCoverageThreshold = $null,

    [Parameter(Mandatory=$False)]
    [Nullable[double]]
    $BranchCoverageThreshold = $null
)

# Turn off trace and stop on any error
Set-PSDebug -Off
$ErrorActionPreference = "Stop"

# TODO: Pass thresholds to coverlet collector when supported
[xml]$codeCoverageXML = Get-Content $CodeCoverageFile

# Line Coverage
if ($LineCoverageThreshold)
{
    if (($LineCoverageThreshold -lt 0) -or ($LineCoverageThreshold -gt 1))
    {
        throw [ArgumentOutOfRangeException]::new("LineCoverageThreshold", $LineCoverageThreshold, "Line coverage threshold must be between 0 and 1")
    }

    $lineCoverage = Select-Xml -Xml $codeCoverageXML -XPath "/coverage/@line-rate" | % {[double]::Parse($_.Node.Value)}
    if ($lineCoverage -lt $LineCoverageThreshold)
    {
        throw "{0:P} line coverage failed to meet threshold {1:P}" -f $lineCoverage, $LineCoverageThreshold
    }
    else
    {
        Write-Host ("Passed with {0:P} line coverage!" -f $lineCoverage)
    }
}

# Branch Coverage
if ($BranchCoverageThreshold)
{
    if (($BranchCoverageThreshold -lt 0) -or ($BranchCoverageThreshold -gt 1))
    {
        throw [ArgumentOutOfRangeException]::new("BranchCoverageThreshold", $BranchCoverageThreshold, "Branch coverage threshold must be between 0 and 1")
    }

    $branchCoverage = Select-Xml -Xml $codeCoverageXML -XPath "/coverage/@branch-rate" | % {[double]::Parse($_.Node.Value)}
    if ($branchCoverage -lt $BranchCoverageThreshold)
    {
        throw "{0:P} branch coverage failed to meet threshold {1:P}" -f $branchCoverage, $BranchCoverageThreshold
    }
    else
    {
        Write-Host ("Passed with {0:P} branch coverage!" -f $branchCoverage)
    }
}
