param(
    [Parameter(Position=0,mandatory=$true)]
    [string] $backupFile
    )

Write-Output "[*] Executing restore..."
$backup_token = Get-Content ./_data/backup.txt
docker-compose run consul-backup consul-backup -i consul:8500 -r -t $backup_token  $backupFile
