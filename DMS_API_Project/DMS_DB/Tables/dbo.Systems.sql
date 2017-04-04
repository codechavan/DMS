CREATE TABLE [dbo].[Systems]
(
	[SystemId] NUMERIC IDENTITY CONSTRAINT PK_Systems_SystemId PRIMARY KEY,
	[SystemName] nvarchar(200) NOT NULL  CONSTRAINT UQ_Systems_SystemName UNIQUE, 
	[SystemIsActive] BIT NOT NULL CONSTRAINT DF_Systems_SystemIsActive DEFAULT (0),
    [SystemCreatedOn] DATETIME NOT NULL  CONSTRAINT DF_Systems_SystemCreatedOn DEFAULT GETDATE(),
    [SystemModifiedBy] NUMERIC NULL CONSTRAINT FK_Systems_SystemModifiedBy FOREIGN KEY REFERENCES [dbo].[Users]([UserId]),
    [SystemModifiedOn] DATETIME NULL
)
