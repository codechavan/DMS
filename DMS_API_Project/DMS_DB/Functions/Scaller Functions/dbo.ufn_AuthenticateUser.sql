CREATE FUNCTION [dbo].[ufn_AuthenticateUser]
(
	@SystemId	NUMERIC,	
	@UserName	NVARCHAR(50),	
	@Password	NVARCHAR(100)
)
RETURNS BIT
AS
BEGIN
	IF EXISTS (SELECT 1
				FROM [dbo].[Users]
				WHERE COALESCE([SystemId], 0) = @SystemId 
					AND [UserName] = @UserName
					AND [UserPassword] = @Password
					AND [UserIsActive] = 1
					AND [UserIsLock] = 0)
	BEGIN
		RETURN 1;
	END

	RETURN 0;
END
