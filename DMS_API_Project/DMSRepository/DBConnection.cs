using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.IO;

namespace DMS.Repository
{
    public class DBConnection
    {

        #region Constant Values
        protected const string prm_Return = "@Return";
        protected const string prm_OutValue = "@OutValue";
        #endregion

        #region Static Varibale
        public static string ConnectionString = GetAppSetting("DMS_DB_Conn");
        #endregion

        #region Private Variables
        private SqlParameter returnSqlParam = null;
        private SqlConnection TempSqlCon = null;
        private string ConnString
        {
            get
            {
                return ConnectionString;
            }
        }
        #endregion

        #region Protected Properties
        protected SqlParameter SQLReturnParameter
        {
            get
            {
                if (returnSqlParam == null)
                {

                    returnSqlParam = new SqlParameter(prm_Return, SqlDbType.BigInt);
                    returnSqlParam.Direction = ParameterDirection.ReturnValue;

                }
                return returnSqlParam;
            }
        }
        protected SqlConnection SqlConn
        {
            get
            {
                if (TempSqlCon == null)
                {
                    TempSqlCon = new SqlConnection(ConnString);
                }
                if (TempSqlCon.State == ConnectionState.Closed || TempSqlCon.State == ConnectionState.Broken)
                {
                    TempSqlCon.Open();
                }
                return TempSqlCon;
            }
        }
        #endregion

        #region Constructor
        public DBConnection()
        {

        }
        #endregion

        #region Public Method
        public static string GetAppSetting(string key)
        {
            string value = ConfigurationManager.AppSettings[key];
            if (!string.IsNullOrEmpty(value))
            {
                return value;
            }
            return string.Empty;
        }
        #endregion

        #region Protected Method

        protected void Dispose()
        {
            if (TempSqlCon != null)
            {
                if (TempSqlCon.State == ConnectionState.Open)
                {
                    TempSqlCon.Close();
                }
            }
            returnSqlParam = null;
        }

        protected DataTable GenerateTypeNumberList(double[] numbers, string tableName, string columnName)
        {
            DataTable dt = new DataTable(tableName);
            dt.Columns.Add(columnName);
            if (numbers != null)
            {
                foreach (double d in numbers)
                {
                    dt.Rows.Add(new Object[] { d });
                }
            }
            return dt;
        }
        protected DataTable GenerateTypeNumberList(long[] numbers, string tableName, string columnName)
        {
            DataTable dt = new DataTable(tableName);
            dt.Columns.Add(new DataColumn(columnName, Type.GetType("System.Decimal")));
            if (numbers != null)
            {
                foreach (long d in numbers)
                {
                    dt.Rows.Add(new Object[] { d });
                }
            }
            return dt;
        }
        protected SqlParameter Param_TypeNumberList(string parameterName, double[] numbers, string tableName, string columnName)
        {
            SqlParameter prmN = new SqlParameter(parameterName, GenerateTypeNumberList(numbers, tableName, columnName));
            prmN.SqlDbType = SqlDbType.Structured;
            return prmN;
        }
        protected SqlParameter Param_TypeNumberList(string parameterName, long[] numbers, string tableName, string columnName)
        {
            SqlParameter prmN = new SqlParameter(parameterName, GenerateTypeNumberList(numbers, tableName, columnName));
            prmN.SqlDbType = SqlDbType.Structured;
            return prmN;
        }

        #endregion

    }
}
