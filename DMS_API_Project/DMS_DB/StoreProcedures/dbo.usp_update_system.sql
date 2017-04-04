CREATE PROCEDURE [dbo].[usp_Update_System]
(
	@SystemId				NUMERIC,
	@SystemName				NVARCHAR(200),
	@SystemIsActive			BIT,
	@SystemModifiedBy		NUMERIC,
	@ErrorCode				INT	OUT,
	@ErrorDescription		NVARCHAR(500)	OUT
)
AS
BEGIN
		
	BEGIN TRY	

	--Validations
		BEGIN
			IF COALESCE(@SystemId,0) <= 0
			BEGIN
				SELECT @ErrorCode = -1, @ErrorDescription = 'System Id can not be empty or less than 0';
				RETURN;
			END

			IF COALESCE(@SystemName,'') = ''
			BEGIN
				SELECT @ErrorCode = -2, @ErrorDescription = 'DMS System name can not be empty';
				RETURN;
			END

			IF NOT EXISTS (SELECT 1 FROM [dbo].[Systems] WHERE SystemId = @SystemId)
			BEGIN
				SELECT @ErrorCode = -3, @ErrorDescription = 'DMS System not exist with provide code';
				RETURN;
			END

			IF NOT EXISTS (SELECT 1 FROM [dbo].[Users] WHERE UserId = @SystemModifiedBy)
			BEGIN
				SELECT @ErrorCode = -4, @ErrorDescription = 'Login user code not found in user master';
				RETURN;
			END
		END
			
		UPDATE [dbo].[Systems]
		SET [SystemName]		= @SystemName,
			[SystemIsActive]	= @SystemIsActive,
			[SystemModifiedBy]	= @SystemModifiedBy,
			[SystemModifiedOn]	= GETDATE()
		WHERE SystemId = @SystemId

		SELECT @ErrorCode = 1, @ErrorDescription = 'System updated successfully';
		RETURN;

	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
		BEGIN
			ROLLBACK TRANSACTION
		END

		SELECT @ErrorCode = 0, @ErrorDescription = 'Error while updating new dms system';

		THROW;
	END CATCH
END