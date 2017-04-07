CREATE PROCEDURE [dbo].[usp_Get_DocumentAccess]
(
	@SystemId								NUMERIC,
	@DocuementObjectType					INT,
	@DocuementObjectId						NUMERIC,
	@DocumentAccessForUserId				NUMERIC,
	@CanRead								BIT OUT,
	@CanWrite								BIT OUT,
	@CanDelete								BIT OUT,
	@IsInhereted							BIT OUT,
	@InheretedFolderId						NUMERIC = 0 OUT,
	@InheretedFolderName					NVARCHAR(250) = NULL OUT,
	@OutDocumentObjectUserRoleMappingId		NUMERIC OUT,
	@ErrorDescription						NVARCHAR(500) OUT 
)
AS
BEGIN
		
		--Declaration & Initialise
		BEGIN
			DECLARE 
				@UserRoleId					NUMERIC,
				@ParentDocumentFolderId		NUMERIC

			SELECT @CanRead = 0, @CanWrite = 0, @CanDelete = 0
		END

		--Validations
		BEGIN
			IF COALESCE(@SystemId, 0) = 0
			BEGIN
				SELECT @OutDocumentObjectUserRoleMappingId = -1, @ErrorDescription = 'Dms System not provided';
				RETURN @OutDocumentObjectUserRoleMappingId;
			END
			IF COALESCE(@DocumentAccessForUserId, 0) = 0
			BEGIN
				SELECT @OutDocumentObjectUserRoleMappingId = -2, @ErrorDescription = 'Created by/modified by not provided';
				RETURN @OutDocumentObjectUserRoleMappingId;
			END
			IF COALESCE(@DocuementObjectType, 0) <> 1 OR COALESCE(@DocuementObjectType, 0) <> 2
			BEGIN
				SELECT @OutDocumentObjectUserRoleMappingId = -3, @ErrorDescription = 'Invalid document object type';
				RETURN @OutDocumentObjectUserRoleMappingId;
			END
			IF COALESCE(@DocuementObjectId, 0) = 0
			BEGIN
				SELECT @OutDocumentObjectUserRoleMappingId = -4, @ErrorDescription = 'Document object id can not be blank';
				RETURN @OutDocumentObjectUserRoleMappingId;
			END

		END
		
		SELECT @UserRoleId = UserRoleId FROM Users WHERE [UserId] = @DocumentAccessForUserId AND COALESCE([SystemId], 0) = @SystemId

		BEGIN
			
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

			IF NOT EXISTS(SELECT 1 FROM [dbo].[Users] WHERE UserId = @DocumentAccessForUserId AND COALESCE([SystemId], 0) = @SystemId)
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
					WHERE [SystemId] = @SystemID 
						AND [DocuementObjectType] = @DocuementObjectType
						AND [DocuementObjectId] = @DocuementObjectId
						AND [UserRoleId] = @UserRoleId
					)
		BEGIN
			SELECT @CanRead = CanRead, @CanWrite = CanWrite, @CanDelete = CanDelete, @IsInhereted = 0
			FROM [dbo].[DocumentObjectUserRoleMappings]
			WHERE [SystemId] = @SystemID 
				AND [DocuementObjectType] = @DocuementObjectType
				AND [DocuementObjectId] = @DocuementObjectId
				AND [UserRoleId] = @UserRoleId
			SELECT @OutDocumentObjectUserRoleMappingId = 1, @ErrorDescription = '';
			RETURN @OutDocumentObjectUserRoleMappingId;
		END

		IF COALESCE(@DocuementObjectType, 0) = 1
		BEGIN
			SELECT @ParentDocumentFolderId = [DocumentFolderId]
			FROM [dbo].[DocumentFiles]
			WHERE  [DocumentFileId] = @DocuementObjectId
				AND [SystemId] = @SystemId
		END
		ELSE IF COALESCE(@DocuementObjectType, 0) = 2
		BEGIN
			SELECT @ParentDocumentFolderId = [ParentDocumentFolderId]
			FROM [dbo].[DocumentFolders]
			WHERE  [DocumentFolderId] = @DocuementObjectId 
				AND [SystemId] = @SystemId
		END

		WHILE COALESCE(@ParentDocumentFolderId, 0) <> 0
		BEGIN
			IF EXISTS (SELECT 1 
					FROM [dbo].[DocumentObjectUserRoleMappings]
					WHERE [SystemId] = @SystemId
						AND [DocuementObjectType] = 2
						AND [DocuementObjectId] = @ParentDocumentFolderId
						AND [UserRoleId] = @UserRoleId
					)
			BEGIN
				--TO DO: required where condition or use within inner join
				SELECT @CanRead = CanRead, @CanWrite = CanWrite, @CanDelete = CanDelete,
					@IsInhereted = 1, @InheretedFolderId = @ParentDocumentFolderId, @InheretedFolderName = DocFold.[DocumentFolderName]
				FROM [dbo].[DocumentObjectUserRoleMappings] DocMap
					INNER JOIN [dbo].[DocumentFolders] DocFold
					ON DocMap.[SystemId] = DocFold.[SystemId]
					AND DocMap.[DocuementObjectId] = DocFold.[DocumentFolderId]
					AND DocMap.[DocuementObjectType] = 2
				WHERE DocMap.[SystemId] = @SystemId 
					AND DocMap.[DocuementObjectType] = 2
					AND DocMap.[DocuementObjectId] = @ParentDocumentFolderId
					AND DocMap.[UserRoleId] = @UserRoleId
				SELECT @OutDocumentObjectUserRoleMappingId = 1, @ErrorDescription = '';
				RETURN @OutDocumentObjectUserRoleMappingId;
			END

			SELECT @ParentDocumentFolderId = [ParentDocumentFolderId]
			FROM [dbo].[DocumentFolders]
			WHERE  [DocumentFolderId] = @ParentDocumentFolderId 
				AND [SystemId] = @SystemId
			
		END

		SELECT @CanRead = 0, @CanWrite = 0, @CanDelete = 0, @IsInhereted= 0, @OutDocumentObjectUserRoleMappingId = 1, @ErrorDescription = '';
		RETURN @OutDocumentObjectUserRoleMappingId;
END
