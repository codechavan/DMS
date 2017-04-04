CREATE PROCEDURE [dbo].[usp_Get_DocumentPropertiesName]
(
	@SystemId	NUMERIC
)
AS
BEGIN

	SELECT * 
	FROM [dbo].[vw_DocumentFilePropertiesNames]
	WHERE [SystemId] = @SystemId
	        
END