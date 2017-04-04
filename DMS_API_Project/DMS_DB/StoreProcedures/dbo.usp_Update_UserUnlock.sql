CREATE PROCEDURE [dbo].[usp_Update_UserUnlock]
(
	@UserId						NUMERIC,
	@UserLastUnblockBy			NUMERIC,
	@OutUserId					NUMERIC OUT,
	@ErrorDescription			NVARCHAR(500) OUT 
)
AS
BEGIN
	
	
	--Validations
	BEGIN
		
		IF COALESCE(@UserId,0) = 0
		BEGIN
			SELECT @OutUserId = -1, @ErrorDescription = 'User code not provided';
			RETURN;
		END
		
		IF NOT EXISTS(SELECT 1 FROM [dbo].[Users] WHERE UserId = @UserLastUnblockBy)
		BEGIN
			SELECT @OutUserId = -2, @ErrorDescription = 'Login user not found in system';
			RETURN;
		END

		IF NOT EXISTS(SELECT 1 FROM [dbo].[Users] WHERE UserId = @UserId)
		BEGIN
			SELECT @OutUserId = -3, @ErrorDescription = 'User not found in system for password update';
			RETURN;
		END

		IF EXISTS(SELECT 1 FROM [dbo].[Users] WHERE UserId = @UserId AND [UserIsLock] = 0)
		BEGIN
			SELECT @OutUserId = -4, @ErrorDescription = 'User is not locked';
			RETURN;
		END

	END

	UPDATE [dbo].[Users]
	SET
		[UserIsLock]			= 0,
		[UserLoginFailCount]	= 0,
		[UserLastUnblockBy]		= @UserLastUnblockBy
	WHERE UserId = @UserId
	
	SELECT @OutUserId = @UserId, @ErrorDescription = 'User unlock successfully';

END
