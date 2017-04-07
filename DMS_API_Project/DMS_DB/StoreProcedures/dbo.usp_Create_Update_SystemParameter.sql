CREATE PROCEDURE [dbo].[usp_Create_Update_SystemParameter]
(
		@SystemId							NUMERIC,
		@SystemParameterId					NUMERIC,
		@SystemParameterValue				NVARCHAR(100),
		@SystemParameterValueCreatedBy		NUMERIC,
		@SystemParameterValueId				NUMERIC OUT,
		@ErrorDescription					NVARCHAR(500) OUT
)
AS
BEGIN

	--Required field Validations
	BEGIN
		IF COALESCE(@SystemId,0) <= 0
		BEGIN
			SELECT @SystemParameterValueId = -1, @ErrorDescription = 'Dms System not provided';
			RETURN;
		END

		IF COALESCE(@SystemParameterId,0) <= 0
		BEGIN
			SELECT @SystemParameterValueId = -2, @ErrorDescription = 'System Parameter id not provided';
			RETURN;
		END

		IF COALESCE(@SystemParameterValueCreatedBy,0) <= 0
		BEGIN
			SELECT @SystemParameterValueId = -3, @ErrorDescription = 'System Parameter created by / modified by not provided';
			RETURN;
		END
	END

	--Validations
	BEGIN
		IF NOT EXISTS(SELECT 1 FROM [dbo].[Systems] WHERE SystemId = @SystemId)
		BEGIN
			SELECT @SystemParameterValueId = -5, @ErrorDescription = 'System Id not found in system';
			RETURN;
		END
		IF NOT EXISTS(SELECT 1 FROM [dbo].[SystemParameters] WHERE SystemParameterId = @SystemParameterId)
		BEGIN
			SELECT @SystemParameterValueId = -6, @ErrorDescription = 'Parameter not found with provided system parameter id';
			RETURN;
		END
		IF NOT EXISTS(SELECT 1 FROM [dbo].[Users] WHERE UserId = @SystemParameterValueCreatedBy)
		BEGIN
			SELECT @SystemParameterValueId = -7, @ErrorDescription = 'User not found in system for parameter value update';
			RETURN;
		END
		IF NOT EXISTS(SELECT 1 FROM [dbo].[Users] WHERE UserId = @SystemParameterValueCreatedBy AND COALESCE([SystemId], 0) = @SystemId)
		BEGIN
			SELECT @SystemParameterValueId = -8, @ErrorDescription = 'Invalid user, this user not belongs to selected system';
			RETURN;
		END
	END

	IF EXISTS (SELECT 1 FROM [dbo].[SystemParameterValues] WHERE [SystemID] = @SystemId AND [SystemParameterId] = @SystemParameterId)
	BEGIN
		UPDATE [dbo].[SystemParameterValues]
		SET SystemParameterValue = @SystemParameterValue,
			SystemParameterValueModifiedBy = @SystemParameterValueCreatedBy,
			SystemParameterValueModifiedOn = GETDATE()
		WHERE [SystemID] = @SystemId
			AND [SystemParameterId] = @SystemParameterId

		SELECT @SystemParameterValueId = 1, @ErrorDescription = 'System parameter value updated successfully';
		RETURN;
	END
	ELSE
	BEGIN
		INSERT INTO [dbo].[SystemParameterValues]([SystemID],[SystemParameterId],[SystemParameterValue],[SystemParameterValueCreatedBy],[SystemParameterValueCreatedOn])
		VALUES (@SystemId, @SystemParameterId, @SystemParameterValue, @SystemParameterValueCreatedBy, GETDATE())
		
		SELECT @SystemParameterValueId = 1, @ErrorDescription = 'New system parameter value created successfully';
		RETURN;
	END
END