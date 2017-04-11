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
    public class DocumentFolderBL : IDisposable
    {
        Logger logger = null;

        DocumentFolderDAL sysVal = null;

        string ConnectionStringName = String.Empty;

        DocumentFolderDAL DocumentFolderRepository
        {
            get
            {
                if (sysVal == null)
                {
                    sysVal = new DocumentFolderSQL(ConnectionStringName);
                }
                return sysVal;
            }
        }

        public DocumentFolderBL(string connectionStringName)
        {
            this.ConnectionStringName = connectionStringName;
            logger = new Logger("DMS.BL.DocumentFolderBL");
        }


        public FunctionReturnStatus UpdateFolder(DocumentFolder folder)
        {
            if (folder == null)
            {
                return new FunctionReturnStatus(StatusType.Error, "Invalid data");
            }
            if (folder.FolderId <= 0)
            {
                return new FunctionReturnStatus(StatusType.Error, "Invalid FolderId");
            }
            if (folder.SystemId <= 0)
            {
                return new FunctionReturnStatus(StatusType.Error, "Invalid SystemId");
            }
            if (folder.ModifiedBy <= 0)
            {
                return new FunctionReturnStatus(StatusType.Error, "Invalid ModifiedBy");
            }
            if (string.IsNullOrEmpty(folder.FolderName))
            {
                return new FunctionReturnStatus(StatusType.Error, "FolderName invalid");
            }
            return DocumentFolderRepository.CreateUpdateFolder(folder);
        }

        public FunctionReturnStatus CreateFolder(DocumentFolder folder)
        {
            if (folder == null)
            {
                return new FunctionReturnStatus(StatusType.Error, "Invalid data");
            }
            if (folder.SystemId <= 0)
            {
                return new FunctionReturnStatus(StatusType.Error, "Invalid SystemId");
            }
            if (folder.CreatedBy <= 0)
            {
                return new FunctionReturnStatus(StatusType.Error, "Invalid CreatedBy");
            }
            if (string.IsNullOrEmpty(folder.FolderName))
            {
                return new FunctionReturnStatus(StatusType.Error, "invalid FolderName");
            }
            return DocumentFolderRepository.CreateUpdateFolder(folder);
        }


        public DocumentFolderSearchData GetFolders(long systemId)
        {
            try
            {
                DocumentFolderSearchParameter searchParameters = new DocumentFolderSearchParameter();
                searchParameters.SystemId = systemId;
                return GetFolders(searchParameters, null);
            }
            catch (Exception ex)
            {
                logger.LogEvent(ex.ToString(), LogLevel.Error);
                throw;
            }
        }

        public DocumentFolderSearchData GetFolder(long parentFolderId)
        {
            try
            {
                DocumentFolderSearchParameter searchParameters = new DocumentFolderSearchParameter();
                searchParameters.FolderId = parentFolderId;
                return GetFolders(searchParameters, null);
            }
            catch (Exception ex)
            {
                logger.LogEvent(ex.ToString(), LogLevel.Error);
                throw;
            }
        }

        public DocumentFolderSearchData GetFolders(DocumentFolderSearchParameter searchParameters, PagingDetails pageDetail)
        {
            try
            {
                return DocumentFolderRepository.GetFolders(searchParameters, pageDetail);
            }
            catch (Exception ex)
            {
                logger.LogEvent(ex.ToString(), LogLevel.Error);
                throw;
            }
        }

        public List<DocumentFolderTree> GetDocumentFolderTree(long systemId)
        {
            try
            {
                return DocumentFolderRepository.GetDocumentFolderTree(systemId);
            }
            catch (Exception ex)
            {
                logger.LogEvent(ex.ToString(), LogLevel.Error);
                throw;
            }
        }

        public DocumentSearchData GetDocumentObjectList(DocumentSearchParameter searchParameters, PagingDetails pageDetail)
        {
            try
            {
                //if (searchParameters == null)
                //{
                //    return null;
                //}
                //if (searchParameters.SystemId <= 0)
                //{
                //    return null;
                //}
                return DocumentFolderRepository.GetDocumentObjectList(searchParameters, pageDetail);
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
