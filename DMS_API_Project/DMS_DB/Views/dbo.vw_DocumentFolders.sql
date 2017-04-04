CREATE VIEW [dbo].[vw_DocumentFolders]
	AS 
(
	SELECT DocFold.[DocumentFolderId],
		DocFold.[SystemId],
		DocFold.[DocumentFolderName],
		DocFold.[ParentDocumentFolderId],
		DocFold.[DocumentFolderIsDeleted],
		DocFold.[DocumentFolderCreatedBy],
		DocFold.[DocumentFolderCreatedOn],
		DocFold.[DocumentFolderModifiedBy],
		DocFold.[DocumentFolderModifiedOn],
		USC.[UserName] AS CreatedByUserName,
		USC.[UserFullName] AS CreatedByUserFullName,
		USM.[UserName] AS ModifiedByUserName,
		USM.[UserFullName] AS ModifiedByUserFullName
	FROM [dbo].[DocumentFolders] DocFold
		LEFT JOIN [dbo].[Systems] Sy
		ON Sy.SystemId = DocFold.[SystemId]
		LEFT JOIN [dbo].[Users] USC
		ON DocFold.DocumentFolderCreatedBy = USC.UserId
		LEFT JOIN [dbo].[Users] USM
		ON DocFold.DocumentFolderModifiedBy = USM.UserId
)
