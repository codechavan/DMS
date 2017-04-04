CREATE PROCEDURE [dbo].[usp_Remove_DocumentAccess]
	@SystemId								NUMERIC,
	@DocuementObjectType					INT,
	@DocuementObjectId						NUMERIC,
	@UserRoleId								NUMERIC,
	@DocumentObjectUserRoleMappingDeletedBy	NUMERIC ,
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
			IF COALESCE(@DocumentObjectUserRoleMappingDeletedBy, 0) = 0
			BEGIN
				SELECT @OutDocumentObjectUserRoleMappingId = -2, @ErrorDescription = 'Mapping Deleted by user id not provided';
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
			IF NOT EXISTS(SELECT 1 FROM [dbo].[Users] WHERE UserId = @DocumentObjectUserRoleMappingDeletedBy AND [SystemId] = @SystemId)
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
		
		IF NOT EXISTS (SELECT 1 
					FROM [dbo].[DocumentObjectUserRoleMappings]
					WHERE [SystemId] = @SystemId 
						AND [DocuementObjectType] = @DocuementObjectType
						AND [DocuementObjectId] = @DocuementObjectId
						AND [UserRoleId] = @UserRoleId
					)
		BEGIN
			SELECT @OutDocumentObjectUserRoleMappingId = -10, @ErrorDescription = 'Mapping not found for selected object';
			RETURN @OutDocumentObjectUserRoleMappingId;
		END
		ELSE
		BEGIN
			DELETE FROM [dbo].[DocumentObjectUserRoleMappings]
			WHERE [SystemId] = @SystemId 
				AND [DocuementObjectType] = @DocuementObjectType
				AND [DocuementObjectId] = @DocuementObjectId
				AND [UserRoleId] = @UserRoleId
			
			SELECT @OutDocumentObjectUserRoleMappingId = 1, @ErrorDescription = 'User role removed successfully';
		END

		RETURN @OutDocumentObjectUserRoleMappingId;
	END TRY
	BEGIN CATCH
		SELECT @OutDocumentObjectUserRoleMappingId = 0, @ErrorDescription = 'Error while creating document file';
		THROW;
	END CATCH
END