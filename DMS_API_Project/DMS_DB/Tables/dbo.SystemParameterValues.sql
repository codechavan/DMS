CREATE TABLE [dbo].[SystemParameterValues]
(
	[SystemID] NUMERIC NOT NULL CONSTRAINT FK_SystemParameterValues_SystemID FOREIGN KEY REFERENCES [dbo].[Systems]([SystemId]),
	[SystemParameterId] NUMERIC NOT NULL CONSTRAINT FK_SystemParameterValues_SystemParameterId FOREIGN KEY REFERENCES [dbo].[SystemParameters]([SystemParameterId]),
	[SystemParameterValue] NVARCHAR(100) NOT NULL,
	[SystemParameterValueCreatedBy] NUMERIC CONSTRAINT FK_SystemParameterValues_SystemParameterValueCreatedBy FOREIGN KEY REFERENCES [dbo].[Users]([UserId]) NOT NULL,
	[SystemParameterValueCreatedOn] Datetime CONSTRAINT DF_SystemParameterValues_SystemParameterValueCreatedOn DEFAULT(GETDATE()) NOT NULL,
	[SystemParameterValueModifiedBy] NUMERIC CONSTRAINT FK_SystemParameterValues_SystemParameterValueModifiedBy FOREIGN KEY REFERENCES [dbo].[Users]([UserId]),
	[SystemParameterValueModifiedOn] Datetime,
	CONSTRAINT PK_SystemParameterValues_SystemID_SystemParameterId PRIMARY KEY ([SystemID], [SystemParameterId])
)
