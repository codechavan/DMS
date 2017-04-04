CREATE FUNCTION [dbo].[ufn_GenerateSystemAdminLogonToken]
(
	@AdminId	NUMERIC
)
RETURNS NVARCHAR(1000)
AS
BEGIN
	--DECLARATION
	BEGIN
		DECLARE @LogonToken NVARCHAR(1000) = '',
			@Username		NVARCHAR(50),
			@Password		NVARCHAR(100)
	END

	BEGIN
		IF COALESCE(@AdminId, 0) <= 0
		BEGIN
			RETURN @LogonToken;
		END

		IF NOT EXISTS(SELECT 1 FROM [dbo].[SystemAdmins] WHERE [AdminId] = @AdminId)
		BEGIN
			RETURN @LogonToken;
		END
	END

	SELECT @Username = SyAdm.[UserName],
		@Password = SyAdm.[Password]
	FROM [dbo].[SystemAdmins] SyAdm
	WHERE [AdminId] = @AdminId

	SELECT @LogonToken = [dbo].[ufn_EncryptText](CAST(@AdminId AS NVARCHAR(100)) + ':' + @Username + ':' + @Password + ':' + FORMAT(GETDATE(),'dd-MMM-yyyy+HH_mm_ss'));

	RETURN @LogonToken
END
