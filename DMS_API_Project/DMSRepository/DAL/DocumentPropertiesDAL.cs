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
    public abstract class DocumentPropertiesDAL
    {
        protected string ConnectionStringName = String.Empty;

        public DocumentPropertiesDAL(string connectionStringName)
        {
            this.ConnectionStringName = connectionStringName;
        }

        public abstract FunctionReturnStatus UpdateDocumentProperties(DocumentProperties paramValue);

        public abstract List<DocumentProperties> GetDocumentProperties(DocumentPropertiesSearchParameter searchParameters);

        protected List<DocumentProperties> CreateDocumentPropertiesObject(IDataReader objReader)
        {
            List<DocumentProperties> lstProperties = new List<DocumentProperties>();
            DocumentProperties properties;
            bool isnull = true;

            while (objReader.Read())
            {
                isnull = false;
                properties = new DocumentProperties();
                properties.Propertynames = new DocumentPropertiesNames();

                properties.DocumentFileId = objReader[Views.vw_DocumentFileProperties.DocumentFileId] != DBNull.Value ? Convert.ToInt64(objReader[Views.vw_DocumentFileProperties.DocumentFileId]) : 0;
                properties.Field1Value = objReader[Views.vw_DocumentFileProperties.Field1Value] != DBNull.Value ? Convert.ToString(objReader[Views.vw_DocumentFileProperties.Field1Value]) : null;
                properties.Field2Value = objReader[Views.vw_DocumentFileProperties.Field2Value] != DBNull.Value ? Convert.ToString(objReader[Views.vw_DocumentFileProperties.Field2Value]) : null;
                properties.Field3Value = objReader[Views.vw_DocumentFileProperties.Field3Value] != DBNull.Value ? Convert.ToString(objReader[Views.vw_DocumentFileProperties.Field3Value]) : null;
                properties.Field4Value = objReader[Views.vw_DocumentFileProperties.Field4Value] != DBNull.Value ? Convert.ToString(objReader[Views.vw_DocumentFileProperties.Field4Value]) : null;
                properties.Field5Value = objReader[Views.vw_DocumentFileProperties.Field5Value] != DBNull.Value ? Convert.ToString(objReader[Views.vw_DocumentFileProperties.Field5Value]) : null;
                properties.Field6Value = objReader[Views.vw_DocumentFileProperties.Field6Value] != DBNull.Value ? Convert.ToString(objReader[Views.vw_DocumentFileProperties.Field6Value]) : null;
                properties.Field7Value = objReader[Views.vw_DocumentFileProperties.Field7Value] != DBNull.Value ? Convert.ToString(objReader[Views.vw_DocumentFileProperties.Field7Value]) : null;
                properties.Field8Value = objReader[Views.vw_DocumentFileProperties.Field8Value] != DBNull.Value ? Convert.ToString(objReader[Views.vw_DocumentFileProperties.Field8Value]) : null;
                properties.Field9Value = objReader[Views.vw_DocumentFileProperties.Field9Value] != DBNull.Value ? Convert.ToString(objReader[Views.vw_DocumentFileProperties.Field9Value]) : null;
                properties.Field10Value = objReader[Views.vw_DocumentFileProperties.Field10Value] != DBNull.Value ? Convert.ToString(objReader[Views.vw_DocumentFileProperties.Field10Value]) : null;
                properties.FileName = objReader[Views.vw_DocumentFileProperties.FileName] != DBNull.Value ? Convert.ToString(objReader[Views.vw_DocumentFileProperties.FileName]) : null;
                properties.FolderName = objReader[Views.vw_DocumentFileProperties.FolderName] != DBNull.Value ? Convert.ToString(objReader[Views.vw_DocumentFileProperties.FolderName]) : null;
                properties.PropertyValueCreatedBy = objReader[Views.vw_DocumentFileProperties.PropertyNameCreatedBy] != DBNull.Value ? Convert.ToInt64(objReader[Views.vw_DocumentFileProperties.PropertyNameCreatedBy]) : 0;
                properties.PropertyValueCreatedOn = objReader[Views.vw_DocumentFileProperties.PropertyValueCreatedOn] != DBNull.Value ? Convert.ToDateTime(objReader[Views.vw_DocumentFileProperties.PropertyValueCreatedOn]) : DateTime.Now;
                properties.PropertyValueCreatedByUserName = objReader[Views.vw_DocumentFileProperties.PropertyValueCreatedByUserName] != DBNull.Value ? Convert.ToString(objReader[Views.vw_DocumentFileProperties.PropertyValueCreatedByUserName]) : null;
                properties.PropertyValueCreatedByUserFullName = objReader[Views.vw_DocumentFileProperties.PropertyValueCreatedByUserFullName] != DBNull.Value ? Convert.ToString(objReader[Views.vw_DocumentFileProperties.PropertyValueCreatedByUserFullName]) : null;
                properties.PropertyValueModifiedBy = objReader[Views.vw_DocumentFileProperties.PropertyValueModifiedBy] != DBNull.Value ? Convert.ToInt64(objReader[Views.vw_DocumentFileProperties.PropertyValueModifiedBy]) : 0;
                properties.PropertyValueModifiedOn = null;
                if (objReader[Views.vw_DocumentFileProperties.PropertyValueModifiedOn] != DBNull.Value)
                {
                    properties.PropertyValueModifiedOn = Convert.ToDateTime(objReader[Views.vw_DocumentFileProperties.PropertyValueModifiedOn]);
                }
                properties.PropertyValueModifiedByUserName = objReader[Views.vw_DocumentFileProperties.PropertyValueModifiedByUserName] != DBNull.Value ? Convert.ToString(objReader[Views.vw_DocumentFileProperties.PropertyValueModifiedByUserName]) : null;
                properties.PropertyValueModifiedByUserFullName = objReader[Views.vw_DocumentFileProperties.PropertyValueModifiedByUserFullName] != DBNull.Value ? Convert.ToString(objReader[Views.vw_DocumentFileProperties.PropertyValueModifiedByUserFullName]) : null;



                properties.Propertynames.SystemId = objReader[Views.vw_DocumentFilePropertiesNames.SystemId] != DBNull.Value ? Convert.ToInt64(objReader[Views.vw_DocumentFilePropertiesNames.SystemId]) : 0;
                properties.Propertynames.SystemName = objReader[Views.vw_DocumentFilePropertiesNames.SystemName] != DBNull.Value ? Convert.ToString(objReader[Views.vw_DocumentFilePropertiesNames.SystemName]) : null;

                properties.Propertynames.Field1Name = objReader[Views.vw_DocumentFilePropertiesNames.Field1Name] != DBNull.Value ? Convert.ToString(objReader[Views.vw_DocumentFilePropertiesNames.Field1Name]) : null;
                properties.Propertynames.Field2Name = objReader[Views.vw_DocumentFilePropertiesNames.Field2Name] != DBNull.Value ? Convert.ToString(objReader[Views.vw_DocumentFilePropertiesNames.Field2Name]) : null;
                properties.Propertynames.Field3Name = objReader[Views.vw_DocumentFilePropertiesNames.Field3Name] != DBNull.Value ? Convert.ToString(objReader[Views.vw_DocumentFilePropertiesNames.Field3Name]) : null;
                properties.Propertynames.Field4Name = objReader[Views.vw_DocumentFilePropertiesNames.Field4Name] != DBNull.Value ? Convert.ToString(objReader[Views.vw_DocumentFilePropertiesNames.Field4Name]) : null;
                properties.Propertynames.Field5Name = objReader[Views.vw_DocumentFilePropertiesNames.Field5Name] != DBNull.Value ? Convert.ToString(objReader[Views.vw_DocumentFilePropertiesNames.Field5Name]) : null;
                properties.Propertynames.Field6Name = objReader[Views.vw_DocumentFilePropertiesNames.Field6Name] != DBNull.Value ? Convert.ToString(objReader[Views.vw_DocumentFilePropertiesNames.Field6Name]) : null;
                properties.Propertynames.Field7Name = objReader[Views.vw_DocumentFilePropertiesNames.Field7Name] != DBNull.Value ? Convert.ToString(objReader[Views.vw_DocumentFilePropertiesNames.Field7Name]) : null;
                properties.Propertynames.Field8Name = objReader[Views.vw_DocumentFilePropertiesNames.Field8Name] != DBNull.Value ? Convert.ToString(objReader[Views.vw_DocumentFilePropertiesNames.Field8Name]) : null;
                properties.Propertynames.Field9Name = objReader[Views.vw_DocumentFilePropertiesNames.Field9Name] != DBNull.Value ? Convert.ToString(objReader[Views.vw_DocumentFilePropertiesNames.Field9Name]) : null;
                properties.Propertynames.Field10Name = objReader[Views.vw_DocumentFilePropertiesNames.Field10Name] != DBNull.Value ? Convert.ToString(objReader[Views.vw_DocumentFilePropertiesNames.Field10Name]) : null;

                properties.Propertynames.CreatedBy = objReader[Views.vw_DocumentFilePropertiesNames.CreatedBy] != DBNull.Value ? Convert.ToInt64(objReader[Views.vw_DocumentFilePropertiesNames.CreatedBy]) : 0;
                properties.Propertynames.CreatedByUserName = objReader[Views.vw_DocumentFilePropertiesNames.CreatedByUserName] != DBNull.Value ? Convert.ToString(objReader[Views.vw_DocumentFilePropertiesNames.CreatedByUserName]) : null;
                properties.Propertynames.CreatedByUserFullName = objReader[Views.vw_DocumentFilePropertiesNames.CreatedByUserFullName] != DBNull.Value ? Convert.ToString(objReader[Views.vw_DocumentFilePropertiesNames.CreatedByUserFullName]) : null;
                properties.Propertynames.CreatedOn = objReader[Views.vw_DocumentFilePropertiesNames.CreatedOn] != DBNull.Value ? Convert.ToDateTime(objReader[Views.vw_DocumentFilePropertiesNames.CreatedOn]) : DateTime.Now;


                properties.Propertynames.ModifiedBy = objReader[Views.vw_DocumentFilePropertiesNames.ModifiedBy] != DBNull.Value ? Convert.ToInt64(objReader[Views.vw_DocumentFilePropertiesNames.ModifiedBy]) : 0;
                properties.Propertynames.ModifiedByUserName = objReader[Views.vw_DocumentFilePropertiesNames.ModifiedByUserName] != DBNull.Value ? Convert.ToString(objReader[Views.vw_DocumentFilePropertiesNames.ModifiedByUserName]) : null;
                properties.Propertynames.ModifiedByUserFullName = objReader[Views.vw_DocumentFilePropertiesNames.ModifiedByUserFullName] != DBNull.Value ? Convert.ToString(objReader[Views.vw_DocumentFilePropertiesNames.ModifiedByUserFullName]) : null;
                properties.Propertynames.ModifiedOn = null;
                if (objReader[Views.vw_DocumentFilePropertiesNames.ModifiedOn] != DBNull.Value)
                {
                    properties.Propertynames.ModifiedOn = Convert.ToDateTime(objReader[Views.vw_DocumentFilePropertiesNames.ModifiedOn]);
                }

                lstProperties.Add(properties);
            }

            if (isnull) { return null; }
            else { return lstProperties; }
        }

    }
}
