CREATE PROCEDURE [dbo].[usp_Get_DocumentPropertiesName]
(
	@SystemId	NUMERIC
)
AS
BEGIN

	SELECT [SystemId],
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
		DocumentFilePropertiesNamesCreatedByUserName,
		DocumentFilePropertiesNamesCreatedByUserFullName,
		DocumentFilePropertiesNamesModifiedByUserName,
		DocumentFilePropertiesNamesModifiedByUserFullName,
		DocumentFilePropertiesNamesSystemName
	FROM [dbo].[vw_DocumentFilePropertiesNames]
	WHERE [SystemId] = @SystemId
	        
END