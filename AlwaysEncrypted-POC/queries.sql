﻿

CREATE TABLE [dbo].[Client](
	[id] [uniqueidentifier] NOT NULL,
	[name] [nvarchar](250) NOT NULL,
	[birthdate] [datetime] NOT NULL,
	[rank] [tinyint] NOT NULL,
	[ccn] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Client] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


SELECT * FROM sys.column_master_keys
SELECT * FROM sys.column_encryption_keys

DROP COLUMN ENCRYPTION KEY myCEK
DROP COLUMN MASTER KEY myCMK

CREATE COLUMN MASTER KEY custom_CMK
    WITH (  
        KEY_STORE_PROVIDER_NAME = 'CUSTOM_KEY_STORE',  
        KEY_PATH = 'key_path'   
		)   

CREATE COLUMN ENCRYPTION KEY custom_CEK   
WITH VALUES  
  (  
    COLUMN_MASTER_KEY = custom_CMK,   
    ALGORITHM = 'algorithm_name',   
    ENCRYPTED_VALUE = 0xAAE6D671564C40BB97DA14704C2DD985C3694987EE40F7D8F2427D53CD5E6C585C41D986B3166B45C81B39A10677725B
  )   

DROP COLUMN ENCRYPTION KEY custom_CEK
DROP COLUMN MASTER KEY custom_CMK

