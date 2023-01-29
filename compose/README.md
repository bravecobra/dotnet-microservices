# Docker Compose setup

## Description

### First time setup

We first want to start the `docker-compose` environment. To do so, cd into the compose directory, generate the certificate for the `apigateway`,on windows/macos trust the certificate, start `docker-compose`, then call the init script to initialize `vault` and seed it.

```powershell
cd compose
.\_scripts\apigateway-gen-certificate.ps1
dotnet dev-certs https --trust
docker-compose -d up
.\_scripts\init.ps1
```

### Backing up

Once up and running you can create a backup of the KV values of consul.

```powershell
cd compose
.\_scripts\consul-backup.ps1
```

This will create a backup file in `./compose/_data/consul/backup`

### Bring the environment down

Make sure you create a backup of `consul`'s KV before bringing down `docker-compose`. That's because `consul` will loose all KV value upon restart.

```powershell
cd compose
.\_scripts\consul-backup.ps1
docker-compose -d down
```

To bring it back up again as it was before, start `docker-compose` again, restore the backup and unseal `vault` with the keys, saved in the init step. You might need to remove the lock in consul under `vault/core/lock` as the backup also saves the `vault`'s lock entry

```powershell
cd compose
docker-compose -d up
.\_scripts\consul-restore.ps1
.\_scripts\vault-unseal.ps1
```
