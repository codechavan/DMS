CREATE VIEW [dbo].[vw_DocumentFileProperties]
	AS 
(
	SELECT DocFileProp.DocumentFileId,
		DocFileProp.[Field1Value],
		DocFileProp.[Field2Value],
		DocFileProp.[Field3Value],
		DocFileProp.[Field4Value],
		DocFileProp.[Field5Value],
		DocFileProp.[Field6Value],
		DocFileProp.[Field7Value],
		DocFileProp.[Field8Value],
		DocFileProp.[Field9Value],
		DocFileProp.[Field10Value],
		DocFileProp.[DocumentFilePropertyCreatedBy],
		DocFileProp.[DocumentFilePropertyCreatedOn],
		DocFileProp.[DocumentFilePropertyModifiedBy],
		DocFileProp.[DocumentFilePropertyModifiedOn],
		USC.[UserName]				AS DocumentFilePropertiesCreatedByUserName,
		USC.[UserFullName]			AS DocumentFilePropertiesCreatedByUserFullName,
		USM.[UserName]				AS DocumentFilePropertiesModifiedByUserName,
		USM.[UserFullName]			AS DocumentFilePropertiesModifiedByUserFullName,
		DocFilePropName.[SystemId],
		DocFilePropName.[DocumentFilePropertiesNamesSystemName],
		DocFilePropName.[Field1Name],
		DocFilePropName.[Field2Name],
		DocFilePropName.[Field3Name],
		DocFilePropName.[Field4Name],
		DocFilePropName.[Field5Name],
		DocFilePropName.[Field6Name],
		DocFilePropName.[Field7Name],
		DocFilePropName.[Field8Name],
		DocFilePropName.[Field9Name],
		DocFilePropName.[Field10Name],
		DocFilePropName.[DocumentFilePropertiesNameCreatedBy],
		DocFilePropName.[DocumentFilePropertiesNameCreatedOn],
		DocFilePropName.[DocumentFilePropertiesNameModifiedBy],
		DocFilePropName.[DocumentFilePropertiesNameModifiedOn],
		DocFilePropName.[DocumentFilePropertiesNamesCreatedByUserName],
		DocFilePropName.[DocumentFilePropertiesNamesCreatedByUserFullName],
		DocFilePropName.[DocumentFilePropertiesNamesModifiedByUserName],
		DocFilePropName.[DocumentFilePropertiesNamesModifiedByUserFullName],
		DocFile.[DocumentFileName],
		DocFold.[DocumentFolderName]
	FROM [dbo].[DocumentFileProperties] DocFileProp
		INNER JOIN [dbo].[DocumentFiles] DocFile
			ON DocFile.DocumentFileId = DocFileProp.DocumentFileId
		INNER JOIN [dbo].[DocumentFolders] DocFold
			ON DocFold.SystemId = DocFile.[SystemId]
			AND DocFold.DocumentFolderId = DocFile.[DocumentFolderId]
		INNER JOIN [dbo].[vw_DocumentFilePropertiesNames] DocFilePropName
			ON DocFilePropName.[SystemId] = DocFile.[SystemId]
		INNER JOIN [dbo].[Users] USC
			ON DocFileProp.[DocumentFilePropertyCreatedBy] = USC.UserId
		INNER JOIN [dbo].[Users] USM
			ON DocFileProp.[DocumentFilePropertyModifiedBy] = USM.UserId
)
