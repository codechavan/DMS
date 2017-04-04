IF NOT EXISTS (SELECT 1 FROM [dbo].[SystemAdmins] WHERE [UserName] = 'SysAdmin')
BEGIN
	INSERT INTO [dbo].[SystemAdmins]([UserName], [FullName], [Password], [EmailId], [CreatedBy])
	VALUES ('SysAdmin', 'System Admin', '', 'sagarchavanit@gmail.com', IDENT_CURRENT('[dbo].[SystemAdmin]'))
END
