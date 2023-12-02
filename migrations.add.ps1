$ErrorActionPreference = "Stop"

. .\migrations_common.ps1

$configurationJson = Get-Content configuration.json | ConvertFrom-Json
$buildConfiguration = $configurationJson.configuration
$projects = $configurationJson."ef6-projects"

$projectAlias = $args[0]
$migrationName = $args[1]
$additionalArgs = $args | Select-Object -Skip 2

$project = GetProjectByAlias $projects $projectAlias

if ($project -eq $null)
{
    $temp = $projects.alias | ForEach-Object { "`"$( $_ )`"" }
    Write-Error "Could not find project by alias `"$projectAlias`". Available aliases: $( $temp -join ", " )."
    exit -1
}

$assemblyPath = "$( $project."project-directory" )/bin/$buildConfiguration/net472/$( $project.name ).dll"

.\ef6.ps1 migrations add `
    --project-dir $project."project-directory" `
    --assembly $assemblyPath `
    $migrationName `
    $additionalArgs