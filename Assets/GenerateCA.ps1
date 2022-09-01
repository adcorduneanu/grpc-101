$rootcert = New-SelfSignedCertificate -CertStoreLocation cert:\CurrentUser\My -DnsName "Poc Lab CA" -KeyUsage CertSign -TextExtension @("2.5.29.19={text}cA=true&pathLength=0")

Write-host "Certificate Thumbprint: $($rootcert.Thumbprint)"

Export-Certificate -Cert $rootcert -FilePath .\poc_lab_ca.cer

Import-Certificate -FilePath .\poc_lab_ca.cer -CertStoreLocation Cert:\LocalMachine\Root
