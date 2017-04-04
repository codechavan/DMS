CREATE VIEW [dbo].[vw_Configurations]
	AS 
(
	SELECT Con.[ConfigurationCode],
		Con.[ConfigurationValue],
		Con.[ConfigurationDefaultValue],
		Con.[ConfigurationDescription],
		Con.[Remarks],
		Con.[ConfigurationModifiedBy],
		Con.[ConfigurationModifiedOn],
		US.[UserName] AS ModifiedByUserName
	FROM [dbo].[Configurations] Con
		LEFT JOIN [dbo].[SystemAdmins] US
			ON Con.ConfigurationModifiedBy = US.AdminId
)
