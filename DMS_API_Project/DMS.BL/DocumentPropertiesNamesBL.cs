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
    public class DocumentPropertiesNamesBL : IDisposable
    {
        Logger logger = null;

        DocumentPropertiesNamesDAL sysVal = null;

        string ConnectionStringName = String.Empty;

        DocumentPropertiesNamesDAL DocumentPropertiesNamesRepository
        {
            get
            {
                if (sysVal == null)
                {
                    sysVal = new DocumentPropertiesNamesSQL(ConnectionStringName);
                }
                return sysVal;
            }
        }

        public DocumentPropertiesNamesBL(string connectionStringName)
        {
            this.ConnectionStringName = connectionStringName;
            logger = new Logger("DMS.BL.ConfigurationBL");
        }


        public FunctionReturnStatus CreateUpdatePropertiesNames(DocumentPropertiesNames docPropertiesNames)
        {
            if (docPropertiesNames == null)
            {
                return new FunctionReturnStatus(StatusType.Error, "Invalid data");
            }
            if (docPropertiesNames.SystemId <= 0)
            {
                return new FunctionReturnStatus(StatusType.Error, "SystemId invalid");
            }
            if (docPropertiesNames.CreatedBy <= 0 && docPropertiesNames.ModifiedBy <= 0)
            {
                return new FunctionReturnStatus(StatusType.Error, "Invalid CreatedBy/ModifiedBy");
            }

            return DocumentPropertiesNamesRepository.CreateUpdateDocumentPropertiesNames(docPropertiesNames);
        }


        public DocumentPropertiesNames GetConfiguration(long systemId)
        {
            try
            {
                DocumentPropertiesNamesSearchParamater searchParameters = new DocumentPropertiesNamesSearchParamater();
                searchParameters.SystemId = systemId;
                return GetDocumentPropertiesNames(searchParameters, null);
            }
            catch (Exception ex)
            {
                logger.LogEvent(ex.ToString(), LogLevel.Error);
                throw;
            }
        }

        public DocumentPropertiesNames GetDocumentPropertiesNames(DocumentPropertiesNamesSearchParamater searchParameters, PagingDetails pageDetail)
        {
            try
            {
                return DocumentPropertiesNamesRepository.GetDocumentPropertiesNames(searchParameters, pageDetail);
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
