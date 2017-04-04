CREATE TABLE [dbo].[Users]
(
	[UserId] NUMERIC IDENTITY CONSTRAINT PK_Users_UserId PRIMARY KEY, 
	[SystemId] NUMERIC CONSTRAINT FK_Users_SystemID FOREIGN KEY REFERENCES [dbo].[Systems]([SystemId]),
    [UserName] NVARCHAR(50) NOT NULL, 
	[UserPassword] NVARCHAR(100) NULL,
    [UserFullName] NVARCHAR(150) NOT NULL, 
    [UserEmailId] NVARCHAR(100) NULL, 
    [UserRoleId] NUMERIC NULL CONSTRAINT FK_Users_UserRoleId FOREIGN KEY REFERENCES [dbo].[UserRoles]([UserRoleId]), 
    [UserIsAdmin] BIT NOT NULL CONSTRAINT DF_Users_UserIsAdmin DEFAULT (0), 
    [UserIsActive] BIT NOT NULL CONSTRAINT DF_Users_UserIsActive DEFAULT (1),
    [UserIsLock] BIT NOT NULL CONSTRAINT DF_Users_UserIsLock DEFAULT (0),
    [UserLastUnblockBy] NUMERIC CONSTRAINT FK_Users_UserLastUnblockBy FOREIGN KEY REFERENCES [dbo].[Users]([UserId]),
    [UserLastPasswordChangedOn] DATETIME NULL,
    [UserLastPasswordChangedBy] NUMERIC CONSTRAINT FK_Users_UserLastPasswordChangedBy FOREIGN KEY REFERENCES [dbo].[Users]([UserId]),
    [UserLoginFailCount] INT NOT NULL CONSTRAINT DF_Users_UserLoginFailCount DEFAULT (0),
    [UserLastLoginDate] Datetime NULL,
	[UserCreatedBy] NUMERIC CONSTRAINT FK_Users_UserCreatedBy FOREIGN KEY REFERENCES [dbo].[Users]([UserId]) NOT NULL,
	[UserCreatedOn] Datetime CONSTRAINT DF_Users_UserCreatedOn DEFAULT(GETDATE()) NOT NULL,
	[UserModifiedBy] NUMERIC CONSTRAINT FK_Users_UserModifiedBy FOREIGN KEY REFERENCES [dbo].[Users]([UserId]),
	[UserModifiedOn] Datetime,
	CONSTRAINT UQ_Users_SystemID_UserName UNIQUE ([SystemId], [UserName])
)
