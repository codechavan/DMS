CREATE TABLE [dbo].[DocumentFolders]
(
	[DocumentFolderId] NUMERIC IDENTITY CONSTRAINT PK_DocumentFolders_DocumentFolderId PRIMARY KEY NONCLUSTERED,
	[SystemId] NUMERIC CONSTRAINT FK_DocumentFolders_SystemId_Systems_SystemId FOREIGN KEY REFERENCES [dbo].[Systems]([SystemId]) NOT NULL,
	[DocumentFolderName] NVARCHAR(250) NOT NULL,
	[DocumentFolderIsDeleted] BIT NOT NULL CONSTRAINT DF_DocumentFolders_DocumentFolderIsDeleted DEFAULT (0),
	[ParentDocumentFolderId] NUMERIC CONSTRAINT FK_DocumentFolders_ParentDocumentFolderId__DocumentFolders_DocumentFolderId FOREIGN KEY REFERENCES [dbo].[DocumentFolders]([DocumentFolderId]),
    [DocumentFolderCreatedBy] NUMERIC NOT NULL CONSTRAINT FK_DocumentFolders_DocumentFolderCreatedBy_Users_UserId FOREIGN KEY REFERENCES [dbo].[Users]([UserId]),
    [DocumentFolderCreatedOn] DATETIME NOT NULL  CONSTRAINT DF_DocumentFolders_DocumentFolderCreatedOn DEFAULT GETDATE(),
    [DocumentFolderModifiedBy] NUMERIC NULL CONSTRAINT FK_DocumentFolders_DocumentFolderModifiedBy_Users_UserId FOREIGN KEY REFERENCES [dbo].[Users]([UserId]),
    [DocumentFolderModifiedOn] DATETIME NULL,
	CONSTRAINT UQ_DocumentFolders_SystemId_ParentDocumentFolderId_DocumentFolderName UNIQUE ([SystemId], [ParentDocumentFolderId], [DocumentFolderName], [DocumentFolderIsDeleted])
)