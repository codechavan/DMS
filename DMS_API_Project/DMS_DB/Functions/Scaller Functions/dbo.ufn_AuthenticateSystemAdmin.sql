CREATE FUNCTION [dbo].[ufn_AuthenticateSystemAdmin]
(
	@UserName	NVARCHAR(50),	
	@Password	NVARCHAR(100)
)
RETURNS BIT
AS
BEGIN
	IF EXISTS (SELECT 1
				FROM [dbo].[SystemAdmins]
				WHERE [UserName] = @UserName
					AND [dbo].[ufn_DecryptText]([Password]) = @Password
			)
	BEGIN
		RETURN 1;
	END

	RETURN 0;
END
