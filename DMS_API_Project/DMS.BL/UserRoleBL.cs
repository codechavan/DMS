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
    public class UserRoleBL : IDisposable
    {
        Logger logger = null;

        UserRoleDAL urRole = null;

        string ConnectionStringName = String.Empty;

        UserRoleDAL UserRoleRepository
        {
            get
            {
                if (urRole == null)
                {
                    urRole = new UserRoleSQL(ConnectionStringName);
                }
                return urRole;
            }
        }

        public UserRoleBL(string connectionStringName)
        {
            this.ConnectionStringName = connectionStringName;
            logger = new Logger("DMS.BL.SystemBL");
        }


        public FunctionReturnStatus CreateUserRole(DmsUserRole userRole)
        {
            if (userRole == null)
            {
                return new FunctionReturnStatus(StatusType.Error, "Invalid data");
            }
            if (userRole.SystemId <= 0)
            {
                return new FunctionReturnStatus(StatusType.Error, "Invalid system provided");
            }
            if (userRole.RoleId > 0)
            {
                return new FunctionReturnStatus(StatusType.Error, "Invalid user role id provided");
            }
            if (string.IsNullOrEmpty(userRole.RoleName))
            {
                return new FunctionReturnStatus(StatusType.Error, "User role name can not be blank");
            }
            if (userRole.CreatedBy <= 0)
            {
                return new FunctionReturnStatus(StatusType.Error, "Invalid data for created by user id");
            }
            return UserRoleRepository.CreateUpdateUserRole(userRole);
        }

        public FunctionReturnStatus UpdateUserRole(DmsUserRole userRole)
        {
            if (userRole == null)
            {
                return new FunctionReturnStatus(StatusType.Error, "Invalid data");
            }
            if (userRole.SystemId <= 0)
            {
                return new FunctionReturnStatus(StatusType.Error, "Invalid system provided");
            }
            if (userRole.RoleId <= 0)
            {
                return new FunctionReturnStatus(StatusType.Error, "Invalid user role id provided");
            }
            if (string.IsNullOrEmpty(userRole.RoleName))
            {
                return new FunctionReturnStatus(StatusType.Error, "User role name can not be blank");
            }
            if (userRole.ModifiedBy <= 0)
            {
                return new FunctionReturnStatus(StatusType.Error, "Invalid data for created by user id");
            }
            return UserRoleRepository.CreateUpdateUserRole(userRole);
        }

        public DmsUserRole GetUserRole(long userRoleId)
        {
            if (userRoleId <= 0)
            {
                return null;
            }
            DmsUserRoleSearchParameter searchParameters = new DmsUserRoleSearchParameter();
            searchParameters.RoleId = userRoleId;
            var lstIserRoles = GetUserRole(searchParameters, null);
            if (lstIserRoles != null && lstIserRoles.LstData != null)
            {
                if (lstIserRoles.LstData.Count == 1)
                {
                    return lstIserRoles.LstData[0];
                }
            }
            return null;
        }

        public DmsUserRoleSearchData GetUserRole(DmsUserRoleSearchParameter searchParameters, PagingDetails pageDetail)
        {
            try
            {
                return UserRoleRepository.GetUserRole(searchParameters, pageDetail);
            }
            catch (Exception ex)
            {
                logger.LogEvent(ex.ToString(), LogLevel.Error);
                throw;
            }
        }



        public void Dispose()
        {
            logger = null;
            urRole = null;
        }
    }
}
