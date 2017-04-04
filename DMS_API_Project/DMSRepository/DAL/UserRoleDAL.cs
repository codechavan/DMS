using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DMS.Model;
using System.Data;
using DMS.DatabaseConstants;

namespace DMS.Repository.DAL
{
    public abstract class UserRoleDAL
    {
        protected string ConnectionStringName;

        public UserRoleDAL(string connectionStringName)
        {
            this.ConnectionStringName = connectionStringName;
        }

        public abstract FunctionReturnStatus CreateUpdateUserRole(DmsUserRole userRole);

        public abstract DmsUserRoleSearchData GetUserRole(DmsUserRoleSearchParameter searchParameters, PagingDetails pageDetail);

        protected string GetUserRoleSearchParameterString(DmsUserRoleSearchParameter searchParameters)
        {
            List<string> lstConditions = new List<string>();

            if (searchParameters != null)
            {
                if (searchParameters.SystemId != 0)
                {
                    lstConditions.Add(Views.DmsUserRoles.SystemId + "=" + searchParameters.SystemId);
                }
                if (searchParameters.RoleId != 0)
                {
                    lstConditions.Add(Views.DmsUserRoles.UserRoleId + "=" + searchParameters.RoleId);
                }
                if (!string.IsNullOrEmpty(searchParameters.RoleName))
                {
                    lstConditions.Add(Views.DmsUserRoles.UserRoleName + "='" + SQLSafety.GetSQLSafeString(searchParameters.RoleName) + "'");
                }
                if (!string.IsNullOrEmpty(searchParameters.Description))
                {
                    lstConditions.Add(Views.DmsUserRoles.UserRoleDescription + "='" + SQLSafety.GetSQLSafeString(searchParameters.Description) + "'");
                }
                if (!string.IsNullOrEmpty(searchParameters.SystemName))
                {
                    lstConditions.Add(Views.DmsUserRoles.SystemName + "='" + SQLSafety.GetSQLSafeString(searchParameters.SystemName) + "'");
                }
            }
            return string.Join(" AND ", lstConditions);
        }

        protected string GetUserRoleOrderBy(string OrderByString)
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

                    if (columnName == Views.DmsUserRoles.UserRoleName.ToUpper())
                    {
                        lstColumns.Add(Views.DmsUserRoles.UserRoleName + " " + orderBy);
                    }
                    else if (columnName == Views.DmsUserRoles.SystemId.ToUpper())
                    {
                        lstColumns.Add(Views.DmsUserRoles.SystemId + " " + orderBy);
                    }
                    else if (columnName == Views.DmsUserRoles.UserRoleDescription.ToUpper())
                    {
                        lstColumns.Add(Views.DmsUserRoles.UserRoleDescription + " " + orderBy);
                    }
                    else if (columnName == Views.DmsUserRoles.SystemName.ToUpper())
                    {
                        lstColumns.Add(Views.DmsUserRoles.SystemName + " " + orderBy);
                    }
                }
            }
            return string.Join(", ", lstColumns);
        }

        protected List<DmsUserRole> CreateUserRoleObjects(IDataReader objReader)
        {
            List<DmsUserRole> lstRoles = new List<DmsUserRole>();
            DmsUserRole role;
            bool isnull = true;

            while (objReader.Read())
            {
                isnull = false;
                role = new DmsUserRole();
                role.RoleId = objReader[Views.DmsUserRoles.UserRoleId] != DBNull.Value ? Convert.ToInt64(objReader[Views.DmsUserRoles.UserRoleId]) : 0;
                role.SystemId = objReader[Views.DmsUserRoles.SystemId] != DBNull.Value ? Convert.ToInt64(objReader[Views.DmsUserRoles.SystemId]) : 0;
                role.SystemName = objReader[Views.DmsUserRoles.SystemName] != DBNull.Value ? Convert.ToString(objReader[Views.DmsUserRoles.SystemName]) : null;
                role.RoleName = objReader[Views.DmsUserRoles.UserRoleName] != DBNull.Value ? Convert.ToString(objReader[Views.DmsUserRoles.UserRoleName]) : null;
                role.Description = objReader[Views.DmsUserRoles.UserRoleDescription] != DBNull.Value ? Convert.ToString(objReader[Views.DmsUserRoles.UserRoleDescription]) : null;
                role.CreatedOn = objReader[Views.DmsUserRoles.UserRoleCreatedOn] != DBNull.Value ? Convert.ToDateTime(objReader[Views.DmsUserRoles.UserRoleCreatedOn]) : DateTime.Now;
                role.CreatedBy = objReader[Views.DmsUserRoles.UserRoleCreatedBy] != DBNull.Value ? Convert.ToInt64(objReader[Views.DmsUserRoles.UserRoleCreatedBy]) : 0;
                role.ModifiedBy = objReader[Views.DmsUserRoles.UserRoleModifiedBy] != DBNull.Value ? Convert.ToInt64(objReader[Views.DmsUserRoles.UserRoleModifiedBy]) : 0;
                role.ModifiedOn = null;
                if (objReader[Views.DmsUserRoles.UserRoleModifiedBy] != DBNull.Value)
                {
                    role.ModifiedOn = Convert.ToDateTime(objReader[Views.DmsUserRoles.UserRoleModifiedBy]);
                }
                lstRoles.Add(role);
            }

            if (isnull) { return null; }
            else { return lstRoles; }
        }
    }
}
