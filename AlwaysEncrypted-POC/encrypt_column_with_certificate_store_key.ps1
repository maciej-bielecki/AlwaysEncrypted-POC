
#Import the SqlServer PowerShell module.
Import-Module "SqlServer"

# Connect to your database.
$serverName = "localhost\SQLEXPRESS"
$databaseName = "AlwaysEncryptedPOC"

$connStr = "Server = " + $serverName + "; Database = " + $databaseName + "; Integrated Security = True"
$connection = New-Object Microsoft.SqlServer.Management.Common.ServerConnection
$connection.ConnectionString = $connStr
$connection.Connect()

$server = New-Object Microsoft.SqlServer.Management.Smo.Server($connection)
$database = $server.Databases[$databaseName] 

# Create a SqlColumnMasterKeySettings object that contains information about the location of your column master key.
$certificateThumbprint = "636120dce1892f1bf044f71047f27aef32fb72ba";
$cmkSettings = New-SqlCertificateStoreColumnMasterKeySettings -CertificateStoreLocation "CurrentUser" -Thumbprint $certificateThumbprint

#Create the metadata about the column master key in your database. $cmkName need to be provided
$cmkName = "myCMK"
New-SqlColumnMasterKey -Name $cmkName -InputObject $database -ColumnMasterKeySettings $cmkSettings

#Create the column encryption key metadata in the database. $cekName need to be provided (optionally EncryptedValue can be provided)
$cekName = "myCEK"
New-SqlColumnEncryptionKey -Name $cekName -InputObject $database -ColumnMasterKeyName $cmkName


# Change encryption schema
$encryptionChanges = @()

# Add changes for table [dbo].[Employee_Information]
$columnName = "dbo.Client.ccn"
$encryptionChanges += New-SqlColumnEncryptionSettings -ColumnName $columnName -EncryptionType Deterministic -EncryptionKey $cekName

Set-SqlColumnEncryption -ColumnEncryptionSettings $encryptionChanges -InputObject $database
