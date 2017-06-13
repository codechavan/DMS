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
    public class SystemAdminBL : IDisposable
    {
        Logger logger = null;

        SystemAdminDAL dmsSys = null;
        string ConnectionStringName = string.Empty;

        SystemAdminDAL SystemAdminRepository
        {
            get
            {
                if (dmsSys == null)
                {
                    dmsSys = new SystemAdminSQL(ConnectionStringName);
                }
                return dmsSys;
            }
        }

        public SystemAdminBL(string connectionStringName)
        {
            this.ConnectionStringName = connectionStringName;
            logger = new Logger("DMS.BL.DmsUserBL");
        }

        public FunctionReturnStatus CreateUser(SystemAdmin admin)
        {
            try
            {
                if (admin == null)
                {
                    return new FunctionReturnStatus(StatusType.Error, "Invalid data");
                }
                if (string.IsNullOrEmpty(admin.UserName))
                {
                    return new FunctionReturnStatus(StatusType.Error, "username can not be empty");
                }
                if (string.IsNullOrEmpty(admin.FullName))
                {
                    return new FunctionReturnStatus(StatusType.Error, "FullName can not be empty");
                }
                if (string.IsNullOrEmpty(admin.Password))
                {
                    return new FunctionReturnStatus(StatusType.Error, "Password can not be empty");
                }
                if (string.IsNullOrEmpty(admin.EmailId))
                {
                    return new FunctionReturnStatus(StatusType.Error, "EmailId can not be empty");
                }
                if (admin.CreatedBy <= 0)
                {
                    return new FunctionReturnStatus(StatusType.Error, "CreatedBy can not be empty");
                }
                return SystemAdminRepository.CreateUpdateSystemAdmin(admin);
            }
            catch (Exception ex)
            {
                logger.LogEvent(ex.ToString(), LogLevel.Error);
                throw;
            }
        }

        public FunctionReturnStatus UpdateUser(SystemAdmin admin)
        {
            try
            {
                if (admin == null)
                {
                    return new FunctionReturnStatus(StatusType.Error, "Invalid data");
                }
                if (admin.AdminId <= 0)
                {
                    return new FunctionReturnStatus(StatusType.Error, "User Id can not be blank");
                }
                if (string.IsNullOrEmpty(admin.UserName))
                {
                    return new FunctionReturnStatus(StatusType.Error, "username can not be empty");
                }
                if (string.IsNullOrEmpty(admin.FullName))
                {
                    return new FunctionReturnStatus(StatusType.Error, "FullName can not be empty");
                }
                if (string.IsNullOrEmpty(admin.EmailId))
                {
                    return new FunctionReturnStatus(StatusType.Error, "EmailId can not be empty");
                }
                if (admin.ModifiedBy <= 0)
                {
                    return new FunctionReturnStatus(StatusType.Error, "ModifiedBy can not be empty");
                }
                return SystemAdminRepository.CreateUpdateSystemAdmin(admin);
            }
            catch (Exception ex)
            {
                logger.LogEvent(ex.ToString(), LogLevel.Error);
                throw;
            }
        }

        public SystemAdmin GetSystemAdmin(long adminId)
        {
            try
            {
                if (adminId <= 0)
                {
                    return null;
                }
                SystemAdminSearchParameter searchParameters = new SystemAdminSearchParameter();
                searchParameters.AdminId = adminId;
                var lstUsers = GetSystemAdmin(searchParameters);
                if (lstUsers != null && lstUsers.LstData != null)
                {
                    if (lstUsers.LstData.Count == 1)
                    {
                        return lstUsers.LstData[0];
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

        public SystemAdminSearchData GetSystemAdmin(SystemAdminSearchParameter searchParameters)
        {
            try
            {
                return SystemAdminRepository.GetSystemAdmin(searchParameters);
            }
            catch (Exception ex)
            {
                logger.LogEvent(ex.ToString(), LogLevel.Error);
                throw;
            }
        }

        public FunctionReturnStatus ChangePasword(long adminId, string oldpassword, string newpassword, long updatedByUser)
        {
            try
            {
                if (adminId <= 0)
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
                return SystemAdminRepository.ChangePasword(adminId, oldpassword, newpassword, updatedByUser);
            }
            catch (Exception ex)
            {
                logger.LogEvent(ex.ToString(), LogLevel.Error);
                throw;
            }
        }

        public FunctionReturnStatus UserLogin(string username, string password)
        {
            try
            {
                if (string.IsNullOrEmpty(username))
                {
                    return new FunctionReturnStatus(StatusType.Error, "username can not be empty");
                }
                if (string.IsNullOrEmpty(password))
                {
                    return new FunctionReturnStatus(StatusType.Error, "UserName can not be empty");
                }
                return SystemAdminRepository.Login(username, password);
            }
            catch (Exception ex)
            {
                logger.LogEvent(ex.ToString(), LogLevel.Error);
                throw;
            }
        }

        public SystemAdminLoginIdentity AuthenticateLogonToken(string logonToken)
        {
            try
            {
                if (string.IsNullOrEmpty(logonToken))
                {
                    throw new ArgumentNullException("logonToken");
                }

                return SystemAdminRepository.AuthenticateSystemAdminLogonToken(logonToken);
            }
            catch (Exception ex)
            {
                logger.LogEvent(ex.ToString(), LogLevel.Error);
                throw;
            }
        }

        public bool IsAuthenticateUser(string username, string password)
        {
            SQLScallerFunctions functions = new SQLScallerFunctions(ConnectionStringName);
            return functions.AuthenticateSystemAdmin(username, password);
        }

        public void Dispose()
        {
            logger = null;
            dmsSys = null;
        }
    }
}
