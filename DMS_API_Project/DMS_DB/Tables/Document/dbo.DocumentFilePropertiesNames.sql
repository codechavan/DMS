CREATE TABLE [dbo].[DocumentFilePropertiesNames]
(
	SystemId                        numeric  
			CONSTRAINT FK_DocumentFilePropertiesNames_SystemId_Systems_SystemId REFERENCES [dbo].[Systems]([SystemId])
			CONSTRAINT PK_DocumentFilePropertiesNames_SystemId PRIMARY KEY,
	Field1Name                      nvarchar(100) ,
	Field2Name                      nvarchar(100) ,
	Field3Name                      nvarchar(100) ,
	Field4Name                      nvarchar(100) ,
	Field5Name                      nvarchar(100) ,
	Field6Name                      nvarchar(100) ,
	Field7Name                      nvarchar(100) ,
	Field8Name                      nvarchar(100) ,
	Field9Name                      nvarchar(100) ,
	Field10Name                     nvarchar(100) ,
	DocumentFilePropertiesNameCreatedBy numeric CONSTRAINT FK_DocumentFilePropertiesNames_DocumentPropertiesNameCreatedBy_Users_UserId REFERENCES [dbo].[Users](UserId) NOT NULL,
	DocumentFilePropertiesNameCreatedOn datetime CONSTRAINT DF_DocumentFilePropertiesNames_DocumentPropertiesNameCreatedOn DEFAULT (getdate()) NOT NULL,
	DocumentFilePropertiesNameModifiedBy	numeric CONSTRAINT FK_DocumentFilePropertiesNames_DocumentPropertiesNameModifiedBy_Users_UserId REFERENCES [dbo].[Users](UserId),   
	DocumentFilePropertiesNameModifiedOn	datetime 
)
