Write-Output "[*] Config local environment..."
$global:VAULT_ADDR="http://127.0.0.1:8200"

Function InitVault (){
    docker-compose exec vault vault operator init -address="$global:VAULT_ADDR" > ./_data/keys.txt
    ((Get-Content ./_data/keys.txt | Select-String -Pattern 'Initial Root Token:') -split ":\s+")[1].Replace("[0m","")
}

Write-Output "[*] Init vault..."
$global:VAULT_TOKEN = InitVault
& "$PSScriptRoot\vault-unseal.ps1"
& "$PSScriptRoot\vault-seed.ps1"
& "$PSScriptRoot\apigateway-gen-certificate.ps1"

