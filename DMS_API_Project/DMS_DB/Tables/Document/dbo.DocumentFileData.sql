CREATE TABLE [dbo].[DocumentFileData]
(
	[DocumentFileDataId] UNIQUEIDENTIFIER CONSTRAINT PK_DocumentFileData_DocumentFileDataId PRIMARY KEY DEFAULT newsequentialid() ROWGUIDCOL, 
    [DocumentFileID] NUMERIC NOT NULL CONSTRAINT FK_DocumentFileData_DocumentFileId_DocumentFiles_DocumentFileId FOREIGN KEY REFERENCES [dbo].[DocumentFiles]([DocumentFileId]),
    [FileData] VARBINARY(MAX) NOT NULL,-- FILESTREAM,
    [IsActive] BIT NOT NULL,
	[DocumentFileCreatedBy] NUMERIC NOT NULL CONSTRAINT FK_DocumentFileData_DocumentFileCreatedBy FOREIGN KEY REFERENCES [dbo].[Users]([UserId]),
    [DocumentFileCreatedOn] DATETIME NOT NULL CONSTRAINT DF_DocumentFileData_DocumentFileCreatedOn DEFAULT (GETDATE())
)
