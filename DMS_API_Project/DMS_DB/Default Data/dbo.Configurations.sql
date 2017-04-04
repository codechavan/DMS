IF NOT EXISTS (SELECT 1 FROM [dbo].[Configurations] WHERE ConfigurationCode = 'NEW_SYS_DEF_STATE')
BEGIN
	INSERT INTO [dbo].[Configurations](ConfigurationCode, ConfigurationValue, ConfigurationDefaultValue, ConfigurationDescription, Remarks)
	VALUES ('NEW_SYS_DEF_STATE', '0', '0', 'New system default active state',NULL)
END
