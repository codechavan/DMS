CREATE VIEW [dbo].[vw_DocumentFilePropertiesNames]
	AS 
(
	SELECT DocPropName.[SystemId],
		DocPropName.[Field1Name],
		DocPropName.[Field2Name],
		DocPropName.[Field3Name],
		DocPropName.[Field4Name],
		DocPropName.[Field5Name],
		DocPropName.[Field6Name],
		DocPropName.[Field7Name],
		DocPropName.[Field8Name],
		DocPropName.[Field9Name],
		DocPropName.[Field10Name],
		DocPropName.[DocumentFilePropertiesNameCreatedBy],
		DocPropName.[DocumentFilePropertiesNameCreatedOn],
		DocPropName.[DocumentFilePropertiesNameModifiedBy],
		DocPropName.[DocumentFilePropertiesNameModifiedOn],
		USC.[UserName] AS DocumentFilePropertiesNamesCreatedByUserName,
		USC.[UserFullName] AS DocumentFilePropertiesNamesCreatedByUserFullName,
		USM.[UserName] AS DocumentFilePropertiesNamesModifiedByUserName,
		USM.[UserFullName] AS DocumentFilePropertiesNamesModifiedByUserFullName,
		Sy.[SystemName] AS DocumentFilePropertiesNamesSystemName
	FROM [dbo].[DocumentFilePropertiesNames] DocPropName
		INNER JOIN [dbo].[Systems] Sy
			ON Sy.SystemId = DocPropName.[SystemId]
		INNER JOIN [dbo].[Users] USC
			ON DocPropName.[DocumentFilePropertiesNameCreatedBy] = USC.UserId
		LEFT JOIN [dbo].[Users] USM
			ON DocPropName.[DocumentFilePropertiesNameModifiedBy] = USM.UserId
)
