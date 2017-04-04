CREATE VIEW [dbo].[vw_UserRoles]
AS
(

	SELECT UR.[UserRoleId],
		UR.[SystemId],
		UR.[UserRoleName],
		UR.[UserRoleDescription],
		UR.[UserRoleCreatedBy],
		UR.[UserRoleCreatedOn],
		UR.[UserRoleModifiedBy],
		UR.[UserRoleModifiedOn],
		Sy.SystemName 
	FROM [dbo].[UserRoles] UR
		INNER JOIN [dbo].[Systems] Sy
		ON UR.[SystemId] = Sy.[SystemId]

)