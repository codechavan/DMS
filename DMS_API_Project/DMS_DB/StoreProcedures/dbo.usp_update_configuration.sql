CREATE PROCEDURE [dbo].[usp_Update_Configuration]
(
	@ConfigurationCode			VARCHAR(50),
	@ConfigurationValue			NVARCHAR(500),
	@Remarks					NVARCHAR(500),
	@ConfigurationModifiedBy	NUMERIC,
	@ErrorCode					INT	OUT,
	@ErrorDescription			NVARCHAR(500)	OUT
)
AS
BEGIN
	--Validations
	BEGIN
		IF COALESCE(@ConfigurationCode,'') = ''
		BEGIN
			SELECT @ErrorCode = -1, @ErrorDescription = 'Configuration code can not be empty';
			RETURN;
		END
		IF NOT EXISTS (SELECT 1 FROM [dbo].[Configurations] WHERE [ConfigurationCode] = @ConfigurationCode)
		BEGIN
			SELECT @ErrorCode = -2, @ErrorDescription = 'Configuration code not exists';
			RETURN;
		END
		IF COALESCE(@ConfigurationModifiedBy,0) <= 0
		BEGIN
			SELECT @ErrorCode = -3, @ErrorDescription = '@ConfigurationModifiedBy not provided';
			RETURN;
		END
		IF NOT EXISTS(SELECT 1 FROM [dbo].[Users] WHERE UserId = @ConfigurationModifiedBy)
		BEGIN
			SELECT @ErrorCode = -4, @ErrorDescription = 'User not found in system for configuration value update';
			RETURN;
		END
	END

	UPDATE [dbo].[Configurations]
	SET [ConfigurationValue] = @ConfigurationValue,
		[Remarks] = @Remarks,
		[ConfigurationModifiedBy] = @ConfigurationModifiedBy,
		[ConfigurationModifiedOn] = GETDATE()
	WHERE [ConfigurationCode] = @ConfigurationCode
	
	SELECT @ErrorCode = 0, @ErrorDescription = 'Configuration value updated successfully';
	RETURN;
END
