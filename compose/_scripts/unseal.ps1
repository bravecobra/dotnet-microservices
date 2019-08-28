$global:VAULT_ADDR="http://127.0.0.1:8200"
Function UnsealVault () {
    
    $key1 = ((Get-Content ./_data/keys.txt | Select-String -Pattern 'Key 1:') -split ":\s+")[1].Replace("[0m","")
    docker-compose exec vault vault operator unseal -address="$global:VAULT_ADDR" $key1
    $key2 = ((Get-Content ./_data/keys.txt | Select-String -Pattern 'Key 2:') -split ":\s+")[1].Replace("[0m","")
    docker-compose exec vault vault operator unseal -address="$global:VAULT_ADDR" $key2
    $key3 = ((Get-Content ./_data/keys.txt | Select-String -Pattern 'Key 3:') -split ":\s+")[1].Replace("[0m","")
    docker-compose exec vault vault operator unseal -address="$global:VAULT_ADDR" $key3
}
Write-Output "[*] Unseal vault..."
UnsealVault