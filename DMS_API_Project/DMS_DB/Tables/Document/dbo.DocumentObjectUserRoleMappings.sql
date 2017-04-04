CREATE TABLE [dbo].[DocumentObjectUserRoleMappings]
(
	[SystemId] NUMERIC CONSTRAINT FK_DocumentObjectUserRoleMappings_SystemID_Systems_SystemId FOREIGN KEY REFERENCES [dbo].[Systems]([SystemId]) NOT NULL, 
	[DocuementObjectType] INT NOT NULL CONSTRAINT CK_DocumentObjectUserRoleMappings_DocuementObjectType CHECK ([DocuementObjectType] IN (1, 2)), 
	[DocuementObjectId] NUMERIC NOT NULL, 
	[UserRoleId] NUMERIC CONSTRAINT FK_DocumentObjectUserRoleMappings_UserRoleId_UserRoles_UserRoleId FOREIGN KEY REFERENCES [dbo].[UserRoles]([UserRoleId]) NOT NULL, 
    [CanRead] BIT NOT NULL CONSTRAINT DF_DocumentObjectUserRoleMappings_CanRead DEFAULT (0),
    [CanWrite] BIT NOT NULL CONSTRAINT DF_DocumentObjectUserRoleMappings_CanWrite DEFAULT (0),
    [CanDelete] BIT NOT NULL CONSTRAINT DF_DocumentObjectUserRoleMappings_CanDelete DEFAULT (0),
    [DocumentObjectUserRoleMappingCreatedBy] NUMERIC NOT NULL CONSTRAINT FK_DocumentObjectUserRoleMappings_DocumentObjectUserRoleMappingCreatedBy_Users_UserId FOREIGN KEY REFERENCES [dbo].[Users]([UserId]),
    [DocumentObjectUserRoleMappingCreatedOn] DATETIME NOT NULL CONSTRAINT DF_DocumentObjectUserRoleMappings_DocumentObjectUserRoleMappingCreatedOn DEFAULT (GETDATE()), 
    [DocumentObjectUserRoleMappingModifiedBy] NUMERIC NULL CONSTRAINT FK_DocumentObjectUserRoleMappings_DocumentObjectUserRoleMappingModifiedBy_Users_UserId FOREIGN KEY REFERENCES [dbo].[Users]([UserId]),
    [DocumentObjectUserRoleMappingModifiedOn] DATETIME NULL, 
	CONSTRAINT PK_DocumentObjectUserRoleMappings_SystemID_DocuementObjectType_DocuementObjectId_UserRoleId PRIMARY KEY ([SystemId], [DocuementObjectType], [DocuementObjectId], [UserRoleId])
)
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'1 - File
2 - Folder',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'DocumentObjectUserRoleMappings',
    @level2type = N'COLUMN',
    @level2name = N'DocuementObjectType'