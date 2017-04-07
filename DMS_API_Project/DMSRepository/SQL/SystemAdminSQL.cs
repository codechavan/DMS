using Chavan.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DMS.Repository.DAL;
using DMS.Model;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using DMS.DatabaseConstants;
using System.Data;

namespace DMS.Repository.SQL
{
    public class SystemAdminSQL : SystemAdminDAL
    {
        Logger logger = null;

        public SystemAdminSQL(string connectionStringName)
            : base(connectionStringName)
        {
            logger = new Logger("DMS.Repository.SQL.SystemAdminSQL");
        }


        public override FunctionReturnStatus CreateUpdateSystemAdmin(SystemAdmin admin)
        {
            Database database;
            DbCommand dbCommand;
            FunctionReturnStatus status = new FunctionReturnStatus();
            try
            {
                DatabaseProviderFactory factory = new DatabaseProviderFactory();
                database = factory.Create(ConnectionStringName);
                dbCommand = database.GetStoredProcCommand(StoreProcedures.dbo.usp_Create_Update_SystemAdmin);

                database.AddInParameter(dbCommand, StoreProcedures.dbo.usp_Create_Update_SystemAdmin_Parameters.AdminId, DbType.Int64, admin.AdminId);
                database.AddInParameter(dbCommand, StoreProcedures.dbo.usp_Create_Update_SystemAdmin_Parameters.UserName, DbType.String, admin.UserName);
                database.AddInParameter(dbCommand, StoreProcedures.dbo.usp_Create_Update_SystemAdmin_Parameters.FullName, DbType.String, admin.FullName);
                database.AddInParameter(dbCommand, StoreProcedures.dbo.usp_Create_Update_SystemAdmin_Parameters.EmailId, DbType.String, admin.EmailId);
                database.AddInParameter(dbCommand, StoreProcedures.dbo.usp_Create_Update_SystemAdmin_Parameters.CreatedBy, DbType.Int64, (admin.ModifiedBy <= 0 ? admin.CreatedBy : admin.ModifiedBy));
                database.AddInParameter(dbCommand, StoreProcedures.dbo.usp_Create_Update_SystemAdmin_Parameters.Password, DbType.String, admin.Password);

                database.AddOutParameter(dbCommand, StoreProcedures.dbo.usp_Create_Update_SystemAdmin_Parameters.OutAdminId, DbType.Int64, int.MaxValue);
                database.AddOutParameter(dbCommand, StoreProcedures.dbo.usp_Create_Update_SystemAdmin_Parameters.ErrorDescription, DbType.String, 500);

                database.ExecuteNonQuery(dbCommand);

                status.Data = database.GetParameterValue(dbCommand, StoreProcedures.dbo.usp_Create_Update_SystemAdmin_Parameters.OutAdminId);
                status.Message = database.GetParameterValue(dbCommand, StoreProcedures.dbo.usp_Create_Update_SystemAdmin_Parameters.ErrorDescription).ToString();
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
                status.Message = "Error while creating new user";
                status.StatusType = StatusType.Error;
                return status;
            }
            finally
            {
                database = null;
            }
        }

        public override SystemAdminSearchData GetSystemAdmin(SystemAdminSearchParameter searchParameters, PagingDetails pageDetail)
        {
            Database database;
            DbCommand dbCommand;
            try
            {

                if (searchParameters == null)
                {
                    searchParameters = new SystemAdminSearchParameter();
                }
                if (pageDetail == null)
                {
                    pageDetail = new PagingDetails();
                }

                DatabaseProviderFactory factory = new DatabaseProviderFactory();
                database = factory.Create(ConnectionStringName);
                dbCommand = database.GetStoredProcCommand(StoreProcedures.dbo.usp_Get_Users);

                database.AddInParameter(dbCommand, StoreProcedures.dbo.usp_Get_Users_Parameters.PageIndex, DbType.Int32, pageDetail.PageIndex);
                database.AddInParameter(dbCommand, StoreProcedures.dbo.usp_Get_Users_Parameters.PageSize, DbType.Int32, pageDetail.PageSize);
                database.AddInParameter(dbCommand, StoreProcedures.dbo.usp_Get_Users_Parameters.WhereCondition, DbType.String, GetSystemAdminParameterString(searchParameters));
                database.AddInParameter(dbCommand, StoreProcedures.dbo.usp_Get_Users_Parameters.OrderBy, DbType.String, GetSystemAdminOrderBy(pageDetail.OrderBy));

                SystemAdminSearchData LstData = new SystemAdminSearchData();
                List<SystemAdmin> lstSysAdmin = null;
                using (IDataReader objReader = database.ExecuteReader(dbCommand))
                {
                    lstSysAdmin = CreateSystemAdminObject(objReader);
                    if (objReader.NextResult())
                    {
                        if (objReader.Read())
                        {
                            LstData.RecordCount = objReader[StoreProcedures.dbo.usp_Get_Users_Parameters.Column_RecordCount] != DBNull.Value ? Convert.ToInt64(objReader[StoreProcedures.dbo.usp_Get_Users_Parameters.Column_RecordCount]) : 0;
                        }
                    }
                }
                LstData.LstData = lstSysAdmin;
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

        public override FunctionReturnStatus Login(string username, string password)
        {
            Database database;
            DbCommand dbCommand;
            FunctionReturnStatus status = new FunctionReturnStatus();
            try
            {
                DatabaseProviderFactory factory = new DatabaseProviderFactory();
                database = factory.Create(ConnectionStringName);
                dbCommand = database.GetStoredProcCommand(StoreProcedures.dbo.usp_Get_SystemAdminLogin);

                database.AddInParameter(dbCommand, StoreProcedures.dbo.usp_Get_SystemAdminLogin_Parameters.UserName, DbType.String, username);
                database.AddInParameter(dbCommand, StoreProcedures.dbo.usp_Get_SystemAdminLogin_Parameters.Password, DbType.String, password);

                database.AddOutParameter(dbCommand, StoreProcedures.dbo.usp_Get_SystemAdminLogin_Parameters.OutAdminId, DbType.Int64, int.MaxValue);
                database.AddOutParameter(dbCommand, StoreProcedures.dbo.usp_Get_SystemAdminLogin_Parameters.ErrorDescription, DbType.String, 500);

                SystemAdmin user = null;
                using (IDataReader objReader = database.ExecuteReader(dbCommand))
                {
                    var data = CreateSystemAdminObject(objReader);
                    if (data != null)
                    {
                        if (data.Count == 1)
                        {
                            user = data[0];
                        }
                    }
                }

                status.Data = database.GetParameterValue(dbCommand, StoreProcedures.dbo.usp_Get_SystemAdminLogin_Parameters.OutAdminId);
                status.Message = database.GetParameterValue(dbCommand, StoreProcedures.dbo.usp_Get_SystemAdminLogin_Parameters.ErrorDescription).ToString();
                if (Convert.ToInt64(status.Data) > 0 && user != null)
                {
                    status.StatusType = StatusType.Success;
                    user.LogonToken = GenerateSystemAdminLogonToken(user.AdminId);
                    status.Data = user;
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
                status.Message = "Error while login, kindly contact system administrator";
                status.StatusType = StatusType.Error;
                return status;
            }
            finally
            {
                database = null;
            }
        }

        public override FunctionReturnStatus ChangePasword(long userId, string oldPassword, string newPassword, long updatedByUser)
        {
            Database database;
            DbCommand dbCommand;
            FunctionReturnStatus status = new FunctionReturnStatus();
            try
            {
                DatabaseProviderFactory factory = new DatabaseProviderFactory();
                database = factory.Create(ConnectionStringName);
                dbCommand = database.GetStoredProcCommand(StoreProcedures.dbo.usp_Update_SystemAdminPassword);

                database.AddInParameter(dbCommand, StoreProcedures.dbo.usp_Update_SystemAdminPassword_Parameters.AdminId, DbType.Int64, userId);
                database.AddInParameter(dbCommand, StoreProcedures.dbo.usp_Update_SystemAdminPassword_Parameters.UpdatedBy, DbType.Int64, updatedByUser);
                database.AddInParameter(dbCommand, StoreProcedures.dbo.usp_Update_SystemAdminPassword_Parameters.Password, DbType.String, newPassword);

                database.AddOutParameter(dbCommand, StoreProcedures.dbo.usp_Update_SystemAdminPassword_Parameters.OutAdminId, DbType.Int64, int.MaxValue);
                database.AddOutParameter(dbCommand, StoreProcedures.dbo.usp_Update_SystemAdminPassword_Parameters.ErrorDescription, DbType.String, 500);

                database.ExecuteNonQuery(dbCommand);

                status.Data = database.GetParameterValue(dbCommand, StoreProcedures.dbo.usp_Update_SystemAdminPassword_Parameters.OutAdminId);
                status.Message = database.GetParameterValue(dbCommand, StoreProcedures.dbo.usp_Update_SystemAdminPassword_Parameters.ErrorDescription).ToString();
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
                status.Message = "Error while updating password";
                status.StatusType = StatusType.Error;
                return status;
            }
            finally
            {
                database = null;
            }
        }

        public override SystemAdminLoginIdentity AuthenticateSystemAdminLogonToken(string logonToken)
        {
            Database database;
            DbCommand dbCommand;
            try
            {
                DatabaseProviderFactory factory = new DatabaseProviderFactory();
                database = factory.Create(ConnectionStringName);
                dbCommand = database.GetStoredProcCommand(StoreProcedures.dbo.usp_Get_SystemAdminLogonDetail);

                database.AddInParameter(dbCommand, StoreProcedures.dbo.usp_Get_SystemAdminLogonDetail_Parameters.LogonToken, DbType.String, Chavan.Common.EncryptionHelper.Instance.Decrypt64(logonToken));
                SystemAdminLoginIdentity identity = null;
                using (IDataReader objReader = database.ExecuteReader(dbCommand))
                {
                    identity = CreateBasicAuthenticationIdentity(objReader);
                }
                return identity;
            }
            catch (Exception ex)
            {
                logger.LogEvent(ex.ToString(), LogLevel.Error);
                throw;
            }
            finally
            {
                database = null;
            }
        }

    }
}
