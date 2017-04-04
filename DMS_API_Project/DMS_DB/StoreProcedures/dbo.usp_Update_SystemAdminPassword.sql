CREATE PROCEDURE [dbo].[usp_Update_SystemAdminPassword]
(
	@AdminId					NUMERIC,
	@Password					NVARCHAR(100),
	@UpdatedBy					NUMERIC,
	@OutAdminId					NUMERIC OUT,
	@ErrorDescription			NVARCHAR(500) OUT 
)
AS
BEGIN
	
	
	--Validations
	BEGIN

		IF COALESCE(@AdminId,0) = 0
		BEGIN
			SELECT @OutAdminId = -1, @ErrorDescription = 'User id not provided';
			RETURN;
		END
		IF COALESCE(@Password,'') = ''
		BEGIN
			SELECT @OutAdminId = -2, @ErrorDescription = 'User password not provided';
			RETURN;
		END
		IF NOT EXISTS(SELECT 1 FROM [dbo].[SystemAdmins] WHERE AdminId = @AdminId)
		BEGIN
			SELECT @OutAdminId = -3, @ErrorDescription = 'Invalid userid';
			RETURN;
		END
		IF NOT EXISTS(SELECT 1 FROM [dbo].[SystemAdmins] WHERE AdminId = @UpdatedBy)
		BEGIN
			SELECT @OutAdminId = -4, @ErrorDescription = 'Invalid updated by userid';
			RETURN;
		END

	END


	UPDATE [dbo].[SystemAdmins]
	SET
		[Password]					= [dbo].[ufn_EncryptText](@Password),
		[LastPasswordChangedBy]		= @UpdatedBy,
		[LastPasswordChangedOn]		= GETDATE()
	WHERE [AdminId] = @AdminId
			
	SELECT @OutAdminId = @AdminId, @ErrorDescription = 'User password updated successfully';

END