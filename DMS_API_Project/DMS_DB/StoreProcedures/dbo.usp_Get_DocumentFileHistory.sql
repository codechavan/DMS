CREATE PROCEDURE [dbo].[usp_Get_DocumentFileHistory]
(
	@DocumentFileId		NUMERIC
)
AS
BEGIN

	SELECT *
	FROM [dbo].[vw_DocumentFileHistory]
	WHERE [DocumentFileId] = @DocumentFileId

END