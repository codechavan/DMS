using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.DatabaseConstants
{
    public static class TableColumns
    {
        public static class DmsSystems
        {
            public const string SystemId = "SystemId";
            public const string SystemName = "SystemName";
            public const string IsActive = "SystemIsActive";
            public const string CreatedOn = "SystemCreatedOn";
            public const string ModifiedBy = "SystemModifiedBy";
            public const string ModifiedOn = "SystemModifiedOn";
        }

        public static class DmsUserRoles
        {
            public const string RoleId = "UserRoleId";
            public const string SystemId = "SystemId";
            public const string RoleName = "UserRoleName";
            public const string Description = "UserRoleDescription";
            public const string CreatedBy = "UserRoleCreatedBy";
            public const string CreatedOn = "UserRoleCreatedOn";
            public const string ModifiedBy = "UserRoleModifiedBy";
            public const string ModifiedOn = "UserRoleModifiedOn";
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
            public const string CreatedBy = "UserCreatedBy";
            public const string CreatedOn = "UserCreatedOn";
            public const string ModifiedBy = "UserModifiedBy";
            public const string ModifiedOn = "UserModifiedOn";
        }

        public static class SystemAdmins
        {
            public const string AdminId = "AdminId";
            public const string UserName = "UserName";
            public const string Password = "Password";
            public const string FullName = "FullName";
            public const string EmailId = "EmailId";
            public const string LastLogin = "LastLogin";
            public const string LastPasswordChangedOn = "LastPasswordChangedOn";
            public const string LastPasswordChangedBy = "LastPasswordChangedBy";
            public const string CreatedBy = "CreatedBy";
            public const string CreatedOn = "CreatedOn";
            public const string ModifiedBy = "ModifiedBy";
            public const string ModifiedOn = "ModifiedOn";
        }

        public static class Configurations
        {
            public const string ConfigurationCode = "ConfigurationCode";
            public const string Value = "ConfigurationValue";
            public const string DefaultValue = "ConfigurationDefaultValue";
            public const string Description = "ConfigurationDescription";
            public const string Remarks = "Remarks";
            public const string ModifiedBy = "ConfigurationModifiedBy";
            public const string ModifiedOn = "ConfigurationModifiedOn";
        }

        public static class SystemParameters
        {
            public const string ParameterId = "SystemParameterId";
            public const string ParameterName = "SystemParameterName";
            public const string Description = "SystemParameterDescription";
            public const string DefaultValue = "SystemParameterDefaultValue";
            public const string CreatedOn = "SystemParameterCreatedOn";
        }

        public static class SystemParameterValues
        {
            public const string SystemID = "SystemID";
            public const string ParameterId = "SystemParameterId";
            public const string ParameterValue = "SystemParameterValue";
            public const string CreatedBy = "SystemParameterValueCreatedBy";
            public const string CreatedOn = "SystemParameterValueCreatedOn";
            public const string ModifiedBy = "SystemParameterValueModifiedBy";
            public const string ModifiedOn = "SystemParameterValueModifiedOn";
        }

        public static class DocumentFilePropertiesNames
        {
            public const string SystemId = "SystemId";
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
        }

        public static class DocumentFolders
        {
            public const string DocumentFolderId = "DocumentFolderId";
            public const string SystemId = "SystemId";
            public const string DocumentFolderName = "DocumentFolderName";
            public const string ParentDocumentFolderId = "ParentDocumentFolderId";
            public const string DocumentFolderIsDeleted = "DocumentFolderIsDeleted";
            public const string CreatedBy = "DocumentFolderCreatedBy";
            public const string CreatedOn = "DocumentFolderCreatedOn";
            public const string ModifiedBy = "DocumentFolderModifiedBy";
            public const string ModifiedOn = "DocumentFolderModifiedOn";
        }


        public static class DocumentFiles
        {
            public const string FileId = "DocumentFileId";
            public const string FolderId = "DocumentFolderId";
            public const string SystemId = "SystemId";
            public const string FileName = "DocumentFileName";
            public const string IsDeleted = "DocumentFileIsDeleted";
            public const string CreatedBy = "DocumentFileCreatedBy";
            public const string CreatedOn = "DocumentFileCreatedOn";
            public const string ModifiedBy = "DocumentFileModifiedBy";
            public const string ModifiedOn = "DocumentFileModifiedOn";
        }

        public static class DocumentFileData
        {
            public const string FileDataId = "DocumentFileDataId";
            public const string FileId = "DocumentFileID";
            public const string FileData = "FileData";
            public const string IsActive = "IsActive";
            public const string CreatedBy = "DocumentFileCreatedBy";
            public const string CreatedOn = "DocumentFileCreatedOn";
        }

        public static class DocumentFileProperties
        {
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
            public const string CreatedBy = "DocumentFilePropertyCreatedBy";
            public const string CreatedOn = "DocumentFilePropertyCreatedOn";
            public const string ModifiedBy = "DocumentFilePropertyModifiedBy";
            public const string ModifiedOn = "DocumentFilePropertyModifiedOn";
        }

        public static class DocumentObjectUserRoleMappings
        {
            public const string SystemId = "SystemId";
            public const string DocuementObjectType = "DocuementObjectType";
            public const string DocuementObjectId = "DocuementObjectId";
            public const string UserRoleId = "UserRoleId";
            public const string CanRead = "CanRead";
            public const string CanWrite = "CanWrite";
            public const string CanDelete = "CanDelete";
            public const string CreatedBy = "DocumentObjectUserRoleMappingCreatedBy";
            public const string CreatedOn = "DocumentObjectUserRoleMappingCreatedOn";
            public const string ModifiedBy = "DocumentObjectUserRoleMappingModifiedBy";
            public const string ModifiedOn = "DocumentObjectUserRoleMappingModifiedOn";
        }
    }
}
