$workingDirectory = Split-Path -Path $pwd -Parent
$apiModelDocumentationFile = "$workingDirectory\swagger.json"
$outputDirectory = "$workingDirectory\Api"
$targetFolder = ".\Api"

Write-Output "Trying to generate API models"

java -Dmodels -DapiTests=fals -DmodelDocs=false -DmodelTests=false `
-jar swagger-codegen-cli.jar generate -i $apiModelDocumentationFile -l csharp -o $outputDirectory `
--additional-properties optionalProjectFile=false,packageName=Swagger,sourceFolder=.$targetFolder

Write-Output "Done"