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
    public abstract class DocumentPropertiesNamesDAL
    {
        protected string ConnectionStringName = String.Empty;

        public DocumentPropertiesNamesDAL(string connectionStringName)
        {
            this.ConnectionStringName = connectionStringName;
        }

        public abstract FunctionReturnStatus CreateUpdateDocumentPropertiesNames(DocumentPropertiesNames propertiesName);

        public abstract DocumentPropertiesNames GetDocumentPropertiesNames(DocumentPropertiesNamesSearchParamater searchParameters, PagingDetails pageDetail);


        protected string GetDocumentPropertiesNamesSearchParameterString(DocumentPropertiesNamesSearchParamater searchParameters)
        {
            List<string> lstConditions = new List<string>();

            if (searchParameters != null)
            {
                if (searchParameters.SystemId > 0)
                {
                    lstConditions.Add(Views.vw_DocumentFilePropertiesNames.SystemId + "=" + searchParameters.SystemId);
                }
                if (!string.IsNullOrEmpty(searchParameters.SystemName))
                {
                    lstConditions.Add(Views.vw_DocumentFilePropertiesNames.SystemName + "='" + SQLSafety.GetSQLSafeString(searchParameters.SystemName) + "'");
                }
            }
            return string.Join(" AND ", lstConditions);
        }

        protected string GetDocumentPropertiesNamesOrderBy(string OrderByString)
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
                    if (columnName == Views.vw_DocumentFilePropertiesNames.SystemId.ToUpper())
                    {
                        lstColumns.Add(Views.vw_DocumentFilePropertiesNames.SystemId + " " + orderBy);
                    }
                    else if (columnName == Views.vw_DocumentFilePropertiesNames.SystemName.ToUpper())
                    {
                        lstColumns.Add(Views.vw_DocumentFilePropertiesNames.SystemName + " " + orderBy);
                    }
                    else if (columnName == Views.vw_DocumentFilePropertiesNames.Field1Name.ToUpper())
                    {
                        lstColumns.Add(Views.vw_DocumentFilePropertiesNames.Field1Name + " " + orderBy);
                    }
                    else if (columnName == Views.vw_DocumentFilePropertiesNames.Field2Name.ToUpper())
                    {
                        lstColumns.Add(Views.vw_DocumentFilePropertiesNames.Field2Name + " " + orderBy);
                    }
                    else if (columnName == Views.vw_DocumentFilePropertiesNames.Field3Name.ToUpper())
                    {
                        lstColumns.Add(Views.vw_DocumentFilePropertiesNames.Field3Name + " " + orderBy);
                    }
                    else if (columnName == Views.vw_DocumentFilePropertiesNames.Field4Name.ToUpper())
                    {
                        lstColumns.Add(Views.vw_DocumentFilePropertiesNames.Field4Name + " " + orderBy);
                    }
                    else if (columnName == Views.vw_DocumentFilePropertiesNames.Field5Name.ToUpper())
                    {
                        lstColumns.Add(Views.vw_DocumentFilePropertiesNames.Field5Name + " " + orderBy);
                    }
                    else if (columnName == Views.vw_DocumentFilePropertiesNames.Field6Name.ToUpper())
                    {
                        lstColumns.Add(Views.vw_DocumentFilePropertiesNames.Field6Name + " " + orderBy);
                    }
                    else if (columnName == Views.vw_DocumentFilePropertiesNames.Field7Name.ToUpper())
                    {
                        lstColumns.Add(Views.vw_DocumentFilePropertiesNames.Field7Name + " " + orderBy);
                    }
                    else if (columnName == Views.vw_DocumentFilePropertiesNames.Field8Name.ToUpper())
                    {
                        lstColumns.Add(Views.vw_DocumentFilePropertiesNames.Field8Name + " " + orderBy);
                    }
                    else if (columnName == Views.vw_DocumentFilePropertiesNames.Field9Name.ToUpper())
                    {
                        lstColumns.Add(Views.vw_DocumentFilePropertiesNames.Field9Name + " " + orderBy);
                    }
                    else if (columnName == Views.vw_DocumentFilePropertiesNames.Field10Name.ToUpper())
                    {
                        lstColumns.Add(Views.vw_DocumentFilePropertiesNames.Field10Name + " " + orderBy);
                    }
                    else if (columnName == Views.vw_DocumentFilePropertiesNames.CreatedOn.ToUpper())
                    {
                        lstColumns.Add(Views.vw_DocumentFilePropertiesNames.CreatedOn + " " + orderBy);
                    }
                    else if (columnName == Views.vw_DocumentFilePropertiesNames.ModifiedOn.ToUpper())
                    {
                        lstColumns.Add(Views.vw_DocumentFilePropertiesNames.ModifiedOn + " " + orderBy);
                    }
                }
            }
            return string.Join(", ", lstColumns);
        }

        protected List<DocumentPropertiesNames> CreateDocumentPropertiesNamesObject(IDataReader objReader)
        {
            List<DocumentPropertiesNames> lstConfigurations = new List<DocumentPropertiesNames>();
            DocumentPropertiesNames configValues;
            bool isnull = true;

            while (objReader.Read())
            {
                isnull = false;
                configValues = new DocumentPropertiesNames();
                configValues.SystemId = objReader[Views.vw_DocumentFilePropertiesNames.SystemId] != DBNull.Value ? Convert.ToInt64(objReader[Views.vw_DocumentFilePropertiesNames.SystemId]) : 0;
                configValues.SystemName = objReader[Views.vw_DocumentFilePropertiesNames.SystemName] != DBNull.Value ? Convert.ToString(objReader[Views.vw_DocumentFilePropertiesNames.SystemName]) : null;

                configValues.Field1Name = objReader[Views.vw_DocumentFilePropertiesNames.Field1Name] != DBNull.Value ? Convert.ToString(objReader[Views.vw_DocumentFilePropertiesNames.Field1Name]) : null;
                configValues.Field2Name = objReader[Views.vw_DocumentFilePropertiesNames.Field2Name] != DBNull.Value ? Convert.ToString(objReader[Views.vw_DocumentFilePropertiesNames.Field2Name]) : null;
                configValues.Field3Name = objReader[Views.vw_DocumentFilePropertiesNames.Field3Name] != DBNull.Value ? Convert.ToString(objReader[Views.vw_DocumentFilePropertiesNames.Field3Name]) : null;
                configValues.Field4Name = objReader[Views.vw_DocumentFilePropertiesNames.Field4Name] != DBNull.Value ? Convert.ToString(objReader[Views.vw_DocumentFilePropertiesNames.Field4Name]) : null;
                configValues.Field5Name = objReader[Views.vw_DocumentFilePropertiesNames.Field5Name] != DBNull.Value ? Convert.ToString(objReader[Views.vw_DocumentFilePropertiesNames.Field5Name]) : null;
                configValues.Field6Name = objReader[Views.vw_DocumentFilePropertiesNames.Field6Name] != DBNull.Value ? Convert.ToString(objReader[Views.vw_DocumentFilePropertiesNames.Field6Name]) : null;
                configValues.Field7Name = objReader[Views.vw_DocumentFilePropertiesNames.Field7Name] != DBNull.Value ? Convert.ToString(objReader[Views.vw_DocumentFilePropertiesNames.Field7Name]) : null;
                configValues.Field8Name = objReader[Views.vw_DocumentFilePropertiesNames.Field8Name] != DBNull.Value ? Convert.ToString(objReader[Views.vw_DocumentFilePropertiesNames.Field8Name]) : null;
                configValues.Field9Name = objReader[Views.vw_DocumentFilePropertiesNames.Field9Name] != DBNull.Value ? Convert.ToString(objReader[Views.vw_DocumentFilePropertiesNames.Field9Name]) : null;
                configValues.Field10Name = objReader[Views.vw_DocumentFilePropertiesNames.Field10Name] != DBNull.Value ? Convert.ToString(objReader[Views.vw_DocumentFilePropertiesNames.Field10Name]) : null;

                configValues.CreatedBy = objReader[Views.vw_DocumentFilePropertiesNames.CreatedBy] != DBNull.Value ? Convert.ToInt64(objReader[Views.vw_DocumentFilePropertiesNames.CreatedBy]) : 0;
                configValues.CreatedByUserName = objReader[Views.vw_DocumentFilePropertiesNames.CreatedByUserName] != DBNull.Value ? Convert.ToString(objReader[Views.vw_DocumentFilePropertiesNames.CreatedByUserName]) : null;
                configValues.CreatedByUserFullName = objReader[Views.vw_DocumentFilePropertiesNames.CreatedByUserFullName] != DBNull.Value ? Convert.ToString(objReader[Views.vw_DocumentFilePropertiesNames.CreatedByUserFullName]) : null;
                configValues.CreatedOn = objReader[Views.vw_DocumentFilePropertiesNames.CreatedOn] != DBNull.Value ? Convert.ToDateTime(objReader[Views.vw_DocumentFilePropertiesNames.CreatedOn]) : DateTime.Now;


                configValues.ModifiedBy = objReader[Views.vw_DocumentFilePropertiesNames.ModifiedBy] != DBNull.Value ? Convert.ToInt64(objReader[Views.vw_DocumentFilePropertiesNames.ModifiedBy]) : 0;
                configValues.ModifiedByUserName = objReader[Views.vw_DocumentFilePropertiesNames.ModifiedByUserName] != DBNull.Value ? Convert.ToString(objReader[Views.vw_DocumentFilePropertiesNames.ModifiedByUserName]) : null;
                configValues.ModifiedByUserFullName = objReader[Views.vw_DocumentFilePropertiesNames.ModifiedByUserFullName] != DBNull.Value ? Convert.ToString(objReader[Views.vw_DocumentFilePropertiesNames.ModifiedByUserFullName]) : null;
                configValues.ModifiedOn = null;
                if (objReader[Views.vw_DocumentFilePropertiesNames.ModifiedOn] != DBNull.Value)
                {
                    configValues.ModifiedOn = Convert.ToDateTime(objReader[Views.vw_DocumentFilePropertiesNames.ModifiedOn]);
                }
                lstConfigurations.Add(configValues);
            }

            if (isnull) { return null; }
            else { return lstConfigurations; }
        }

    }
}
