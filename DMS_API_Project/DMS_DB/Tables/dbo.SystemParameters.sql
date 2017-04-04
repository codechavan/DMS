CREATE TABLE [dbo].[SystemParameters]
(
	[SystemParameterId] NUMERIC CONSTRAINT PK_SystemParameters_SystemParameterId PRIMARY KEY IDENTITY,
	[SystemParameterName] nvarchar(100) CONSTRAINT UQ_SystemParameters_SystemParameterName UNIQUE NOT NULL,
	[SystemParameterDescription] nvarchar(500),
	[SystemParameterDefaultValue] nvarchar(100) NOT NULL,
	[SystemParameterCreatedOn] Datetime CONSTRAINT DF_SystemParameters_SystemParameterCreatedOn DEFAULT(GETDATE()) NOT NULL,
)
