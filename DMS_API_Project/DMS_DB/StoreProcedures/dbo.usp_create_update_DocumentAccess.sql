CREATE PROCEDURE [dbo].[usp_Create_Update_DocumentAccess]
	@SystemId								NUMERIC,
	@DocuementObjectType					INT,
	@DocuementObjectId						NUMERIC,
	@UserRoleId								NUMERIC,
	@CanRead								BIT,
	@CanWrite								BIT,
	@CanDelete								BIT,
	@DocumentObjectUserRoleMappingCreatedBy	NUMERIC ,
	@OutDocumentObjectUserRoleMappingId		NUMERIC OUT,
	@ErrorDescription						NVARCHAR(500) OUT 
AS
BEGIN
	BEGIN TRY

		--Validations
		BEGIN
			IF COALESCE(@SystemId, 0) = 0
			BEGIN
				SELECT @OutDocumentObjectUserRoleMappingId = -1, @ErrorDescription = 'Dms System not provided';
				RETURN @OutDocumentObjectUserRoleMappingId;
			END
			IF COALESCE(@DocumentObjectUserRoleMappingCreatedBy, 0) = 0
			BEGIN
				SELECT @OutDocumentObjectUserRoleMappingId = -2, @ErrorDescription = 'Created by/modified by not provided';
				RETURN @OutDocumentObjectUserRoleMappingId;
			END
			IF COALESCE(@DocuementObjectType, 0) <> 1 OR COALESCE(@DocuementObjectType, 0) <> 2
			BEGIN
				SELECT @OutDocumentObjectUserRoleMappingId = -3, @ErrorDescription = 'Invalid document object type';
				RETURN @OutDocumentObjectUserRoleMappingId;
			END
			IF COALESCE(@UserRoleId, 0) = 0
			BEGIN
				SELECT @OutDocumentObjectUserRoleMappingId = -4, @ErrorDescription = 'User role can not be empty';
				RETURN @OutDocumentObjectUserRoleMappingId;
			END
			
			IF NOT EXISTS(SELECT 1 FROM [dbo].[Systems] WHERE SystemId = @SystemId)
			BEGIN
				SELECT @OutDocumentObjectUserRoleMappingId = -5, @ErrorDescription = 'Dms System not available with specified system id';
				RETURN @OutDocumentObjectUserRoleMappingId;
			END
			IF NOT EXISTS(SELECT 1 FROM [dbo].[Users] WHERE UserId = @DocumentObjectUserRoleMappingCreatedBy AND [SystemId] = @SystemId)
			BEGIN
				SELECT @OutDocumentObjectUserRoleMappingId = -6, @ErrorDescription = 'Login user not found in system';
				RETURN @OutDocumentObjectUserRoleMappingId;
			END
			IF NOT EXISTS (SELECT * FROM [dbo].[UserRoles] Where [UserRoleId] = @UserRoleId AND [SystemId] = @SystemId)
			BEGIN
				SELECT @OutDocumentObjectUserRoleMappingId = -7, @ErrorDescription = 'Provided user role id not exists in system';
				RETURN @OutDocumentObjectUserRoleMappingId;
			END

			IF COALESCE(@DocuementObjectType, 0) = 1
			BEGIN
				IF NOT EXISTS (SELECT * FROM [dbo].[DocumentFiles] Where [DocumentFileId] = @DocuementObjectId AND [SystemId] = @SystemId)
				BEGIN
					SELECT @OutDocumentObjectUserRoleMappingId = -8, @ErrorDescription = 'Provided document file id not exists in system';
					RETURN @OutDocumentObjectUserRoleMappingId;
				END
			END
			ELSE IF COALESCE(@DocuementObjectType, 0) = 2
			BEGIN
				IF NOT EXISTS (SELECT * FROM [dbo].[DocumentFolders] Where [DocumentFolderId] = @DocuementObjectId AND [SystemId] = @SystemId)
				BEGIN
					SELECT @OutDocumentObjectUserRoleMappingId = -9, @ErrorDescription = 'Provided document folder id not exists in system';
					RETURN @OutDocumentObjectUserRoleMappingId;
				END
			END
			
		END
		
		IF EXISTS (SELECT 1 
					FROM [dbo].[DocumentObjectUserRoleMappings]
					WHERE [SystemId] = @SystemId 
						AND [DocuementObjectType] = @DocuementObjectType
						AND [DocuementObjectId] = @DocuementObjectId
						AND [UserRoleId] = @UserRoleId
					)
		BEGIN
			INSERT INTO [dbo].[DocumentObjectUserRoleMappings]
			(
				[SystemId],
				[DocuementObjectType],
				[DocuementObjectId],
				[UserRoleId],
				[CanRead],
				[CanWrite],
				[CanDelete],
				[DocumentObjectUserRoleMappingCreatedBy]
			)
			VALUES
			(
				@SystemId,
				@DocuementObjectType,
				@DocuementObjectId,
				@UserRoleId,
				@CanRead,						
				@CanWrite,							
				@CanDelete,							
				@DocumentObjectUserRoleMappingCreatedBy
			)

			SELECT @OutDocumentObjectUserRoleMappingId = 1, @ErrorDescription = 'User role updated successfully';
		END
		ELSE
		BEGIN
			UPDATE [dbo].[DocumentObjectUserRoleMappings]
			SET 
				[CanRead] = @CanRead,
				[CanWrite] = @CanWrite,
				[CanDelete] = @CanDelete,
				[DocumentObjectUserRoleMappingModifiedBy] = @DocumentObjectUserRoleMappingCreatedBy,
				[DocumentObjectUserRoleMappingModifiedOn] = GETDATE()
			WHERE [SystemId] = @SystemId 
				AND [DocuementObjectType] = @DocuementObjectType
				AND [DocuementObjectId] = @DocuementObjectId
				AND [UserRoleId] = @UserRoleId
			
			SELECT @OutDocumentObjectUserRoleMappingId = 1, @ErrorDescription = 'User role updated successfully';
		END

		RETURN @OutDocumentObjectUserRoleMappingId;
	END TRY
	BEGIN CATCH
		SELECT @OutDocumentObjectUserRoleMappingId = 0, @ErrorDescription = 'Error while creating document file';
		THROW;
	END CATCH
END