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
    public class DocumentFileBL : IDisposable
    {
        Logger logger = null;

        DocumentFileDAL sysVal = null;

        string ConnectionStringName = String.Empty;

        DocumentFileDAL DocumentFileRepository
        {
            get
            {
                if (sysVal == null)
                {
                    sysVal = new DocumentFileSQL(ConnectionStringName);
                }
                return sysVal;
            }
        }

        public DocumentFileBL(string connectionStringName)
        {
            this.ConnectionStringName = connectionStringName;
            logger = new Logger("DMS.BL.DocumentFileBL");
        }

        public FunctionReturnStatus UploadFile(DocumentFile file, DocumentProperties properties)
        {
            if (file == null)
            {
                return new FunctionReturnStatus(StatusType.Error, "Invalid data");
            }
            if (file.SystemId <= 0)
            {
                return new FunctionReturnStatus(StatusType.Error, "Invalid SystemId");
            }
            if (file.FolderId <= 0)
            {
                return new FunctionReturnStatus(StatusType.Error, "Invalid SystemId");
            }
            if (file.FileData.Length <= 0)
            {
                return new FunctionReturnStatus(StatusType.Error, "Invalid FileData");
            }
            if (string.IsNullOrEmpty(file.FileName))
            {
                return new FunctionReturnStatus(StatusType.Error, "Invalid FileName");
            }
            if (file.ModifiedBy <= 0)
            {
                return new FunctionReturnStatus(StatusType.Error, "Invalid ModifiedBy");
            }

            return DocumentFileRepository.CreateUpdateFileWithAttribute(file, properties);
        }

        public FunctionReturnStatus UploadFile(DocumentFile file)
        {
            return DocumentFileRepository.CreateUpdateFileWithAttribute(file, null);
        }


        public DocumentFile GetFiles(long documentId)
        {
            try
            {
                DocumentFileSearchParameter searchParameters = new DocumentFileSearchParameter();
                searchParameters.FileId = documentId;
                var lstConfiguration = GetFiles(searchParameters);
                if (lstConfiguration != null && lstConfiguration.LstData != null)
                {
                    if (lstConfiguration.LstData.Count == 1)
                    {
                        return lstConfiguration.LstData[0];
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                logger.LogEvent(ex.ToString(), LogLevel.Error);
                throw;
            }
        }

        public DocumentFileSearchData GetFiles(DocumentFileSearchParameter searchParameters)
        {
            try
            {
                return DocumentFileRepository.GetFiles(searchParameters);
            }
            catch (Exception ex)
            {
                logger.LogEvent(ex.ToString(), LogLevel.Error);
                throw;
            }
        }


        public IList<DocumentFileHistory> GetFileHistory(long documentId)
        {
            try
            {
                if (documentId <= 0)
                {
                    return null;
                }
                return DocumentFileRepository.GetFileHistory(documentId);
            }
            catch (Exception ex)
            {
                logger.LogEvent(ex.ToString(), LogLevel.Error);
                throw;
            }
        }

        public DocumentProperties GetDocumentFileProperties(long documentId)
        {
            try
            {
                if (documentId <= 0)
                {
                    return null;
                }
                return DocumentFileRepository.GetFilesProperties(documentId);
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
