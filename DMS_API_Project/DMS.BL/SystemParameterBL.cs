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
    public class SystemParameterBL : IDisposable
    {
        Logger logger = null;

        SystemParameterDAL sysVal = null;

        string ConnectionStringName = String.Empty;

        SystemParameterDAL SystemParameterRepository
        {
            get
            {
                if (sysVal == null)
                {
                    sysVal = new SystemParameterSQL(ConnectionStringName);
                }
                return sysVal;
            }
        }

        public SystemParameterBL(string connectionStringName)
        {
            this.ConnectionStringName = connectionStringName;
            logger = new Logger("DMS.BL.SystemBL");
        }



        public FunctionReturnStatus UpdateSystemParameterValue(SystemParameterValue systemParamValue)
        {
            if (systemParamValue == null)
            {
                return new FunctionReturnStatus(StatusType.Error, "Invalid data");
            }
            if (systemParamValue.SystemId <= 0)
            {
                return new FunctionReturnStatus(StatusType.Error, "Invalid SystemId");
            }
            if (systemParamValue.ParameterId <= 0)
            {
                return new FunctionReturnStatus(StatusType.Error, "Invalid ParameterId");
            }
            if (systemParamValue.ModifiedBy <= 0)
            {
                return new FunctionReturnStatus(StatusType.Error, "Invalid ModifiedBy");
            }
            return SystemParameterRepository.UpdateSystemParameterValue(systemParamValue);
        }


        public SystemParameterValueSearchData GetSystemParameterValue(long systemId)
        {
            if (systemId <= 0)
            {
                return null;
            }
            SystemParameterSearchParameters searchParameters = new SystemParameterSearchParameters();
            searchParameters.SystemId = systemId;
            return GetSystemParameterValues(searchParameters);
        }

        public SystemParameterValueSearchData GetSystemParameterValues(SystemParameterSearchParameters searchParameters)
        {
            try
            {
                return SystemParameterRepository.GetSystemParameterValue(searchParameters);
            }
            catch (Exception ex)
            {
                logger.LogEvent(ex.ToString(), LogLevel.Error);
                throw;
            }
        }


        public SystemParameter GetSystemParameter()
        {
            try
            {
                IList<SystemParameter> lstSystemParams = GetSystemParameter(0);
                if (lstSystemParams != null)
                {
                    if (lstSystemParams.Count == 1)
                    {
                        return lstSystemParams[0];
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

        public IList<SystemParameter> GetSystemParameter(long systemParameterId)
        {
            try
            {
                return SystemParameterRepository.GetSystemParameter(systemParameterId);
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
            sysVal = null;
        }
    }
}
