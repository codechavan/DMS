using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DMS.UI
{
    public class WebConstants
    {
        public const string SubmitLogonMethod = "/Home/SubmitLogin";

        #region Session Storage

        public const string SessionAuthorizationIdentifier = "AuthorizationHeader";
        public const string SessionTokenIdIdentifier = "TokenId";
        public const string SessionAllowCredentials = "AllowCredentials";
        #endregion

        #region Common
        private static string dmsAPIURL;
        public static string DMSAPIURL
        {
            get
            {
                if (string.IsNullOrEmpty(dmsAPIURL))
                {
                    dmsAPIURL = System.Configuration.ConfigurationManager.AppSettings["DMS.API.URL"];
                }
                return dmsAPIURL;
            }
        }

        #endregion

        #region API URL

        public const string SystemDropDownAPI = "api/System/GetSystemDropdown";
        public const string CreateDmsSystemAPI = "api/System/CreateDmsSystem";
        public const string LogonAPI = "api/UserAccount/Login";

        #endregion


        #region Header Name

        public const string HeaderLogin = "DMS Logon";
        //public const string HeaderLogin = "api/UserAccount/Login";

        #endregion
    }
}