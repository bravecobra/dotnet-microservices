#!/bin/bash -xe
#dotnet dev-certs https -ep ../_data/apigateway/https/aspnetapp.pfx -p crypticpassword
sudo apt install libnss3-tools
curl -L https://github.com/FiloSottile/mkcert/releases/download/v1.4.0/mkcert-v1.4.0-linux-amd64 -o ./_scripts/mkcert
chmod 0700 ./_scripts/mkcert
mkdir -p ./_config/fabio/ssl/
./_scripts/mkcert -cert-file ./_config/fabio/ssl/localhost+2.pem, -key-file ./_config/fabio/ssl/localhost+2-key.pem localhost 127.0.0.1 ::1