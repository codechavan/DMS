CREATE FUNCTION [dbo].[ufn_GenerateLogonToken]
(
	@SystemId			NUMERIC,
	@UserId				NUMERIC
)
RETURNS NVARCHAR(1000)
AS
BEGIN
	--DECLARATION
	BEGIN
		DECLARE @LogonToken NVARCHAR(1000) = '',
			@Username		NVARCHAR(50),
			@Password		NVARCHAR(100),
			@SystemName		NVARCHAR(200)
	END

	BEGIN
		IF COALESCE(@SystemId,0) <= 0
		BEGIN
			RETURN @LogonToken;
		END
		IF COALESCE(@UserId,0) <= 0
		BEGIN
			RETURN @LogonToken;
		END
		IF NOT EXISTS(SELECT 1 FROM [dbo].[Systems] WHERE SystemId = @SystemId)
		BEGIN
			RETURN @LogonToken;
		END
		IF NOT EXISTS(SELECT 1 FROM [dbo].[Users] WHERE UserId = @UserId AND [SystemId] = @SystemId)
		BEGIN
			RETURN @LogonToken;
		END
	END

	SELECT @Username = USR.[UserName],
		@Password = USR.[UserPassword],
		@SystemName = Sy.[SystemName]
	FROM [dbo].[Users] USR
		INNER JOIN [dbo].[Systems] Sy
		ON USR.[SystemId] = Sy.[SystemId]
	WHERE USR.[UserId] = @UserId
		AND Sy.[SystemId] = @SystemId

	SELECT @LogonToken = [dbo].[ufn_EncryptText](CAST(@UserId AS NVARCHAR(100)) + ':' + CAST(@SystemId  AS NVARCHAR(100)) + ':' + @Username + ':' + @SystemName + ':' + @Password + ':' + FORMAT(GETDATE(),'dd-MMM-yyyy+HH_mm_ss')); --dateformat => 20170228

	RETURN @LogonToken
END
