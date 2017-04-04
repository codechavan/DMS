using DMS.DatabaseConstants;
using DMS.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.Repository.DAL
{
    public abstract class SystemsDAL
    {
        protected string ConnectionStringName;

        public SystemsDAL(string connectionStringName)
        {
            this.ConnectionStringName = connectionStringName;
        }

        public abstract FunctionReturnStatus CreateDmsSystem(DmsSystem system, DmsUser dmsUser, DmsUserRole userRole);

        public abstract FunctionReturnStatus UpdateDmsSystem(DmsSystem system);

        public abstract DmsSystemSearchData GetSystem(DmsSystemSearchParameters searchParameters, PagingDetails pageDetail);


        protected string GetSystemSearchParameterString(DmsSystemSearchParameters searchParameters)
        {
            List<string> lstConditions = new List<string>();

            if (searchParameters != null)
            {
                if (searchParameters.SystemId != 0)
                {
                    lstConditions.Add(TableColumns.DmsSystems.SystemId + "=" + searchParameters.SystemId);
                }
                if (!string.IsNullOrEmpty(searchParameters.SystemName))
                {
                    lstConditions.Add(TableColumns.DmsSystems.SystemName + "='" + SQLSafety.GetSQLSafeString(searchParameters.SystemName) + "'");
                }
                if (searchParameters.IsActive != null)
                {
                    lstConditions.Add(TableColumns.DmsSystems.IsActive + "=" + searchParameters.IsActive);
                }
            }
            return string.Join(" AND ", lstConditions);
        }

        protected string GetSystemOrderBy(string OrderByString)
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
                    if (columnName == TableColumns.DmsSystems.IsActive.ToUpper())
                    {
                        lstColumns.Add(TableColumns.DmsSystems.IsActive + " " + orderBy);
                    }
                    else if (column == TableColumns.DmsSystems.SystemId.ToUpper())
                    {
                        lstColumns.Add(TableColumns.DmsSystems.SystemId + " " + orderBy);
                    }
                    else if (columnName == TableColumns.DmsSystems.SystemName.ToUpper())
                    {
                        lstColumns.Add(TableColumns.DmsSystems.SystemName + " " + orderBy);
                    }
                }
            }
            return string.Join(", ", lstColumns);
        }

        protected List<DmsSystem> CreateSystemObject(IDataReader objReader)
        {
            List<DmsSystem> lstDmsSystem = new List<DmsSystem>();
            DmsSystem dmsSys;
            bool isnull = true;

            while (objReader.Read())
            {
                isnull = false;
                dmsSys = new DmsSystem();
                dmsSys.SystemId = objReader[TableColumns.DmsSystems.SystemId] != DBNull.Value ? Convert.ToInt64(objReader[TableColumns.DmsSystems.SystemId]) : 0;
                dmsSys.SystemName = objReader[TableColumns.DmsSystems.SystemName] != DBNull.Value ? Convert.ToString(objReader[TableColumns.DmsSystems.SystemName]) : null;
                dmsSys.IsActive = objReader[TableColumns.DmsSystems.IsActive] != DBNull.Value ? Convert.ToBoolean(objReader[TableColumns.DmsSystems.IsActive]) : false;
                dmsSys.CreatedOn = objReader[TableColumns.DmsSystems.CreatedOn] != DBNull.Value ? Convert.ToDateTime(objReader[TableColumns.DmsSystems.CreatedOn]) : DateTime.Now;
                dmsSys.ModifiedBy = objReader[TableColumns.DmsSystems.ModifiedBy] != DBNull.Value ? Convert.ToInt64(objReader[TableColumns.DmsSystems.ModifiedBy]) : 0;
                dmsSys.ModifiedOn = null;
                if (objReader[TableColumns.DmsSystems.ModifiedOn] != DBNull.Value)
                {
                    dmsSys.ModifiedOn = Convert.ToDateTime(objReader[TableColumns.DmsSystems.ModifiedOn]);
                }
                lstDmsSystem.Add(dmsSys);
            }

            if (isnull) { return null; }
            else { return lstDmsSystem; }
        }
    }
}
