CREATE FUNCTION [dbo].[ufn_EncryptText]
(
	@DecryptedText NVARCHAR(1000)
)
RETURNS NVARCHAR(1000)
AS
BEGIN
	DECLARE  
		@mpasstext	NVARCHAR(1000),
		@mlen		INT
	
	SET @mlen = 1  
	SET @mpasstext = ''  

	WHILE @mlen <= LEN(@DecryptedText)  
	BEGIN  
  		SET @mpasstext = LTRIM(RTRIM(@mpasstext)) + CHAR(ASCII(SUBSTRING(@DecryptedText, @mlen, 1)) + 10 + @mlen)  
  		SET @mlen = @mlen + 1  
    END

	RETURN @mpasstext
END
