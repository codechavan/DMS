IF NOT EXISTS (SELECT 1 FROM [dbo].[SystemParameters] WHERE SystemParameterName = 'LOCK_USER_ON_LOGIN_FAIL')
BEGIN
	INSERT INTO [dbo].[SystemParameters]
	(
		SystemParameterName,
		SystemParameterDescription,
		SystemParameterDefaultValue,
		SystemParameterCreatedOn
	)
	VALUES
	(
		'LOCK_USER_ON_LOGIN_FAIL',
		'Lock user after number of failed login, 0 - don''t lock negative value not allowed, Any number greated than 0 will check for lock the user after login fail',
		'0',
		GETDATE()
	)
END

IF NOT EXISTS (SELECT 1 FROM [dbo].[SystemParameters] WHERE SystemParameterName = 'NUM_OF_FILE_PROPERTIES')
BEGIN
	INSERT INTO [dbo].[SystemParameters]
	(
		SystemParameterName,
		SystemParameterDescription,
		SystemParameterDefaultValue,
		SystemParameterCreatedOn
	)
	VALUES
	(
		'NUM_OF_FILE_PROPERTIES',
		'Number of properties enable for each files, Value should be between 0 to 10',
		'0',
		GETDATE()
	)
END

IF NOT EXISTS (SELECT 1 FROM [dbo].[SystemParameters] WHERE SystemParameterName = 'CAN_UPLOAD_FILE_FROM_UI')
BEGIN
	INSERT INTO [dbo].[SystemParameters]
	(
		SystemParameterName,
		SystemParameterDescription,
		SystemParameterDefaultValue,
		SystemParameterCreatedOn
	)
	VALUES
	(
		'CAN_UPLOAD_FILE_FROM_UI',
		'0. Only can upload from provided API
		1. User can upload file by using UI',
		'0',
		GETDATE()
	)
END
