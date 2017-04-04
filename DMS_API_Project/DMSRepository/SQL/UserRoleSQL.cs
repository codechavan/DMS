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
    public class UserRoleSQL : UserRoleDAL
    {
        Logger logger = null;

        public UserRoleSQL(string connectionStringName)
            : base(connectionStringName)
        {
            logger = new Logger("DMS.Repository.SQL.UserRoleSQL");
        }

        public override FunctionReturnStatus CreateUpdateUserRole(DmsUserRole userRole)
        {
            Database database;
            DbCommand dbCommand;
            FunctionReturnStatus status = new FunctionReturnStatus();
            try
            {
                DatabaseProviderFactory factory = new DatabaseProviderFactory();
                database = factory.Create(ConnectionStringName);
                dbCommand = database.GetStoredProcCommand(StoreProcedures.dbo.usp_Create_Update_Userrole);

                database.AddInParameter(dbCommand, StoreProcedures.dbo.usp_Create_Update_Userrole_Parameters.SystemId, DbType.Int64, userRole.SystemId);
                database.AddInParameter(dbCommand, StoreProcedures.dbo.usp_Create_Update_Userrole_Parameters.UserRoleId, DbType.Int64, userRole.RoleId);
                database.AddInParameter(dbCommand, StoreProcedures.dbo.usp_Create_Update_Userrole_Parameters.UserRoleName, DbType.String, userRole.RoleName);
                database.AddInParameter(dbCommand, StoreProcedures.dbo.usp_Create_Update_Userrole_Parameters.UserRoleDescription, DbType.Int64, userRole.Description);
                database.AddInParameter(dbCommand, StoreProcedures.dbo.usp_Create_Update_Userrole_Parameters.UserRoleCreatedBy, DbType.Int64, (userRole.ModifiedBy <= 0 ? userRole.CreatedBy : userRole.ModifiedBy));

                database.AddOutParameter(dbCommand, StoreProcedures.dbo.usp_Create_Update_Userrole_Parameters.UserRoleIdOut, DbType.Int64, int.MaxValue);
                database.AddOutParameter(dbCommand, StoreProcedures.dbo.usp_Create_Update_Userrole_Parameters.ErrorDescription, DbType.String, 500);

                database.ExecuteNonQuery(dbCommand);

                status.Data = database.GetParameterValue(dbCommand, StoreProcedures.dbo.usp_Create_Update_Userrole_Parameters.UserRoleIdOut);
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

        public override DmsUserRoleSearchData GetUserRole(DmsUserRoleSearchParameter searchParameters, PagingDetails pageDetail)
        {
            Database database;
            DbCommand dbCommand;
            try
            {
                if (searchParameters == null)
                {
                    searchParameters = new DmsUserRoleSearchParameter();
                }
                if (pageDetail == null)
                {
                    pageDetail = new PagingDetails();
                }
                DatabaseProviderFactory factory = new DatabaseProviderFactory();
                database = factory.Create(ConnectionStringName);
                dbCommand = database.GetStoredProcCommand(StoreProcedures.dbo.usp_Get_Userroles);

                database.AddInParameter(dbCommand, StoreProcedures.dbo.usp_Get_Userroles_Parameters.PageIndex, DbType.Int32, pageDetail.PageIndex);
                database.AddInParameter(dbCommand, StoreProcedures.dbo.usp_Get_Userroles_Parameters.PageSize, DbType.Int32, pageDetail.PageSize);
                database.AddInParameter(dbCommand, StoreProcedures.dbo.usp_Get_Userroles_Parameters.WhereCondition, DbType.String, GetUserRoleSearchParameterString(searchParameters));
                database.AddInParameter(dbCommand, StoreProcedures.dbo.usp_Get_Userroles_Parameters.OrderBy, DbType.String, GetUserRoleOrderBy(pageDetail.OrderBy));

                DmsUserRoleSearchData LstData = new DmsUserRoleSearchData();
                List<DmsUserRole> lstUserRoles = null;
                using (IDataReader objReader = database.ExecuteReader(dbCommand))
                {
                    lstUserRoles = CreateUserRoleObjects(objReader); 
                    if (objReader.NextResult())
                    {
                        if (objReader.Read())
                        {
                            LstData.RecordCount = objReader[StoreProcedures.dbo.usp_Get_Userroles_Parameters.Column_RecordCount] != DBNull.Value ? Convert.ToInt64(objReader[StoreProcedures.dbo.usp_Get_Userroles_Parameters.Column_RecordCount]) : 0;
                        }
                    }
                }
                LstData.LstData = lstUserRoles;
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
