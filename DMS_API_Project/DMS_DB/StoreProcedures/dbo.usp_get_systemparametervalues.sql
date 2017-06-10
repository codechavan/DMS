CREATE PROCEDURE [dbo].[usp_Get_SystemParameterValues]
(
	@SystemId				NUMERIC = 0,  
	@iPageIndex				INT = 0,  
	@iPageSize				INT = 0,  
	@strWhere				VARCHAR(MAX) = '',  
	@strOrderBy				VARCHAR(8000) = ''
)
AS
BEGIN

	DECLARE	@strSQL NVARCHAR(MAX),  
			@iRowStart INT,  
			@iRowEnd INT,
			@strObject VARCHAR(MAX)
  
   SELECT	@iPageIndex = COALESCE( @iPageIndex, 0 ),  
		   	@iPageSize = COALESCE( @iPageSize, 0 ),  
		   	@strWhere = COALESCE( @strWhere, '' ),  
		   	@strOrderBy = COALESCE( @strOrderBy, '' )  
    
	SELECT	@iRowStart = ( ( @iPageIndex - 1 ) * ( @iPageSize ) ) + 1,  
			@iRowEnd = ( @iRowStart - 1 ) + @iPageSize,  
			@iRowStart = ( CASE WHEN @iRowStart <= 0 THEN 1 ELSE @iRowStart END ),  
			@iRowEnd = ( CASE WHEN @iRowEnd <= 0 THEN 2147483647 ELSE @iRowEnd END ),     
			@strOrderBy = ( CASE WHEN @strOrderBy = '' THEN '[SystemName], [SystemParameterName]' ELSE @strOrderBy END )  
	
	
	SELECT	@strObject = '(SELECT S.[SystemName],
			SP.[SystemParameterName],
			SP.[SystemParameterDescription],
			SP.[SystemParameterDefaultValue],
			COALESCE(SPV.[SystemID],' + CAST(@SystemId AS NVARCHAR(20)) +') AS [SystemID],
			SPV.[SystemParameterId],
			CASE WHEN SPV.[SystemParameterId] IS NOT NULL THEN
				SPV.[SystemParameterValue] 
				ELSE SP.[SystemParameterDefaultValue]
			END AS [SystemParameterValue],
			SPV.[SystemParameterValueCreatedBy],
			SPV.[SystemParameterValueCreatedOn],
			SPV.[SystemParameterValueModifiedBy],
			SPV.[SystemParameterValueModifiedOn]
	FROM 
		[dbo].[SystemParameters] SP
		LEFT JOIN [dbo].[SystemParameterValues] SPV
			ON SP.[SystemParameterId] = SPV.[SystemParameterId]
			AND SPV.[SystemID] = ' + CAST(@SystemId AS NVARCHAR(20)) +'
		LEFT JOIN [dbo].[Systems] S
			ON SPV.[SystemID] = S.[SystemId]) T'
	        
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