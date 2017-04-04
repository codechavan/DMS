CREATE CLUSTERED INDEX 
	[IX_DocumentFiles_SystemId_DocumentFolderId_DocumentFileId]
ON
	[dbo].[DocumentFiles] ([SystemId], [DocumentFolderId], [DocumentFileId])
