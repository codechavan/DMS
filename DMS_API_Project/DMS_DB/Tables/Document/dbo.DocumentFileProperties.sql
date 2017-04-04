CREATE TABLE [dbo].[DocumentFileProperties]
(
	[DocumentFileId] NUMERIC CONSTRAINT FK_DocumentFileProperties_DocumentFileId_DocumentFiles_DocumentFileId FOREIGN KEY REFERENCES [dbo].[DocumentFiles]([DocumentFileId]) NOT NULL
		CONSTRAINT PK_DocumentFileProperty_DocumentFileId PRIMARY KEY,
	[Field1Value]	NVARCHAR(250),
	[Field2Value]	NVARCHAR(250),
	[Field3Value]	NVARCHAR(250),
	[Field4Value]	NVARCHAR(250),
	[Field5Value]	NVARCHAR(250),
	[Field6Value]	NVARCHAR(250),
	[Field7Value]	NVARCHAR(250),
	[Field8Value]	NVARCHAR(250),
	[Field9Value]	NVARCHAR(250),
	[Field10Value]	NVARCHAR(250),
    [DocumentFilePropertyCreatedBy] NUMERIC NOT NULL CONSTRAINT FK_DocumentFileProperties_DocumentFilePropertyCreatedBy_Users_UserId FOREIGN KEY REFERENCES [dbo].[Users]([UserId]),
    [DocumentFilePropertyCreatedOn] DATETIME NOT NULL  CONSTRAINT DF_DocumentFileProperties_DocumentFilePropertyCreatedOn DEFAULT GETDATE(),
    [DocumentFilePropertyModifiedBy] NUMERIC NULL CONSTRAINT FK_DocumentFileProperties_DocumentFilePropertyModifiedBy_Users_UserId FOREIGN KEY REFERENCES [dbo].[Users]([UserId]),
    [DocumentFilePropertyModifiedOn] DATETIME NULL,
)
