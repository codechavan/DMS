CREATE PROCEDURE [dbo].[usp_Get_SystemAdminLogonDetail]
(
	@LogonToken NVARCHAR(1000)
)
AS
BEGIN
	
	--DECLARATION
	BEGIN
		DECLARE @DecryptedToken NVARCHAR(1000)

		DECLARE
			@UserId		NUMERIC,
			@Username	NVARCHAR(50),
			@Password	NVARCHAR(100),
			@LogonTime	DATETIME,
			@IsSuccess	BIT	= 0

		IF OBJECT_ID('tempdb.dbo.#LogonDetails', 'U') IS NOT NULL
			DROP TABLE #LogonDetails
		CREATE TABLE #LogonDetails
		(
			RowId	NUMERIC,
			Data	NVARCHAR(300)
		)
	END

	BEGIN TRY
		SELECT @DecryptedToken = [dbo].[ufn_DecryptText](@LogonToken)

		IF (SELECT COUNT(1) FROM [dbo].[ufn_Split](@DecryptedToken, ':')) <> 4
		BEGIN
			GOTO ReturnLogonDetail;
		END

		INSERT INTO #LogonDetails
		(
			RowId,
			Data
		)
		SELECT ROW_NUMBER() OVER(ORDER BY (SELECT 0)),
			Item
		FROM [dbo].[ufn_Split](@DecryptedToken, ':')

		SELECT @UserId		= (SELECT Data FROM #LogonDetails WHERE RowId = 1)
		SELECT @Username	= (SELECT Data FROM #LogonDetails WHERE RowId = 2)
		SELECT @Password	= (SELECT Data FROM #LogonDetails WHERE RowId = 3)
		SELECT @LogonTime	= CAST((SELECT REPLACE(REPLACE(Data, '_',':'), '+', ' ') FROM #LogonDetails WHERE RowId = 4) AS DATETIME)
		SELECT @IsSuccess	= CASE WHEN DATEDIFF(Day, @LogonTime, GETDATE()) = 0 THEN 1 ELSE 0 END
	END TRY
	BEGIN CATCH
		SELECT @IsSuccess = 0
	END CATCH

ReturnLogonDetail:

	SELECT @UserId		AS [AdminId],
		   @Username	AS [Username],
		   @Password	AS [Password],
		   @LogonTime	AS LogonTime,
		   @IsSuccess	AS [IsSuccess]
	
	IF OBJECT_ID('tempdb.dbo.#LogonDetails', 'U') IS NOT NULL
		DROP TABLE #LogonDetails
END