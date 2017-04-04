CREATE PROCEDURE [dbo].[usp_Update_UserPassword]
(
	@UserId						NUMERIC,
	@SystemId					NUMERIC,
	@UserPassword				NVARCHAR(100),
	@UserLastPasswordChangedBy	NUMERIC,
	@OutUserId					NUMERIC OUT,
	@ErrorDescription			NVARCHAR(500) OUT 
)
AS
BEGIN
	
	
	--Validations
	BEGIN
		IF COALESCE(@SystemId,0) = 0
		BEGIN
			SELECT @OutUserId = -1, @ErrorDescription = 'Dms System not provided';
			RETURN;
		END

		IF COALESCE(@UserId,0) = 0
		BEGIN
			SELECT @OutUserId = -2, @ErrorDescription = 'User code not provided';
			RETURN;
		END

		IF COALESCE(@UserPassword,'') = ''
		BEGIN
			SELECT @OutUserId = -3, @ErrorDescription = 'User password not provided';
			RETURN;
		END
		
		IF NOT EXISTS(SELECT 1 FROM [dbo].[Users] WHERE UserId = @UserLastPasswordChangedBy)
		BEGIN
			SELECT @OutUserId = -4, @ErrorDescription = 'Login user not found in system';
			RETURN;
		END

		IF NOT EXISTS(SELECT 1 FROM [dbo].[Users] WHERE UserId = @UserId)
		BEGIN
			SELECT @OutUserId = -5, @ErrorDescription = 'User not found in system for password update';
			RETURN;
		END

		
		IF NOT EXISTS(SELECT 1 FROM [dbo].[Systems] WHERE SystemId = @SystemId)
		BEGIN
			SELECT @OutUserId = -6, @ErrorDescription = 'System Id not found in system';
			RETURN;
		END

	END


	UPDATE [dbo].[Users]
	SET
		[UserPassword]					= [dbo].[ufn_EncryptText](@UserPassword),
		[UserLastPasswordChangedBy]		= @UserLastPasswordChangedBy,
		[UserLoginFailCount]			= 0,
		[UserIsLock]					= 0,
		[UserLastPasswordChangedOn]		= GETDATE()
	WHERE UserId = @UserId
		AND SystemId = @SystemId
			
	SELECT @OutUserId = @UserId, @ErrorDescription = 'User password updated successfully';

END