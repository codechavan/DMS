CREATE VIEW [dbo].[vw_Users]
AS
(

	SELECT US.[UserId],
		US.[SystemId],
		US.[UserRoleId],
		US.[UserName],
		US.[UserFullName],
		US.[UserEmailId],
		US.[UserPassword],
		US.[UserIsActive],
		US.[UserIsAdmin],
		US.[UserIsLock],
		US.[UserLastLoginDate],
		US.[UserLastPasswordChangedBy],
		US.[UserLastPasswordChangedOn],
		US.[UserLastUnblockBy],
		US.[UserLoginFailCount],
		US.[UserCreatedBy],
		US.[UserCreatedOn],
		US.[UserModifiedBy],
		US.[UserModifiedOn],
		UR.[UserRoleName],
		UR.[UserRoleDescription],
		Sy.SystemName 
	FROM [dbo].[Users] US
		INNER JOIN [dbo].[Systems] Sy
			ON US.[SystemId] = Sy.[SystemId]
		INNER JOIN [dbo].[UserRoles] UR
			ON US.[UserRoleId] = UR.[UserRoleId]

)