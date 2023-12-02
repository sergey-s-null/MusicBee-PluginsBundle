function GetProjectByAlias
{
    param ($projects, $projectAlias)

    if ($projectAlias -eq $null)
    {
        Write-Warning "alias is null"
        return $null
    }

    $project = $projects.Where({ $_.alias -eq $projectAlias })
    if ($project.Count -eq 0)
    {
        Write-Warning "count 0"
        return $null;
    }

    if ($project.Count -ge 2)
    {
        $temp = $projects | ForEach-Object  { "`"$( $_.name )`"" }
        Write-Warning "Found multiple projects by alias `"$projectAlias`" ($( $project.Count )): $( $temp -join ", " )"
        return $null;
    }

    return $project
}
