
CREATE PROCEDURE [dbo].[usp_Create_System]
	@SystemName				NVARCHAR(200),
	@UserName				NVARCHAR(50),
	@UserPassword			NVARCHAR(100),
	@UserFullName			NVARCHAR(150),
	@UserEmailId			NVARCHAR(100),
	@UserRoleName			NVARCHAR(100),
	@UserRoleDescription	NVARCHAR(500)	= NULL,
	@SystemId				NUMERIC			OUT,
	@ErrorDescription		NVARCHAR(500)	OUT 
AS
BEGIN
	
	BEGIN TRY	

		--Variable declaration
		BEGIN
			DECLARE @UserId NUMERIC,
				@UserRoleIdOut NUMERIC,
				@SystemIsActive BIT
		END


		--Validations
		BEGIN
			IF COALESCE(@SystemName,'') = ''
			BEGIN
				SELECT @SystemId = -1, @ErrorDescription = 'DMS System name can not be empty';
				RETURN;
			END
			IF COALESCE(@UserName,'') = ''
			BEGIN
				SELECT @SystemId = -2, @ErrorDescription = 'User name can not be empty';
				RETURN;
			END
			IF COALESCE(@UserEmailId,'') = ''
			BEGIN
				SELECT @SystemId = -4, @ErrorDescription = 'User email id can not be empty';
				RETURN;
			END
			IF COALESCE(@UserPassword,'') = ''
			BEGIN
				SELECT @SystemId = -3, @ErrorDescription = 'User password can not be empty';
				RETURN;
			END
			IF COALESCE(@UserFullName,'') = ''
			BEGIN
				SELECT @SystemId = -4, @ErrorDescription = 'User full name can not be empty';
				RETURN;
			END
			IF COALESCE(@UserRoleName,'') = ''
			BEGIN
				SELECT @SystemId = -5, @ErrorDescription = 'User role name can not be empty';
				RETURN;
			END


			IF EXISTS (SELECT 1 FROM [dbo].[Systems] WHERE [SystemName] = @SystemName)
			BEGIN
				SELECT @SystemId = -6, @ErrorDescription = 'DMS System already exists with similar name';
				RETURN;
			END

		END

		--Insert
		BEGIN
			BEGIN TRANSACTION

			SELECT @SystemIsActive = COALESCE(CAST([dbo].[ufn_GetConfigurationValue]('NEW_SYS_DEF_STATE') AS BIT), 0)

			INSERT INTO [dbo].[Systems](SystemName, SystemIsActive)
			VALUES(@SystemName, @SystemIsActive)

			SELECT @SystemId = SCOPE_IDENTITY()

			EXEC [dbo].[usp_Create_Update_User]
						@UserId					= 0,
						@SystemId				= @SystemId,
						@UserName				= @UserName,
						@UserPassword			= @UserPassword,
						@UserFullName			= @UserFullName,
						@UserEmailId			= @UserEmailId,
						@UserRoleId				= 0,
						@UserIsAdmin			= 1,
						@UserIsActive			= 1,
						@UserCreatedBy			= 0,
						@OutUserId				= @UserId OUT,
						@ErrorDescription		= @ErrorDescription OUT
				
			IF @UserId <= 0
			BEGIN
				SELECT @SystemId = @UserId, @ErrorDescription = @ErrorDescription;
				ROLLBACK TRANSACTION;
				RETURN
			END

			EXEC [dbo].[usp_Create_Update_Userrole]
					@UserRoleId				= 0,
					@SystemId				= @SystemId,
					@UserRoleName			= @UserRoleName,
					@UserRoleDescription	= @UserRoleDescription,
					@UserRoleCreatedBy		= @UserId,
					@UserRoleIdOut			= @UserRoleIdOut OUT,
					@ErrorDescription		= @ErrorDescription OUT
			
			IF @UserRoleIdOut <= 0
			BEGIN
				SELECT @SystemId = @UserRoleIdOut, @ErrorDescription = @ErrorDescription;
				ROLLBACK TRANSACTION;
				RETURN
			END

			--Update role for user
			UPDATE [dbo].[Users] SET [UserRoleId] = @UserRoleIdOut WHERE UserId = @UserId
			
			SELECT @ErrorDescription = 'New DMS System created succesfully';

			COMMIT TRANSACTION
		END

	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
		BEGIN
			ROLLBACK TRANSACTION
		END

		SELECT @SystemId = 0, @ErrorDescription = 'Error while creating new dms system';
		THROW;
	END CATCH
END