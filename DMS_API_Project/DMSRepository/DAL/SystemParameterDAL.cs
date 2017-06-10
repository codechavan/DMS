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
    public abstract class SystemParameterDAL
    {
        protected string ConnectionStringName = String.Empty;

        public SystemParameterDAL(string connectionStringName)
        {
            this.ConnectionStringName = connectionStringName;
        }

        public abstract FunctionReturnStatus UpdateSystemParameterValue(SystemParameterValue paramValue);

        public abstract SystemParameterValueSearchData GetSystemParameterValue(SystemParameterSearchParameters searchParameters);

        public abstract List<SystemParameter> GetSystemParameter(long systemParameterId);



        protected string GetSystemParameterValueSearchParameterString(SystemParameterSearchParameters searchParameters)
        {
            List<string> lstConditions = new List<string>();

            if (searchParameters != null)
            {
                lstConditions.Add(Views.vw_SystemParameterValues.SystemId + "=" + searchParameters.SystemId);
                if (!string.IsNullOrEmpty(searchParameters.SystemParameterName))
                {
                    lstConditions.Add(Views.vw_SystemParameterValues.ParameterName + "='" + SQLSafety.GetSQLSafeString(searchParameters.SystemParameterName) + "'");
                }
                if (searchParameters.SystemParameterId > 0)
                {
                    lstConditions.Add(Views.vw_SystemParameterValues.SystemParameterId + "=" + searchParameters.SystemParameterId);
                }
            }
            return string.Join(" AND ", lstConditions);
        }

        protected string GetSystemParameterValueOrderBy(string OrderByString)
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
                    if (columnName == Views.vw_SystemParameterValues.ParameterName.ToUpper())
                    {
                        lstColumns.Add(Views.vw_SystemParameterValues.ParameterName + " " + orderBy);
                    }
                    else if (column == Views.vw_SystemParameterValues.Description.ToUpper())
                    {
                        lstColumns.Add(Views.vw_SystemParameterValues.Description + " " + orderBy);
                    }
                    else if (columnName == Views.vw_SystemParameterValues.CreatedOn.ToUpper())
                    {
                        lstColumns.Add(Views.vw_SystemParameterValues.CreatedOn + " " + orderBy);
                    }
                    else if (columnName == Views.vw_SystemParameterValues.ModifiedOn.ToUpper())
                    {
                        lstColumns.Add(Views.vw_SystemParameterValues.ModifiedOn + " " + orderBy);
                    }
                    else if (columnName == Views.vw_SystemParameterValues.SystemParameterId.ToUpper())
                    {
                        lstColumns.Add(Views.vw_SystemParameterValues.SystemParameterId + " " + orderBy);
                    }
                    else if (columnName == Views.vw_SystemParameterValues.ParameterValue.ToUpper())
                    {
                        lstColumns.Add(Views.vw_SystemParameterValues.ParameterValue + " " + orderBy);
                    }
                }
            }
            return string.Join(", ", lstColumns);
        }

        protected List<SystemParameterValue> CreateSystemParameterValueObject(IDataReader objReader)
        {
            List<SystemParameterValue> lstParamValues = new List<SystemParameterValue>();
            SystemParameterValue paramValue;
            bool isnull = true;

            while (objReader.Read())
            {
                isnull = false;
                paramValue = new SystemParameterValue();
                paramValue.SystemId = objReader[Views.vw_SystemParameterValues.SystemId] != DBNull.Value ? Convert.ToInt64(objReader[Views.vw_SystemParameterValues.SystemId]) : 0;
                paramValue.SystemName = objReader[Views.vw_SystemParameterValues.SystemName] != DBNull.Value ? Convert.ToString(objReader[Views.vw_SystemParameterValues.SystemName]) : null;
                paramValue.DefaultValue = objReader[Views.vw_SystemParameterValues.DefaultValue] != DBNull.Value ? Convert.ToString(objReader[Views.vw_SystemParameterValues.DefaultValue]) : null;
                paramValue.Description = objReader[Views.vw_SystemParameterValues.Description] != DBNull.Value ? Convert.ToString(objReader[Views.vw_SystemParameterValues.Description]) : null;
                paramValue.ParameterName = objReader[Views.vw_SystemParameterValues.ParameterName] != DBNull.Value ? Convert.ToString(objReader[Views.vw_SystemParameterValues.ParameterName]) : null;
                paramValue.ParameterValue = objReader[Views.vw_SystemParameterValues.ParameterValue] != DBNull.Value ? Convert.ToString(objReader[Views.vw_SystemParameterValues.ParameterValue]) : null;
                paramValue.ParameterId = objReader[Views.vw_SystemParameterValues.SystemParameterId] != DBNull.Value ? Convert.ToInt64(objReader[Views.vw_SystemParameterValues.SystemParameterId]) : 0;

                paramValue.CreatedOn = objReader[Views.vw_SystemParameterValues.CreatedOn] != DBNull.Value ? Convert.ToDateTime(objReader[Views.vw_SystemParameterValues.CreatedOn]) : DateTime.Now;
                paramValue.CreatedBy = objReader[Views.vw_SystemParameterValues.CreatedBy] != DBNull.Value ? Convert.ToInt64(objReader[Views.vw_SystemParameterValues.CreatedBy]) : 0;
                paramValue.ModifiedBy = objReader[Views.vw_SystemParameterValues.ModifiedBy] != DBNull.Value ? Convert.ToInt64(objReader[Views.vw_SystemParameterValues.ModifiedBy]) : 0;
                paramValue.ModifiedOn = null;
                if (objReader[Views.vw_SystemParameterValues.ModifiedOn] != DBNull.Value)
                {
                    paramValue.ModifiedOn = Convert.ToDateTime(objReader[Views.vw_SystemParameterValues.ModifiedOn]);
                }
                lstParamValues.Add(paramValue);
            }

            if (isnull) { return null; }
            else { return lstParamValues; }
        }

        protected List<SystemParameter> CreateSystemParameterObject(IDataReader objReader)
        {
            List<SystemParameter> lstParam = new List<SystemParameter>();
            SystemParameter param;
            bool isnull = true;

            while (objReader.Read())
            {
                isnull = false;
                param = new SystemParameter();

                param.SystemParameterId = objReader[Views.vw_SystemParameterValues.SystemParameterId] != DBNull.Value ? Convert.ToInt64(objReader[Views.vw_SystemParameterValues.SystemParameterId]) : 0;
                param.SystemParameterName = objReader[Views.vw_SystemParameterValues.ParameterName] != DBNull.Value ? Convert.ToString(objReader[Views.vw_SystemParameterValues.ParameterName]) : null;
                param.ParameterDescription = objReader[Views.vw_SystemParameterValues.Description] != DBNull.Value ? Convert.ToString(objReader[Views.vw_SystemParameterValues.Description]) : null;
                param.ParameterDefaultValue = objReader[Views.vw_SystemParameterValues.DefaultValue] != DBNull.Value ? Convert.ToString(objReader[Views.vw_SystemParameterValues.DefaultValue]) : null;
                param.CreatedOn = objReader[Views.vw_SystemParameterValues.CreatedOn] != DBNull.Value ? Convert.ToDateTime(objReader[Views.vw_SystemParameterValues.CreatedOn]) : DateTime.Now;

                lstParam.Add(param);
            }

            if (isnull) { return null; }
            else { return lstParam; }
        }
    }
}
