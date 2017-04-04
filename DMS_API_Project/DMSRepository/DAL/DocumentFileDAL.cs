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
    public abstract class DocumentFileDAL
    {
        protected string ConnectionStringName = String.Empty;

        public DocumentFileDAL(string connectionStringName)
        {
            this.ConnectionStringName = connectionStringName;
        }

        public abstract FunctionReturnStatus CreateUpdateFileWithAttribute(DocumentFile file, DocumentProperties properties);

        public abstract DocumentFileSearchData GetFiles(DocumentFileSearchParameter searchParameters, PagingDetails pageDetail);

        public abstract DocumentProperties GetFilesProperties(long documentId);

        public abstract List<DocumentFileHistory> GetFileHistory(long documentId);


        protected string GetDocumentFolderParameterString(DocumentFileSearchParameter searchParameters)
        {
            List<string> lstConditions = new List<string>();

            if (searchParameters != null)
            {
                if (!string.IsNullOrEmpty(searchParameters.FolderName))
                {
                    lstConditions.Add(Views.vw_DocumentFiles.FolderName + "='" + SQLSafety.GetSQLSafeString(searchParameters.FolderName) + "'");
                }
                if (searchParameters.FolderId > 0)
                {
                    lstConditions.Add(Views.vw_DocumentFiles.FolderId + "=" + searchParameters.FolderId);
                }
                if (searchParameters.FileId > 0)
                {
                    lstConditions.Add(Views.vw_DocumentFiles.FileId + "=" + searchParameters.FileId);
                }
                if (searchParameters.SystemId > 0)
                {
                    lstConditions.Add(Views.vw_DocumentFiles.SystemId + "=" + searchParameters.SystemId);
                }
                if (searchParameters.IsDeleted != null)
                {
                    lstConditions.Add(Views.vw_DocumentFiles.IsDeleted + "=" + searchParameters.IsDeleted.Value);
                }
                if (!string.IsNullOrEmpty(searchParameters.FileName))
                {
                    lstConditions.Add(Views.vw_DocumentFiles.FileName + "='" + SQLSafety.GetSQLSafeString(searchParameters.FileName) + "'");
                }
            }
            return string.Join(" AND ", lstConditions);
        }

        protected string GetDocumentFolderOrderBy(string OrderByString)
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
                    if (columnName == Views.vw_DocumentFiles.FolderId.ToUpper())
                    {
                        lstColumns.Add(Views.vw_DocumentFiles.FolderId + " " + orderBy);
                    }
                    else if (columnName == Views.vw_DocumentFiles.FolderName.ToUpper())
                    {
                        lstColumns.Add(Views.vw_DocumentFiles.FolderName + " " + orderBy);
                    }
                    else if (columnName == Views.vw_DocumentFiles.FileName.ToUpper())
                    {
                        lstColumns.Add(Views.vw_DocumentFiles.FileName + " " + orderBy);
                    }
                    else if (columnName == Views.vw_DocumentFiles.FileId.ToUpper())
                    {
                        lstColumns.Add(Views.vw_DocumentFiles.FileId + " " + orderBy);
                    }
                    else if (columnName == Views.vw_DocumentFiles.SystemId.ToUpper())
                    {
                        lstColumns.Add(Views.vw_DocumentFiles.SystemId + " " + orderBy);
                    }
                    else if (columnName == Views.vw_DocumentFiles.CreatedOn.ToUpper())
                    {
                        lstColumns.Add(Views.vw_DocumentFiles.CreatedOn + " " + orderBy);
                    }
                    else if (columnName == Views.vw_DocumentFiles.ModifiedOn.ToUpper())
                    {
                        lstColumns.Add(Views.vw_DocumentFiles.ModifiedOn + " " + orderBy);
                    }
                    else if (columnName == Views.vw_DocumentFiles.IsDeleted.ToUpper())
                    {
                        lstColumns.Add(Views.vw_DocumentFiles.IsDeleted + " " + orderBy);
                    }
                }
            }
            return string.Join(", ", lstColumns);
        }

        protected List<DocumentFile> CreateDocumentFolderObject(IDataReader objReader)
        {
            List<DocumentFile> lstFiles = new List<DocumentFile>();
            DocumentFile file;
            bool isnull = true;

            while (objReader.Read())
            {
                isnull = false;
                file = new DocumentFile();
                file.FileId = objReader[Views.vw_DocumentFiles.FileId] != DBNull.Value ? Convert.ToInt64(objReader[Views.vw_DocumentFiles.FileId]) : 0;
                file.FolderId = objReader[Views.vw_DocumentFiles.FolderId] != DBNull.Value ? Convert.ToInt64(objReader[Views.vw_DocumentFiles.FolderId]) : 0;
                file.SystemId = objReader[Views.vw_DocumentFiles.SystemId] != DBNull.Value ? Convert.ToInt64(objReader[Views.vw_DocumentFiles.SystemId]) : 0;
                file.FileName = objReader[Views.vw_DocumentFiles.FileName] != DBNull.Value ? Convert.ToString(objReader[Views.vw_DocumentFiles.FileName]) : null;
                file.FolderName = objReader[Views.vw_DocumentFiles.FolderName] != DBNull.Value ? Convert.ToString(objReader[Views.vw_DocumentFiles.FolderName]) : null;
                file.IsDeleted = objReader[Views.vw_DocumentFiles.IsDeleted] != DBNull.Value ? Convert.ToBoolean(objReader[Views.vw_DocumentFiles.IsDeleted]) : false;

                file.CreatedByUserName = objReader[Views.vw_DocumentFiles.CreatedByUserName] != DBNull.Value ? Convert.ToString(objReader[Views.vw_DocumentFiles.ModifiedByUserName]) : null;
                file.CreatedByUserFullName = objReader[Views.vw_DocumentFiles.CreatedByUserFullName] != DBNull.Value ? Convert.ToString(objReader[Views.vw_DocumentFiles.ModifiedByUserFullName]) : null;
                file.CreatedBy = objReader[Views.vw_DocumentFiles.CreatedBy] != DBNull.Value ? Convert.ToInt64(objReader[Views.vw_DocumentFiles.CreatedBy]) : 0;
                file.CreatedOn = objReader[Views.vw_DocumentFiles.CreatedOn] != DBNull.Value ? Convert.ToDateTime(objReader[Views.vw_DocumentFiles.CreatedOn]) : DateTime.Now;

                file.ModifiedByUserName = objReader[Views.vw_DocumentFiles.ModifiedByUserName] != DBNull.Value ? Convert.ToString(objReader[Views.vw_DocumentFiles.ModifiedByUserName]) : null;
                file.ModifiedByUserFullName = objReader[Views.vw_DocumentFiles.ModifiedByUserFullName] != DBNull.Value ? Convert.ToString(objReader[Views.vw_DocumentFiles.ModifiedByUserFullName]) : null;
                file.ModifiedBy = objReader[Views.vw_DocumentFiles.ModifiedBy] != DBNull.Value ? Convert.ToInt64(objReader[Views.vw_DocumentFiles.ModifiedBy]) : 0;
                file.ModifiedOn = null;
                if (objReader[Views.vw_DocumentFiles.ModifiedOn] != DBNull.Value)
                {
                    file.ModifiedOn = Convert.ToDateTime(objReader[Views.vw_DocumentFiles.ModifiedOn]);
                }
                lstFiles.Add(file);
            }

            if (isnull) { return null; }
            else { return lstFiles; }
        }

    }
}
