CREATE PROCEDURE [dbo].[usp_Create_Update_Userrole]
	@UserRoleId				NUMERIC			= 0,
	@SystemId				NUMERIC,
	@UserRoleName			NVARCHAR(100),
	@UserRoleDescription	NVARCHAR(500)	= NULL,
	@UserRoleCreatedBy		NUMERIC,
	@UserRoleIdOut			NUMERIC OUT,
	@ErrorDescription		NVARCHAR(500) OUT
AS
BEGIN
	BEGIN TRY
		
		--Validations
		BEGIN
			IF COALESCE(@SystemId,0) = 0
			BEGIN
				SELECT @UserRoleIdOut = -1, @ErrorDescription = 'Dms System not provided';
				RETURN;
			END
			IF COALESCE(@UserRoleName,'') = ''
			BEGIN
				SELECT @UserRoleIdOut = COALESCE(@UserRoleId,-2), @ErrorDescription = 'Role name can not be empty';
				RETURN
			END

			IF NOT EXISTS(SELECT 1 FROM [dbo].[Systems] WHERE SystemId = @SystemId)
			BEGIN
				SELECT @UserRoleIdOut = COALESCE(@UserRoleId,-3), @ErrorDescription = 'Dms System not available with specified system id';
				RETURN;
			END
			
			IF EXISTS(SELECT 1 FROM [dbo].[UserRoles] WHERE SystemId = @SystemId AND UserRoleName = @UserRoleName) AND COALESCE(@UserRoleID,0) = 0
			BEGIN
				SELECT @UserRoleIdOut = COALESCE(@UserRoleId,-4), @ErrorDescription = 'User role already exists with similar name in system';
				RETURN;
			END

			IF NOT EXISTS(SELECT 1 FROM [dbo].[UserRoles] WHERE SystemId = @SystemId AND UserRoleId = @UserRoleId) AND COALESCE(@UserRoleID,0) <> 0
			BEGIN
				SELECT @UserRoleIdOut = COALESCE(@UserRoleId,-5), @ErrorDescription = 'User role not found in system';
				RETURN;
			END
			
			IF NOT EXISTS (SELECT 1 FROM [dbo].[Users] WHERE UserId = @UserRoleCreatedBy)
			BEGIN
				SELECT @UserRoleIdOut = -4, @ErrorDescription = 'Login user code not found in user master';
				RETURN;
			END
		END

		--Insert/Update
		BEGIN
			IF COALESCE(@UserRoleID,0) = 0
			BEGIN
				INSERT INTO [dbo].[UserRoles](
					SystemId,
					UserRoleName,
					UserRoleDescription,
					UserRoleCreatedBy
				)
				VALUES(
					@SystemId,
					@UserRoleName,
					@UserRoleDescription,
					@UserRoleCreatedBy
				)

				SELECT @UserRoleIdOut = SCOPE_IDENTITY(), @ErrorDescription = 'User role created successfully';
				RETURN;
			END
			ELSE
			BEGIN
				UPDATE [dbo].[UserRoles]
				SET UserRoleName = @UserRoleName,
					UserRoleDescription = @UserRoleDescription,
					UserRoleModifiedBy = @UserRoleCreatedBy,
					UserRoleModifiedOn = GETDATE()
				WHERE UserRoleId = @UserRoleId
					AND SystemId = @SystemId
				
				SELECT @UserRoleIdOut = @UserRoleId, @ErrorDescription = 'User role updated successfully';
				RETURN;
			END
		END
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
		BEGIN
			ROLLBACK TRANSACTION
		END

		SELECT @UserRoleIdOut = 0, @ErrorDescription = 'Error while updating user role';

		THROW;
	END CATCH
END