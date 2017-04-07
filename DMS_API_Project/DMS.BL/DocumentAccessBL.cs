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
    public class DocumentAccessBL : IDisposable
    {
        Logger logger = null;

        DocumentAccessDAL sysVal = null;

        string ConnectionStringName = String.Empty;

        DocumentAccessDAL DocumentAccessRepository
        {
            get
            {
                if (sysVal == null)
                {
                    sysVal = new DocumentAccessSQL(ConnectionStringName);
                }
                return sysVal;
            }
        }

        public DocumentAccessBL(string connectionStringName)
        {
            this.ConnectionStringName = connectionStringName;
            logger = new Logger("DMS.BL.DocumentAccessBL");
        }

        public FunctionReturnStatus UpdateConfigurationValue(DocumentAccess documentAccess)
        {
            if (documentAccess == null)
            {
                return new FunctionReturnStatus(StatusType.Error, "Invalid data");
            }
            if (documentAccess.SystemId <= 0)
            {
                return new FunctionReturnStatus(StatusType.Error, "Invalid SystemId");
            }
            if (documentAccess.ObjectId <= 0)
            {
                return new FunctionReturnStatus(StatusType.Error, "Invalid ObjectId");
            }
            if (documentAccess.UserRoleId <= 0)
            {
                return new FunctionReturnStatus(StatusType.Error, "Invalid UserRoleId");
            }
            if (documentAccess.ModifiedBy <= 0)
            {
                return new FunctionReturnStatus(StatusType.Error, "Invalid ModifiedBy");
            }
            return DocumentAccessRepository.CreateUpdateDocumentAccess(documentAccess);
        }

        public FunctionReturnStatus GetDocumentAccess(DocumentAccessSearchParameter searchParameter)
        {
            try
            {
                if (searchParameter == null)
                {
                    return new FunctionReturnStatus(StatusType.Error, "Invalid data");
                }
                if (searchParameter.SystemId <= 0)
                {
                    return new FunctionReturnStatus(StatusType.Error, "Invalid SystemId");
                }
                if (searchParameter.ObjectId <= 0)
                {
                    return new FunctionReturnStatus(StatusType.Error, "Invalid ObjectId");
                }
                if (searchParameter.UserId <= 0)
                {
                    return new FunctionReturnStatus(StatusType.Error, "Invalid UserId");
                }
                return DocumentAccessRepository.GetDocumentAccess(searchParameter);
            }
            catch (Exception ex)
            {
                logger.LogEvent(ex.ToString(), LogLevel.Error);
                throw;
            }
        }

        public FunctionReturnStatus RemoveDocumentAccess(DocumentAccess documentAccess)
        {
            try
            {
                if (documentAccess == null)
                {
                    return new FunctionReturnStatus(StatusType.Error, "Invalid data");
                }
                if (documentAccess.SystemId <= 0)
                {
                    return new FunctionReturnStatus(StatusType.Error, "Invalid SystemId");
                }
                if (documentAccess.ObjectId <= 0)
                {
                    return new FunctionReturnStatus(StatusType.Error, "Invalid ObjectId");
                }
                if (documentAccess.UserRoleId <= 0)
                {
                    return new FunctionReturnStatus(StatusType.Error, "Invalid UserRoleId");
                }
                return DocumentAccessRepository.RemoveDocumentAccess(documentAccess);
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
