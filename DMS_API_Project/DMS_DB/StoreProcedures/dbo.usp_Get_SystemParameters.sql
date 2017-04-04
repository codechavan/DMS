CREATE PROCEDURE [dbo].[usp_Get_SystemParameters]
(
	@SystemParameterId	NUMERIC = NULL
)
AS
BEGIN

	SELECT *
	FROM [dbo].[SystemParameters]
	WHERE @SystemParameterId = SystemParameterId
		OR COALESCE(@SystemParameterId, 0) = 0
  
END