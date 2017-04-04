CREATE PROCEDURE [dbo].[usp_Create_Update_DocumentFile]
	@DocumentFileId					NUMERIC		= 0,
	@DocumentFolderId				NUMERIC	,
	@SystemId						NUMERIC,
	@DocumentFileName				NVARCHAR(250),
	@FileData						VARBINARY(MAX),
	@DocumentFileCreatedBy			NUMERIC,
	@OutDocumentFileId				NUMERIC OUT,
	@ErrorDescription				NVARCHAR(500) OUT 
AS
BEGIN
	BEGIN TRY

		--Validations
		BEGIN
			IF COALESCE(@SystemId, 0) = 0
			BEGIN
				SELECT @OutDocumentFileId = -1, @ErrorDescription = 'Dms System not provided';
				RETURN;
			END
			IF COALESCE(@DocumentFileName, '') = ''
			BEGIN
				SELECT @OutDocumentFileId = -2, @ErrorDescription = 'File name not provided';
				RETURN;
			END
			IF COALESCE(@DocumentFileCreatedBy, 0) = 0
			BEGIN
				SELECT @OutDocumentFileId = -3, @ErrorDescription = 'File name created by/modified by not provided';
				RETURN;
			END
			IF COALESCE(@DocumentFolderId, 0) = 0
			BEGIN
				SELECT @OutDocumentFileId = -4, @ErrorDescription = 'Folder id not provided';
				RETURN;
			END
			
			
			IF NOT EXISTS(SELECT 1 FROM [dbo].[Systems] WHERE SystemId = @SystemId)
			BEGIN
				SELECT @OutDocumentFileId = -5, @ErrorDescription = 'Dms System not available with specified system id';
				RETURN;
			END
			IF NOT EXISTS(SELECT 1 FROM [dbo].[Users] WHERE UserId = @DocumentFileCreatedBy AND [SystemId] = @SystemId)
			BEGIN
				SELECT @OutDocumentFileId = -6, @ErrorDescription = 'Login user not found in system';
				RETURN;
			END
			IF NOT EXISTS (SELECT * FROM [dbo].[DocumentFolders] Where [DocumentFolderId] = @DocumentFolderId AND [SystemId] = @SystemId)
			BEGIN
				SELECT @OutDocumentFileId = -7, @ErrorDescription = 'Provided document folder id not exists in system';
				RETURN;
			END

			IF COALESCE(@DocumentFileId, 0) <> 0
			BEGIN
				IF NOT EXISTS (SELECT * FROM [dbo].[DocumentFiles] Where [DocumentFolderId] = @DocumentFolderId AND [DocumentFileId] = @DocumentFileId AND [SystemId] = @SystemId)
				BEGIN
					SELECT @OutDocumentFileId = -8, @ErrorDescription = 'Provided document file id not exists in system';
					RETURN;
				END
			END

			IF EXISTS (SELECT 1
						FROM [dbo].[DocumentFiles] 
						Where [SystemId] = @SystemId
						AND [DocumentFolderId] = @DocumentFolderId
						AND [DocumentFileName] = @DocumentFileName
						AND ([DocumentFileId] <> COALESCE(@DocumentFileId, 0))
					)
			BEGIN
				SELECT @OutDocumentFileId = -9, @ErrorDescription = 'Provided document file name already exists in selected folder';
				RETURN;
			END

			IF COALESCE(@DocumentFileId, 0) = 0 AND @FileData IS NULL
			BEGIN
				SELECT @OutDocumentFileId = -10, @ErrorDescription = 'File content can not be empty';
				RETURN;
			END
			
		END
		
		IF COALESCE(@DocumentFileId, 0) = 0
		BEGIN
			INSERT INTO [dbo].[DocumentFiles]
			(
				SystemId,
				[DocumentFolderId],
				[DocumentFileName],
				[DocumentFileCreatedBy]
			)
			VALUES
			(
				@SystemId,
				@DocumentFolderId,
				@DocumentFileName,
				@DocumentFileCreatedBy
			)
			SELECT @OutDocumentFileId = SCOPE_IDENTITY(), @ErrorDescription = 'Document file created successfully';
			SELECT @DocumentFileId = @OutDocumentFileId
		END
		ELSE
		BEGIN
			UPDATE [dbo].[DocumentFiles]
			SET
				[DocumentFileName] = @DocumentFileName,
				[DocumentFolderId] = @DocumentFolderId,
				DocumentFileModifiedBy = @DocumentFileCreatedBy,
				DocumentFileModifiedOn = GETDATE()
			WHERE [DocumentFileId] = @DocumentFileId
				AND SystemId = @SystemId
			
			SELECT @OutDocumentFileId = @DocumentFileId, @ErrorDescription = 'Document file updated successfully';
		END

		IF @FileData IS NOT NULL
		BEGIN
			UPDATE [dbo].[DocumentFileData]
			SET [IsActive] = 0
			WHERE [DocumentFileID] = @DocumentFileId
				AND [IsActive] = 1

			INSERT INTO [dbo].[DocumentFileData](DocumentFileID, FileData, IsActive, DocumentFileCreatedBy)
			VALUES(@DocumentFileId, @FileData, 1, @DocumentFileCreatedBy);
		END

		RETURN;
	END TRY
	BEGIN CATCH
		SELECT @OutDocumentFileId = 0, @ErrorDescription = 'Error while creating document file';
		THROW;
	END CATCH
END