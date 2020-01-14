

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

