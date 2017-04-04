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
    public class UsersSQL : UsersDAL
    {
        Logger logger = null;

        public UsersSQL(string connectionStringName)
            : base(connectionStringName)
        {
            logger = new Logger("DMS.Repository.SQL.UsersSQL");
        }

        public override FunctionReturnStatus CreateUpdateUser(DmsUser user)
        {
            Database database;
            DbCommand dbCommand;
            FunctionReturnStatus status = new FunctionReturnStatus();
            try
            {
                DatabaseProviderFactory factory = new DatabaseProviderFactory();
                database = factory.Create(ConnectionStringName);
                dbCommand = database.GetStoredProcCommand(StoreProcedures.dbo.usp_create_update_user);

                database.AddInParameter(dbCommand, StoreProcedures.dbo.usp_create_update_user_Parameters.SystemId, DbType.Int64, user.SystemId);
                database.AddInParameter(dbCommand, StoreProcedures.dbo.usp_create_update_user_Parameters.UserId, DbType.Int64, user.UserId);
                database.AddInParameter(dbCommand, StoreProcedures.dbo.usp_create_update_user_Parameters.UserName, DbType.String, user.UserName);
                database.AddInParameter(dbCommand, StoreProcedures.dbo.usp_create_update_user_Parameters.UserFullName, DbType.String, user.FullName);
                database.AddInParameter(dbCommand, StoreProcedures.dbo.usp_create_update_user_Parameters.UserEmailId, DbType.String, user.EmailId);
                database.AddInParameter(dbCommand, StoreProcedures.dbo.usp_create_update_user_Parameters.UserCreatedBy, DbType.Int64, (user.ModifiedBy <= 0 ? user.CreatedBy : user.ModifiedBy));
                database.AddInParameter(dbCommand, StoreProcedures.dbo.usp_create_update_user_Parameters.UserIsActive, DbType.Boolean, user.IsActive);
                database.AddInParameter(dbCommand, StoreProcedures.dbo.usp_create_update_user_Parameters.UserIsAdmin, DbType.Boolean, user.IsAdmin);
                database.AddInParameter(dbCommand, StoreProcedures.dbo.usp_create_update_user_Parameters.UserPassword, DbType.String, user.Password);
                database.AddInParameter(dbCommand, StoreProcedures.dbo.usp_create_update_user_Parameters.UserRoleId, DbType.Int64, user.RoleID);

                database.AddOutParameter(dbCommand, StoreProcedures.dbo.usp_create_update_user_Parameters.OutUserId, DbType.Int64, int.MaxValue);
                database.AddOutParameter(dbCommand, StoreProcedures.dbo.usp_create_update_user_Parameters.ErrorDescription, DbType.String, 500);

                database.ExecuteNonQuery(dbCommand);

                status.Data = database.GetParameterValue(dbCommand, StoreProcedures.dbo.usp_create_update_user_Parameters.OutUserId);
                status.Message = database.GetParameterValue(dbCommand, StoreProcedures.dbo.usp_create_update_user_Parameters.ErrorDescription).ToString();
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

        public override FunctionReturnStatus Login(long systemId, string username, string password)
        {
            Database database;
            DbCommand dbCommand;
            FunctionReturnStatus status = new FunctionReturnStatus();
            try
            {
                DatabaseProviderFactory factory = new DatabaseProviderFactory();
                database = factory.Create(ConnectionStringName);
                dbCommand = database.GetStoredProcCommand(StoreProcedures.dbo.usp_Get_UserLogin);

                database.AddInParameter(dbCommand, StoreProcedures.dbo.usp_Get_UserLogin_Parameters.SystemId, DbType.Int64, systemId);
                database.AddInParameter(dbCommand, StoreProcedures.dbo.usp_Get_UserLogin_Parameters.UserName, DbType.String, username);
                database.AddInParameter(dbCommand, StoreProcedures.dbo.usp_Get_UserLogin_Parameters.UserPassword, DbType.String, password);

                database.AddOutParameter(dbCommand, StoreProcedures.dbo.usp_Get_UserLogin_Parameters.OutUserId, DbType.Int64, int.MaxValue);
                database.AddOutParameter(dbCommand, StoreProcedures.dbo.usp_Get_UserLogin_Parameters.ErrorDescription, DbType.String, 500);

                DmsUser user = null;
                using (IDataReader objReader = database.ExecuteReader(dbCommand))
                {
                    var data = CreateUserObject(objReader);
                    if (data != null)
                    {
                        if (data.Count == 1)
                        {
                            user = data[0];
                        }
                    }
                }

                status.Data = database.GetParameterValue(dbCommand, StoreProcedures.dbo.usp_Get_UserLogin_Parameters.OutUserId);
                status.Message = database.GetParameterValue(dbCommand, StoreProcedures.dbo.usp_Get_UserLogin_Parameters.ErrorDescription).ToString();
                if (Convert.ToInt64(status.Data) > 0 && user != null)
                {
                    status.StatusType = StatusType.Success;
                    user.LogonToken = GenerateLogonToken(systemId, user.UserId);
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

        public override BasicAuthenticationIdentity AuthenticateLogonToken(string logonToken)
        {
            Database database;
            DbCommand dbCommand;
            try
            {
                DatabaseProviderFactory factory = new DatabaseProviderFactory();
                database = factory.Create(ConnectionStringName);
                dbCommand = database.GetStoredProcCommand(StoreProcedures.dbo.usp_Get_LogonDetail);

                database.AddInParameter(dbCommand, StoreProcedures.dbo.usp_Get_LogonDetail_Parameters.LogonToken, DbType.String, Chavan.Common.EncryptionHelper.Instance.Decrypt64(logonToken));
                BasicAuthenticationIdentity identity = null;
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

        public override DmsUserSearchData GetUser(DmsUserSearchParameter searchParameters, PagingDetails pageDetail)
        {
            Database database;
            DbCommand dbCommand;
            try
            {
                if (searchParameters == null)
                {
                    searchParameters = new DmsUserSearchParameter();
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
                database.AddInParameter(dbCommand, StoreProcedures.dbo.usp_Get_Users_Parameters.WhereCondition, DbType.String, GetUserSearchParameterString(searchParameters));
                database.AddInParameter(dbCommand, StoreProcedures.dbo.usp_Get_Users_Parameters.OrderBy, DbType.String, GetUserOrderBy(pageDetail.OrderBy));

                DmsUserSearchData data = new DmsUserSearchData();
                data.pageDetail = pageDetail;
                List<DmsUser> lstUsers = null;
                using (IDataReader objReader = database.ExecuteReader(dbCommand))
                {
                    lstUsers = CreateUserObject(objReader);
                    if (objReader.NextResult())
                    {
                        if (objReader.Read())
                        {
                            data.RecordCount = objReader[StoreProcedures.dbo.usp_Get_Users_Parameters.Column_RecordCount] != DBNull.Value ? Convert.ToInt64(objReader[StoreProcedures.dbo.usp_Get_Users_Parameters.Column_RecordCount]) : 0;
                        }
                    }
                }
                data.LstData = lstUsers;

                return data;
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

        public override FunctionReturnStatus ChangePasword(long systemId, long userId, string oldpassword, string newPassword, long updatedByUser)
        {
            Database database;
            DbCommand dbCommand;
            FunctionReturnStatus status = new FunctionReturnStatus();
            try
            {
                DatabaseProviderFactory factory = new DatabaseProviderFactory();
                database = factory.Create(ConnectionStringName);
                dbCommand = database.GetStoredProcCommand(StoreProcedures.dbo.usp_Update_UserPassword);

                database.AddInParameter(dbCommand, StoreProcedures.dbo.usp_Update_UserPassword_Parameters.SystemId, DbType.Int64, systemId);
                database.AddInParameter(dbCommand, StoreProcedures.dbo.usp_Update_UserPassword_Parameters.UserId, DbType.Int64, userId);
                database.AddInParameter(dbCommand, StoreProcedures.dbo.usp_Update_UserPassword_Parameters.UserLastPasswordChangedBy, DbType.Int64, updatedByUser);
                database.AddInParameter(dbCommand, StoreProcedures.dbo.usp_Update_UserPassword_Parameters.UserPassword, DbType.String, newPassword);

                database.AddOutParameter(dbCommand, StoreProcedures.dbo.usp_Update_UserPassword_Parameters.OutUserId, DbType.Int64, int.MaxValue);
                database.AddOutParameter(dbCommand, StoreProcedures.dbo.usp_Update_UserPassword_Parameters.ErrorDescription, DbType.String, 500);

                database.ExecuteNonQuery(dbCommand);

                status.Data = database.GetParameterValue(dbCommand, StoreProcedures.dbo.usp_Update_UserPassword_Parameters.OutUserId);
                status.Message = database.GetParameterValue(dbCommand, StoreProcedures.dbo.usp_Update_UserPassword_Parameters.ErrorDescription).ToString();
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

        public override FunctionReturnStatus UnlockUser(long systemId, long userId, long updatedByUser)
        {
            Database database;
            DbCommand dbCommand;
            FunctionReturnStatus status = new FunctionReturnStatus();
            try
            {
                DatabaseProviderFactory factory = new DatabaseProviderFactory();
                database = factory.Create(ConnectionStringName);
                dbCommand = database.GetStoredProcCommand(StoreProcedures.dbo.usp_Update_UserUnlock);

                database.AddInParameter(dbCommand, StoreProcedures.dbo.usp_Update_UserUnlock_Parameters.UserId, DbType.Int64, userId);
                database.AddInParameter(dbCommand, StoreProcedures.dbo.usp_Update_UserUnlock_Parameters.UserLastUnblockBy, DbType.Int64, updatedByUser);

                database.AddOutParameter(dbCommand, StoreProcedures.dbo.usp_Update_UserUnlock_Parameters.OutUserId, DbType.Int64, int.MaxValue);
                database.AddOutParameter(dbCommand, StoreProcedures.dbo.usp_Update_UserUnlock_Parameters.ErrorDescription, DbType.String, 500);

                database.ExecuteNonQuery(dbCommand);

                status.Data = database.GetParameterValue(dbCommand, StoreProcedures.dbo.usp_Update_UserPassword_Parameters.OutUserId);
                status.Message = database.GetParameterValue(dbCommand, StoreProcedures.dbo.usp_Update_UserPassword_Parameters.ErrorDescription).ToString();
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
                status.Message = "Error while unlock user";
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
