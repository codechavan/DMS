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
    public class DocumentPropertiesSQL : DocumentPropertiesDAL, IDisposable
    {
        Logger logger = null;

        public DocumentPropertiesSQL(string connectionStringName)
            : base(connectionStringName)
        {
            logger = new Logger("DMS.Repository.SQL.DocumentPropertiesSQL");
        }


        public override FunctionReturnStatus UpdateDocumentProperties(DocumentProperties paramValue)
        {
            Database database;
            FunctionReturnStatus status = new FunctionReturnStatus();
            try
            {
                DatabaseProviderFactory factory = new DatabaseProviderFactory();
                database = factory.Create(ConnectionStringName);

                status = UpdateDocumentProperties(database, paramValue, null);

                return status;
            }
            catch (Exception ex)
            {
                logger.LogEvent(ex.ToString(), LogLevel.Error);
                status.Message = "Error while updating document properties names";
                status.StatusType = StatusType.Error;
                return status;
            }
            finally
            {
                database = null;
            }
        }

        public override List<DocumentProperties> GetDocumentProperties(DocumentPropertiesSearchParameter searchParameters)
        {
            Database database;
            DbCommand dbCommand;
            try
            {
                DatabaseProviderFactory factory = new DatabaseProviderFactory();
                database = factory.Create(ConnectionStringName);
                dbCommand = database.GetStoredProcCommand(StoreProcedures.dbo.usp_Get_DocumentFileProperties);

                if (searchParameters == null)
                {
                    searchParameters = new DocumentPropertiesSearchParameter();
                }

                database.AddInParameter(dbCommand, StoreProcedures.dbo.usp_Get_DocumentFileProperties_Parameters.DocumentFileId, DbType.Int64, searchParameters.DocumentFileId);

                using (IDataReader objReader = database.ExecuteReader(dbCommand))
                {
                    return CreateDocumentPropertiesObject(objReader);
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

        internal FunctionReturnStatus UpdateDocumentProperties(Database database, DocumentProperties paramValue, DbTransaction transaction)
        {

            FunctionReturnStatus status = new FunctionReturnStatus();
            DbCommand dbCommand;

            dbCommand = database.GetStoredProcCommand(StoreProcedures.dbo.usp_Create_Update_DocumentFileProperties);

            database.AddInParameter(dbCommand, StoreProcedures.dbo.usp_Create_Update_DocumentFileProperties_Parameters.DocumentFileId, DbType.Int64, paramValue.DocumentFileId);
            database.AddInParameter(dbCommand, StoreProcedures.dbo.usp_Create_Update_DocumentFileProperties_Parameters.Field1Value, DbType.String, paramValue.Field1Value);
            database.AddInParameter(dbCommand, StoreProcedures.dbo.usp_Create_Update_DocumentFileProperties_Parameters.Field2Value, DbType.String, paramValue.Field2Value);
            database.AddInParameter(dbCommand, StoreProcedures.dbo.usp_Create_Update_DocumentFileProperties_Parameters.Field3Value, DbType.String, paramValue.Field3Value);
            database.AddInParameter(dbCommand, StoreProcedures.dbo.usp_Create_Update_DocumentFileProperties_Parameters.Field4Value, DbType.String, paramValue.Field4Value);
            database.AddInParameter(dbCommand, StoreProcedures.dbo.usp_Create_Update_DocumentFileProperties_Parameters.Field5Value, DbType.String, paramValue.Field5Value);
            database.AddInParameter(dbCommand, StoreProcedures.dbo.usp_Create_Update_DocumentFileProperties_Parameters.Field6Value, DbType.String, paramValue.Field6Value);
            database.AddInParameter(dbCommand, StoreProcedures.dbo.usp_Create_Update_DocumentFileProperties_Parameters.Field7Value, DbType.String, paramValue.Field7Value);
            database.AddInParameter(dbCommand, StoreProcedures.dbo.usp_Create_Update_DocumentFileProperties_Parameters.Field8Value, DbType.String, paramValue.Field8Value);
            database.AddInParameter(dbCommand, StoreProcedures.dbo.usp_Create_Update_DocumentFileProperties_Parameters.Field9Value, DbType.String, paramValue.Field9Value);
            database.AddInParameter(dbCommand, StoreProcedures.dbo.usp_Create_Update_DocumentFileProperties_Parameters.Field10Value, DbType.String, paramValue.Field10Value);

            database.AddInParameter(dbCommand, StoreProcedures.dbo.usp_Create_Update_DocumentFileProperties_Parameters.CreatedBy, DbType.Int64, (paramValue.PropertyValueModifiedBy <= 0 ? paramValue.PropertyValueCreatedBy : paramValue.PropertyValueModifiedBy));

            database.AddOutParameter(dbCommand, StoreProcedures.dbo.usp_Create_Update_DocumentFileProperties_Parameters.ErrorCode, DbType.Int64, int.MaxValue);
            database.AddOutParameter(dbCommand, StoreProcedures.dbo.usp_Create_Update_DocumentFileProperties_Parameters.ErrorDescription, DbType.String, 500);

            if (transaction != null)
            {
                database.ExecuteNonQuery(dbCommand, transaction);
            }
            else
            {
                database.ExecuteNonQuery(dbCommand);
            }

            status.Data = database.GetParameterValue(dbCommand, StoreProcedures.dbo.usp_Create_Update_DocumentFileProperties_Parameters.ErrorCode);
            status.Message = database.GetParameterValue(dbCommand, StoreProcedures.dbo.usp_Create_Update_DocumentFileProperties_Parameters.ErrorDescription).ToString();
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

        public void Dispose()
        {
            logger = null;
        }
    }
}
