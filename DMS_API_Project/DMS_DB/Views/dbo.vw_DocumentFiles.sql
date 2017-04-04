CREATE VIEW [dbo].[vw_DocumentFiles]
	AS 
(
	SELECT DocFile.[DocumentFileId] ,
		DocFile.[SystemId] ,
		DocFile.[DocumentFolderId] ,
		DocFile.[DocumentFileName] ,
		DocFile.[DocumentFileIsDeleted] ,
		DocFile.[DocumentFileCreatedBy] ,
		DocFile.[DocumentFileCreatedOn] ,
		DocFile.[DocumentFileModifiedBy] ,
		DocFile.[DocumentFileModifiedOn] ,
		DocFold.[DocumentFolderName],
		USC.[UserName] AS DocumentFileCreatedByUserName,
		USC.[UserFullName] AS DocumentFileCreatedByUserFullName,
		USM.[UserName] AS DocumentFileModifiedByUserName,
		USM.[UserFullName] AS DocumentFileModifiedByUserFullName
	FROM [dbo].[DocumentFiles] DocFile
		LEFT JOIN [dbo].[DocumentFolders] DocFold
		ON DocFold.SystemId = DocFile.[SystemId]
		AND DocFold.DocumentFolderId = DocFile.[DocumentFolderId]
		LEFT JOIN [dbo].[Systems] Sy
		ON Sy.SystemId = DocFile.[SystemId]
		LEFT JOIN [dbo].[Users] USC
		ON DocFile.DocumentFileCreatedBy = USC.UserId
		LEFT JOIN [dbo].[Users] USM
		ON DocFile.DocumentFileModifiedBy = USM.UserId
)
