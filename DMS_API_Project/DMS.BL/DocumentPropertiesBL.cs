using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DMS.Model;
using Chavan.Logger;
using DMS.Repository.SQL;
using DMS.Repository.DAL;

namespace DMS.BL
{
    public class DocumentPropertiesBL : IDisposable
    {
        Logger logger = null;

        DocumentPropertiesDAL sysVal = null;

        string ConnectionStringName = String.Empty;

        DocumentPropertiesDAL DocumentPropertiesRepository
        {
            get
            {
                if (sysVal == null)
                {
                    sysVal = new DocumentPropertiesSQL(ConnectionStringName);
                }
                return sysVal;
            }
        }

        public DocumentPropertiesBL(string connectionStringName)
        {
            this.ConnectionStringName = connectionStringName;
            logger = new Logger("DMS.BL.ConfigurationBL");
        }

        public FunctionReturnStatus UpdateDocumentProperties(DocumentProperties paramValue)
        {
            if (paramValue == null)
            {
                return new FunctionReturnStatus(StatusType.Error, "Invalid data");
            }
            if (paramValue.DocumentFileId <= 0)
            {
                return new FunctionReturnStatus(StatusType.Error, "SystemId DocumentFileId");
            }
            if (paramValue.PropertyValueCreatedBy <= 0 && paramValue.PropertyValueModifiedBy <= 0)
            {
                return new FunctionReturnStatus(StatusType.Error, "Invalid CreatedBy/ModifiedBy");
            }
            return DocumentPropertiesRepository.UpdateDocumentProperties(paramValue);
        }

        public DocumentProperties GetDocumentProperties(long documentId) {
            try
            {
                DocumentPropertiesSearchParameter searchParameters = new DocumentPropertiesSearchParameter();
                searchParameters.DocumentFileId = documentId;
                IList<DocumentProperties> lstDocProp =  GetDocumentProperties(searchParameters);
                if (lstDocProp.Count == 1)
                {
                    return lstDocProp[0];
                }
                return null;
            }
            catch (Exception ex)
            {
                logger.LogEvent(ex.ToString(), LogLevel.Error);
                throw;
            }
        }
        
        public IList<DocumentProperties> GetDocumentProperties(DocumentPropertiesSearchParameter searchParameters)
        {
            try
            {
                return DocumentPropertiesRepository.GetDocumentProperties(searchParameters);
            }
            catch (Exception ex)
            {
                logger.LogEvent(ex.ToString(), LogLevel.Error);
                throw;
            }
        }

        public void Dispose()
        {
            logger = null;
            sysVal = null;
        }
    }
}
