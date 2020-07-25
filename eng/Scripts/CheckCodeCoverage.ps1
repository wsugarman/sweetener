param
(
    [Parameter(Mandatory=$True)]
    [string]
    $TestResultsFolder,

    [Parameter(Mandatory=$False)]
    [Nullable[double]]
    $LineCoverageThreshold = $null,

    [Parameter(Mandatory=$False)]
    [Nullable[double]]
    $BranchCoverageThreshold = $null,

    [Parameter(Mandatory=$False)]
    [string]
    $CoberturaCoverageFileName = "coverage.cobertura.xml"
)

# Turn off trace and stop on any error
Set-PSDebug -Off
$ErrorActionPreference = "Stop"

# TODO: Pass thresholds to coverlet collector when supported
$CodeCoveragePath = [System.IO.Path]::Combine($TestResultsFolder, $CoberturaCoverageFileName)
[xml]$CodeCoverageXML = Get-Content $CodeCoveragePath

# Line Coverage
if ($LineCoverageThreshold)
{
    if (($LineCoverageThreshold -lt 0) -or ($LineCoverageThreshold -gt 1))
    {
        throw [ArgumentOutOfRangeException]::new("LineCoverageThreshold", $LineCoverageThreshold, "Line coverage threshold must be between 0 and 1")
    }

    $LineCoverage = Select-Xml -Xml $CodeCoverageXML -XPath "/coverage/@line-rate" | % {[double]::Parse($_.Node.Value)}
    if ($LineCoverage -lt $LineCoverageThreshold)
    {
        throw "Line coverage {0:P} failed to meet threshold {1:P}" -f $LineCoverage, $LineCoverageThreshold
    }
}

# Branch Coverage
if ($BranchCoverageThreshold)
{
    if (($BranchCoverageThreshold -lt 0) -or ($BranchCoverageThreshold -gt 1))
    {
        throw [ArgumentOutOfRangeException]::new("BranchCoverageThreshold", $BranchCoverageThreshold, "Branch coverage threshold must be between 0 and 1")
    }

    $BranchCoverage = Select-Xml -Xml $CodeCoverageXML -XPath "/coverage/@branch-rate" | % {[double]::Parse($_.Node.Value)}
    if ($BranchCoverage -lt $BranchCoverageThreshold)
    {
        throw "Branch coverage {0:P} failed to meet threshold {1:P}" -f $BranchCoverage, $BranchCoverageThreshold
    }
}
