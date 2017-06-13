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
    public class SystemBL : IDisposable
    {
        Logger logger = null;

        SystemsSQL dmsSys = null;

        string ConnectionStringName = String.Empty;

        SystemsDAL SystemRepository
        {
            get
            {
                if (dmsSys == null)
                {
                    dmsSys = new SystemsSQL(ConnectionStringName);
                }
                return dmsSys;
            }
        }

        public SystemBL(string connectionStringName)
        {
            this.ConnectionStringName = connectionStringName;
            logger = new Logger("DMS.BL.SystemBL");
        }

        public FunctionReturnStatus AddDmsSystem(DmsSystem system, DmsUser dmsUser, DmsUserRole userRole)
        {
            try
            {
                if (system == null)
                {
                    return new FunctionReturnStatus(StatusType.Error, "Invalid data");
                }
                if (dmsUser == null)
                {
                    return new FunctionReturnStatus(StatusType.Error, "Invalid data");
                }
                if (userRole == null)
                {
                    return new FunctionReturnStatus(StatusType.Error, "Invalid data");
                }

                if (string.IsNullOrEmpty(system.SystemName))
                {
                    return new FunctionReturnStatus(StatusType.Error, "systemName can not be empty");
                }
                if (string.IsNullOrEmpty(dmsUser.UserName))
                {
                    return new FunctionReturnStatus(StatusType.Error, "UserName can not be empty");
                }
                if (string.IsNullOrEmpty(dmsUser.FullName))
                {
                    return new FunctionReturnStatus(StatusType.Error, "UserFullName can not be empty");
                }
                if (string.IsNullOrEmpty(dmsUser.Password))
                {
                    return new FunctionReturnStatus(StatusType.Error, "UserPassword can not be empty");
                }
                if (string.IsNullOrEmpty(userRole.RoleName))
                {
                    return new FunctionReturnStatus(StatusType.Error, "UserRoleName can not be empty");
                }

                return SystemRepository.CreateDmsSystem(system, dmsUser, userRole);
            }
            catch (Exception ex)
            {
                logger.LogEvent(ex.ToString(), LogLevel.Error);
                throw;
            }
        }

        public FunctionReturnStatus UpdateDmsSystem(DmsSystem system)
        {
            try
            {
                if (system == null)
                {
                    return new FunctionReturnStatus(StatusType.Error, "Invalid data");
                }
                if (string.IsNullOrEmpty(system.SystemName))
                {
                    return new FunctionReturnStatus(StatusType.Error, "systemName can not be empty");
                }
                if (system.ModifiedBy <= 0)
                {
                    return new FunctionReturnStatus(StatusType.Error, "System Modified by user id can not be empty");
                }
                return SystemRepository.UpdateDmsSystem(system);
            }
            catch (Exception ex)
            {
                logger.LogEvent(ex.ToString(), LogLevel.Error);
                throw;
            }
        }

        public DmsSystem GetSystem(long systemId)
        {
            if (systemId <= 0)
            {
                return null;
            }
            DmsSystemSearchParameters searchParameters = new DmsSystemSearchParameters();
            searchParameters.SystemId = systemId;
            var lstSys = GetSystem(searchParameters);
            if (lstSys != null && lstSys.LstData != null)
            {
                if (lstSys.LstData.Count == 1)
                {
                    return lstSys.LstData[0];
                }
            }
            return null;
        }

        public DmsSystemSearchData GetSystem(DmsSystemSearchParameters searchParameters)
        {
            try
            {
                return SystemRepository.GetSystem(searchParameters);
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
            dmsSys = null;
        }
    }
}
