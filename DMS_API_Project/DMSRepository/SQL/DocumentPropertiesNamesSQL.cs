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
    public class DocumentPropertiesNamesSQL : DocumentPropertiesNamesDAL
    {
        Logger logger = null;

        public DocumentPropertiesNamesSQL(string connectionStringName)
            : base(connectionStringName)
        {
            logger = new Logger("DMS.Repository.SQL.DocumentFilePropertiesNamesSQL");
        }

        public override FunctionReturnStatus CreateUpdateDocumentPropertiesNames(DocumentPropertiesNames propertiesName)
        {
            Database database;
            DbCommand dbCommand;
            FunctionReturnStatus status = new FunctionReturnStatus();
            try
            {
                DatabaseProviderFactory factory = new DatabaseProviderFactory();
                database = factory.Create(ConnectionStringName);
                dbCommand = database.GetStoredProcCommand(StoreProcedures.dbo.usp_Create_Update_DocumentPropertiesName);

                database.AddInParameter(dbCommand, StoreProcedures.dbo.usp_Create_Update_DocumentPropertiesName_Parameters.SystemId, DbType.Int64, propertiesName.SystemId);
                database.AddInParameter(dbCommand, StoreProcedures.dbo.usp_Create_Update_DocumentPropertiesName_Parameters.Field1Name, DbType.String, propertiesName.Field1Name);
                database.AddInParameter(dbCommand, StoreProcedures.dbo.usp_Create_Update_DocumentPropertiesName_Parameters.Field2Name, DbType.String, propertiesName.Field2Name);
                database.AddInParameter(dbCommand, StoreProcedures.dbo.usp_Create_Update_DocumentPropertiesName_Parameters.Field3Name, DbType.String, propertiesName.Field3Name);
                database.AddInParameter(dbCommand, StoreProcedures.dbo.usp_Create_Update_DocumentPropertiesName_Parameters.Field4Name, DbType.String, propertiesName.Field4Name);
                database.AddInParameter(dbCommand, StoreProcedures.dbo.usp_Create_Update_DocumentPropertiesName_Parameters.Field5Name, DbType.String, propertiesName.Field5Name);
                database.AddInParameter(dbCommand, StoreProcedures.dbo.usp_Create_Update_DocumentPropertiesName_Parameters.Field6Name, DbType.String, propertiesName.Field6Name);
                database.AddInParameter(dbCommand, StoreProcedures.dbo.usp_Create_Update_DocumentPropertiesName_Parameters.Field7Name, DbType.String, propertiesName.Field7Name);
                database.AddInParameter(dbCommand, StoreProcedures.dbo.usp_Create_Update_DocumentPropertiesName_Parameters.Field8Name, DbType.String, propertiesName.Field8Name);
                database.AddInParameter(dbCommand, StoreProcedures.dbo.usp_Create_Update_DocumentPropertiesName_Parameters.Field9Name, DbType.String, propertiesName.Field9Name);
                database.AddInParameter(dbCommand, StoreProcedures.dbo.usp_Create_Update_DocumentPropertiesName_Parameters.Field10Name, DbType.String, propertiesName.Field10Name);
                database.AddInParameter(dbCommand, StoreProcedures.dbo.usp_Create_Update_DocumentPropertiesName_Parameters.CreatedBy, DbType.Int64, (propertiesName.ModifiedBy <= 0 ? propertiesName.CreatedBy : propertiesName.ModifiedBy));

                database.AddOutParameter(dbCommand, StoreProcedures.dbo.usp_Create_Update_DocumentPropertiesName_Parameters.ErrorCode, DbType.Int64, int.MaxValue);
                database.AddOutParameter(dbCommand, StoreProcedures.dbo.usp_Create_Update_DocumentPropertiesName_Parameters.ErrorDescription, DbType.String, 500);

                database.ExecuteNonQuery(dbCommand);

                status.Data = database.GetParameterValue(dbCommand, StoreProcedures.dbo.usp_Create_Update_DocumentPropertiesName_Parameters.ErrorCode);
                status.Message = database.GetParameterValue(dbCommand, StoreProcedures.dbo.usp_Create_Update_DocumentPropertiesName_Parameters.ErrorDescription).ToString();
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
                status.Message = "Error while updating document properties names";
                status.StatusType = StatusType.Error;
                return status;
            }
            finally
            {
                database = null;
            }
        }

        public override DocumentPropertiesNames GetDocumentPropertiesNames(DocumentPropertiesNamesSearchParamater searchParameters, PagingDetails pageDetail)
        {
            Database database;
            DbCommand dbCommand;
            try
            {
                DatabaseProviderFactory factory = new DatabaseProviderFactory();
                database = factory.Create(ConnectionStringName);
                dbCommand = database.GetStoredProcCommand(StoreProcedures.dbo.usp_Get_DocumentPropertiesName);

                if (searchParameters == null)
                {
                    searchParameters = new DocumentPropertiesNamesSearchParamater();
                }
                if (pageDetail == null)
                {
                    pageDetail = new PagingDetails();
                }

                database.AddInParameter(dbCommand, StoreProcedures.dbo.usp_Get_DocumentPropertiesName_Parameters.SystemId, DbType.Int64, searchParameters.SystemId);

                List<DocumentPropertiesNames> lstDocPropNames = null;
                using (IDataReader objReader = database.ExecuteReader(dbCommand))
                {
                    lstDocPropNames = CreateDocumentPropertiesNamesObject(objReader);
                    if (lstDocPropNames != null)
                    {
                        if (lstDocPropNames.Count == 1)
                        {
                            return lstDocPropNames[0];
                        }
                    }
                }
                return null;
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
