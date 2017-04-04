CREATE CLUSTERED INDEX 
	[IX_DocumentFolders_SystemId_DocumentFolderId]
ON
	[dbo].[DocumentFolders]([SystemId], [DocumentFolderId])
