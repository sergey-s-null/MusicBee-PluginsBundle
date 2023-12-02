$ErrorActionPreference = "Stop"

$json = Get-Content configuration.json | ConvertFrom-Json
$buildConfiguration = $json.configuration

$sourceDir = [System.IO.Path]::GetFullPath(".\Plugin.Main\bin\$buildConfiguration\net472")
$targetDirectory = ${Env:ProgramFiles(x86)} + "\MusicBee\Plugins"

$pattern = Join-Path $sourceDir "*.dll"
$filesToCopy = Get-ChildItem $pattern

Write-Output "Start copy dll's. Configuration: $buildConfiguration."
Write-Output "Copy $( $filesToCopy.Count ) files."
Write-Output "From `"$sourceDir`""
Write-Output "To `"$targetDirectory`""
Write-Output "Files:"
Write-Output $filesToCopy.Name | ForEach-Object { "    $_" }

Copy-Item $filesToCopy.FullName $targetDirectory

Write-Output "Dll's successfully copied"