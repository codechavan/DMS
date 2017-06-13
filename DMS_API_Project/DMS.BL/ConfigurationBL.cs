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
    public class ConfigurationBL : IDisposable
    {
        Logger logger = null;

        ConfigurationDAL sysVal = null;

        string ConnectionStringName = String.Empty;

        ConfigurationDAL ConfigurationRepository
        {
            get
            {
                if (sysVal == null)
                {
                    sysVal = new ConfigurationSQL(ConnectionStringName);
                }
                return sysVal;
            }
        }

        public ConfigurationBL(string connectionStringName)
        {
            this.ConnectionStringName = connectionStringName;
            logger = new Logger("DMS.BL.ConfigurationBL");
        }


        public FunctionReturnStatus UpdateConfigurationValue(SysConfiguration configuration)
        {
            if (configuration == null)
            {
                return new FunctionReturnStatus(StatusType.Error, "Invalid data");
            }
            if (configuration.ModifiedBy <= 0)
            {
                return new FunctionReturnStatus(StatusType.Error, "Invalid ModifiedBy");
            }
            if (string.IsNullOrEmpty(configuration.ConfigurationCode))
            {
                return new FunctionReturnStatus(StatusType.Error, "ConfigurationCode invalid");
            }
            if (string.IsNullOrEmpty(configuration.Value))
            {
                return new FunctionReturnStatus(StatusType.Error, "Value invalid");
            }
            if (string.IsNullOrEmpty(configuration.Remarks))
            {
                return new FunctionReturnStatus(StatusType.Error, "Remarks invalid");
            }
            return ConfigurationRepository.UpdateConfigurationValue(configuration);
        }

        public SysConfiguration GetConfiguration(string configurationCode)
        {
            try
            {
                ConfigurationSearchParameter searchParameters = new ConfigurationSearchParameter();
                searchParameters.ConfigurationCode = configurationCode;
                var lstConfiguration = GetConfiguration(searchParameters);
                if (lstConfiguration != null && lstConfiguration.LstData != null)
                {
                    if (lstConfiguration.LstData.Count == 1)
                    {
                        return lstConfiguration.LstData[0];
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

        public SysConfigurationSearchData GetConfiguration(ConfigurationSearchParameter searchParameters)
        {
            try
            {
                return ConfigurationRepository.GetConfigurations(searchParameters);
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
