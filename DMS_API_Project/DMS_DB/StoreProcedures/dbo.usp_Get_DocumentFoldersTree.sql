CREATE PROCEDURE [dbo].[usp_Get_DocumentFoldersTree]
(
	@SystemId	NUMERIC = 0
)
AS
BEGIN

	SELECT 
		DocumentFolderId,
		DocumentFolderName,
		ParentDocumentFolderId
	FROM [dbo].[DocumentFolders]
	WHERE [SystemId] = @SystemId
		AND [DocumentFolderIsDeleted] = 0
	ORDER BY [ParentDocumentFolderId] ASC

END