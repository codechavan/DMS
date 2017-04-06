using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.DatabaseConstants
{
    public class StoreProcedures
    {
        public class dbo
        {
            public const string usp_Get_Documents = "[dbo].[usp_Get_Documents]";
            public class usp_Get_Documents_Parameters
            {
                public const string SystemId = "@SystemId";
                public const string ParentFolderId = "@ParentFolderId";
                public const string iPageIndex = "@iPageIndex";
                public const string iPageSize = "@iPageSize";
                public const string strWhere = "@strWhere";
                public const string strOrderBy = "@strOrderBy";
            }

            #region Document Access

            public const string usp_Get_DocumentAccess = "[dbo].[usp_Get_DocumentAccess]";
            public class usp_Get_DocumentAccess_Parameters
            {
                public const string SystemId = "@SystemId";
                public const string ObjectType = "@DocuementObjectType";
                public const string ObjectId = "@DocuementObjectId";
                public const string ForUserId = "@DocumentAccessForUserId";
                public const string CanRead = "@CanRead";
                public const string CanWrite = "@CanWrite";
                public const string CanDelete = "@CanDelete";
                public const string IsInhereted = "@IsInhereted";
                public const string InheretedFolderId = "@InheretedFolderId";
                public const string InheretedFolderName = "@InheretedFolderName";
                public const string OutDocumentObjectUserRoleMappingId = "@OutDocumentObjectUserRoleMappingId";
                public const string ErrorDescription = "@ErrorDescription";
            }

            public const string usp_Remove_DocumentAccess = "[dbo].[usp_Remove_DocumentAccess]";
            public class usp_Remove_DocumentAccess_Parameters
            {
                public const string SystemId = "@SystemId";
                public const string ObjectType = "@DocuementObjectType";
                public const string ObjectId = "@DocuementObjectId";
                public const string UserRoleId = "@UserRoleId";
                public const string DeletedBy = "@DocumentObjectUserRoleMappingDeletedBy";
                public const string OutDocumentObjectUserRoleMappingId = "@OutDocumentObjectUserRoleMappingId";
                public const string ErrorDescription = "@ErrorDescription";

            }

            public const string usp_Create_Update_DocumentAccess = "[dbo].[usp_Create_Update_DocumentAccess]";
            public class usp_Create_Update_DocumentAccess_Parameters
            {
                public const string SystemId = "@SystemId";
                public const string ObjectType = "@DocuementObjectType";
                public const string ObjectId = "@DocuementObjectId";
                public const string UserRoleId = "@UserRoleId";
                public const string CanRead = "@CanRead";
                public const string CanWrite = "@CanWrite";
                public const string CanDelete = "@CanDelete";
                public const string CreatedBy = "@DocumentObjectUserRoleMappingCreatedBy";
                public const string OutDocumentObjectUserRoleMappingId = "@OutDocumentObjectUserRoleMappingId";
                public const string ErrorDescription = "@ErrorDescription";
            }

            #endregion

            #region DMS Systems

            public const string usp_create_system = "[dbo].[usp_create_system]";
            public const string usp_Update_System = "[dbo].[usp_Update_System]";
            public const string usp_Get_Systems = "[dbo].[usp_Get_Systems]";

            public class usp_create_system_Parameters
            {
                public const string SystemId = "@SystemId";
                public const string SystemName = "@SystemName";
                public const string UserName = "@UserName";
                public const string UserPassword = "@UserPassword";
                public const string UserFullName = "@UserFullName";
                public const string UserEmailId = "@UserEmailId";
                public const string UserRoleName = "@UserRoleName";
                public const string UserRoleDescription = "@UserRoleDescription";
                public const string ErrorDescription = "@ErrorDescription";
            }
            public class usp_Update_System_Parameters
            {
                public const string SystemId = "@SystemId";
                public const string SystemName = "@SystemName";
                public const string SystemIsActive = "@SystemIsActive";
                public const string SystemModifiedBy = "@SystemModifiedBy";
                public const string ErrorCode = "@ErrorCode";
                public const string ErrorDescription = "@ErrorDescription";
            }
            public class usp_Get_Systems_Parameters
            {
                public const string PageIndex = "@iPageIndex";
                public const string PageSize = "@iPageSize";
                public const string WhereCondition = "@strWhere";
                public const string OrderBy = "@strOrderBy";
                public const string Column_RecordCount = "RecordCount";
            }
            #endregion

            #region Userrole

            public const string usp_Create_Update_Userrole = "[dbo].[usp_Create_Update_Userrole]";
            public const string usp_Get_Userroles = "[dbo].[usp_Get_Userroles]";

            public class usp_Create_Update_Userrole_Parameters
            {
                public const string SystemId = "@SystemId";
                public const string UserRoleId = "@UserRoleId";
                public const string UserRoleName = "@UserRoleName";
                public const string UserRoleDescription = "@UserRoleDescription";
                public const string UserRoleCreatedBy = "@UserRoleCreatedBy";
                public const string UserRoleIdOut = "@UserRoleIdOut";
                public const string ErrorDescription = "@ErrorDescription";
            }
            public class usp_Get_Userroles_Parameters
            {
                public const string PageIndex = "@iPageIndex";
                public const string PageSize = "@iPageSize";
                public const string WhereCondition = "@strWhere";
                public const string OrderBy = "@strOrderBy";
                public const string Column_RecordCount = "RecordCount";
            }
            #endregion

            #region Dms User

            public const string usp_Get_Users = "[dbo].[usp_Get_Users]";
            public const string usp_create_update_user = "[dbo].[usp_create_update_user]";
            public const string usp_Get_UserLogin = "[dbo].[usp_Get_UserLogin]";
            public const string usp_Get_LogonDetail = "[dbo].[usp_get_LogonDetail]";
            public const string usp_Update_UserPassword = "[dbo].[usp_Update_UserPassword]";
            public const string usp_Update_UserUnlock = "[dbo].[usp_Update_UserUnlock]";

            public class usp_create_update_user_Parameters
            {
                public const string UserId = "@UserId";
                public const string SystemId = "@SystemId";
                public const string UserName = "@UserName";
                public const string UserPassword = "@UserPassword";
                public const string UserFullName = "@UserFullName";
                public const string UserEmailId = "@UserEmailId";
                public const string UserRoleId = "@UserRoleId";
                public const string UserIsAdmin = "@UserIsAdmin";
                public const string UserIsActive = "@UserIsActive";
                public const string UserCreatedBy = "@UserCreatedBy";
                public const string OutUserId = "@OutUserId";
                public const string ErrorDescription = "@ErrorDescription";
            }
            public class usp_Get_Users_Parameters
            {
                public const string PageIndex = "@iPageIndex";
                public const string PageSize = "@iPageSize";
                public const string WhereCondition = "@strWhere";
                public const string OrderBy = "@strOrderBy";
                public const string Column_RecordCount = "RecordCount";
            }
            public class usp_Get_UserLogin_Parameters
            {
                public const string UserName = "@UserName";
                public const string SystemId = "@SystemId";
                public const string UserPassword = "@UserPassword";
                public const string OutUserId = "@OutUserId";
                public const string ErrorDescription = "@ErrorDescription";
            }
            public class usp_Get_LogonDetail_Parameters
            {
                public const string LogonToken = "@LogonToken";
            }
            public class usp_Update_UserPassword_Parameters
            {
                public const string UserId = "@UserId";
                public const string SystemId = "@SystemId";
                public const string UserPassword = "@UserPassword";
                public const string UserLastPasswordChangedBy = "@UserLastPasswordChangedBy";
                public const string OutUserId = "@OutUserId";
                public const string ErrorDescription = "@ErrorDescription";
            }
            public class usp_Update_UserUnlock_Parameters
            {
                public const string UserId = "@UserId";
                public const string UserLastUnblockBy = "@UserLastUnblockBy";
                public const string OutUserId = "@OutUserId";
                public const string ErrorDescription = "@ErrorDescription";
            }
            #endregion

            #region System Parameter

            public const string usp_Get_SystemParameterValues = "[dbo].[usp_Get_SystemParameterValues]";
            public const string usp_Create_Update_SystemParameter = "[dbo].[usp_Create_Update_SystemParameter]";
            public const string usp_Get_SystemParameters = "[dbo].[usp_Get_SystemParameters]";

            public class usp_Get_SystemParameterValues_Parameters
            {
                public const string PageIndex = "@iPageIndex";
                public const string PageSize = "@iPageSize";
                public const string WhereCondition = "@strWhere";
                public const string OrderBy = "@strOrderBy";
                public const string Column_RecordCount = "RecordCount";
            }

            public class usp_Create_Update_SystemParameter_Parameters
            {

                public const string SystemId = "@SystemId";
                public const string SystemParameterId = "@SystemParameterId";
                public const string SystemParameterValue = "@SystemParameterValue";
                public const string SystemParameterValueCreatedBy = "@SystemParameterValueCreatedBy";
                public const string SystemParameterValueId = "@SystemParameterValueId";
                public const string ErrorDescription = "@ErrorDescription";
            }

            public class usp_Get_SystemParameters_Parameters
            {
                public const string SystemParameterId = "@SystemParameterId";
            }
            #endregion

            #region System Admin
            
            public const string usp_Create_Update_SystemAdmin = "[dbo].[usp_Create_Update_SystemAdmin]";
            public const string usp_Get_SystemAdmins = "[dbo].[usp_Get_SystemAdmins]";
            public const string usp_Get_SystemAdminLogin = "[dbo].[usp_Get_SystemAdminLogin]";
            public const string usp_Get_SystemAdminLogonDetail = "[dbo].[usp_Get_SystemAdminLogonDetail]";
            public const string usp_Update_SystemAdminPassword = "[dbo].[usp_Update_SystemAdminPassword]";

            public class usp_Get_SystemAdmins_Parameters
            {
                public const string PageIndex = "@iPageIndex";
                public const string PageSize = "@iPageSize";
                public const string WhereCondition = "@strWhere";
                public const string OrderBy = "@strOrderBy";
                public const string Column_RecordCount = "RecordCount";
            }
            public class usp_Create_Update_SystemAdmin_Parameters
            {
                public const string AdminId = "@AdminId";
                public const string UserName = "@UserName";
                public const string Password = "@Password";
                public const string FullName = "@FullName";
                public const string EmailId = "@EmailId";
                public const string CreatedBy = "@CreatedBy";
                public const string OutAdminId = "@OutAdminId";
                public const string ErrorDescription = "@ErrorDescription";
            }
            public class usp_Get_SystemAdminLogin_Parameters
            {
                public const string UserName = "@UserName";
                public const string Password = "@Password";
                public const string OutAdminId = "@OutAdminId";
                public const string ErrorDescription = "@ErrorDescription";
            }
            public class usp_Get_SystemAdminLogonDetail_Parameters
            {
                public const string LogonToken = "@LogonToken";
            }
            public class usp_Update_SystemAdminPassword_Parameters
            {
                public const string AdminId = "@AdminId";
                public const string Password = "@Password";
                public const string UpdatedBy = "@UpdatedBy";
                public const string OutAdminId = "@OutAdminId";
                public const string ErrorDescription = "@ErrorDescription";
            }
            #endregion

            #region Configurations

            public const string usp_Update_Configuration = "[dbo].[usp_Update_Configuration]";
            public const string usp_Get_Configurations = "[dbo].[usp_Get_Configurations]";

            public class usp_Update_Configuration_Parameters
            {
                public const string ConfigurationCode = "@ConfigurationCode";
                public const string ConfigurationValue = "@ConfigurationValue";
                public const string Remarks = "@Remarks";
                public const string ConfigurationModifiedBy = "@ConfigurationModifiedBy";
                public const string ErrorCode = "@ErrorCode";
                public const string ErrorDescription = "@ErrorDescription";
            }
            public class usp_Get_Configurations_Parameters
            {
                public const string PageIndex = "@iPageIndex";
                public const string PageSize = "@iPageSize";
                public const string WhereCondition = "@strWhere";
                public const string OrderBy = "@strOrderBy";
                public const string Column_RecordCount = "RecordCount";
            }
            #endregion

            #region Document Properties Name
            
            public const string usp_Get_DocumentPropertiesName = "[dbo].[usp_Get_DocumentPropertiesName]";
            public const string usp_Create_Update_DocumentPropertiesName = "[dbo].[usp_Create_Update_DocumentPropertiesName]";

            public class usp_Get_DocumentPropertiesName_Parameters
            {
                public const string SystemId = "@SystemId";
            }

            public class usp_Create_Update_DocumentPropertiesName_Parameters
            {
                public const string SystemId = "@SystemId";
                public const string Field1Name = "@Field1Name";
                public const string Field2Name = "@Field2Name";
                public const string Field3Name = "@Field3Name";
                public const string Field4Name = "@Field4Name";
                public const string Field5Name = "@Field5Name";
                public const string Field6Name = "@Field6Name";
                public const string Field7Name = "@Field7Name";
                public const string Field8Name = "@Field8Name";
                public const string Field9Name = "@Field9Name";
                public const string Field10Name = "@Field10Name";
                public const string CreatedBy = "@CreatedBy";
                public const string ErrorCode = "@ErrorCode";
                public const string ErrorDescription = "@ErrorDescription";
            }
            #endregion

            #region Document Folders

            public const string usp_Create_Update_DocumentFolder = "[dbo].[usp_Create_Update_DocumentFolder]";
            public const string usp_Get_DocumentFolders = "[dbo].[usp_Get_DocumentFolders]";
            
            public class usp_Get_DocumentFolders_Parameters
            {
                public const string PageIndex = "@iPageIndex";
                public const string PageSize = "@iPageSize";
                public const string WhereCondition = "@strWhere";
                public const string OrderBy = "@strOrderBy";
                public const string Column_RecordCount = "RecordCount";
            }
            public class usp_Create_Update_DocumentFolder_Parameters
            {
                public const string DocumentFolderId = "@DocumentFolderId";
                public const string SystemId = "@SystemId";
                public const string DocumentFolderName = "@DocumentFolderName";
                public const string ParentDocumentFolderId = "@ParentDocumentFolderId";
                public const string CreatedBy = "@DocumentFolderCreatedBy";
                public const string OutDocumentFolderId = "@OutDocumentFolderId";
                public const string ErrorDescription = "@ErrorDescription";
            }
            #endregion

            #region Document Files

            public const string usp_Create_Update_DocumentFile = "[dbo].[usp_Create_Update_DocumentFile]";
            public const string usp_Create_Update_DocumentFileProperties = "[dbo].[usp_Create_Update_DocumentFileProperties]";
            public const string usp_Get_DocumentFileHistory = "[dbo].[usp_Get_DocumentFileHistory]";
            public const string usp_Get_DocumentFileProperties = "[dbo].[usp_Get_DocumentFileProperties]";
            public const string usp_Get_DocumentFiles = "[dbo].[usp_Get_DocumentFiles]";

            public class usp_Create_Update_DocumentFile_Parameters
            {
                public const string SystemId = "@SystemId";
                public const string FolderId = "@DocumentFolderId";
                public const string FileId = "@DocumentFileId";
                public const string FileName = "@DocumentFileName";
                public const string FileData = "@FileData";
                public const string CreatedBy = "@DocumentFileCreatedBy";
                public const string OutDocumentFileId = "@OutDocumentFileId";
                public const string ErrorDescription = "@ErrorDescription";
            }
            public class usp_Create_Update_DocumentFileProperties_Parameters
            {
                public const string DocumentFileId = "@DocumentFileId";
                public const string Field1Value = "@Field1Value";
                public const string Field2Value = "@Field2Value";
                public const string Field3Value = "@Field3Value";
                public const string Field4Value = "@Field4Value";
                public const string Field5Value = "@Field5Value";
                public const string Field6Value = "@Field6Value";
                public const string Field7Value = "@Field7Value";
                public const string Field8Value = "@Field8Value";
                public const string Field9Value = "@Field9Value";
                public const string Field10Value = "@Field10Value";
                public const string DocumentFilePropertyCreatedBy = "@DocumentFilePropertyCreatedBy";
                public const string OutDocumentFileId = "@OutDocumentFileId";
                public const string ErrorDescription = "@ErrorDescription";
            }
            public class usp_Get_DocumentFileHistory_Parameters
            {
                public const string DocumentFileId = "@DocumentFileId";
            }
            public class usp_Get_DocumentFileProperties_Parameters
            {
                public const string DocumentFileId = "@DocumentFileId";
            }
            public class usp_Get_DocumentFiles_Parameters
            {
                public const string PageIndex = "@iPageIndex";
                public const string PageSize = "@iPageSize";
                public const string WhereCondition = "@strWhere";
                public const string OrderBy = "@strOrderBy";
                public const string Column_RecordCount = "RecordCount";
            }
            #endregion

        }
    }
}
