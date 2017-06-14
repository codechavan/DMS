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
    public abstract class DocumentFolderDAL
    {
        protected string ConnectionStringName = String.Empty;

        public DocumentFolderDAL(string connectionStringName)
        {
            this.ConnectionStringName = connectionStringName;
        }


        public abstract FunctionReturnStatus CreateUpdateFolder(DocumentFolder folder);

        public abstract DocumentFolderSearchData GetFolders(DocumentFolderSearchParameter searchParameters);

        public abstract DocumentSearchData GetDocumentObjectList(DocumentSearchParameter searchParameters);

        public abstract List<DocumentFolderTree> GetDocumentFolderTree(DocumentFolderTreeSearchParameters searchParameters);

        protected string GetDocumentObjectsParameterString(DocumentSearchParameter searchParameters)
        {
            List<string> lstConditions = new List<string>();

            if (searchParameters != null)
            {
                if (!string.IsNullOrEmpty(searchParameters.ObjectName))
                {
                    lstConditions.Add(Views.usp_Get_Documents.Name + " LIKE '%" + SQLSafety.GetSQLSafeString(searchParameters.ObjectName) + "%'");
                }
            }
            return string.Join(" AND ", lstConditions);
        }

        protected string GetDocumentObjectOrderBy(string OrderByString)
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
                    if (columnName == Views.usp_Get_Documents.ObjectType.ToUpper())
                    {
                        lstColumns.Add(Views.usp_Get_Documents.ObjectType + " " + orderBy);
                    }
                    if (columnName == Views.usp_Get_Documents.Name.ToUpper())
                    {
                        lstColumns.Add(Views.usp_Get_Documents.Name + " " + orderBy);
                    }
                    if (columnName == Views.usp_Get_Documents.CreatedOn.ToUpper())
                    {
                        lstColumns.Add(Views.usp_Get_Documents.CreatedOn + " " + orderBy);
                    }
                    if (columnName == Views.usp_Get_Documents.ModifiedOn.ToUpper())
                    {
                        lstColumns.Add(Views.usp_Get_Documents.ModifiedOn + " " + orderBy);
                    }
                }
            }
            return string.Join(", ", lstColumns);
        }

        protected List<Document> CreateDocumentObject(IDataReader objReader)
        {
            List<Document> lstFolders = new List<Document>();
            Document folder;
            bool isnull = true;

            while (objReader.Read())
            {
                isnull = false;
                folder = new Document();
                folder.ObjectId = objReader[Views.usp_Get_Documents.ObjectId] != DBNull.Value ? Convert.ToInt64(objReader[Views.usp_Get_Documents.ObjectId]) : 0;
                folder.ObjectType = (DocumentObjectType)(objReader[Views.usp_Get_Documents.ObjectType] != DBNull.Value ? Convert.ToInt64(objReader[Views.usp_Get_Documents.ObjectType]) : 1);
                folder.Name = objReader[Views.usp_Get_Documents.Name] != DBNull.Value ? Convert.ToString(objReader[Views.usp_Get_Documents.Name]) : null;
                folder.IsDeleted = objReader[Views.usp_Get_Documents.IsDeleted] != DBNull.Value ? Convert.ToBoolean(objReader[Views.usp_Get_Documents.IsDeleted]) : false;

                folder.CreatedBy = objReader[Views.usp_Get_Documents.CreatedBy] != DBNull.Value ? Convert.ToInt64(objReader[Views.usp_Get_Documents.CreatedBy]) : 0;
                folder.CreatedOn = objReader[Views.usp_Get_Documents.CreatedOn] != DBNull.Value ? Convert.ToDateTime(objReader[Views.usp_Get_Documents.CreatedOn]) : DateTime.Now;
                folder.ModifiedBy = objReader[Views.usp_Get_Documents.ModifiedBy] != DBNull.Value ? Convert.ToInt64(objReader[Views.usp_Get_Documents.ModifiedBy]) : 0;
                folder.ModifiedOn = null;
                if (objReader[Views.usp_Get_Documents.ModifiedOn] != DBNull.Value)
                {
                    folder.ModifiedOn = Convert.ToDateTime(objReader[Views.usp_Get_Documents.ModifiedOn]);
                }
                lstFolders.Add(folder);
            }

            if (isnull) { return null; }
            else { return lstFolders; }
        }



        protected string GetDocumentFolderParameterString(DocumentFolderSearchParameter searchParameters)
        {
            List<string> lstConditions = new List<string>();

            if (searchParameters != null)
            {
                if (!string.IsNullOrEmpty(searchParameters.FolderName))
                {
                    lstConditions.Add(Views.vw_DocumentFolders.FolderName + "='" + SQLSafety.GetSQLSafeString(searchParameters.FolderName) + "'");
                }
                else if (searchParameters.FolderId > 0)
                {
                    lstConditions.Add(Views.vw_DocumentFolders.FolderId + "=" + searchParameters.FolderId);
                }
                else if (searchParameters.ParentFolderId > 0)
                {
                    lstConditions.Add(Views.vw_DocumentFolders.ParentFolderId + "=" + searchParameters.ParentFolderId);
                }
                else if (searchParameters.SystemId > 0)
                {
                    lstConditions.Add(Views.vw_DocumentFolders.SystemId + "=" + searchParameters.SystemId);
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
                    if (columnName == Views.vw_DocumentFolders.FolderId.ToUpper())
                    {
                        lstColumns.Add(Views.vw_DocumentFolders.FolderId + " " + orderBy);
                    }
                    else if (columnName == Views.vw_DocumentFolders.FolderName.ToUpper())
                    {
                        lstColumns.Add(Views.vw_DocumentFolders.FolderName + " " + orderBy);
                    }
                    else if (columnName == Views.vw_DocumentFolders.CreatedOn.ToUpper())
                    {
                        lstColumns.Add(Views.vw_DocumentFolders.CreatedOn + " " + orderBy);
                    }
                    else if (columnName == Views.vw_DocumentFolders.CreatedByUserName.ToUpper())
                    {
                        lstColumns.Add(Views.vw_DocumentFolders.CreatedByUserName + " " + orderBy);
                    }
                    else if (columnName == Views.vw_DocumentFolders.IsDeleted.ToUpper())
                    {
                        lstColumns.Add(Views.vw_DocumentFolders.IsDeleted + " " + orderBy);
                    }
                    else if (columnName == Views.vw_DocumentFolders.ParentFolderId.ToUpper())
                    {
                        lstColumns.Add(Views.vw_DocumentFolders.ParentFolderId + " " + orderBy);
                    }
                    else if (columnName == Views.vw_DocumentFolders.SystemId.ToUpper())
                    {
                        lstColumns.Add(Views.vw_DocumentFolders.SystemId + " " + orderBy);
                    }
                    else if (columnName == Views.vw_DocumentFolders.ModifiedByUserName.ToUpper())
                    {
                        lstColumns.Add(Views.vw_DocumentFolders.ModifiedByUserName + " " + orderBy);
                    }
                }
            }
            return string.Join(", ", lstColumns);
        }

        protected List<DocumentFolder> CreateDocumentFolderObject(IDataReader objReader)
        {
            List<DocumentFolder> lstFolders = new List<DocumentFolder>();
            DocumentFolder folder;
            bool isnull = true;

            while (objReader.Read())
            {
                isnull = false;
                folder = new DocumentFolder();
                folder.FolderId = objReader[Views.vw_DocumentFolders.FolderId] != DBNull.Value ? Convert.ToInt64(objReader[Views.vw_DocumentFolders.FolderId]) : 0;
                folder.SystemId = objReader[Views.vw_DocumentFolders.SystemId] != DBNull.Value ? Convert.ToInt64(objReader[Views.vw_DocumentFolders.SystemId]) : 0;
                folder.FolderName = objReader[Views.vw_DocumentFolders.FolderName] != DBNull.Value ? Convert.ToString(objReader[Views.vw_DocumentFolders.FolderName]) : null;
                folder.IsDelete = objReader[Views.vw_DocumentFolders.IsDeleted] != DBNull.Value ? Convert.ToBoolean(objReader[Views.vw_DocumentFolders.IsDeleted]) : false;
                folder.ParentFolderId = objReader[Views.vw_DocumentFolders.ParentFolderId] != DBNull.Value ? Convert.ToInt64(objReader[Views.vw_DocumentFolders.ParentFolderId]) : 0;

                folder.CreatedByUserName = objReader[Views.vw_DocumentFolders.CreatedByUserName] != DBNull.Value ? Convert.ToString(objReader[Views.vw_DocumentFolders.ModifiedByUserName]) : null;
                folder.CreatedByUserFullName = objReader[Views.vw_DocumentFolders.CreatedByUserFullName] != DBNull.Value ? Convert.ToString(objReader[Views.vw_DocumentFolders.ModifiedByUserFullName]) : null;
                folder.CreatedBy = objReader[Views.vw_DocumentFolders.CreatedBy] != DBNull.Value ? Convert.ToInt64(objReader[Views.vw_DocumentFolders.CreatedBy]) : 0;
                folder.CreatedOn = objReader[Views.vw_DocumentFolders.CreatedOn] != DBNull.Value ? Convert.ToDateTime(objReader[Views.vw_DocumentFolders.CreatedOn]) : DateTime.Now;

                folder.ModifiedByUserName = objReader[Views.vw_DocumentFolders.ModifiedByUserName] != DBNull.Value ? Convert.ToString(objReader[Views.vw_DocumentFolders.ModifiedByUserName]) : null;
                folder.ModifiedByUserFullName = objReader[Views.vw_DocumentFolders.ModifiedByUserFullName] != DBNull.Value ? Convert.ToString(objReader[Views.vw_DocumentFolders.ModifiedByUserFullName]) : null;
                folder.ModifiedBy = objReader[Views.vw_DocumentFolders.ModifiedBy] != DBNull.Value ? Convert.ToInt64(objReader[Views.vw_DocumentFolders.ModifiedBy]) : 0;
                folder.ModifiedOn = null;
                if (objReader[Views.vw_DocumentFolders.ModifiedOn] != DBNull.Value)
                {
                    folder.ModifiedOn = Convert.ToDateTime(objReader[Views.vw_DocumentFolders.ModifiedOn]);
                }
                lstFolders.Add(folder);
            }

            if (isnull) { return null; }
            else { return lstFolders; }
        }


        protected List<DocumentFolderTree> CreateDocumentFolderTree(IDataReader objReader, long selectedFolderId)
        {
            List<DocumentFolderTree> lstFolders = new List<DocumentFolderTree>();
            DocumentFolderTree folder;
            while (objReader.Read())
            {
                folder = new DocumentFolderTree();
                folder.id = objReader[Views.usp_Get_DocumentFoldersTree.FolderId] != DBNull.Value ? Convert.ToInt64(objReader[Views.usp_Get_DocumentFoldersTree.FolderId]) : 0;
                folder.text = objReader[Views.usp_Get_DocumentFoldersTree.FolderName] != DBNull.Value ? Convert.ToString(objReader[Views.usp_Get_DocumentFoldersTree.FolderName]) : null;
                folder.parentId = objReader[Views.usp_Get_DocumentFoldersTree.ParentId] != DBNull.Value ? Convert.ToInt64(objReader[Views.usp_Get_DocumentFoldersTree.ParentId]) : 0;
                folder.state = new TreeState();
                if (folder.id == selectedFolderId)
                {
                    folder.state.selected = true;
                    folder.state.expanded = true;
                }
                lstFolders.Add(folder);
            }

            return lstFolders;
        }

        protected List<DocumentFolderTree> GenerateFolderTree(List<DocumentFolderTree> allFolders, long parentFolderId)
        {
            List<DocumentFolderTree> lstFolders = null;
            foreach (var item in allFolders)
            {
                if (item.parentId == parentFolderId)
                {
                    item.nodes = GenerateFolderTree(allFolders, item.id);
                    if (lstFolders == null)
                    {
                        lstFolders = new List<DocumentFolderTree>();
                    }
                    if (item.nodes != null)
                    {
                        if (item.nodes.Any(cus => cus.state.expanded == true))
                        {
                            item.state.expanded = true;
                        }
                    }
                    lstFolders.Add(item);
                }
            }
            return lstFolders;
        }


    }
}
