CREATE PROCEDURE [dbo].[usp_Create_Update_DocumentFolder]
	@DocumentFolderId				NUMERIC		= 0,
	@SystemId						NUMERIC,
	@DocumentFolderName				NVARCHAR(250),
	@ParentDocumentFolderId			NUMERIC		= NULL,
	@DocumentFolderCreatedBy		NUMERIC,
	@OutDocumentFolderId			NUMERIC OUT,
	@ErrorDescription				NVARCHAR(500) OUT 
AS
BEGIN
	BEGIN TRY

		--Validations
		BEGIN
			IF COALESCE(@SystemId, 0) = 0
			BEGIN
				SELECT @OutDocumentFolderId = -1, @ErrorDescription = 'Dms System not provided';
				RETURN;
			END
			IF COALESCE(@DocumentFolderName, '') = ''
			BEGIN
				SELECT @OutDocumentFolderId = -2, @ErrorDescription = 'Folder name not provided';
				RETURN;
			END
			IF COALESCE(@DocumentFolderCreatedBy, 0) = 0
			BEGIN
				SELECT @OutDocumentFolderId = -3, @ErrorDescription = 'Folder name created by/modified by not provided';
				RETURN;
			END
			
			IF NOT EXISTS(SELECT 1 FROM [dbo].[Systems] WHERE SystemId = @SystemId)
			BEGIN
				SELECT @OutDocumentFolderId = -4, @ErrorDescription = 'Dms System not available with specified system id';
				RETURN;
			END
			IF NOT EXISTS(SELECT 1 FROM [dbo].[Users] WHERE UserId = @DocumentFolderCreatedBy AND [SystemId] = @SystemId)
			BEGIN
				SELECT @OutDocumentFolderId = -5, @ErrorDescription = 'Login user not found in system';
				RETURN;
			END

			IF COALESCE(@DocumentFolderId, 0) <> 0
			BEGIN
				IF NOT EXISTS (SELECT * FROM [dbo].[DocumentFolders] Where [DocumentFolderId] = @DocumentFolderId AND [SystemId] = @SystemId)
				BEGIN
					SELECT @OutDocumentFolderId = -6, @ErrorDescription = 'Provided document folder id not exists in system';
					RETURN;
				END
			END

			IF COALESCE(@ParentDocumentFolderId, 0) <> 0
			BEGIN
				IF NOT EXISTS (SELECT * FROM [dbo].[DocumentFolders] Where [DocumentFolderId] = @ParentDocumentFolderId AND [SystemId] = @SystemId)
				BEGIN
					SELECT @OutDocumentFolderId = -7, @ErrorDescription = 'Provided parent document id not exists in system';
					RETURN;
				END
			END
			ELSE
			BEGIN
				SELECT @ParentDocumentFolderId = NULL;
			END

			IF EXISTS (SELECT * 
						FROM [dbo].[DocumentFolders] 
						Where (COALESCE(ParentDocumentFolderId, 0) = (CASE WHEN @ParentDocumentFolderId IS NULL THEN 0 ELSE @ParentDocumentFolderId END)) 
						AND [SystemId] = @SystemId
						AND [DocumentFolderName] = @DocumentFolderName
						AND ([DocumentFolderId] <> COALESCE(@DocumentFolderId, 0))
					)
			BEGIN
				SELECT @OutDocumentFolderId = -7, @ErrorDescription = 'Provided document name already exists in parent folder';
				RETURN;
			END
		END
		
		IF COALESCE(@DocumentFolderId, 0) = 0
		BEGIN
			INSERT INTO [dbo].[DocumentFolders]
			(
				SystemId,
				DocumentFolderName,
				ParentDocumentFolderId,
				DocumentFolderCreatedBy
			)
			VALUES
			(
				@SystemId,
				@DocumentFolderName,
				@ParentDocumentFolderId,
				@DocumentFolderCreatedBy
			)
			SELECT @OutDocumentFolderId = SCOPE_IDENTITY(), @ErrorDescription = 'Document folder created successfully';
		END
		ELSE
		BEGIN
			UPDATE [dbo].[DocumentFolders]
			SET
				DocumentFolderName = @DocumentFolderName,
				ParentDocumentFolderId = @ParentDocumentFolderId,
				DocumentFolderModifiedBy = @DocumentFolderCreatedBy,
				DocumentFolderModifiedOn = GETDATE()
			WHERE [DocumentFolderId] = @DocumentFolderId
				AND SystemId = @SystemId
			
			SELECT @OutDocumentFolderId = @DocumentFolderId, @ErrorDescription = 'Document folder updated successfully';
		END
		RETURN;
	END TRY
	BEGIN CATCH
		SELECT @OutDocumentFolderId = 0, @ErrorDescription = 'Error while creating user';
		THROW;
	END CATCH
END