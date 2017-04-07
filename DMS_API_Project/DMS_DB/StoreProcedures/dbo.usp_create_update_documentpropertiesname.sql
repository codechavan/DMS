CREATE PROCEDURE [dbo].[usp_Create_Update_DocumentPropertiesName]
(
	@SystemId						NUMERIC,
	@Field1Name						NVARCHAR(100) ,
	@Field2Name						NVARCHAR(100) ,
	@Field3Name						NVARCHAR(100) ,
	@Field4Name						NVARCHAR(100) ,
	@Field5Name						NVARCHAR(100) ,
	@Field6Name						NVARCHAR(100) ,
	@Field7Name						NVARCHAR(100) ,
	@Field8Name						NVARCHAR(100) ,
	@Field9Name						NVARCHAR(100) ,
	@Field10Name					NVARCHAR(100) ,
	@CreatedBy						NUMERIC ,
	@ErrorCode						NUMERIC OUT,
	@ErrorDescription				NVARCHAR(500) OUT
)
AS
BEGIN
	--Validations
	BEGIN
			IF COALESCE(@SystemId,0) = 0
			BEGIN
				SELECT @ErrorCode = -1, @ErrorDescription = 'Dms System not provided';
				RETURN;
			END

			IF NOT EXISTS(SELECT 1 FROM [dbo].[Systems] WHERE SystemId = @SystemId)
			BEGIN
				SELECT @ErrorCode = -2, @ErrorDescription = 'Dms System not available with specified system id';
				RETURN;
			END
			
			IF NOT EXISTS (SELECT 1 FROM [dbo].[Users] WHERE UserId = @CreatedBy)
			BEGIN
				SELECT @ErrorCode = -3, @ErrorDescription = 'Login user code not found in user master';
				RETURN;
			END
		END

	IF EXISTS (SELECT 1 FROM [dbo].[DocumentFilePropertiesNames] WHERE [SystemId] = @SystemId)
	BEGIN
		UPDATE [dbo].[DocumentFilePropertiesNames]
		SET
			Field1Name		= @Field1Name,
			Field2Name		= @Field2Name,
			Field3Name 		= @Field3Name,
			Field4Name 		= @Field4Name,
			Field5Name 		= @Field5Name,
			Field6Name 		= @Field6Name,
			Field7Name 		= @Field7Name,
			Field8Name 		= @Field8Name,
			Field9Name 		= @Field9Name,
			Field10Name		= @Field10Name,
			DocumentFilePropertiesNameModifiedBy = @CreatedBy,
			DocumentFilePropertiesNameModifiedOn = GETDATE()
			WHERE 
			[SystemId] = @SystemId
			
			SELECT @ErrorCode = 1, @ErrorDescription = 'Document properties configuration saved successfully';
	END 
	ELSE
	BEGIN
		INSERT INTO [dbo].[DocumentFilePropertiesNames] 
			(
				SystemId, 
				Field1Name,
				Field2Name,
				Field3Name, 
				Field4Name,
				Field5Name,
				Field6Name,
				Field7Name,
				Field8Name,
				Field9Name,
				Field10Name, 
				DocumentFilePropertiesNameCreatedBy
			)
		VALUES
			(
				@SystemId,
				@Field1Name,
				@Field2Name,
				@Field3Name,
				@Field4Name,
				@Field5Name,
				@Field6Name,
				@Field7Name,
				@Field8Name,
				@Field9Name,
				@Field10Name,
				@CreatedBy
			)
		
		SELECT @ErrorCode = 1, @ErrorDescription = 'Document properties configuration saved successfully';
	END
END

