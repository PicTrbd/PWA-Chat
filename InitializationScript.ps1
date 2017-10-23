Write-Host "`n###################################################################" -ForegroundColor Yellow
Write-Host "STARTING " -ForegroundColor Yellow
Write-Host "###################################################################`n" -ForegroundColor Yellow

Start-Process -FilePath "https://aka.ms/devicelogin"

Write-Host "`nCopy & Paster the code just below in the web page just opened ...`n" -ForegroundColor Red

Az Login

Write-Host "`nWhich subscription would you like to use ?  " -ForegroundColor Green -NoNewline

$Subscription = Read-Host 

Az Account Set --subscription $Subscription

Write-Host "`n###################################################################" -ForegroundColor Yellow
Write-Host "RESOURCE GROUP CREATION " -ForegroundColor Yellow
Write-Host "###################################################################`n" -ForegroundColor Yellow

Az Group Create --location "West Europe" --name "pwachatpushterraform"

Write-Host "`n###################################################################" -ForegroundColor Yellow
Write-Host "STORAGE ACCOUNT CREATION " -ForegroundColor Yellow
Write-Host "###################################################################`n" -ForegroundColor Yellow

Az Storage Account Create --name "terraformstorageacc" --resource-group "pwachatpushterraform" --sku "Standard_LRS"

Write-Host "`n###################################################################" -ForegroundColor Yellow
Write-Host "STORAGE ACCOUNT CONNECTION STRING " -ForegroundColor Yellow
Write-Host "###################################################################`n" -ForegroundColor Yellow

Az Storage Account show-connection-string --name "terraformstorageacc" --resource-group "pwachatpushterraform"

Write-Host  "`nCopy & Paste the connection string above :  " -ForegroundColor Green

$ConnectionString = Read-Host 

Az Storage Container Create -n "terraform-state-storage" --connection-string $ConnectionString

Az Storage Account Keys List -g "pwachatpushterraform" -n "terraformstorageacc"

Write-Host  "`nCopy & Paste the Access Key above :  " -ForegroundColor Green

$AccessKey = Read-Host 

Write-Host .\terraform.exe init "access_key=$AccessKey"
cd Terraform
.\terraform.exe init -backend-config "access_key=$AccessKey"

Write-Host "`n###################################################################" -ForegroundColor Magenta
Write-Host "YOU ARE DONE !" -ForegroundColor Magenta
Write-Host "###################################################################`n" -ForegroundColor Magenta