CREATE PROCEDURE [dbo].[usp_Get_SystemAdminLogin]
(
	@UserName					NVARCHAR(50),
	@Password					NVARCHAR(100),
	@OutAdminId					NUMERIC OUT,
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
		IF COALESCE(@UserName,'') = ''
		BEGIN
			SELECT @OutAdminId = -1, @ErrorDescription = 'User name not provided';
			RETURN;
		END
		IF COALESCE(@Password,'') = ''
		BEGIN
			SELECT @OutAdminId = -2, @ErrorDescription = 'User password not provided';
			RETURN;
		END
	END

	--Verify login
	BEGIN
		IF NOT EXISTS(SELECT 1 FROM [dbo].[SystemAdmins] WHERE [UserName] = @UserName AND [dbo].[ufn_DecryptText]([Password]) =  @Password)
		BEGIN
			SELECT @OutAdminId = -3, @ErrorDescription = 'Invalid username/password';
		END
	END

	SELECT 
	[AdminId],
	[UserName],
	[Password],
	[FullName],
	[EmailId],
	[LastLogin], 
	[LastPasswordChangedOn],
	[LastPasswordChangedBy],
	[CreatedBy], 
	[CreatedOn],
	[ModifiedBy],
	[ModifiedOn]
	FROM [dbo].[SystemAdmins] 
	WHERE [UserName] = @UserName 
		AND [dbo].[ufn_DecryptText]([Password]) = @Password

	UPDATE [dbo].[SystemAdmins] 
	SET [LastLogin] = GETDATE()
	WHERE [UserName] = @UserName
			
	SELECT @OutAdminId = 1, @ErrorDescription = 'Login success';
	RETURN;

END