$utilPath = [System.IO.Path]::Combine($Env:UserProfile, ".nuget", "packages", "entityframework", "6.4.4", "tools", "net45", "win-x86", "ef6.exe")
& $utilPath $args