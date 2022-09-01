$rootca = Get-ChildItem cert:\CurrentUser\my | where-object {$_.Subject -match "CN=Poc Lab CA"}

#Path can be changed to 'cert:\CurrentUser\My\' if needed
New-SelfSignedCertificate -certstorelocation cert:\LocalMachine\My -dnsname localhost,$(hostname),'127.0.0.1' -Signer $rootca
New-SelfSignedCertificate -certstorelocation cert:\LocalMachine\My -dnsname client-grpc -Signer $rootca
New-SelfSignedCertificate -certstorelocation cert:\LocalMachine\My -dnsname client-http -Signer $rootca