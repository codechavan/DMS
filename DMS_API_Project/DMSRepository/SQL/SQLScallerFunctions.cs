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
    public class SQLScallerFunctions
    {
        Logger logger = null;
        protected string ConnectionStringName;

        public SQLScallerFunctions(string connectionStringName)
        {
            this.ConnectionStringName = connectionStringName;
            logger = new Logger("DMS.Repository.SQL.SQLScallerFunctions");
        }

        private static string EncodeBase64(string text)
        {
            if (text == null)
            {
                return null;
            }
            byte[] textAsBytes = System.Text.Encoding.UTF8.GetBytes(text);
            return System.Convert.ToBase64String(textAsBytes);
        }

        public string GenerateLogonToken(long systemId, long userId)
        {
            Database database;
            DbCommand dbCommand;
            string logonToken = String.Empty;
            try
            {
                DatabaseProviderFactory factory = new DatabaseProviderFactory();
                database = factory.Create(ConnectionStringName);
                dbCommand = database.GetSqlStringCommand("SELECT [dbo].[ufn_GenerateLogonToken](@SystemId, @UserId)");

                database.AddInParameter(dbCommand, "@SystemId", DbType.Int64, systemId);
                database.AddInParameter(dbCommand, "@UserId", DbType.Int64, userId);

                logonToken = database.ExecuteScalar(dbCommand).ToString();
                logonToken = Chavan.Common.EncryptionHelper.Instance.Encrypt64(logonToken);
                logonToken = EncodeBase64(logonToken);
                return logonToken;
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

        public string GetConfigurationValue(string configurationCode)
        {
            Database database;
            DbCommand dbCommand;
            string configurationValue = String.Empty;
            try
            {
                DatabaseProviderFactory factory = new DatabaseProviderFactory();
                database = factory.Create(ConnectionStringName);
                dbCommand = database.GetSqlStringCommand("[dbo].[ufn_GetConfigurationValue](@ConfigurationCode)");

                database.AddInParameter(dbCommand, "@ConfigurationCode", DbType.String, configurationCode);

                configurationValue = database.ExecuteScalar(dbCommand).ToString();
                return configurationValue;
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
        
        public string GetSystemParameterValue(long systemId, string systemParameterName)
        {
            Database database;
            DbCommand dbCommand;
            string systemParameterValue = String.Empty;
            try
            {
                DatabaseProviderFactory factory = new DatabaseProviderFactory();
                database = factory.Create(ConnectionStringName);
                dbCommand = database.GetSqlStringCommand("[dbo].[ufn_GetSystemParameterValue](@SystemID, @SystemParameterName)");

                database.AddInParameter(dbCommand, "@ConfigurationCode", DbType.Int64, systemId);
                database.AddInParameter(dbCommand, "@SystemParameterName", DbType.String, systemParameterName);

                systemParameterValue = database.ExecuteScalar(dbCommand).ToString();
                return systemParameterValue;
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

        public bool AuthenticateUser(long systemId, string username, string password)
        {
            Database database;
            DbCommand dbCommand;
            bool isValid = false;
            try
            {
                DatabaseProviderFactory factory = new DatabaseProviderFactory();
                database = factory.Create(ConnectionStringName);
                dbCommand = database.GetSqlStringCommand("SELECT [dbo].[ufn_AuthenticateUser](@SystemId, @UserName, @Password)");

                database.AddInParameter(dbCommand, "@SystemId", DbType.Int64, systemId);
                database.AddInParameter(dbCommand, "@UserName", DbType.String, username);
                database.AddInParameter(dbCommand, "@Password", DbType.String, password);

                isValid = Convert.ToBoolean( database.ExecuteScalar(dbCommand));
                return isValid;
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

        public string GenerateSystemAdminLogonToken(long adminId)
        {
            Database database;
            DbCommand dbCommand;
            string logonToken = String.Empty;
            try
            {
                DatabaseProviderFactory factory = new DatabaseProviderFactory();
                database = factory.Create(ConnectionStringName);
                dbCommand = database.GetSqlStringCommand("SELECT [dbo].[ufn_GenerateSystemAdminLogonToken](@AdminId)");

                database.AddInParameter(dbCommand, "@AdminId", DbType.Int64, adminId);

                logonToken = database.ExecuteScalar(dbCommand).ToString();
                logonToken = Chavan.Common.EncryptionHelper.Instance.Encrypt64(logonToken);
                logonToken = EncodeBase64(logonToken);
                return logonToken;
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

        public bool AuthenticateSystemAdmin(string username, string password)
        {
            Database database;
            DbCommand dbCommand;
            bool isValid = false;
            try
            {
                DatabaseProviderFactory factory = new DatabaseProviderFactory();
                database = factory.Create(ConnectionStringName);
                dbCommand = database.GetSqlStringCommand("SELECT [dbo].[ufn_AuthenticateSystemAdmin](@UserName, @Password)");

                database.AddInParameter(dbCommand, "@UserName", DbType.String, username);
                database.AddInParameter(dbCommand, "@Password", DbType.String, password);

                isValid = Convert.ToBoolean(database.ExecuteScalar(dbCommand));
                return isValid;
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
