#!/bin/bash -xe
echo "[*] Executing restore..."
docker-compose run backup consul-backup -i consul:8500 -r -t $(grep 'backup_token:' ./_data/keys.txt | awk -v RS='\r\n' '{printf $2}') $1
