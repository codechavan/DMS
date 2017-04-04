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
    public abstract class ConfigurationDAL
    {
        protected string ConnectionStringName = String.Empty;

        public ConfigurationDAL(string connectionStringName)
        {
            this.ConnectionStringName = connectionStringName;
        }

        public abstract FunctionReturnStatus UpdateConfigurationValue(SysConfiguration paramValue);

        public abstract SysConfigurationSearchData GetConfigurations(ConfigurationSearchParameter searchParameters, PagingDetails pageDetail);



        protected string GetConfigurationSearchParameterString(ConfigurationSearchParameter searchParameters)
        {
            List<string> lstConditions = new List<string>();

            if (searchParameters != null)
            {
                if (!string.IsNullOrEmpty(searchParameters.ConfigurationCode))
                {
                    lstConditions.Add(Views.vw_Configurations.ConfigurationCode + "='" + SQLSafety.GetSQLSafeString(searchParameters.ConfigurationCode) + "'");
                }
            }
            return string.Join(" AND ", lstConditions);
        }

        protected string GetConfigurationsOrderBy(string OrderByString)
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
                    if (columnName == Views.vw_Configurations.ConfigurationCode.ToUpper())
                    {
                        lstColumns.Add(Views.vw_Configurations.ConfigurationCode + " " + orderBy);
                    }
                    else if (column == Views.vw_Configurations.DefaultValue.ToUpper())
                    {
                        lstColumns.Add(Views.vw_Configurations.DefaultValue + " " + orderBy);
                    }
                    else if (column == Views.vw_Configurations.Description.ToUpper())
                    {
                        lstColumns.Add(Views.vw_Configurations.Description + " " + orderBy);
                    }
                    else if (column == Views.vw_Configurations.ModifiedByUserName.ToUpper())
                    {
                        lstColumns.Add(Views.vw_Configurations.ModifiedByUserName + " " + orderBy);
                    }
                    else if (column == Views.vw_Configurations.Remarks.ToUpper())
                    {
                        lstColumns.Add(Views.vw_Configurations.Remarks + " " + orderBy);
                    }
                    else if (column == Views.vw_Configurations.Value.ToUpper())
                    {
                        lstColumns.Add(Views.vw_Configurations.Value + " " + orderBy);
                    }
                    else if (column == Views.vw_Configurations.ModifiedOn.ToUpper())
                    {
                        lstColumns.Add(Views.vw_Configurations.ModifiedOn + " " + orderBy);
                    }
                }
            }
            return string.Join(", ", lstColumns);
        }

        protected List<SysConfiguration> CreateConfigurationObject(IDataReader objReader)
        {
            List<SysConfiguration> lstConfigurations = new List<SysConfiguration>();
            SysConfiguration configValues;
            bool isnull = true;

            while (objReader.Read())
            {
                isnull = false;
                configValues = new SysConfiguration();
                configValues.ConfigurationCode = objReader[Views.vw_Configurations.ConfigurationCode] != DBNull.Value ? Convert.ToString(objReader[Views.vw_Configurations.ConfigurationCode]) : null;
                configValues.DefaultValue = objReader[Views.vw_Configurations.DefaultValue] != DBNull.Value ? Convert.ToString(objReader[Views.vw_Configurations.DefaultValue]) : null;
                configValues.Description = objReader[Views.vw_Configurations.Description] != DBNull.Value ? Convert.ToString(objReader[Views.vw_Configurations.Description]) : null;
                configValues.ModifiedByUserName = objReader[Views.vw_Configurations.ModifiedByUserName] != DBNull.Value ? Convert.ToString(objReader[Views.vw_Configurations.ModifiedByUserName]) : null;
                configValues.Remarks = objReader[Views.vw_Configurations.Remarks] != DBNull.Value ? Convert.ToString(objReader[Views.vw_Configurations.Remarks]) : null;
                configValues.Value = objReader[Views.vw_Configurations.Value] != DBNull.Value ? Convert.ToString(objReader[Views.vw_Configurations.Value]) : null;

                configValues.ModifiedBy = objReader[Views.vw_Configurations.ModifiedBy] != DBNull.Value ? Convert.ToInt64(objReader[Views.vw_Configurations.ModifiedBy]) : 0;
                configValues.ModifiedOn = null;
                if (objReader[Views.vw_Configurations.ModifiedOn] != DBNull.Value)
                {
                    configValues.ModifiedOn = Convert.ToDateTime(objReader[Views.vw_Configurations.ModifiedOn]);
                }
                lstConfigurations.Add(configValues);
            }

            if (isnull) { return null; }
            else { return lstConfigurations; }
        }

    }
}
