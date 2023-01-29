#!/bin/bash -xe
## CONFIG LOCAL ENV
echo "[*] Config local environment..."
alias vault='docker-compose exec vault vault "$@"'
export VAULT_ADDR=http://127.0.0.1:8200

## INIT VAULT
echo "[*] Init vault..."
vault init -address=${VAULT_ADDR} > ./_data/keys.txt
export VAULT_TOKEN=$(grep 'Initial Root Token:' ./_data/keys.txt | awk '{print substr($NF, 1, length($NF)-1)}')

/bin/bash ./_scripts/vault-unseal.sh
/bin/bash ./_scripts/vault-seed.sh
/bin/bash ./_scripts/apigateway-gen-certificate.sh
