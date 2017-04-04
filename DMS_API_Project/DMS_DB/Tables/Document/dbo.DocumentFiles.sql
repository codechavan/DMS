CREATE TABLE [dbo].[DocumentFiles]
(
	[DocumentFileId] NUMERIC CONSTRAINT PK_DocumentFiles_DocumentFileId PRIMARY KEY NONCLUSTERED IDENTITY,
	[SystemId] NUMERIC CONSTRAINT FK_DocumentFiles_SystemId_Systems_SystemId FOREIGN KEY REFERENCES [dbo].[Systems]([SystemId]) NOT NULL,
	[DocumentFolderId] NUMERIC CONSTRAINT FK_DocumentFiles_DocumentFolderId_DocumentFolders_DocumentFolderId FOREIGN KEY REFERENCES [dbo].[DocumentFolders]([DocumentFolderId]) NOT NULL,
	[DocumentFileName] NVARCHAR(250) NOT NULL,
	[DocumentFileIsDeleted] BIT NOT NULL CONSTRAINT DF_DocumentFiles_DocumentFileIsDeleted DEFAULT (0),
    [DocumentFileCreatedBy] NUMERIC NOT NULL CONSTRAINT FK_DocumentFiles_DocumentFileCreatedBy_Users_UserId FOREIGN KEY REFERENCES [dbo].[Users]([UserId]),
    [DocumentFileCreatedOn] DATETIME NOT NULL  CONSTRAINT DF_DocumentFiles_DocumentFileCreatedOn DEFAULT GETDATE(),
    [DocumentFileModifiedBy] NUMERIC NULL CONSTRAINT FK_DocumentFiles_DocumentFileModifiedBy_Users_UserId FOREIGN KEY REFERENCES [dbo].[Users]([UserId]),
    [DocumentFileModifiedOn] DATETIME NULL,
	CONSTRAINT UQ_DocumentFiles_SystemId_DocumentFolderId_DocumentFileName_DocumentFileIsDeleted UNIQUE ([SystemId], [DocumentFolderId], [DocumentFileName], [DocumentFileIsDeleted])
)