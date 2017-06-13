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
    public class SystemsSQL : SystemsDAL
    {
        Logger logger = null;

        public SystemsSQL(string connectionStringName)
            : base(connectionStringName)
        {
            logger = new Logger("DMS.Repository.SQL.SystemsSQL");
        }

        public override FunctionReturnStatus CreateDmsSystem(DmsSystem system, DmsUser dmsUser, DmsUserRole userRole)
        {
            Database database;
            DbCommand dbCommand;
            FunctionReturnStatus status = new FunctionReturnStatus();
            try
            {
                DatabaseProviderFactory factory = new DatabaseProviderFactory();
                database = factory.Create(ConnectionStringName);
                dbCommand = database.GetStoredProcCommand(StoreProcedures.dbo.usp_create_system);

                database.AddInParameter(dbCommand, StoreProcedures.dbo.usp_create_system_Parameters.SystemName, DbType.String, system.SystemName);
                database.AddInParameter(dbCommand, StoreProcedures.dbo.usp_create_system_Parameters.UserName, DbType.String, dmsUser.UserName);
                database.AddInParameter(dbCommand, StoreProcedures.dbo.usp_create_system_Parameters.UserFullName, DbType.String, dmsUser.FullName);
                database.AddInParameter(dbCommand, StoreProcedures.dbo.usp_create_system_Parameters.UserPassword, DbType.String, dmsUser.Password);
                database.AddInParameter(dbCommand, StoreProcedures.dbo.usp_create_system_Parameters.UserRoleName, DbType.String, userRole.RoleName);
                database.AddInParameter(dbCommand, StoreProcedures.dbo.usp_create_system_Parameters.UserRoleDescription, DbType.String, userRole.Description);
                database.AddInParameter(dbCommand, StoreProcedures.dbo.usp_create_system_Parameters.UserEmailId, DbType.String, dmsUser.EmailId);

                database.AddOutParameter(dbCommand, StoreProcedures.dbo.usp_create_system_Parameters.SystemId, DbType.Int64, int.MaxValue);
                database.AddOutParameter(dbCommand, StoreProcedures.dbo.usp_create_system_Parameters.ErrorDescription, DbType.String, 500);

                database.ExecuteNonQuery(dbCommand);

                status.Data = database.GetParameterValue(dbCommand, StoreProcedures.dbo.usp_create_system_Parameters.SystemId);
                status.Message = database.GetParameterValue(dbCommand, StoreProcedures.dbo.usp_create_system_Parameters.ErrorDescription).ToString();
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
                status.Message = "Error while creating new dms system";
                status.StatusType = StatusType.Error;
                return status;
            }
            finally
            {
                database = null;
            }
        }

        public override FunctionReturnStatus UpdateDmsSystem(DmsSystem system)
        {
            Database database;
            DbCommand dbCommand;
            FunctionReturnStatus status = new FunctionReturnStatus();
            try
            {
                DatabaseProviderFactory factory = new DatabaseProviderFactory();
                database = factory.Create(ConnectionStringName);
                dbCommand = database.GetStoredProcCommand(StoreProcedures.dbo.usp_Update_System);

                database.AddInParameter(dbCommand, StoreProcedures.dbo.usp_Update_System_Parameters.SystemId, DbType.Int64, system.SystemId);
                database.AddInParameter(dbCommand, StoreProcedures.dbo.usp_Update_System_Parameters.SystemName, DbType.String, system.SystemName);
                database.AddInParameter(dbCommand, StoreProcedures.dbo.usp_Update_System_Parameters.SystemIsActive, DbType.Boolean, system.IsActive);
                database.AddInParameter(dbCommand, StoreProcedures.dbo.usp_Update_System_Parameters.SystemModifiedBy, DbType.Int64, system.ModifiedBy);

                database.AddOutParameter(dbCommand, StoreProcedures.dbo.usp_Update_System_Parameters.ErrorCode, DbType.Int64, int.MaxValue);
                database.AddOutParameter(dbCommand, StoreProcedures.dbo.usp_Update_System_Parameters.ErrorDescription, DbType.String, 500);

                database.ExecuteNonQuery(dbCommand);

                status.Data = database.GetParameterValue(dbCommand, StoreProcedures.dbo.usp_Update_System_Parameters.ErrorCode);
                status.Message = database.GetParameterValue(dbCommand, StoreProcedures.dbo.usp_Update_System_Parameters.ErrorDescription).ToString();
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
                status.Message = "Error while updating dms system";
                status.StatusType = StatusType.Error;
                return status;
            }
            finally
            {
                database = null;
            }
        }

        public override DmsSystemSearchData GetSystem(DmsSystemSearchParameters searchParameters)
        {
            Database database;
            DbCommand dbCommand;
            try
            {
                if (searchParameters == null)
                {
                    searchParameters = new DmsSystemSearchParameters();
                }
                if (searchParameters.PageDetail == null)
                {
                    searchParameters.PageDetail = new PagingDetails();
                }

                DatabaseProviderFactory factory = new DatabaseProviderFactory();
                database = factory.Create(ConnectionStringName);
                dbCommand = database.GetStoredProcCommand(StoreProcedures.dbo.usp_Get_Systems);

                database.AddInParameter(dbCommand, StoreProcedures.dbo.usp_Get_Systems_Parameters.PageIndex, DbType.Int32, searchParameters.PageDetail.PageIndex);
                database.AddInParameter(dbCommand, StoreProcedures.dbo.usp_Get_Systems_Parameters.PageSize, DbType.Int32, searchParameters.PageDetail.PageSize);
                database.AddInParameter(dbCommand, StoreProcedures.dbo.usp_Get_Systems_Parameters.WhereCondition, DbType.String, GetSystemSearchParameterString(searchParameters));
                database.AddInParameter(dbCommand, StoreProcedures.dbo.usp_Get_Systems_Parameters.OrderBy, DbType.String, GetSystemOrderBy(searchParameters.PageDetail.OrderBy));

                DmsSystemSearchData LstData = new DmsSystemSearchData();
                List<DmsSystem> lstSystems = null;
                using (IDataReader objReader = database.ExecuteReader(dbCommand))
                {
                    lstSystems = CreateSystemObject(objReader);
                    if (objReader.NextResult())
                    {
                        if (objReader.Read())
                        {
                            LstData.RecordCount = objReader[StoreProcedures.dbo.usp_Get_Systems_Parameters.Column_RecordCount] != DBNull.Value ? Convert.ToInt64(objReader[StoreProcedures.dbo.usp_Get_Systems_Parameters.Column_RecordCount]) : 0;
                        }
                    }
                }
                LstData.LstData = lstSystems;
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
