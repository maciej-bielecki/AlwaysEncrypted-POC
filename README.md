# AlwaysEncrypted-POC


## Generating new certificate in windows certificate store, from then we can export it if needed or just look for thumbprint
$cert = New-SelfSignedCertificate -certstorelocation cert:\CurrentUser\my -dnsname maciej.bielecki.ae.test -KeyExportPolicy Exportable -Provider "Microsoft Enhanced RSA and AES Cryptographic Provider"
