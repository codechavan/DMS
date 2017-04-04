using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DMS.Model;
using Chavan.Logger;
using DMS.Repository.SQL;
using DMS.Repository.DAL;

namespace DMS.BL
{
    public class UserBL : IDisposable
    {
        Logger logger = null;

        UsersDAL dmsSys = null;
        string ConnectionStringName = string.Empty;

        UsersDAL UserRepository
        {
            get
            {
                if (dmsSys == null)
                {
                    dmsSys = new UsersSQL(ConnectionStringName);
                }
                return dmsSys;
            }
        }

        public UserBL(string connectionStringName)
        {
            this.ConnectionStringName = connectionStringName;
            logger = new Logger("DMS.BL.DmsUserBL");
        }



        public FunctionReturnStatus CreateUser(DmsUser user)
        {
            try
            {
                if (user == null)
                {
                    return new FunctionReturnStatus(StatusType.Error, "Invalid data");
                }
                if (user.SystemId == 0)
                {
                    return new FunctionReturnStatus(StatusType.Error, "System Id can not be blank");
                }
                if (string.IsNullOrEmpty(user.UserName))
                {
                    return new FunctionReturnStatus(StatusType.Error, "username can not be empty");
                }
                if (string.IsNullOrEmpty(user.FullName))
                {
                    return new FunctionReturnStatus(StatusType.Error, "FullName can not be empty");
                }
                if (string.IsNullOrEmpty(user.Password))
                {
                    return new FunctionReturnStatus(StatusType.Error, "Password can not be empty");
                }
                if (user.CreatedBy <= 0)
                {
                    return new FunctionReturnStatus(StatusType.Error, "CreatedBy can not be empty");
                }
                if (user.RoleID <= 0)
                {
                    return new FunctionReturnStatus(StatusType.Error, "RoleID can not be empty");
                }
                return UserRepository.CreateUpdateUser(user);
            }
            catch (Exception ex)
            {
                logger.LogEvent(ex.ToString(), LogLevel.Error);
                throw;
            }
        }

        public FunctionReturnStatus UpdateUser(DmsUser user)
        {
            try
            {
                if (user == null)
                {
                    return new FunctionReturnStatus(StatusType.Error, "Invalid data");
                }
                if (user.SystemId == 0)
                {
                    return new FunctionReturnStatus(StatusType.Error, "System Id can not be blank");
                }
                if (user.UserId == 0)
                {
                    return new FunctionReturnStatus(StatusType.Error, "User Id can not be blank");
                }
                if (string.IsNullOrEmpty(user.UserName))
                {
                    return new FunctionReturnStatus(StatusType.Error, "username can not be empty");
                }
                if (string.IsNullOrEmpty(user.FullName))
                {
                    return new FunctionReturnStatus(StatusType.Error, "FullName can not be empty");
                }
                if (user.ModifiedBy <= 0)
                {
                    return new FunctionReturnStatus(StatusType.Error, "ModifiedBy can not be empty");
                }
                if (user.RoleID <= 0)
                {
                    return new FunctionReturnStatus(StatusType.Error, "RoleID can not be empty");
                }
                return UserRepository.CreateUpdateUser(user);
            }
            catch (Exception ex)
            {
                logger.LogEvent(ex.ToString(), LogLevel.Error);
                throw;
            }
        }

        public DmsUser GetUser(long userId)
        {
            try
            {
                if (userId <= 0)
                {
                    return null;
                }
                DmsUserSearchParameter searchParameters = new DmsUserSearchParameter();
                searchParameters.UserId = userId;
                DmsUserSearchData lstData = GetUser(searchParameters, null);
                if (lstData != null && lstData.LstData != null)
                {
                    if (lstData.LstData.Count == 1)
                    {
                        return lstData.LstData[0];
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                logger.LogEvent(ex.ToString(), LogLevel.Error);
                throw;
            }
        }

        public DmsUserSearchData GetUser(DmsUserSearchParameter searchParameters, PagingDetails pageDetail)
        {
            try
            {
                return UserRepository.GetUser(searchParameters, pageDetail);
            }
            catch (Exception ex)
            {
                logger.LogEvent(ex.ToString(), LogLevel.Error);
                throw;
            }
        }

        public FunctionReturnStatus ChangePasword(long systemId, long userId, string oldpassword, string newpassword, long updatedByUser)
        {
            try
            {
                if (systemId <= 0)
                {
                    return new FunctionReturnStatus(StatusType.Error, "Invalid data");
                }
                if (userId <= 0)
                {
                    return new FunctionReturnStatus(StatusType.Error, "userId can not be blank");
                }
                if (updatedByUser <= 0)
                {
                    return new FunctionReturnStatus(StatusType.Error, "updated by can not be blank");
                }
                if (string.IsNullOrEmpty(oldpassword))
                {
                    return new FunctionReturnStatus(StatusType.Error, "oldpassword can not be empty");
                }
                if (string.IsNullOrEmpty(newpassword))
                {
                    return new FunctionReturnStatus(StatusType.Error, "newpassword can not be empty");
                }
                return UserRepository.ChangePasword(systemId, userId, oldpassword, newpassword, updatedByUser);
            }
            catch (Exception ex)
            {
                logger.LogEvent(ex.ToString(), LogLevel.Error);
                throw;
            }
        }

        public FunctionReturnStatus UnlockUser(long systemId, long userId, long updatedByUser)
        {
            try
            {
                if (systemId <= 0)
                {
                    return new FunctionReturnStatus(StatusType.Error, "Invalid data");
                }
                if (userId <= 0)
                {
                    return new FunctionReturnStatus(StatusType.Error, "userId can not be blank");
                }
                if (updatedByUser <= 0)
                {
                    return new FunctionReturnStatus(StatusType.Error, "updated by can not be blank");
                }
                return UserRepository.UnlockUser(systemId, userId, updatedByUser);
            }
            catch (Exception ex)
            {
                logger.LogEvent(ex.ToString(), LogLevel.Error);
                throw;
            }
        }



        public FunctionReturnStatus UserLogin(long systemId, string username, string password)
        {
            try
            {
                if (systemId == 0)
                {
                    return new FunctionReturnStatus(StatusType.Error, "Invalid data");
                }
                if (string.IsNullOrEmpty(username))
                {
                    return new FunctionReturnStatus(StatusType.Error, "username can not be empty");
                }
                if (string.IsNullOrEmpty(password))
                {
                    return new FunctionReturnStatus(StatusType.Error, "UserName can not be empty");
                }
                return UserRepository.Login(systemId, username, password);
            }
            catch (Exception ex)
            {
                logger.LogEvent(ex.ToString(), LogLevel.Error);
                throw;
            }
        }

        public BasicAuthenticationIdentity AuthenticateLogonToken(string logonToken)
        {
            try
            {
                if (string.IsNullOrEmpty(logonToken))
                {
                    throw new ArgumentNullException("logonToken");
                }

                return UserRepository.AuthenticateLogonToken(logonToken);
            }
            catch (Exception ex)
            {
                logger.LogEvent(ex.ToString(), LogLevel.Error);
                throw;
            }
        }

        public bool IsAuthenticateUser(long systemId, string username, string password)
        {
            SQLScallerFunctions functions = new SQLScallerFunctions(ConnectionStringName);
            return functions.AuthenticateUser(systemId, username, password);
        }



        public void Dispose()
        {
            logger = null;
            dmsSys = null;
        }
    }
}
