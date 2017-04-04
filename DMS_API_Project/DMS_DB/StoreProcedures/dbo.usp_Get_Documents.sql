CREATE PROCEDURE [dbo].[usp_Get_Documents]
(
	@SystemId			NUMERIC,
	@ParentFolderId		NUMERIC			= 0,
	@iPageIndex			INT				= 0,  
	@iPageSize			INT				= 0,  
	@strWhere			VARCHAR(MAX)	= '',  
	@strOrderBy			VARCHAR(8000)	= ''
)
AS
BEGIN

	--Declaration
	BEGIN

		IF OBJECT_ID('tempdb.dbo.#DocumentObjects', 'U') IS NOT NULL
			DROP TABLE #DocumentObjects
		CREATE TABLE #DocumentObjects
		(
			ObjectId	NUMERIC,
			ObjectType	INT,
			Name		NVARCHAR(250),
			IsDeleted	BIT,
			CreatedBy	NUMERIC,
			CreatedOn	DATETIME,
			ModifiedBy	NUMERIC,
			ModifiedOn	DATETIME
		)

	END

	--Insert into temp table
	BEGIN
		INSERT INTO #DocumentObjects(ObjectId, ObjectType, Name, IsDeleted, CreatedBy, CreatedOn, ModifiedBy, ModifiedOn)
		SELECT [DocumentFileId],
			1,
			[DocumentFileName],
			[DocumentFileIsDeleted],
			[DocumentFileCreatedBy],
			[DocumentFileCreatedOn],
			[DocumentFileModifiedBy],
			[DocumentFileModifiedOn]
		FROM [dbo].[DocumentFiles]
		WHERE COALESCE([DocumentFolderId], 0) = COALESCE(@ParentFolderId, 0)
		UNION ALL
		SELECT [DocumentFolderId],
			2,
			[DocumentFolderName],
			[DocumentFolderIsDeleted],
			[DocumentFolderCreatedBy],
			[DocumentFolderCreatedOn],
			[DocumentFolderModifiedBy],
			[DocumentFolderModifiedOn]
		FROM [dbo].[DocumentFolders]
		WHERE COALESCE([ParentDocumentFolderId], 0) = COALESCE(@ParentFolderId, 0)
	END

	--Implemented paging
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
	
	
		SELECT	@strObject = '#DocumentObjects'
	        
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

	IF OBJECT_ID('tempdb.dbo.#DocumentObjects', 'U') IS NOT NULL
			DROP TABLE #DocumentObjects
END