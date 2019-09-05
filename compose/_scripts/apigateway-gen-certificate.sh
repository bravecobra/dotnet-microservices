#!/bin/bash -xe
dotnet dev-certs https -ep ../_data/apigateway/https/aspnetapp.pfx -p crypticpassword
