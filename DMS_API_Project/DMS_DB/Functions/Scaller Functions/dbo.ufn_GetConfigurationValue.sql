CREATE FUNCTION [dbo].[ufn_GetConfigurationValue]
(
	@ConfigurationCode VARCHAR(50)
)
RETURNS NVARCHAR(500)
AS
BEGIN
	DECLARE @ConfigurationValue NVARCHAR(500)

	SELECT @ConfigurationValue = ConfigurationValue
	FROM [dbo].[Configurations]
	WHERE ConfigurationCode = @ConfigurationCode

	RETURN @ConfigurationValue
END
