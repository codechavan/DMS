CREATE PROCEDURE [dbo].[usp_Get_UserLogin]
(
	@UserName					NVARCHAR(50),
	@UserPassword				NVARCHAR(100),
	@SystemId					NUMERIC,	
	@OutUserId					NUMERIC OUT,
	@ErrorDescription			NVARCHAR(500) OUT 
)
AS
BEGIN
	
	--Variable Declaration
	BEGIN
		DECLARE @MaximumUserLockCount NUMERIC = NULL
	END
		
	--Basic Required Field Validation
	BEGIN
		IF COALESCE(@SystemId,0) = 0
		BEGIN
			SELECT @OutUserId = -1, @ErrorDescription = 'Dms System not provided';
			RETURN;
		END
		IF COALESCE(@UserName,'') = ''
		BEGIN
			SELECT @OutUserId = -2, @ErrorDescription = 'User name not provided';
			RETURN;
		END
		IF COALESCE(@UserPassword,'') = ''
		BEGIN
			SELECT @OutUserId = -3, @ErrorDescription = 'User password not provided';
			RETURN;
		END
	END

	--Verify login
	BEGIN
		IF NOT EXISTS(SELECT 1 
					FROM [dbo].[Users] Usr
						INNER JOIN [dbo].[Systems] Sy
						ON Usr.[SystemId] = Sy.[SystemId]
					WHERE Usr.SystemId = @SystemId 
						AND Usr.UserName = @UserName 
						AND [dbo].[ufn_DecryptText](Usr.UserPassword) = @UserPassword
						AND Usr.[UserIsLock] = 0
						AND Usr.[UserIsActive] = 1
						AND Sy.[SystemIsActive] = 1
					)
		BEGIN
			
			IF NOT EXISTS(SELECT 1 FROM [dbo].[Systems] WHERE SystemId = @SystemId)
			BEGIN
				SELECT @OutUserId = -4, @ErrorDescription = 'System Id not found in system';
				RETURN;
			END

			IF NOT EXISTS(SELECT 1 FROM [dbo].[Systems] WHERE SystemId = @SystemId AND [SystemIsActive] = 1)
			BEGIN
				SELECT @OutUserId = -5, @ErrorDescription = 'System not available';
				RETURN;
			END

			IF EXISTS(SELECT 1 FROM [dbo].[Users] WHERE SystemId = @SystemId AND UserName = @UserName AND [UserIsLock] = 1)
			BEGIN
				SELECT @OutUserId = -6, @ErrorDescription = 'User is locked';
				RETURN;
			END
			
			IF EXISTS(SELECT 1 FROM [dbo].[Users] WHERE SystemId = @SystemId AND UserName = @UserName)
			BEGIN
				
				SELECT @MaximumUserLockCount = [dbo].[ufn_GetSystemParameterValue](@SystemID,'LOCK_USER_ON_LOGIN_FAIL')

				IF COALESCE(@MaximumUserLockCount, 0) > 0
				BEGIN
					UPDATE [dbo].[Users] 
					SET [UserLoginFailCount] = COALESCE([UserLoginFailCount], 0) + 1,
						[UserIsLock] = CASE  
											WHEN COALESCE(@MaximumUserLockCount, 0) = 0 
												THEN 0
											WHEN ([UserLoginFailCount] + 1) >= COALESCE(@MaximumUserLockCount, 0) 
												THEN 1
											ELSE 0
										END
					WHERE SystemId = @SystemId 
						AND UserName = @UserName 
				END
			END

			SELECT @OutUserId = -7, @ErrorDescription = 'Invalid user id & password';
			RETURN;
		END
	END

	SELECT * 
	FROM [dbo].[vw_Users] 
	WHERE SystemId = @SystemId 
		AND UserName = @UserName 
		AND [UserIsLock] = 0 
		AND [UserIsActive] = 1 
		AND [UserPassword] = @UserPassword

	UPDATE [dbo].[Users] 
	SET [UserLastLoginDate] = GETDATE()
	WHERE SystemId = @SystemId 
		AND UserName = @UserName 
		AND [UserIsLock] = 0 
		AND [UserIsActive] = 1 
		AND [dbo].[ufn_DecryptText](UserPassword) = @UserPassword
			
	SELECT @OutUserId = 1, @ErrorDescription = 'Login success';
	RETURN;

END