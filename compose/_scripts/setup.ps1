Write-Output "[*] Config local environment..."
#New-BashStyleAlias -name vault -command 'docker-compose exec vault vault "$args"'
$global:VAULT_ADDR="http://127.0.0.1:8200"

Function InitVault (){
    docker-compose exec vault vault operator init -address="$global:VAULT_ADDR" > ./_data/keys.txt
    ((Get-Content ./_data/keys.txt | Select-String -Pattern 'Initial Root Token:') -split ":\s+")[1].Replace("[0m","")
    # $VAULT_TOKEN=$(grep 'Initial Root Token:' ./_data/keys.txt | awk '{print substr($NF, 1, length($NF)-1)}')
}

Function UnsealVault () {
    
    $key1 = ((Get-Content ./_data/keys.txt | Select-String -Pattern 'Key 1:') -split ":\s+")[1].Replace("[0m","")
    docker-compose exec vault vault operator unseal -address="$global:VAULT_ADDR" $key1
    $key2 = ((Get-Content ./_data/keys.txt | Select-String -Pattern 'Key 2:') -split ":\s+")[1].Replace("[0m","")
    docker-compose exec vault vault operator unseal -address="$global:VAULT_ADDR" $key2
    $key3 = ((Get-Content ./_data/keys.txt | Select-String -Pattern 'Key 3:') -split ":\s+")[1].Replace("[0m","")
    docker-compose exec vault vault operator unseal -address="$global:VAULT_ADDR" $key3
}

Function Auth(){
    
    # Write-Output $global:VAULT_TOKEN
    docker-compose exec vault vault login -address="$global:VAULT_ADDR" $global:VAULT_TOKEN
}

function CreateWebUIUser {
    docker-compose exec vault vault auth-enable  -address="$global:VAULT_ADDR" userpass
    docker-compose exec vault vault policy-write -address="$global:VAULT_ADDR" admin ./config/admin.hcl
    docker-compose exec vault vault write -address="$global:VAULT_ADDR" auth/userpass/users/webui password=webui policies=admin
}

function CreateBackupToken {
    (((docker-compose exec vault vault token-create -address="$global:VAULT_ADDR" -display-name="backup_token" | Select-String -Pattern 'token  ') -split "\s+") -split "\s+")[1]
    # | awk '/token/{i++}i==2' | awk '{print "backup_token: " $2}' >> ./_data/keys.txt
}

function CreateMountAssessment {
    ## MOUNTS
    docker-compose exec vault vault mounts -address="$global:VAULT_ADDR"
    docker-compose exec vault vault mount  -address="$global:VAULT_ADDR" -path=assessment -description="Secrets used in the assessment" generic
    docker-compose exec vault vault write  -address="$global:VAULT_ADDR" assessment/server1_ad value1=name value2=pwd
}


Write-Output "[*] Init vault..."
$global:VAULT_TOKEN = InitVault 
Write-Output "[*] Unseal vault..."
UnsealVault
Write-Output "[*] Auth..."
Auth
Write-Output "[*] Create user... Remember to change the defaults!!"
CreateWebUIUser
Write-Output "[*] Create backup token..."
$global:BACKUP_TOKEN = CreateBackupToken
Write-Output "Backup token : $global:BACKUP_TOKEN"
Write-Output "[*] Creating new mount point..."
CreateMountAssessment