CREATE PROCEDURE [dbo].[usp_Create_Update_DocumentFileProperties]
	@DocumentFileId					NUMERIC		= 0,
	@Field1Value					NVARCHAR(250),
	@Field2Value					NVARCHAR(250),
	@Field3Value					NVARCHAR(250),
	@Field4Value					NVARCHAR(250),
	@Field5Value					NVARCHAR(250),
	@Field6Value					NVARCHAR(250),
	@Field7Value					NVARCHAR(250),
	@Field8Value					NVARCHAR(250),
	@Field9Value					NVARCHAR(250),
	@Field10Value					NVARCHAR(250),
	@DocumentFilePropertyCreatedBy	NUMERIC,
	@OutDocumentFileId				NUMERIC OUT,
	@ErrorDescription				NVARCHAR(500) OUT 
AS
BEGIN
	BEGIN TRY

		--Validations
		BEGIN

			IF COALESCE(@DocumentFilePropertyCreatedBy, 0) = 0
			BEGIN
				SELECT @OutDocumentFileId = -1, @ErrorDescription = 'Created by/modified by not provided';
				RETURN @OutDocumentFileId;
			END
			IF COALESCE(@DocumentFileId, 0) = 0
			BEGIN
				SELECT @OutDocumentFileId = -2, @ErrorDescription = 'Document file id not provided';
				RETURN @OutDocumentFileId;
			END
			
			IF NOT EXISTS(SELECT 1 FROM [dbo].[Users] WHERE UserId = @DocumentFilePropertyCreatedBy)
			BEGIN
				SELECT @OutDocumentFileId = -3, @ErrorDescription = 'Login user not found in system';
				RETURN @OutDocumentFileId;
			END
			IF NOT EXISTS (SELECT * FROM [dbo].[DocumentFiles] Where [DocumentFileId] = @DocumentFileId)
			BEGIN
				SELECT @OutDocumentFileId = -4, @ErrorDescription = 'Provided document folder id not exists in system';
				RETURN @OutDocumentFileId;
			END
			
		END
		
		IF NOT EXISTS (SELECT 1 FROM [dbo].[DocumentFileProperties] WHERE [DocumentFileId] = COALESCE(@DocumentFileId, 0))
		BEGIN
			INSERT INTO [dbo].[DocumentFileProperties]
			(
				[DocumentFileId],
				[Field1Value],
				[Field2Value],
				[Field3Value],
				[Field4Value],
				[Field5Value],
				[Field6Value],
				[Field7Value],
				[Field8Value],
				[Field9Value],
				[Field10Value],
				[DocumentFilePropertyCreatedBy]
			)
			VALUES
			(
				@DocumentFileId,
				@Field1Value,
				@Field2Value,
				@Field3Value,
				@Field4Value,
				@Field5Value,
				@Field6Value,
				@Field7Value,
				@Field8Value,
				@Field9Value,
				@Field10Value,
				@DocumentFilePropertyCreatedBy
			)
			SELECT @OutDocumentFileId = @DocumentFileId, @ErrorDescription = 'Document file properties updated successfully';
		END
		ELSE
		BEGIN
			UPDATE [dbo].[DocumentFileProperties]
			SET
				[Field1Value] = @Field1Value,
				[Field2Value] = @Field2Value,
				[Field3Value] =	@Field3Value,
				[Field4Value] =	@Field4Value,
				[Field5Value] =	@Field5Value,
				[Field6Value] =	@Field6Value,
				[Field7Value] =	@Field7Value,
				[Field8Value] =	@Field8Value,
				[Field9Value] =	@Field9Value,
				[Field10Value] = @Field10Value,
				[DocumentFilePropertyModifiedBy] = @DocumentFilePropertyCreatedBy,
				[DocumentFilePropertyModifiedOn] = GETDATE()
			WHERE [DocumentFileId] = @DocumentFileId
			
			SELECT @OutDocumentFileId = @DocumentFileId, @ErrorDescription = 'Document file properties updated successfully';
		END

		RETURN @OutDocumentFileId;
	END TRY
	BEGIN CATCH
		SELECT @OutDocumentFileId = 0, @ErrorDescription = 'Error while updating document file properties';
		THROW;
	END CATCH
END