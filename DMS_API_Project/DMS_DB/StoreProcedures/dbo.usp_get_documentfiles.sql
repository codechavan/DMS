CREATE PROCEDURE [dbo].[usp_Get_DocumentFiles]
(
	@iPageIndex INT = 0,  
	 @iPageSize INT = 0,  
	 @strWhere VARCHAR(MAX) = '',  
	 @strOrderBy VARCHAR(8000) = ''
)
AS
BEGIN

	DECLARE	@strSQL NVARCHAR(MAX),  
			@iRowStart INT,  
			@iRowEnd INT,
			@strObject VARCHAR(128)
  
   SELECT	@iPageIndex = COALESCE( @iPageIndex, 0 ),  
		   	@iPageSize = COALESCE( @iPageSize, 0 ),  
		   	@strWhere = COALESCE( @strWhere, 'WHERE DocumentFolderId IS NULL' ),  
		   	@strOrderBy = COALESCE( @strOrderBy, '' )  
    
	SELECT	@iRowStart = ( ( @iPageIndex - 1 ) * ( @iPageSize ) ) + 1,  
			@iRowEnd = ( @iRowStart - 1 ) + @iPageSize,  
			@iRowStart = ( CASE WHEN @iRowStart <= 0 THEN 1 ELSE @iRowStart END ),  
			@iRowEnd = ( CASE WHEN @iRowEnd <= 0 THEN 2147483647 ELSE @iRowEnd END ),     
			@strOrderBy = ( CASE WHEN @strOrderBy = '' THEN '[DocumentFileName]' ELSE @strOrderBy END )  
	
	
	SELECT	@strObject = '[dbo].[vw_DocumentFiles]'
	        
	SELECT @strSQL = 'SELECT * '  
		 + 'FROM  ( '  
		 + '    SELECT TOP ( @iRowEnd ) *, '  
		 + '      ROW_NUMBER() OVER( ORDER BY ' + @strOrderBy + ' ) AS RowNumber '    
		 + '    FROM ' + @strObject + ' ' 
		 + ( CASE WHEN @strWhere = '' THEN '' ELSE ' WHERE ' + @strWhere END )  
		 + '   ) Cou '  
		 + 'WHERE RowNumber BETWEEN @iRowStart AND @iRowEnd '  
		 + 'ORDER BY RowNumber '  
  
	--PRINT @strSQL  
	EXECUTE sp_executesql @strSQL, N'@iRowStart INT, @iRowEnd INT', @iRowStart = @iRowStart, @iRowEnd = @iRowEnd  
	  
	SET @strSQL  = 'SELECT COUNT( * ) AS RecordCount '    
		 + 'FROM  ' + @strObject + ' '
		 + ( CASE WHEN @strWhere = '' THEN '' ELSE ' WHERE ' + @strWhere END )  
	
	--PRINT @strSQL    
	EXECUTE sp_executesql @strSQL  
  
END