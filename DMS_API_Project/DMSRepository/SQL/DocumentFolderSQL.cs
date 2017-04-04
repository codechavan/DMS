using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DMS.Repository.DAL;
using Chavan.Logger;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using DMS.Model;
using DMS.DatabaseConstants;
using System.Data;

namespace DMS.Repository.SQL
{
    public class DocumentFolderSQL : DocumentFolderDAL
    {
        Logger logger = null;

        public DocumentFolderSQL(string connectionStringName)
            : base(connectionStringName)
        {
            logger = new Logger("DMS.Repository.SQL.DocumentFolderSQL");
        }

        public override FunctionReturnStatus CreateUpdateFolder(DocumentFolder folder)
        {
            Database database;
            DbCommand dbCommand;
            FunctionReturnStatus status = new FunctionReturnStatus();
            try
            {
                DatabaseProviderFactory factory = new DatabaseProviderFactory();
                database = factory.Create(ConnectionStringName);
                dbCommand = database.GetStoredProcCommand(StoreProcedures.dbo.usp_Create_Update_DocumentFolder);

                database.AddInParameter(dbCommand, StoreProcedures.dbo.usp_Create_Update_DocumentFolder_Parameters.DocumentFolderId, DbType.Int64, folder.FolderId);
                database.AddInParameter(dbCommand, StoreProcedures.dbo.usp_Create_Update_DocumentFolder_Parameters.DocumentFolderName, DbType.String, folder.FolderName);
                database.AddInParameter(dbCommand, StoreProcedures.dbo.usp_Create_Update_DocumentFolder_Parameters.ParentDocumentFolderId, DbType.String, folder.ParentFolderId);
                database.AddInParameter(dbCommand, StoreProcedures.dbo.usp_Create_Update_DocumentFolder_Parameters.SystemId, DbType.String, folder.SystemId);
                database.AddInParameter(dbCommand, StoreProcedures.dbo.usp_Create_Update_DocumentFolder_Parameters.CreatedBy, DbType.String, (folder.ModifiedBy <= 0 ? folder.CreatedBy : folder.ModifiedBy));


                database.AddOutParameter(dbCommand, StoreProcedures.dbo.usp_Create_Update_DocumentFolder_Parameters.OutDocumentFolderId, DbType.Int64, int.MaxValue);
                database.AddOutParameter(dbCommand, StoreProcedures.dbo.usp_Create_Update_DocumentFolder_Parameters.ErrorDescription, DbType.String, 500);

                database.ExecuteNonQuery(dbCommand);

                status.Data = database.GetParameterValue(dbCommand, StoreProcedures.dbo.usp_Create_Update_DocumentFolder_Parameters.OutDocumentFolderId);
                status.Message = database.GetParameterValue(dbCommand, StoreProcedures.dbo.usp_Create_Update_DocumentFolder_Parameters.ErrorDescription).ToString();
                if (Convert.ToInt64(status.Data) > 0)
                {
                    status.StatusType = StatusType.Success;
                }
                else
                {
                    status.StatusType = StatusType.Error;
                }
                return status;
            }
            catch (Exception ex)
            {
                logger.LogEvent(ex.ToString(), LogLevel.Error);
                status.Message = "Error while updating configuration";
                status.StatusType = StatusType.Error;
                return status;
            }
            finally
            {
                database = null;
            }
        }

        public override DocumentFolderSearchData GetFolders(DocumentFolderSearchParameter searchParameters, PagingDetails pageDetail)
        {
            Database database;
            DbCommand dbCommand;
            try
            {
                if (searchParameters == null)
                {
                    searchParameters = new DocumentFolderSearchParameter();
                }
                if (pageDetail == null)
                {
                    pageDetail = new PagingDetails();
                }

                DatabaseProviderFactory factory = new DatabaseProviderFactory();
                database = factory.Create(ConnectionStringName);
                dbCommand = database.GetStoredProcCommand(StoreProcedures.dbo.usp_Get_DocumentFolders);

                database.AddInParameter(dbCommand, StoreProcedures.dbo.usp_Get_DocumentFolders_Parameters.PageIndex, DbType.Int32, pageDetail.PageIndex);
                database.AddInParameter(dbCommand, StoreProcedures.dbo.usp_Get_DocumentFolders_Parameters.PageSize, DbType.Int32, pageDetail.PageSize);
                database.AddInParameter(dbCommand, StoreProcedures.dbo.usp_Get_DocumentFolders_Parameters.WhereCondition, DbType.String, GetDocumentFolderParameterString(searchParameters));
                database.AddInParameter(dbCommand, StoreProcedures.dbo.usp_Get_DocumentFolders_Parameters.OrderBy, DbType.String, GetDocumentFolderOrderBy(pageDetail.OrderBy));

                DocumentFolderSearchData LstData = new DocumentFolderSearchData();
                List<DocumentFolder> lstFolders = null;
                using (IDataReader objReader = database.ExecuteReader(dbCommand))
                {
                    lstFolders = CreateDocumentFolderObject(objReader);
                    if (objReader.NextResult())
                    {
                        if (objReader.Read())
                        {
                            LstData.RecordCount = objReader[StoreProcedures.dbo.usp_Get_DocumentFolders_Parameters.Column_RecordCount] != DBNull.Value ? Convert.ToInt64(objReader[StoreProcedures.dbo.usp_Get_DocumentFolders_Parameters.Column_RecordCount]) : 0;
                        }
                    }
                }
                LstData.LstData = lstFolders;
                LstData.PageDetail = pageDetail;

                return LstData;
            }
            catch (Exception ex)
            {
                logger.LogEvent(ex.ToString(), LogLevel.Error);
                throw new ArgumentException("Error while fetching records");
            }
            finally
            {
                database = null;
            }
        }

    }
}
