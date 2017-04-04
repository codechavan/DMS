using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.DatabaseConstants
{
    public class SQLSafety
    {
        public static string GetSQLSafeString(string sqlString)
        {
            if (string.IsNullOrEmpty(sqlString))
            {
                return sqlString;
            }
            return sqlString.Replace("'", "''");
        }
    }
}
