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
    public class DocumentFileSQL : DocumentFileDAL
    {
        Logger logger = null;

        public DocumentFileSQL(string connectionStringName)
            : base(connectionStringName)
        {
            logger = new Logger("DMS.Repository.SQL.DocumentFileSQL");
        }

        public override FunctionReturnStatus CreateUpdateFileWithAttribute(DocumentFile file, DocumentProperties properties)
        {
            Database database;
            DbCommand dbCommand;
            FunctionReturnStatus status = new FunctionReturnStatus();

            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            database = factory.Create(ConnectionStringName);

            using (DbConnection connection = database.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();

                try
                {
                    dbCommand = database.GetStoredProcCommand(StoreProcedures.dbo.usp_Create_Update_DocumentFile);

                    database.AddInParameter(dbCommand, StoreProcedures.dbo.usp_Create_Update_DocumentFile_Parameters.FileId, DbType.Int64, file.FileId);
                    database.AddInParameter(dbCommand, StoreProcedures.dbo.usp_Create_Update_DocumentFile_Parameters.FileName, DbType.String, file.FileName);
                    database.AddInParameter(dbCommand, StoreProcedures.dbo.usp_Create_Update_DocumentFile_Parameters.FileData, DbType.Binary, file.FileData);
                    database.AddInParameter(dbCommand, StoreProcedures.dbo.usp_Create_Update_DocumentFile_Parameters.FolderId, DbType.Int64, file.FolderId);
                    database.AddInParameter(dbCommand, StoreProcedures.dbo.usp_Create_Update_DocumentFile_Parameters.SystemId, DbType.Int64, file.SystemId);
                    database.AddInParameter(dbCommand, StoreProcedures.dbo.usp_Create_Update_DocumentFile_Parameters.CreatedBy, DbType.Int64, (file.ModifiedBy <= 0 ? file.CreatedBy : file.ModifiedBy));

                    database.AddOutParameter(dbCommand, StoreProcedures.dbo.usp_Create_Update_DocumentFile_Parameters.OutDocumentFileId, DbType.Int64, int.MaxValue);
                    database.AddOutParameter(dbCommand, StoreProcedures.dbo.usp_Create_Update_DocumentFile_Parameters.ErrorDescription, DbType.String, 500);

                    database.ExecuteNonQuery(dbCommand, transaction);

                    status.Data = database.GetParameterValue(dbCommand, StoreProcedures.dbo.usp_Create_Update_DocumentFile_Parameters.OutDocumentFileId);
                    status.Message = database.GetParameterValue(dbCommand, StoreProcedures.dbo.usp_Create_Update_DocumentFile_Parameters.ErrorDescription).ToString();
                    if (Convert.ToInt64(status.Data) > 0)
                    {
                        status.StatusType = StatusType.Success;
                        if (properties != null)
                        {
                            properties.DocumentFileId = Convert.ToInt64(status.Data);
                            using (DocumentPropertiesSQL docProp = new DocumentPropertiesSQL(ConnectionStringName))
                            {
                                FunctionReturnStatus propStatus = new FunctionReturnStatus();
                                propStatus = docProp.UpdateDocumentProperties(database, properties, transaction);
                                if (propStatus.StatusType == StatusType.Success)
                                {
                                    transaction.Commit();
                                }
                                else
                                {
                                    status.StatusType = propStatus.StatusType;
                                    status.Message = propStatus.Message;
                                    transaction.Rollback();
                                }
                            }
                        }
                        else
                        {
                            transaction.Commit();
                        }
                    }
                    else
                    {
                        status.StatusType = StatusType.Error;
                        transaction.Rollback();
                    }
                    return status;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    logger.LogEvent(ex.ToString(), LogLevel.Error);
                    status.Message = "Error while uploading file";
                    status.StatusType = StatusType.Error;
                    return status;
                }
                finally
                {
                    database = null;
                }
            }
        }

        public override DocumentFileSearchData GetFiles(DocumentFileSearchParameter searchParameters, PagingDetails pageDetail)
        {
            Database database;
            DbCommand dbCommand;
            try
            {

                if (searchParameters == null)
                {
                    searchParameters = new DocumentFileSearchParameter();
                }
                if (pageDetail == null)
                {
                    pageDetail = new PagingDetails();
                }

                DatabaseProviderFactory factory = new DatabaseProviderFactory();
                database = factory.Create(ConnectionStringName);
                dbCommand = database.GetStoredProcCommand(StoreProcedures.dbo.usp_Get_DocumentFiles);

                database.AddInParameter(dbCommand, StoreProcedures.dbo.usp_Get_DocumentFiles_Parameters.PageIndex, DbType.Int32, pageDetail.PageIndex);
                database.AddInParameter(dbCommand, StoreProcedures.dbo.usp_Get_DocumentFiles_Parameters.PageSize, DbType.Int32, pageDetail.PageSize);
                database.AddInParameter(dbCommand, StoreProcedures.dbo.usp_Get_DocumentFiles_Parameters.WhereCondition, DbType.String, GetDocumentFileParameterString(searchParameters));
                database.AddInParameter(dbCommand, StoreProcedures.dbo.usp_Get_DocumentFiles_Parameters.OrderBy, DbType.String, GetDocumentFileOrderBy(pageDetail.OrderBy));

                DocumentFileSearchData LstData = new DocumentFileSearchData();
                List<DocumentFile> lstDocuments = null;
                using (IDataReader objReader = database.ExecuteReader(dbCommand))
                {
                    lstDocuments = CreateDocumentFileObject(objReader);
                    if (objReader.NextResult())
                    {
                        if (objReader.Read())
                        {
                            LstData.RecordCount = objReader[StoreProcedures.dbo.usp_Get_DocumentFiles_Parameters.Column_RecordCount] != DBNull.Value ? Convert.ToInt64(objReader[StoreProcedures.dbo.usp_Get_DocumentFiles_Parameters.Column_RecordCount]) : 0;
                        }
                    }
                }
                LstData.LstData = lstDocuments;
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

        public override List<DocumentFileHistory> GetFileHistory(long documentId)
        {
            Database database;
            DbCommand dbCommand;
            try
            {

                DatabaseProviderFactory factory = new DatabaseProviderFactory();
                database = factory.Create(ConnectionStringName);
                dbCommand = database.GetStoredProcCommand(StoreProcedures.dbo.usp_Get_DocumentFileHistory);

                database.AddInParameter(dbCommand, StoreProcedures.dbo.usp_Get_DocumentFileHistory_Parameters.DocumentFileId, DbType.Int32, documentId);

                using (IDataReader objReader = database.ExecuteReader(dbCommand))
                {
                    return CreateDocumentFileHistoryObject(objReader);
                }
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

        public override DocumentProperties GetFilesProperties(long documentId)
        {
            using (DocumentPropertiesSQL docProp = new DocumentPropertiesSQL(ConnectionStringName))
            {
                DocumentPropertiesSearchParameter searchParameter = new DocumentPropertiesSearchParameter();
                searchParameter.DocumentFileId = documentId;

                var propList = docProp.GetDocumentProperties(searchParameter);
                if (propList.Count == 1)
                {
                    return propList[0];
                }
            }
            return null;
        }
    }
}
