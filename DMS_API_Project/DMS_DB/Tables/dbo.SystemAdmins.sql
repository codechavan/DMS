CREATE TABLE [dbo].[SystemAdmins]
(
	[AdminId] NUMERIC IDENTITY CONSTRAINT PK_SystemAdmin_AdminId PRIMARY KEY,
	[UserName] NVARCHAR(50) NOT NULL, 
	[Password] NVARCHAR(100) NOT NULL,
    [FullName] NVARCHAR(150) NOT NULL, 
    [EmailId] NVARCHAR(100) NOT NULL, 
    [LastLogin] DATETIME, 
    [LastPasswordChangedOn] DATETIME NULL,
    [LastPasswordChangedBy] NUMERIC CONSTRAINT FK_SystemAdmins_LastPasswordChangedBy FOREIGN KEY REFERENCES [dbo].[SystemAdmins]([AdminId]),
	[CreatedBy] NUMERIC CONSTRAINT FK_SystemAdmins_CreatedBy FOREIGN KEY REFERENCES [dbo].[SystemAdmins]([AdminId]),
	[CreatedOn] DATETIME CONSTRAINT DF_SystemAdmin_CreatedOn DEFAULT(GETDATE()) NOT NULL,
	[ModifiedBy] NUMERIC CONSTRAINT FK_SystemAdmins_ModifiedBy FOREIGN KEY REFERENCES [dbo].[SystemAdmins]([AdminId]),
	[ModifiedOn] DATETIME
	CONSTRAINT UQ_SystemAdmin_UserName UNIQUE ([UserName])
)
