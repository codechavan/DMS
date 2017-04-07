CREATE PROCEDURE [dbo].[usp_Get_DocumentFileProperties]
(
	@DocumentFileId			NUMERIC
)
AS
BEGIN

	SELECT DocumentFileId,
		[Field1Value],
		[Field2Value],
		[Field3Value],
		[Field4Value],
		[Field5Value],
		[Field6Value],
		[Field7Value],
		[Field8Value],
		[Field9Value],
		[Field10Value],
		[DocumentFilePropertyCreatedBy],
		[DocumentFilePropertyCreatedOn],
		[DocumentFilePropertyModifiedBy],
		[DocumentFilePropertyModifiedOn],
		DocumentFilePropertiesCreatedByUserName,
		DocumentFilePropertiesCreatedByUserFullName,
		DocumentFilePropertiesModifiedByUserName,
		DocumentFilePropertiesModifiedByUserFullName,
		[SystemId],
		[DocumentFilePropertiesNamesSystemName],
		[Field1Name],
		[Field2Name],
		[Field3Name],
		[Field4Name],
		[Field5Name],
		[Field6Name],
		[Field7Name],
		[Field8Name],
		[Field9Name],
		[Field10Name],
		[DocumentFilePropertiesNameCreatedBy],
		[DocumentFilePropertiesNameCreatedOn],
		[DocumentFilePropertiesNameModifiedBy],
		[DocumentFilePropertiesNameModifiedOn],
		[DocumentFilePropertiesNamesCreatedByUserName],
		[DocumentFilePropertiesNamesCreatedByUserFullName],
		[DocumentFilePropertiesNamesModifiedByUserName],
		[DocumentFilePropertiesNamesModifiedByUserFullName],
		[DocumentFileName],
		[DocumentFolderName]
	FROM [dbo].[vw_DocumentFileProperties]
	WHERE [DocumentFileId] = @DocumentFileId 
  
END