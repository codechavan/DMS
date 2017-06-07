CREATE PROCEDURE [dbo].[usp_Create_Update_User]
	@UserId					NUMERIC = 0,
	@SystemId				NUMERIC,
	@UserName				NVARCHAR(50),
	@UserPassword			NVARCHAR(100),
	@UserFullName			NVARCHAR(150),
	@UserEmailId			NVARCHAR(100),
	@UserRoleId				NUMERIC,
	@UserIsAdmin			BIT,
	@UserIsActive			BIT,
	@UserCreatedBy			NUMERIC,
	@OutUserId				NUMERIC OUT,
	@ErrorDescription		NVARCHAR(500) OUT 
AS
BEGIN
	BEGIN TRY

		--Validations
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
			IF COALESCE(@UserPassword,'') = '' AND COALESCE(@UserId, 0)  = 0
			BEGIN
				SELECT @OutUserId = -3, @ErrorDescription = 'User password not provided';
				RETURN;
			END
			IF COALESCE(@UserFullName,'') = ''
			BEGIN
				SELECT @OutUserId = -4, @ErrorDescription = 'User full name not provided';
				RETURN;
			END
			
			IF COALESCE(@UserEmailId, '') = ''
			BEGIN
				SELECT @OutUserId = -4, @ErrorDescription = 'User email id not provided';
				RETURN;
			END

			IF NOT EXISTS(SELECT 1 FROM [dbo].[Systems] WHERE SystemId = @SystemId)
			BEGIN
				SELECT @OutUserId = -5, @ErrorDescription = 'Dms System not available with specified system id';
				RETURN;
			END
			
			IF NOT EXISTS(SELECT 1 FROM [dbo].[UserRoles] WHERE SystemId = @SystemId AND UserRoleId = @UserRoleId) AND COALESCE(@UserRoleId,0) <> 0
			BEGIN
				SELECT @OutUserId = -6, @ErrorDescription = 'User role not available in system';
				RETURN;
			END
			
			IF EXISTS(SELECT 1 FROM [dbo].[Users] WHERE COALESCE([SystemId], 0) = @SystemId AND UserName = @UserName AND (COALESCE(@UserId, 0) = 0 OR COALESCE(@UserId, 0) <> [UserId]))
			BEGIN
				SELECT @OutUserId = -1, @ErrorDescription = 'User already exists with similar username in system';
				RETURN;
			END

			IF NOT EXISTS(SELECT 1 FROM [dbo].[Users] WHERE UserId = @UserCreatedBy) AND (COALESCE(@UserId, 0) <> 0)
			BEGIN
				SELECT @OutUserId = -7, @ErrorDescription = 'Login user not found in system';
				RETURN;
			END

		END
		
		IF COALESCE(@UserId, 0) = 0
		BEGIN
			INSERT INTO [dbo].[Users](
				SystemId,
				UserName,
				UserPassword,
				UserFullName,
				UserEmailId,
				UserRoleId,
				UserIsAdmin,
				UserIsActive,
				UserCreatedBy
			)
			VALUES(
				@SystemId,
				@UserName,
				[dbo].[ufn_EncryptText](@UserPassword),
				@UserFullName,
				@UserEmailId,
				CASE WHEN COALESCE(@UserRoleId,0) = 0 THEN NULL ELSE @UserRoleId END,
				@UserIsAdmin,
				@UserIsActive,
				CASE WHEN COALESCE(@UserCreatedBy,0) = 0 THEN IDENT_CURRENT('dbo.Users') ELSE @UserCreatedBy END
			)
			
			SELECT @OutUserId = SCOPE_IDENTITY(), @ErrorDescription = 'User created successfully';
		END
		ELSE
		BEGIN

			UPDATE [dbo].[Users]
			SET
				UserName = @UserName,
				UserFullName = @UserFullName,
				UserEmailId = @UserEmailId,
				UserRoleId = @UserRoleId,
				UserIsAdmin = @UserIsAdmin,
				UserIsActive = @UserIsActive,
				UserModifiedBy = @UserCreatedBy,
				UserModifiedOn = GETDATE()
			WHERE UserId = @UserId
				AND COALESCE([SystemId], 0) = @SystemId
			
			SELECT @OutUserId = @UserId, @ErrorDescription = 'User updated successfully';
		END
		RETURN;
	END TRY
	BEGIN CATCH
		SELECT @OutUserId = 0, @ErrorDescription = 'Error while creating user';
		THROW;
	END CATCH
END