using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DMS.API
{
    public class WebConstants
    {
        #region Session Storage

        #endregion

        #region Common
        private static string _dMSConnectionStringName;
        public static string DMSConnectionStringName
        {
            get
            {
                if (string.IsNullOrEmpty(_dMSConnectionStringName))
                {
                    _dMSConnectionStringName = System.Configuration.ConfigurationManager.AppSettings["DMSConnectionStringName"];
                }
                return _dMSConnectionStringName;
            }
        }
        #endregion

    }
}