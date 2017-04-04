CREATE TABLE [dbo].[Configurations]
(
	[ConfigurationCode] VARCHAR(50) CONSTRAINT PK_Configurations_ConfigurationCode PRIMARY KEY,
	[ConfigurationValue] NVARCHAR(500),
	[ConfigurationDefaultValue] NVARCHAR(500),
	[ConfigurationDescription] NVARCHAR(500), 
    [Remarks] NVARCHAR(500) NULL,
	[ConfigurationModifiedBy] NUMERIC CONSTRAINT FK_Configurations_ConfigurationModifiedBy_SystemAdmins_AdminId FOREIGN KEY REFERENCES [dbo].[SystemAdmins]([AdminId]),
	[ConfigurationModifiedOn] Datetime,
)
