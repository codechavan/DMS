CREATE PROCEDURE [dbo].[usp_Create_Update_SystemAdmin]
	@AdminId				NUMERIC = 0,
	@UserName				NVARCHAR(50),
	@Password				NVARCHAR(100),
	@FullName				NVARCHAR(150),
	@EmailId				NVARCHAR(100),
	@CreatedBy				NUMERIC,
	@OutAdminId				NUMERIC OUT,
	@ErrorDescription		NVARCHAR(500) OUT 
AS
BEGIN
	BEGIN TRY

		--Validations
		BEGIN
			IF COALESCE(@UserName,'') = ''
			BEGIN
				SELECT @OutAdminId = -1, @ErrorDescription = 'User name not provided';
				RETURN;
			END
			IF COALESCE(@Password,'') = '' AND COALESCE(@AdminId, 0)  = 0
			BEGIN
				SELECT @OutAdminId = -2, @ErrorDescription = 'User password not provided';
				RETURN;
			END
			IF COALESCE(@FullName,'') = ''
			BEGIN
				SELECT @OutAdminId = -3, @ErrorDescription = 'User full name not provided';
				RETURN;
			END
			IF COALESCE(@EmailId, '') = ''
			BEGIN
				SELECT @OutAdminId = -4, @ErrorDescription = 'User email id not provided';
				RETURN;
			END

			IF EXISTS(SELECT 1 FROM [dbo].[SystemAdmins] WHERE AdminId = @AdminId AND COALESCE(@AdminId, 0) <> 0)
			BEGIN
				SELECT @OutAdminId = -1, @ErrorDescription = 'User not found with provided userid';
				RETURN;
			END

			IF EXISTS(SELECT 1 FROM [dbo].[SystemAdmins] WHERE (COALESCE(@AdminId, 0) = 0 OR [AdminId] <> COALESCE(@AdminId, 0)) AND [UserName] = @UserName)
			BEGIN
				SELECT @OutAdminId = -1, @ErrorDescription = 'User already exists with similar username in system';
				RETURN;
			END

			IF NOT EXISTS(SELECT 1 FROM [dbo].[SystemAdmins] WHERE AdminId = @CreatedBy OR COALESCE(@CreatedBy, 0) = 0)
			BEGIN
				SELECT @OutAdminId = -7, @ErrorDescription = 'updated by user not found in system';
				RETURN;
			END

		END
		
		IF COALESCE(@AdminId, 0) = 0
		BEGIN
			INSERT INTO [dbo].[SystemAdmins]
			(
				UserName,
				Password,
				FullName,
				EmailId,
				CreatedBy
			)
			VALUES
			(
				@UserName,
				@Password,
				@FullName,
				@EmailId,
				CASE WHEN COALESCE(@CreatedBy,0) = 0 THEN IDENT_CURRENT('dbo.SystemAdmin') ELSE @CreatedBy END
			)
			
			SELECT @OutAdminId = SCOPE_IDENTITY(), @ErrorDescription = 'User created successfully';
		END
		ELSE
		BEGIN

			UPDATE [dbo].[SystemAdmins]
			SET
				UserName = @UserName,
				FullName = @FullName,
				EmailId = @EmailId,
				ModifiedBy = @CreatedBy,
				ModifiedOn = GETDATE()
			WHERE AdminId = @AdminId
				
			SELECT @OutAdminId = @AdminId, @ErrorDescription = 'User updated successfully';
		END
		RETURN;
	END TRY
	BEGIN CATCH
		SELECT @OutAdminId = 0, @ErrorDescription = 'Error while creating user';
		THROW;
	END CATCH
END