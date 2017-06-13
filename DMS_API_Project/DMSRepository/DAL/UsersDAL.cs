using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DMS.Model;
using DMS.Repository.SQL;
using DMS.DatabaseConstants;
using System.Data;

namespace DMS.Repository.DAL
{
    public abstract class UsersDAL : SQLScallerFunctions
    {

        public UsersDAL(string connectionStringName)
            : base(connectionStringName)
        {
        }

        public abstract FunctionReturnStatus CreateUpdateUser(DmsUser user);

        public abstract DmsUserSearchData GetUser(DmsUserSearchParameter searchParameters);

        public abstract FunctionReturnStatus Login(long systemId, string username, string password);

        public abstract FunctionReturnStatus ChangePasword(long systemId, long userId, string oldpassword, string newpassword, long updatedByUser);

        public abstract FunctionReturnStatus UnlockUser(long systemId, long userId, long updatedByUser);

        public abstract BasicAuthenticationIdentity AuthenticateLogonToken(string logonToken);


        protected List<DmsUser> CreateUserObject(IDataReader objReader)
        {
            List<DmsUser> lstDmsUser = new List<DmsUser>();
            DmsUser usr;
            bool isnull = true;

            while (objReader.Read())
            {
                isnull = false;
                usr = new DmsUser();
                usr.UserId = objReader[Views.DmsUsers.UserId] != DBNull.Value ? Convert.ToInt64(objReader[Views.DmsUsers.UserId]) : 0;
                usr.SystemId = objReader[Views.DmsUsers.SystemId] != DBNull.Value ? Convert.ToInt64(objReader[Views.DmsUsers.SystemId]) : 0;
                usr.RoleID = objReader[Views.DmsUsers.UserRoleId] != DBNull.Value ? Convert.ToInt64(objReader[Views.DmsUsers.UserRoleId]) : 0;
                usr.UserName = objReader[Views.DmsUsers.UserName] != DBNull.Value ? Convert.ToString(objReader[Views.DmsUsers.UserName]) : null;
                usr.FullName = objReader[Views.DmsUsers.UserFullName] != DBNull.Value ? Convert.ToString(objReader[Views.DmsUsers.UserFullName]) : null;
                usr.EmailId = objReader[Views.DmsUsers.UserEmailId] != DBNull.Value ? Convert.ToString(objReader[Views.DmsUsers.UserEmailId]) : null;
                usr.Password = Chavan.Common.EncryptionHelper.Instance.Decrypt(objReader[Views.DmsUsers.UserPassword] != DBNull.Value ? Convert.ToString(objReader[Views.DmsUsers.UserPassword]) : null);
                usr.IsActive = objReader[Views.DmsUsers.UserIsActive] != DBNull.Value ? Convert.ToBoolean(objReader[Views.DmsUsers.UserIsActive]) : false;
                usr.IsAdmin = objReader[Views.DmsUsers.UserIsAdmin] != DBNull.Value ? Convert.ToBoolean(objReader[Views.DmsUsers.UserIsAdmin]) : false;
                usr.IsLock = objReader[Views.DmsUsers.UserIsLock] != DBNull.Value ? Convert.ToBoolean(objReader[Views.DmsUsers.UserIsLock]) : false;
                usr.LastLoginDate = null;
                if (objReader[Views.DmsUsers.UserLastLoginDate] != DBNull.Value)
                {
                    usr.LastLoginDate = Convert.ToDateTime(objReader[Views.DmsUsers.UserLastLoginDate]);
                }
                usr.LastPasswordChangedBy = objReader[Views.DmsUsers.UserLastPasswordChangedBy] != DBNull.Value ? Convert.ToInt64(objReader[Views.DmsUsers.UserLastPasswordChangedBy]) : 0;
                usr.LastPasswordChangedOn = null;
                if (objReader[Views.DmsUsers.UserLastPasswordChangedOn] != DBNull.Value)
                {
                    usr.LastPasswordChangedOn = Convert.ToDateTime(objReader[Views.DmsUsers.UserLastPasswordChangedOn]);
                }
                usr.LastUnblockBy = objReader[Views.DmsUsers.UserLastUnblockBy] != DBNull.Value ? Convert.ToInt64(objReader[Views.DmsUsers.UserLastUnblockBy]) : 0;
                usr.LoginFailCount = objReader[Views.DmsUsers.UserLoginFailCount] != DBNull.Value ? Convert.ToInt32(objReader[Views.DmsUsers.UserLoginFailCount]) : 0;
                usr.CreatedBy = objReader[Views.DmsUsers.UserCreatedBy] != DBNull.Value ? Convert.ToInt64(objReader[Views.DmsUsers.UserCreatedBy]) : 0;
                usr.ModifiedBy = objReader[Views.DmsUsers.UserModifiedBy] != DBNull.Value ? Convert.ToInt64(objReader[Views.DmsUsers.UserModifiedBy]) : 0;
                usr.CreatedOn = objReader[Views.DmsUsers.UserCreatedOn] != DBNull.Value ? Convert.ToDateTime(objReader[Views.DmsUsers.UserCreatedOn]) : DateTime.Now;
                usr.ModifiedOn = null;
                if (objReader[Views.DmsUsers.UserModifiedOn] != DBNull.Value)
                {
                    usr.ModifiedOn = Convert.ToDateTime(objReader[Views.DmsUsers.UserModifiedOn]);
                }
                usr.RoleName = objReader[Views.DmsUsers.UserRoleName] != DBNull.Value ? Convert.ToString(objReader[Views.DmsUsers.UserRoleName]) : null;
                usr.UserRoleDescription = objReader[Views.DmsUsers.UserRoleDescription] != DBNull.Value ? Convert.ToString(objReader[Views.DmsUsers.UserRoleDescription]) : null;
                usr.SystemName = objReader[Views.DmsUsers.SystemName] != DBNull.Value ? Convert.ToString(objReader[Views.DmsUsers.SystemName]) : null;
                lstDmsUser.Add(usr);
            }

            if (isnull) { return null; }
            else { return lstDmsUser; }
        }

        protected BasicAuthenticationIdentity CreateBasicAuthenticationIdentity(IDataReader objReader)
        {
            BasicAuthenticationIdentity identity = null;

            if (objReader.Read())
            {
                long UserId = objReader[Views.Usp_Get_LogonDetail.UserId] != DBNull.Value ? Convert.ToInt64(objReader[Views.Usp_Get_LogonDetail.UserId]) : 0;

                identity = new BasicAuthenticationIdentity(UserId);

                identity.Password = objReader[Views.Usp_Get_LogonDetail.Password] != DBNull.Value ? Convert.ToString(objReader[Views.Usp_Get_LogonDetail.Password]) : null;
                identity.SystemID = objReader[Views.Usp_Get_LogonDetail.SystemId] != DBNull.Value ? Convert.ToInt64(objReader[Views.Usp_Get_LogonDetail.SystemId]) : 0;
                identity.UserName = objReader[Views.Usp_Get_LogonDetail.UserName] != DBNull.Value ? Convert.ToString(objReader[Views.Usp_Get_LogonDetail.UserName]) : null;
                identity.SystemName = objReader[Views.Usp_Get_LogonDetail.SystemName] != DBNull.Value ? Convert.ToString(objReader[Views.Usp_Get_LogonDetail.SystemName]) : null;
                identity.LoginDate = null;
                if (objReader[Views.Usp_Get_LogonDetail.LogonTime] != DBNull.Value)
                {
                    identity.LoginDate = Convert.ToDateTime(objReader[Views.Usp_Get_LogonDetail.LogonTime]);
                }
            }
            identity.IsSuccess = objReader[Views.Usp_Get_LogonDetail.IsSuccess] != DBNull.Value ? Convert.ToBoolean(objReader[Views.Usp_Get_LogonDetail.IsSuccess]) : false;

            return identity;
        }

        protected string GetUserSearchParameterString(DmsUserSearchParameter searchParameters)
        {
            List<string> lstConditions = new List<string>();

            if (searchParameters != null)
            {
                if (searchParameters.SystemId != 0)
                {
                    lstConditions.Add(Views.DmsUsers.SystemId + "=" + searchParameters.SystemId);
                }
                if (searchParameters.UserId != 0)
                {
                    lstConditions.Add(Views.DmsUsers.UserId + "=" + searchParameters.UserId);
                }
                if (searchParameters.UserRoleId != 0)
                {
                    lstConditions.Add(Views.DmsUsers.UserRoleId + "=" + searchParameters.UserRoleId);
                }
                if (!string.IsNullOrEmpty(searchParameters.UserName))
                {
                    lstConditions.Add(Views.DmsUsers.UserName + "='" + SQLSafety.GetSQLSafeString(searchParameters.UserName) + "'");
                }
                if (!string.IsNullOrEmpty(searchParameters.FullName))
                {
                    lstConditions.Add(Views.DmsUsers.UserFullName + "='" + SQLSafety.GetSQLSafeString(searchParameters.FullName) + "'");
                }
            }
            return string.Join(" AND ", lstConditions);
        }

        protected string GetUserOrderBy(string OrderByString)
        {
            List<string> lstColumns = new List<string>();
            string columnName, orderBy;
            if (!string.IsNullOrEmpty(OrderByString))
            {
                foreach (string column in OrderByString.Split(','))
                {
                    if (column.Split(' ').Length == 2)
                    {
                        columnName = column.Split(' ')[0].Trim();
                        orderBy = column.Split(' ')[1].Trim();
                        switch (orderBy.ToLower())
                        {
                            case "asc":
                                orderBy = "ASC";
                                break;
                            case "desc":
                                orderBy = "DESC";
                                break;
                            default:
                                orderBy = "ASC";
                                break;
                        }
                    }
                    else
                    {
                        columnName = column.Trim();
                        orderBy = "ASC";
                    }
                    columnName = columnName.ToUpper();

                    if (columnName == Views.DmsUsers.UserName.ToUpper())
                    {
                        lstColumns.Add(Views.DmsUsers.UserName + " " + orderBy);
                    }
                    else if (columnName == Views.DmsUsers.SystemId.ToUpper())
                    {
                        lstColumns.Add(Views.DmsUsers.SystemId + " " + orderBy);
                    }
                    else if (columnName == Views.DmsUsers.UserId.ToUpper())
                    {
                        lstColumns.Add(Views.DmsUsers.UserId + " " + orderBy);
                    }
                    else if (columnName == Views.DmsUsers.UserFullName.ToUpper())
                    {
                        lstColumns.Add(Views.DmsUsers.UserFullName + " " + orderBy);
                    }
                    else if (columnName == Views.DmsUsers.SystemName.ToUpper())
                    {
                        lstColumns.Add(Views.DmsUsers.SystemName + " " + orderBy);
                    }
                    else if (columnName == Views.DmsUsers.UserIsLock.ToUpper())
                    {
                        lstColumns.Add(Views.DmsUsers.UserIsLock + " " + orderBy);
                    }
                    else if (columnName == Views.DmsUsers.UserIsActive.ToUpper())
                    {
                        lstColumns.Add(Views.DmsUsers.UserIsActive + " " + orderBy);
                    }
                    else if (columnName == Views.DmsUsers.UserIsAdmin.ToUpper())
                    {
                        lstColumns.Add(Views.DmsUsers.UserIsAdmin + " " + orderBy);
                    }
                    else if (columnName == Views.DmsUsers.UserRoleName.ToUpper())
                    {
                        lstColumns.Add(Views.DmsUsers.UserRoleName + " " + orderBy);
                    }
                }
            }
            return string.Join(", ", lstColumns);
        }

    }
}
