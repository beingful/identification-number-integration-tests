param($source, $accessToken)

$workingDirectory = Split-Path -Path $pwd -Parent
$outputFile = "$workingDirectory\swagger.json"

Write-Output "Trying to save API's swagger documentation to the local file"

$headers = @{
    "Authorization" = "Bearer $accessToken"
}

Invoke-RestMethod -Uri $source -Headers $headers -OutFile $outputFile

Write-Output "Done"