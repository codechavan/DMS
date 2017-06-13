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
    public class ConfigurationSQL : ConfigurationDAL
    {
        Logger logger = null;

        public ConfigurationSQL(string connectionStringName)
            : base(connectionStringName)
        {
            logger = new Logger("DMS.Repository.SQL.ConfigurationSQL");
        }


        public override FunctionReturnStatus UpdateConfigurationValue(SysConfiguration paramValue)
        {
            Database database;
            DbCommand dbCommand;
            FunctionReturnStatus status = new FunctionReturnStatus();
            try
            {
                DatabaseProviderFactory factory = new DatabaseProviderFactory();
                database = factory.Create(ConnectionStringName);
                dbCommand = database.GetStoredProcCommand(StoreProcedures.dbo.usp_Update_Configuration);

                database.AddInParameter(dbCommand, StoreProcedures.dbo.usp_Update_Configuration_Parameters.ConfigurationCode, DbType.String, paramValue.ConfigurationCode);
                database.AddInParameter(dbCommand, StoreProcedures.dbo.usp_Update_Configuration_Parameters.ConfigurationValue, DbType.String, paramValue.Value);
                database.AddInParameter(dbCommand, StoreProcedures.dbo.usp_Update_Configuration_Parameters.ConfigurationModifiedBy, DbType.String, paramValue.ModifiedBy);
                database.AddInParameter(dbCommand, StoreProcedures.dbo.usp_Update_Configuration_Parameters.Remarks, DbType.String, paramValue.Remarks);

                database.AddOutParameter(dbCommand, StoreProcedures.dbo.usp_Update_Configuration_Parameters.ErrorCode, DbType.Int64, int.MaxValue);
                database.AddOutParameter(dbCommand, StoreProcedures.dbo.usp_Update_Configuration_Parameters.ErrorDescription, DbType.String, 500);

                database.ExecuteNonQuery(dbCommand);

                status.Data = database.GetParameterValue(dbCommand, StoreProcedures.dbo.usp_Update_Configuration_Parameters.ErrorCode);
                status.Message = database.GetParameterValue(dbCommand, StoreProcedures.dbo.usp_Update_Configuration_Parameters.ErrorDescription).ToString();
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

        public override SysConfigurationSearchData GetConfigurations(ConfigurationSearchParameter searchParameters)
        {
            Database database;
            DbCommand dbCommand;
            try
            {

                if (searchParameters == null)
                {
                    searchParameters = new ConfigurationSearchParameter();
                }
                if (searchParameters.PageDetail == null)
                {
                    searchParameters.PageDetail = new PagingDetails();
                }

                DatabaseProviderFactory factory = new DatabaseProviderFactory();
                database = factory.Create(ConnectionStringName);
                dbCommand = database.GetStoredProcCommand(StoreProcedures.dbo.usp_Get_Configurations);

                database.AddInParameter(dbCommand, StoreProcedures.dbo.usp_Get_Configurations_Parameters.PageIndex, DbType.Int32, searchParameters.PageDetail.PageIndex);
                database.AddInParameter(dbCommand, StoreProcedures.dbo.usp_Get_Configurations_Parameters.PageSize, DbType.Int32, searchParameters.PageDetail.PageSize);
                database.AddInParameter(dbCommand, StoreProcedures.dbo.usp_Get_Configurations_Parameters.WhereCondition, DbType.String, GetConfigurationSearchParameterString(searchParameters));
                database.AddInParameter(dbCommand, StoreProcedures.dbo.usp_Get_Configurations_Parameters.OrderBy, DbType.String, GetConfigurationsOrderBy(searchParameters.PageDetail.OrderBy));

                SysConfigurationSearchData LstData = new SysConfigurationSearchData();
                List<SysConfiguration> lstConfiguration = null;
                using (IDataReader objReader = database.ExecuteReader(dbCommand))
                {
                    lstConfiguration = CreateConfigurationObject(objReader);
                    if (objReader.NextResult())
                    {
                        if (objReader.Read())
                        {
                            LstData.RecordCount = objReader[StoreProcedures.dbo.usp_Get_Configurations_Parameters.Column_RecordCount] != DBNull.Value ? Convert.ToInt64(objReader[StoreProcedures.dbo.usp_Get_Configurations_Parameters.Column_RecordCount]) : 0;
                        }
                    }
                }
                LstData.LstData = lstConfiguration;
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

    }
}
