#dotnet dev-certs https -ep ..\_data\apigateway\https\aspnetapp.pfx -p crypticpassword
# dotnet dev-certs https --trust -q
if (!(Test-Path -Path "$PSScriptRoot\mkcert.exe")) {
    Write-Output "Downloading mkcert.exe v1.4.0"
    $url = "https://github.com/FiloSottile/mkcert/releases/download/v1.4.0/mkcert-v1.4.0-windows-amd64.exe"
    $output = "$PSScriptRoot\mkcert.exe"
    $start_time = Get-Date
    (New-Object System.Net.WebClient).DownloadFile($url, $output)
    Write-Output "Time taken: $((Get-Date).Subtract($start_time).Seconds) second(s)"
}

#Creating new certificates for fabio running on localhost
if (!(Test-Path -Path "$PSScriptRoot\..\_config\fabio\ssl\")) {
    New-Item -ItemType directory -Path .\_config\fabio\ssl
}
#$env:CAROOT = "$PSScriptRoot\..\_config\fabio\ssl\"
.\_scripts\mkcert.exe -cert-file ./_config/fabio/ssl/localhost+2.pem, -key-file ./_config/fabio/ssl/localhost+2-key.pem localhost 127.0.0.1 ::1
Write-Output "Run '.\compose\_scripts\mkcert.exe -install' as Administrator to install the root certificate."