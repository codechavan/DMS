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
    public abstract class SystemAdminDAL : SQLScallerFunctions
    {

        public SystemAdminDAL(string connectionStringName)
            : base(connectionStringName)
        {
        }

        public abstract FunctionReturnStatus CreateUpdateSystemAdmin(SystemAdmin admin);

        public abstract SystemAdminSearchData GetSystemAdmin(SystemAdminSearchParameter searchParameters, PagingDetails pageDetail);

        public abstract FunctionReturnStatus Login(string username, string password);

        public abstract FunctionReturnStatus ChangePasword(long userId, string oldpassword, string newpassword, long updatedByUser);

        public abstract SystemAdminLoginIdentity AuthenticateSystemAdminLogonToken(string logonToken);


        protected List<SystemAdmin> CreateSystemAdminObject(IDataReader objReader)
        {
            List<SystemAdmin> lstAdminUsers = new List<SystemAdmin>();
            SystemAdmin usr;
            bool isnull = true;

            while (objReader.Read())
            {
                isnull = false;
                usr = new SystemAdmin();
                usr.AdminId = objReader[TableColumns.SystemAdmins.AdminId] != DBNull.Value ? Convert.ToInt64(objReader[TableColumns.SystemAdmins.AdminId]) : 0;
                usr.UserName = objReader[TableColumns.SystemAdmins.UserName] != DBNull.Value ? Convert.ToString(objReader[TableColumns.SystemAdmins.UserName]) : null;
                usr.FullName = objReader[TableColumns.SystemAdmins.FullName] != DBNull.Value ? Convert.ToString(objReader[TableColumns.SystemAdmins.FullName]) : null;
                usr.EmailId = objReader[TableColumns.SystemAdmins.EmailId] != DBNull.Value ? Convert.ToString(objReader[TableColumns.SystemAdmins.EmailId]) : null;
                usr.Password = objReader[TableColumns.SystemAdmins.Password] != DBNull.Value ? Convert.ToString(objReader[TableColumns.SystemAdmins.Password]) : null;
                usr.LastLogin = null;
                if (objReader[TableColumns.SystemAdmins.LastLogin] != DBNull.Value)
                {
                    usr.LastLogin = Convert.ToDateTime(objReader[TableColumns.SystemAdmins.LastLogin]);
                }
                usr.LastPasswordChangedBy = objReader[TableColumns.SystemAdmins.LastPasswordChangedBy] != DBNull.Value ? Convert.ToInt64(objReader[TableColumns.SystemAdmins.LastPasswordChangedBy]) : 0;
                usr.LastPasswordChangedOn = null;
                if (objReader[TableColumns.SystemAdmins.LastPasswordChangedOn] != DBNull.Value)
                {
                    usr.LastPasswordChangedOn = Convert.ToDateTime(objReader[TableColumns.SystemAdmins.LastPasswordChangedOn]);
                }
                usr.CreatedBy = objReader[TableColumns.SystemAdmins.CreatedBy] != DBNull.Value ? Convert.ToInt64(objReader[TableColumns.SystemAdmins.CreatedBy]) : 0;
                usr.ModifiedBy = objReader[TableColumns.SystemAdmins.ModifiedBy] != DBNull.Value ? Convert.ToInt64(objReader[TableColumns.SystemAdmins.ModifiedBy]) : 0;
                usr.CreatedOn = objReader[TableColumns.SystemAdmins.CreatedOn] != DBNull.Value ? Convert.ToDateTime(objReader[TableColumns.SystemAdmins.CreatedOn]) : DateTime.Now;
                usr.ModifiedOn = null;
                if (objReader[TableColumns.SystemAdmins.ModifiedOn] != DBNull.Value)
                {
                    usr.ModifiedOn = Convert.ToDateTime(objReader[TableColumns.SystemAdmins.ModifiedOn]);
                }
                lstAdminUsers.Add(usr);
            }

            if (isnull) { return null; }
            else { return lstAdminUsers; }
        }

        protected SystemAdminLoginIdentity CreateBasicAuthenticationIdentity(IDataReader objReader)
        {
            SystemAdminLoginIdentity identity = null;

            if (objReader.Read())
            {
                long AdminId = objReader[Views.usp_Get_SystemAdminLogonDetail.AdminId] != DBNull.Value ? Convert.ToInt64(objReader[Views.usp_Get_SystemAdminLogonDetail.AdminId]) : 0;
                identity = new SystemAdminLoginIdentity(AdminId);
                identity.Password = objReader[Views.usp_Get_SystemAdminLogonDetail.Password] != DBNull.Value ? Convert.ToString(objReader[Views.usp_Get_SystemAdminLogonDetail.Password]) : null;
                identity.UserName = objReader[Views.usp_Get_SystemAdminLogonDetail.UserName] != DBNull.Value ? Convert.ToString(objReader[Views.usp_Get_SystemAdminLogonDetail.UserName]) : null;
                identity.LoginDate = null;
                if (objReader[Views.usp_Get_SystemAdminLogonDetail.LogonTime] != DBNull.Value)
                {
                    identity.LoginDate = Convert.ToDateTime(objReader[Views.usp_Get_SystemAdminLogonDetail.LogonTime]);
                }
            }
            identity.IsSuccess = objReader[Views.usp_Get_SystemAdminLogonDetail.IsSuccess] != DBNull.Value ? Convert.ToBoolean(objReader[Views.usp_Get_SystemAdminLogonDetail.IsSuccess]) : false;

            return identity;
        }

        protected string GetSystemAdminParameterString(SystemAdminSearchParameter searchParameters)
        {
            List<string> lstConditions = new List<string>();

            if (searchParameters != null)
            {
                if (searchParameters.AdminId != 0)
                {
                    lstConditions.Add(TableColumns.SystemAdmins.AdminId + "=" + searchParameters.AdminId);
                }
                if (!string.IsNullOrEmpty(searchParameters.UserName))
                {
                    lstConditions.Add(TableColumns.SystemAdmins.UserName + "='" + SQLSafety.GetSQLSafeString(searchParameters.UserName) + "'");
                }
                if (!string.IsNullOrEmpty(searchParameters.FullName))
                {
                    lstConditions.Add(TableColumns.SystemAdmins.FullName + "='" + SQLSafety.GetSQLSafeString(searchParameters.FullName) + "'");
                }
                if (!string.IsNullOrEmpty(searchParameters.EmailId))
                {
                    lstConditions.Add(TableColumns.SystemAdmins.EmailId + "='" + SQLSafety.GetSQLSafeString(searchParameters.EmailId) + "'");
                }
            }
            return string.Join(" AND ", lstConditions);
        }

        protected string GetSystemAdminOrderBy(string OrderByString)
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

                    if (columnName == TableColumns.SystemAdmins.UserName.ToUpper())
                    {
                        lstColumns.Add(TableColumns.SystemAdmins.UserName + " " + orderBy);
                    }
                    else if (columnName == TableColumns.SystemAdmins.AdminId.ToUpper())
                    {
                        lstColumns.Add(TableColumns.SystemAdmins.AdminId + " " + orderBy);
                    }
                    else if (columnName == TableColumns.SystemAdmins.FullName.ToUpper())
                    {
                        lstColumns.Add(TableColumns.SystemAdmins.FullName + " " + orderBy);
                    }
                    else if (columnName == TableColumns.SystemAdmins.EmailId.ToUpper())
                    {
                        lstColumns.Add(TableColumns.SystemAdmins.EmailId + " " + orderBy);
                    }
                    else if (columnName == TableColumns.SystemAdmins.ModifiedOn.ToUpper())
                    {
                        lstColumns.Add(TableColumns.SystemAdmins.ModifiedOn + " " + orderBy);
                    }
                    else if (columnName == TableColumns.SystemAdmins.CreatedOn.ToUpper())
                    {
                        lstColumns.Add(TableColumns.SystemAdmins.CreatedOn + " " + orderBy);
                    }
                }
            }
            return string.Join(", ", lstColumns);
        }

    }
}
