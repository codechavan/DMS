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
    public class SystemParameterSQL : SystemParameterDAL
    {
        Logger logger = null;

        public SystemParameterSQL(string connectionStringName)
            : base(connectionStringName)
        {
            logger = new Logger("DMS.Repository.SQL.SystemParameterSQL");
        }

        public override FunctionReturnStatus UpdateSystemParameterValue(SystemParameterValue paramValue)
        {
            Database database;
            DbCommand dbCommand;
            FunctionReturnStatus status = new FunctionReturnStatus();
            try
            {
                DatabaseProviderFactory factory = new DatabaseProviderFactory();
                database = factory.Create(ConnectionStringName);
                dbCommand = database.GetStoredProcCommand(StoreProcedures.dbo.usp_Create_Update_SystemParameter);

                database.AddInParameter(dbCommand, StoreProcedures.dbo.usp_Create_Update_SystemParameter_Parameters.SystemId, DbType.Int64, paramValue.SystemId);
                database.AddInParameter(dbCommand, StoreProcedures.dbo.usp_Create_Update_SystemParameter_Parameters.SystemParameterId, DbType.Int64, paramValue.ParameterId);
                database.AddInParameter(dbCommand, StoreProcedures.dbo.usp_Create_Update_SystemParameter_Parameters.SystemParameterValue, DbType.String, paramValue.ParameterValue);
                database.AddInParameter(dbCommand, StoreProcedures.dbo.usp_Create_Update_SystemParameter_Parameters.SystemParameterValueCreatedBy, DbType.Int64, paramValue.ModifiedBy);

                database.AddOutParameter(dbCommand, StoreProcedures.dbo.usp_Create_Update_SystemParameter_Parameters.SystemParameterValueId, DbType.Int64, int.MaxValue);
                database.AddOutParameter(dbCommand, StoreProcedures.dbo.usp_Create_Update_SystemParameter_Parameters.ErrorDescription, DbType.String, 500);

                database.ExecuteNonQuery(dbCommand);

                status.Data = database.GetParameterValue(dbCommand, StoreProcedures.dbo.usp_Create_Update_SystemParameter_Parameters.SystemParameterValueId);
                status.Message = database.GetParameterValue(dbCommand, StoreProcedures.dbo.usp_Create_Update_SystemParameter_Parameters.ErrorDescription).ToString();
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
                status.Message = "Error while updating system parameters";
                status.StatusType = StatusType.Error;
                return status;
            }
            finally
            {
                database = null;
            }
        }

        public override SystemParameterValueSearchData GetSystemParameterValue(SystemParameterSearchParameters searchParameters)
        {
            Database database;
            DbCommand dbCommand;
            try
            {

                if (searchParameters == null)
                {
                    searchParameters = new SystemParameterSearchParameters();
                    searchParameters.SystemId = 0; //TODO : should not fetch for all system id
                }
                if (searchParameters.PageDetail == null)
                {
                    searchParameters.PageDetail = new PagingDetails();
                }

                DatabaseProviderFactory factory = new DatabaseProviderFactory();
                database = factory.Create(ConnectionStringName);
                dbCommand = database.GetStoredProcCommand(StoreProcedures.dbo.usp_Get_SystemParameterValues);

                database.AddInParameter(dbCommand, StoreProcedures.dbo.usp_Get_SystemParameterValues_Parameters.SystemId, DbType.Int64, searchParameters.SystemId);
                database.AddInParameter(dbCommand, StoreProcedures.dbo.usp_Get_SystemParameterValues_Parameters.PageIndex, DbType.Int32, searchParameters.PageDetail.PageIndex);
                database.AddInParameter(dbCommand, StoreProcedures.dbo.usp_Get_SystemParameterValues_Parameters.PageSize, DbType.Int32, searchParameters.PageDetail.PageSize);
                database.AddInParameter(dbCommand, StoreProcedures.dbo.usp_Get_SystemParameterValues_Parameters.WhereCondition, DbType.String, GetSystemParameterValueSearchParameterString(searchParameters));
                database.AddInParameter(dbCommand, StoreProcedures.dbo.usp_Get_SystemParameterValues_Parameters.OrderBy, DbType.String, GetSystemParameterValueOrderBy(searchParameters.PageDetail.OrderBy));

                SystemParameterValueSearchData LstData = new SystemParameterValueSearchData(); 
                List<SystemParameterValue> lstParamValues = null;
                using (IDataReader objReader = database.ExecuteReader(dbCommand))
                {
                    lstParamValues = CreateSystemParameterValueObject(objReader);
                    if (objReader.NextResult())
                    {
                        if (objReader.Read())
                        {
                            LstData.RecordCount = objReader[StoreProcedures.dbo.usp_Get_SystemParameterValues_Parameters.Column_RecordCount] != DBNull.Value ? Convert.ToInt64(objReader[StoreProcedures.dbo.usp_Get_SystemParameterValues_Parameters.Column_RecordCount]) : 0;
                        }
                    }
                }
                LstData.LstData = lstParamValues;
                LstData.PageDetail = searchParameters.PageDetail;
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

        public override List<SystemParameter> GetSystemParameter(long systemParameterId)
        {
            Database database;
            DbCommand dbCommand;
            try
            {
                DatabaseProviderFactory factory = new DatabaseProviderFactory();
                database = factory.Create(ConnectionStringName);
                dbCommand = database.GetStoredProcCommand(StoreProcedures.dbo.usp_Get_SystemParameters);

                database.AddInParameter(dbCommand, StoreProcedures.dbo.usp_Get_SystemParameters_Parameters.SystemParameterId, DbType.Int64, systemParameterId);

                List<SystemParameter> lstParams = null;
                using (IDataReader objReader = database.ExecuteReader(dbCommand))
                {
                    lstParams = CreateSystemParameterObject(objReader);
                }
                return lstParams;
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
