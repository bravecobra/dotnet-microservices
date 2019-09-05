Write-Output "[*] Config local environment..."
$global:VAULT_ADDR="http://127.0.0.1:8200"

Function InitVault (){
    ((Get-Content ./_data/keys.txt | Select-String -Pattern 'Initial Root Token:') -split ":\s+")[1].Replace("[0m","")
}
Function Auth(){
    Write-Output "[*] Auth..."
    docker-compose exec vault vault login -address="$global:VAULT_ADDR" $global:VAULT_TOKEN
}

function CreateWebUIUser {
    Write-Output "[*] Create user... Remember to change the defaults!!"
    docker-compose exec vault vault auth enable  -address="$global:VAULT_ADDR" userpass
    docker-compose exec vault vault policy write -address="$global:VAULT_ADDR" admin ./config/admin.hcl
    docker-compose exec vault vault write -address="$global:VAULT_ADDR" auth/userpass/users/webui password=webui policies=admin
}

function CreateBackupToken {
    Write-Output "[*] Create backup token..."
    $global:BACKUP_TOKEN = (((docker-compose exec vault vault token create -address="$global:VAULT_ADDR" -display-name="backup_token" | Select-String -Pattern 'token  ') -split "\s+") -split "\s+")[1]
    Write-Output "Backup token : $global:BACKUP_TOKEN"
    Write-Output "$global:BACKUP_TOKEN" > ./_data/backup.txt
    Write-Output "[*] Creating new mount point..."
}

function CreateMountAssessment {
    ## MOUNTS
    docker-compose exec vault vault secrets list -address="$global:VAULT_ADDR"
    docker-compose exec vault vault secrets enable  -address="$global:VAULT_ADDR" -path=assessment -description="Secrets used in the assessment" generic
    docker-compose exec vault vault write  -address="$global:VAULT_ADDR" assessment/server1_ad value1=name value2=pwd
}

$global:VAULT_TOKEN = InitVault
Auth
CreateWebUIUser
CreateBackupToken
CreateMountAssessment