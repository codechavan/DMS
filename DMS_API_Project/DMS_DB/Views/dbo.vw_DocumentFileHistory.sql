CREATE VIEW [dbo].[vw_DocumentFileHistory]
	AS 
(
	SELECT DocData.[DocumentFileDataId],
		DocFile.[DocumentFileId],
		DocFile.[DocumentFileName],
		DocFile.[DocumentFolderId],
		DocFile.[DocumentFolderName],
		DocFile.[SystemId],
		DocFile.[DocumentFileIsDeleted],
		DocFile.[DocumentFileCreatedBy],
		DocFile.[DocumentFileCreatedOn],
		DocFile.[DocumentFileCreatedByUserName],
		DocFile.[DocumentFileCreatedByUserFullName],
		DocFile.[DocumentFileModifiedBy],
		DocFile.[DocumentFileModifiedOn],
		DocFile.[DocumentFileModifiedByUserName],
		DocFile.[DocumentFileModifiedByUserFullName],
		USR.[UserName] AS FileUploadedByUserName,
		USR.[UserFullName] AS FileUploadedByUserFullName
	FROM [dbo].[DocumentFileData] DocData
		INNER JOIN [dbo].[vw_DocumentFiles] DocFile
			ON DocData.DocumentFileID = DocFile.[DocumentFileId]
		INNER JOIN [dbo].[Users] USR
			ON DocData.[DocumentFileCreatedBy] = USR.[UserId]
			AND DocFile.[SystemId] = USR.[SystemId]
)
