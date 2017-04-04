CREATE PROCEDURE [dbo].[usp_Get_DocumentFileProperties]
(
	@DocumentFileId			NUMERIC
)
AS
BEGIN

	SELECT *
	FROM [dbo].[vw_DocumentFileProperties]
	WHERE [DocumentFileId] = @DocumentFileId 
  
END