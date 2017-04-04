CREATE FUNCTION [dbo].[ufn_GetSystemParameterValue]
(
	@SystemID NUMERIC,
	@SystemParameterName nvarchar(100)
)
RETURNS NVARCHAR(100)
AS
BEGIN
	DECLARE @ReturnValue NVARCHAR(100);

	SELECT @ReturnValue = COALESCE(SystemParameterValue,SystemParameterDefaultValue,'')
	FROM [dbo].[SystemParameters] SysPar
		LEFT JOIN [dbo].[SystemParameterValues] SysParVal
		ON SysPar.[SystemParameterId] = SysParVal.[SystemParameterId]
		AND SysParVal.SystemID = @SystemID
	WHERE SysPar.[SystemParameterName] = @SystemParameterName

	RETURN @ReturnValue;
END
