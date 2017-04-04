CREATE TABLE [dbo].[UserRoles]
(
	[UserRoleId] NUMERIC CONSTRAINT PK_UserRoles_UserRoleID PRIMARY KEY IDENTITY,
	[SystemId] NUMERIC CONSTRAINT FK_UserRoles_UserRoleSystemID FOREIGN KEY REFERENCES [dbo].[Systems]([SystemId]) NOT NULL,
	[UserRoleName] NVARCHAR(100) NOT NULL,
	[UserRoleDescription] NVARCHAR(500) NULL,
	[UserRoleCreatedBy] NUMERIC CONSTRAINT FK_UserRoles_UserRoleCreatedBy FOREIGN KEY REFERENCES [dbo].[Users]([UserId]) NOT NULL,
	[UserRoleCreatedOn] Datetime CONSTRAINT DF_UserRoles_CreatedOn DEFAULT(GETDATE()) NOT NULL,
	[UserRoleModifiedBy] NUMERIC CONSTRAINT FK_UserRoles_UserRoleModifiedBy FOREIGN KEY REFERENCES [dbo].[Users]([UserId]),
	[UserRoleModifiedOn] Datetime
	CONSTRAINT UQ_UserRoles_UserRoleSystemID_UserRoleName UNIQUE ([SystemId], [UserRoleName])
)
