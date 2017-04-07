CREATE PROCEDURE [dbo].[usp_Get_DocumentFileHistory]
(
	@DocumentFileId		NUMERIC
)
AS
BEGIN

	SELECT  [DocumentFileDataId],
		[DocumentFileId],
		[DocumentFileName],
		[DocumentFolderId],
		[DocumentFolderName],
		[SystemId],
		[DocumentFileIsDeleted],
		[DocumentFileCreatedBy],
		[DocumentFileCreatedOn],
		[DocumentFileCreatedByUserName],
		[DocumentFileCreatedByUserFullName],
		[DocumentFileModifiedBy],
		[DocumentFileModifiedOn],
		[DocumentFileModifiedByUserName],
		[DocumentFileModifiedByUserFullName],
		[FileUploadedByUserName],
		[FileUploadedByUserFullName]
	FROM [dbo].[vw_DocumentFileHistory]
	WHERE [DocumentFileId] = @DocumentFileId

END