Write-Output "[*] Executing backup..."
$backup_token = Get-Content ./_data/backup.txt
docker-compose run --rm consul-backup consul-backup -i consul:8500 -t $backup_token backup_$(Get-date -Format yyyyMMddHHmm)
