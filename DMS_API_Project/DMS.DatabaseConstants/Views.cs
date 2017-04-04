using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.DatabaseConstants
{
    public static class Views
    {
        public static class Usp_Get_LogonDetail
        {
            public const string UserId = "UserId";
            public const string SystemId = "SystemId";
            public const string SystemName = "SystemName";
            public const string UserName = "Username";
            public const string Password = "Password";
            public const string LogonTime = "LogonTime";
            public const string IsSuccess = "IsSuccess";
        }

        public static class usp_Get_SystemAdminLogonDetail
        {
            public const string AdminId = "AdminId";
            public const string UserName = "Username";
            public const string Password = "Password";
            public const string LogonTime = "LogonTime";
            public const string IsSuccess = "IsSuccess";
        }

        public static class DmsUserRoles
        {
            public const string UserRoleId = "UserRoleId";
            public const string SystemId = "SystemId";
            public const string UserRoleName = "UserRoleName";
            public const string UserRoleDescription = "UserRoleDescription";
            public const string UserRoleCreatedBy = "UserRoleCreatedBy";
            public const string UserRoleCreatedOn = "UserRoleCreatedOn";
            public const string UserRoleModifiedBy = "UserRoleModifiedBy";
            public const string UserRoleModifiedOn = "UserRoleModifiedOn";
            public const string SystemName = "SystemName";
        }

        public static class DmsUsers
        {
            public const string UserId = "UserId";
            public const string SystemId = "SystemId";
            public const string UserRoleId = "UserRoleId";
            public const string UserName = "UserName";
            public const string UserFullName = "UserFullName";
            public const string UserEmailId = "UserEmailId";
            public const string UserPassword = "UserPassword";
            public const string UserIsActive = "UserIsActive";
            public const string UserIsAdmin = "UserIsAdmin";
            public const string UserIsLock = "UserIsLock";
            public const string UserLastLoginDate = "UserLastLoginDate";
            public const string UserLastPasswordChangedBy = "UserLastPasswordChangedBy";
            public const string UserLastPasswordChangedOn = "UserLastPasswordChangedOn";
            public const string UserLastUnblockBy = "UserLastUnblockBy";
            public const string UserLoginFailCount = "UserLoginFailCount";
            public const string UserCreatedBy = "UserCreatedBy";
            public const string UserCreatedOn = "UserCreatedOn";
            public const string UserModifiedBy = "UserModifiedBy";
            public const string UserModifiedOn = "UserModifiedOn";
            public const string UserRoleName = "UserRoleName";
            public const string UserRoleDescription = "UserRoleDescription";
            public const string SystemName = "SystemName";
        }

        public static class vw_SystemParameterValues
        {
            public const string SystemName = "SystemName";
            public const string ParameterName = "SystemParameterName";
            public const string Description = "SystemParameterDescription";
            public const string DefaultValue = "SystemParameterDefaultValue";
            public const string SystemId = "SystemID";
            public const string SystemParameterId = "SystemParameterId";
            public const string ParameterValue = "SystemParameterValue";
            public const string CreatedBy = "SystemParameterValueCreatedBy";
            public const string CreatedOn = "SystemParameterValueCreatedOn";
            public const string ModifiedBy = "SystemParameterValueModifiedBy";
            public const string ModifiedOn = "SystemParameterValueModifiedOn";
        }

        public static class vw_Configurations
        {
            public const string ConfigurationCode = "ConfigurationCode";
            public const string Value = "ConfigurationValue";
            public const string DefaultValue = "ConfigurationDefaultValue";
            public const string Description = "ConfigurationDescription";
            public const string Remarks = "Remarks";
            public const string ModifiedBy = "ConfigurationModifiedBy";
            public const string ModifiedOn = "ConfigurationModifiedOn";
            public const string ModifiedByUserName = "ModifiedByUserName";
        }

        public static class vw_DocumentFilePropertiesNames
        {
            public const string SystemId = "SystemId";
            public const string SystemName = "DocumentFilePropertiesNamesSystemName";
            public const string Field1Name = "Field1Name";
            public const string Field2Name = "Field2Name";
            public const string Field3Name = "Field3Name";
            public const string Field4Name = "Field4Name";
            public const string Field5Name = "Field5Name";
            public const string Field6Name = "Field6Name";
            public const string Field7Name = "Field7Name";
            public const string Field8Name = "Field8Name";
            public const string Field9Name = "Field9Name";
            public const string Field10Name = "Field10Name";
            public const string CreatedBy = "DocumentFilePropertiesNameCreatedBy";
            public const string CreatedOn = "DocumentFilePropertiesNameCreatedOn";
            public const string ModifiedBy = "DocumentFilePropertiesNameModifiedBy";
            public const string ModifiedOn = "DocumentFilePropertiesNameModifiedOn";
            public const string CreatedByUserName = "DocumentFilePropertiesNamesCreatedByUserName";
            public const string CreatedByUserFullName = "DocumentFilePropertiesNamesCreatedByUserFullName";
            public const string ModifiedByUserName = "DocumentFilePropertiesNamesModifiedByUserName";
            public const string ModifiedByUserFullName = "DocumentFilePropertiesNamesModifiedByUserFullName";
        }

        public static class vw_DocumentFolders
        {
            public const string FolderId = "DocumentFolderId";
            public const string SystemId = "SystemId";
            public const string FolderName = "DocumentFolderName";
            public const string ParentFolderId = "ParentDocumentFolderId";
            public const string IsDeleted = "DocumentFolderIsDeleted";
            public const string CreatedBy = "DocumentFolderCreatedBy";
            public const string CreatedOn = "DocumentFolderCreatedOn";
            public const string ModifiedBy = "DocumentFolderModifiedBy";
            public const string ModifiedOn = "DocumentFolderModifiedOn";
            public const string CreatedByUserName = "CreatedByUserName";
            public const string CreatedByUserFullName = "CreatedByUserFullName";
            public const string ModifiedByUserName = "ModifiedByUserName";
            public const string ModifiedByUserFullName = "ModifiedByUserFullName";
        }

        public static class vw_DocumentFiles
        {
            public const string FileId = "DocumentFileId";
            public const string FolderId = "DocumentFolderId";
            public const string SystemId = "SystemId";
            public const string FileName = "DocumentFileName";
            public const string FolderName = "DocumentFolderName";
            public const string IsDeleted = "DocumentFileIsDeleted";
            public const string CreatedBy = "DocumentFileCreatedBy";
            public const string CreatedOn = "DocumentFileCreatedOn";
            public const string ModifiedBy = "DocumentFileModifiedBy";
            public const string ModifiedOn = "DocumentFileModifiedOn";
            public const string CreatedByUserName = "DocumentFileCreatedByUserName";
            public const string CreatedByUserFullName = "DocumentFileCreatedByUserFullName";
            public const string ModifiedByUserName = "DocumentFileModifiedByUserName";
            public const string ModifiedByUserFullName = "DocumentFileModifiedByUserFullName";
        }

        public static class vw_DocumentFileHistory
        {
            public const string FileDataId = "DocumentFileDataId";
            public const string FileId = "DocumentFileId";
            public const string FolderId = "DocumentFolderId";
            public const string SystemId = "SystemId";
            public const string FileName = "DocumentFileName";
            public const string FolderName = "DocumentFolderName";
            public const string IsDeleted = "DocumentFileIsDeleted";
            public const string CreatedBy = "DocumentFileCreatedBy";
            public const string CreatedOn = "DocumentFileCreatedOn";
            public const string CreatedByUserName = "DocumentFileCreatedByUserName";
            public const string CreatedByUserFullName = "DocumentFileCreatedByUserFullName";
            public const string ModifiedBy = "DocumentFileModifiedBy";
            public const string ModifiedOn = "DocumentFileModifiedOn";
            public const string ModifiedByUserName = "DocumentFileModifiedByUserName";
            public const string ModifiedByUserFullName = "DocumentFileModifiedByUserFullName";
            public const string FileUploadedByUserName = "FileUploadedByUserName";
            public const string FileUploadedByUserFullName = "FileUploadedByUserFullName";
        }


        public static class vw_DocumentFileProperties
        {
            public const string FileName = "DocumentFileName";
            public const string FolderName = "DocumentFolderName";

            #region Properties Value
            public const string DocumentFileId = "DocumentFileId";
            public const string Field1Value = "Field1Value";
            public const string Field2Value = "Field2Value";
            public const string Field3Value = "Field3Value";
            public const string Field4Value = "Field4Value";
            public const string Field5Value = "Field5Value";
            public const string Field6Value = "Field6Value";
            public const string Field7Value = "Field7Value";
            public const string Field8Value = "Field8Value";
            public const string Field9Value = "Field9Value";
            public const string Field10Value = "Field10Value";
            public const string PropertyValueCreatedBy = "DocumentFilePropertyCreatedBy";
            public const string PropertyValueCreatedOn = "DocumentFilePropertyCreatedOn";
            public const string PropertyValueModifiedBy = "DocumentFilePropertyModifiedBy";
            public const string PropertyValueModifiedOn = "DocumentFilePropertyModifiedOn";
            public const string PropertyValueCreatedByUserName = "DocumentFilePropertiesCreatedByUserName";
            public const string PropertyValueCreatedByUserFullName = "DocumentFilePropertiesCreatedByUserFullName";
            public const string PropertyValueModifiedByUserName = "DocumentFilePropertiesModifiedByUserName";
            public const string PropertyValueModifiedByUserFullName = "DocumentFilePropertiesModifiedByUserFullName";
            #endregion

            #region Properties Name
            public const string SystemId = "SystemId";
            public const string SystemName = "DocumentFilePropertiesNamesSystemName";
            public const string Field1Name = "Field1Name";
            public const string Field2Name = "Field2Name";
            public const string Field3Name = "Field3Name";
            public const string Field4Name = "Field4Name";
            public const string Field5Name = "Field5Name";
            public const string Field6Name = "Field6Name";
            public const string Field7Name = "Field7Name";
            public const string Field8Name = "Field8Name";
            public const string Field9Name = "Field9Name";
            public const string Field10Name = "Field10Name";
            public const string PropertyNameCreatedBy = "DocumentFilePropertiesNameCreatedBy";
            public const string PropertyNameCreatedOn = "DocumentFilePropertiesNameCreatedOn";
            public const string PropertyNameModifiedBy = "DocumentFilePropertiesNameModifiedBy";
            public const string PropertyNameModifiedOn = "DocumentFilePropertiesNameModifiedOn";
            public const string PropertyNameCreatedByUserName = "DocumentFilePropertiesNamesCreatedByUserName";
            public const string PropertyNameCreatedByUserFullName = "DocumentFilePropertiesNamesCreatedByUserFullName";
            public const string PropertyNameModifiedByUserName = "DocumentFilePropertiesNamesModifiedByUserName";
            public const string PropertyNameModifiedByUserFullName = "DocumentFilePropertiesNamesModifiedByUserFullName";
            #endregion
        }

    }
}
