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

Az Group Create --location "West Europe" --name "pwachatpush"

Write-Host "`n###################################################################" -ForegroundColor Yellow
Write-Host "STORAGE ACCOUNT CREATION " -ForegroundColor Yellow
Write-Host "###################################################################`n" -ForegroundColor Yellow

Az Storage Account Create --name "terraformstorageacc" --resource-group "pwachatpush" --sku "Standard_LRS"

Write-Host "`n###################################################################" -ForegroundColor Yellow
Write-Host "STORAGE ACCOUNT CONNECTION STRING " -ForegroundColor Yellow
Write-Host "###################################################################`n" -ForegroundColor Yellow

Az Storage Account show-connection-string --name "terraformstorageacc" --resource-group "pwachatpush"

Write-Host  "`nCopy & Paste the connection string above :  " -ForegroundColor Green

$ConnectionString = Read-Host 

Az Storage Container Create -n "terraform-state-storage" --connection-string $ConnectionString

Az Storage Account Keys List -g "pwachatpush" -n "terraformstorageacc"

Write-Host  "`nYou are almost done, should now copy & paste the access key above on the Release Variables tab in VSTS" -ForegroundColor Green

Write-Host "`n###################################################################" -ForegroundColor Magenta
Write-Host "YOU ARE DONE !" -ForegroundColor Magenta
Write-Host "###################################################################`n" -ForegroundColor Magenta