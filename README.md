# AlwaysEncrypted-POC

This POC project contains simple application that can save and load some data to database.
It also contains examples of setting up keys, providers to use in always encrypted technology.
For more info about technology and setting it up go to: https://docs.microsoft.com/en-us/sql/relational-databases/security/encryption/always-encrypted-database-engine?view=sql-server-ver15

###### Way to generate new certificate in windows certificate store, from then we can export it if needed or just look for thumbprint to use in CMK provisioning:
 
```
$cert = New-SelfSignedCertificate -certstorelocation cert:\CurrentUser\my -dnsname maciej.bielecki.ae.test -KeyExportPolicy Exportable -Provider "Microsoft Enhanced RSA and AES Cryptographic Provider"
```
