param (
    [Parameter(Mandatory = $true)]
    [string]$ServiceNameInput
)

$ErrorActionPreference = 'Stop'

# ---- Find first .sln file ----
$solutionFile = Get-ChildItem -Filter *.sln | Select-Object -First 1

if (-not $solutionFile) {
    throw "No .sln file found in current directory."
}

$name = [System.IO.Path]::GetFileNameWithoutExtension($solutionFile.Name)

# ---- Convert snake_case to PascalCase ----
$pascalCase = ($ServiceNameInput -split '_') |
    ForEach-Object { $_.Substring(0,1).ToUpper() + $_.Substring(1) } |
    Join-String

$service = "${pascalCase}Service"
$folder  = $ServiceNameInput.ToLowerInvariant()

# ---- Create ABP module ----
abp new "$name.$service" -t module --no-ui -o (Join-Path 'services' $folder)

# ---- Remove unwanted projects ----
$pathsToRemove = @(
    '*.IdentityServer',
    '*.MongoDB.Tests',
    '*.MongoDB',
    '*.Host.Shared',
    '*.Installer'
)

foreach ($pattern in $pathsToRemove) {
    Get-ChildItem -Path (Join-Path 'services' $folder) -Recurse -Directory -Filter $pattern -ErrorAction SilentlyContinue |
        Remove-Item -Recurse -Force
}

# ---- Add projects to solution ----
Get-ChildItem -Path (Join-Path 'services' $folder) -Recurse -Filter *.csproj |
    ForEach-Object {
        dotnet sln $solutionFile.FullName add $_.FullName
    }
