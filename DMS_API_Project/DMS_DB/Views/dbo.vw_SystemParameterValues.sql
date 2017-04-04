CREATE VIEW [dbo].[vw_SystemParameterValues]
AS
(

	SELECT S.[SystemName],
			SP.[SystemParameterName],
			SP.[SystemParameterDescription],
			SP.[SystemParameterDefaultValue],
			SPV.[SystemID],
			SPV.[SystemParameterId],
			SPV.[SystemParameterValue],
			SPV.[SystemParameterValueCreatedBy],
			SPV.[SystemParameterValueCreatedOn],
			SPV.[SystemParameterValueModifiedBy],
			SPV.[SystemParameterValueModifiedOn]
	FROM [dbo].[SystemParameterValues] SPV
		INNER JOIN [dbo].[SystemParameters] SP
			ON SP.[SystemParameterId] = SPV.[SystemParameterId]
		INNER JOIN [dbo].[Systems] S
			ON SPV.[SystemID] = S.[SystemId]

)