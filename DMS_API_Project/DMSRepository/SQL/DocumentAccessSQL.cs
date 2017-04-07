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
    public class DocumentAccessSQL : DocumentAccessDAL
    {
        Logger logger = null;

        public DocumentAccessSQL(string connectionStringName)
            : base(connectionStringName)
        {
            logger = new Logger("DMS.Repository.SQL.DocumentAccessSQL");
        }

        public override FunctionReturnStatus CreateUpdateDocumentAccess(DocumentAccess paramValue)
        {
            Database database;
            DbCommand dbCommand;
            FunctionReturnStatus status = new FunctionReturnStatus();
            try
            {
                DatabaseProviderFactory factory = new DatabaseProviderFactory();
                database = factory.Create(ConnectionStringName);
                dbCommand = database.GetStoredProcCommand(StoreProcedures.dbo.usp_Create_Update_DocumentAccess);

                database.AddInParameter(dbCommand, StoreProcedures.dbo.usp_Create_Update_DocumentAccess_Parameters.SystemId, DbType.Int64, paramValue.SystemId);
                database.AddInParameter(dbCommand, StoreProcedures.dbo.usp_Create_Update_DocumentAccess_Parameters.ObjectType, DbType.Int32, paramValue.ObjectType);
                database.AddInParameter(dbCommand, StoreProcedures.dbo.usp_Create_Update_DocumentAccess_Parameters.ObjectId, DbType.Int64, paramValue.ObjectId);
                database.AddInParameter(dbCommand, StoreProcedures.dbo.usp_Create_Update_DocumentAccess_Parameters.UserRoleId, DbType.Int64, paramValue.UserRoleId);
                database.AddInParameter(dbCommand, StoreProcedures.dbo.usp_Create_Update_DocumentAccess_Parameters.CanRead, DbType.Boolean, paramValue.CanRead);
                database.AddInParameter(dbCommand, StoreProcedures.dbo.usp_Create_Update_DocumentAccess_Parameters.CanWrite, DbType.Boolean, paramValue.CanWrite);
                database.AddInParameter(dbCommand, StoreProcedures.dbo.usp_Create_Update_DocumentAccess_Parameters.CanDelete, DbType.Boolean, paramValue.CanDelete);
                database.AddInParameter(dbCommand, StoreProcedures.dbo.usp_Create_Update_DocumentAccess_Parameters.CreatedBy, DbType.Int64, (paramValue.ModifiedBy <= 0 ? paramValue.CreatedBy : paramValue.ModifiedBy));

                database.AddOutParameter(dbCommand, StoreProcedures.dbo.usp_Create_Update_DocumentAccess_Parameters.OutDocumentObjectUserRoleMappingId, DbType.Int64, int.MaxValue);
                database.AddOutParameter(dbCommand, StoreProcedures.dbo.usp_Create_Update_DocumentAccess_Parameters.ErrorDescription, DbType.String, 500);

                database.ExecuteNonQuery(dbCommand);

                status.Data = database.GetParameterValue(dbCommand, StoreProcedures.dbo.usp_Create_Update_DocumentAccess_Parameters.OutDocumentObjectUserRoleMappingId);
                status.Message = database.GetParameterValue(dbCommand, StoreProcedures.dbo.usp_Create_Update_DocumentAccess_Parameters.ErrorDescription).ToString();
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
                status.Message = "Error while updating document access";
                status.StatusType = StatusType.Error;
                return status;
            }
            finally
            {
                database = null;
            }
        }

        public override FunctionReturnStatus RemoveDocumentAccess(DocumentAccess paramValue)
        {
            Database database;
            DbCommand dbCommand;
            FunctionReturnStatus status = new FunctionReturnStatus();
            try
            {
                DatabaseProviderFactory factory = new DatabaseProviderFactory();
                database = factory.Create(ConnectionStringName);
                dbCommand = database.GetStoredProcCommand(StoreProcedures.dbo.usp_Remove_DocumentAccess);

                database.AddInParameter(dbCommand, StoreProcedures.dbo.usp_Remove_DocumentAccess_Parameters.SystemId, DbType.Int64, paramValue.SystemId);
                database.AddInParameter(dbCommand, StoreProcedures.dbo.usp_Remove_DocumentAccess_Parameters.ObjectType, DbType.Int32, paramValue.ObjectType);
                database.AddInParameter(dbCommand, StoreProcedures.dbo.usp_Remove_DocumentAccess_Parameters.ObjectId, DbType.Int64, paramValue.ObjectId);
                database.AddInParameter(dbCommand, StoreProcedures.dbo.usp_Remove_DocumentAccess_Parameters.UserRoleId, DbType.Int64, paramValue.UserRoleId);
                database.AddInParameter(dbCommand, StoreProcedures.dbo.usp_Remove_DocumentAccess_Parameters.DeletedBy, DbType.Int64, (paramValue.ModifiedBy <= 0 ? paramValue.CreatedBy : paramValue.ModifiedBy));

                database.AddOutParameter(dbCommand, StoreProcedures.dbo.usp_Create_Update_DocumentAccess_Parameters.OutDocumentObjectUserRoleMappingId, DbType.Int64, int.MaxValue);
                database.AddOutParameter(dbCommand, StoreProcedures.dbo.usp_Create_Update_DocumentAccess_Parameters.ErrorDescription, DbType.String, 500);

                database.ExecuteNonQuery(dbCommand);

                status.Data = database.GetParameterValue(dbCommand, StoreProcedures.dbo.usp_Create_Update_DocumentAccess_Parameters.OutDocumentObjectUserRoleMappingId);
                status.Message = database.GetParameterValue(dbCommand, StoreProcedures.dbo.usp_Create_Update_DocumentAccess_Parameters.ErrorDescription).ToString();
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
                status.Message = "Error while removing document access";
                status.StatusType = StatusType.Error;
                return status;
            }
            finally
            {
                database = null;
            }
        }

        public override FunctionReturnStatus GetDocumentAccess(DocumentAccessSearchParameter searchParameters)
        {
            if (searchParameters == null)
            {
                searchParameters = new DocumentAccessSearchParameter();
            }
            Database database;
            DbCommand dbCommand;
            FunctionReturnStatus status = new FunctionReturnStatus();
            try
            {
                DatabaseProviderFactory factory = new DatabaseProviderFactory();
                database = factory.Create(ConnectionStringName);
                dbCommand = database.GetStoredProcCommand(StoreProcedures.dbo.usp_Get_DocumentAccess);

                database.AddInParameter(dbCommand, StoreProcedures.dbo.usp_Get_DocumentAccess_Parameters.SystemId, DbType.Int64, searchParameters.SystemId);
                database.AddInParameter(dbCommand, StoreProcedures.dbo.usp_Get_DocumentAccess_Parameters.ObjectType, DbType.Int32, searchParameters.ObjectType);
                database.AddInParameter(dbCommand, StoreProcedures.dbo.usp_Get_DocumentAccess_Parameters.ObjectId, DbType.Int64, searchParameters.ObjectId);
                database.AddInParameter(dbCommand, StoreProcedures.dbo.usp_Get_DocumentAccess_Parameters.ForUserId, DbType.Int64, searchParameters.UserId);

                database.AddOutParameter(dbCommand, StoreProcedures.dbo.usp_Get_DocumentAccess_Parameters.CanDelete, DbType.Boolean, int.MaxValue);
                database.AddOutParameter(dbCommand, StoreProcedures.dbo.usp_Get_DocumentAccess_Parameters.CanRead, DbType.Boolean, int.MaxValue);
                database.AddOutParameter(dbCommand, StoreProcedures.dbo.usp_Get_DocumentAccess_Parameters.CanWrite, DbType.Boolean, int.MaxValue);
                database.AddOutParameter(dbCommand, StoreProcedures.dbo.usp_Get_DocumentAccess_Parameters.IsInhereted, DbType.Boolean, int.MaxValue);
                database.AddOutParameter(dbCommand, StoreProcedures.dbo.usp_Get_DocumentAccess_Parameters.InheretedFolderId, DbType.Int64, int.MaxValue);
                database.AddOutParameter(dbCommand, StoreProcedures.dbo.usp_Get_DocumentAccess_Parameters.InheretedFolderName, DbType.String, 250);
                database.AddOutParameter(dbCommand, StoreProcedures.dbo.usp_Get_DocumentAccess_Parameters.OutDocumentObjectUserRoleMappingId, DbType.Int64, int.MaxValue);
                database.AddOutParameter(dbCommand, StoreProcedures.dbo.usp_Get_DocumentAccess_Parameters.ErrorDescription, DbType.String, 500);

                database.ExecuteNonQuery(dbCommand);

                status.Data = database.GetParameterValue(dbCommand, StoreProcedures.dbo.usp_Create_Update_DocumentAccess_Parameters.OutDocumentObjectUserRoleMappingId);
                status.Message = database.GetParameterValue(dbCommand, StoreProcedures.dbo.usp_Create_Update_DocumentAccess_Parameters.ErrorDescription).ToString();
                if (Convert.ToInt64(status.Data) > 0)
                {
                    DocumentAccessDetail access = new DocumentAccessDetail();
                    access.SystemId = searchParameters.SystemId;
                    access.ObjectType = searchParameters.ObjectType;
                    access.ObjectId = searchParameters.ObjectId;
                    access.UserId = searchParameters.UserId;
                    access.CanDelete = Convert.ToBoolean(database.GetParameterValue(dbCommand, StoreProcedures.dbo.usp_Get_DocumentAccess_Parameters.CanDelete));
                    access.CanRead = Convert.ToBoolean(database.GetParameterValue(dbCommand, StoreProcedures.dbo.usp_Get_DocumentAccess_Parameters.CanRead));
                    access.CanWrite = Convert.ToBoolean(database.GetParameterValue(dbCommand, StoreProcedures.dbo.usp_Get_DocumentAccess_Parameters.CanWrite));
                    access.CanWrite = Convert.ToBoolean(database.GetParameterValue(dbCommand, StoreProcedures.dbo.usp_Get_DocumentAccess_Parameters.CanWrite));

                    access.InheretedFolderId = Convert.ToInt64(database.GetParameterValue(dbCommand, StoreProcedures.dbo.usp_Get_DocumentAccess_Parameters.InheretedFolderId));
                    access.IsInhereted = Convert.ToBoolean(database.GetParameterValue(dbCommand, StoreProcedures.dbo.usp_Get_DocumentAccess_Parameters.IsInhereted));
                    access.InheretedFolderName = Convert.ToString(database.GetParameterValue(dbCommand, StoreProcedures.dbo.usp_Get_DocumentAccess_Parameters.InheretedFolderName));

                    status.Data = access;
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
                status.Message = "Error while fetching document access";
                status.StatusType = StatusType.Error;
                return status;
            }
            finally
            {
                database = null;
            }
        }

    }
}
