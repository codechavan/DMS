using DMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DMS.UI
{
    public class SessionHelper
    {
        //Logger logger = null;

        public SessionHelper()
        {
            //logger = new Logger("DMS.API.HelperClasses.SessionHelper");
        }

        private const string SelectedFolderIdSessionName = "DMS.UI.SelectedFolderId";
        public static long SelectedFolderId
        {
            get
            {
                long folderId = 0;
                if (HttpContext.Current.Session[SelectedFolderIdSessionName] != null)
                {
                    long.TryParse(HttpContext.Current.Session[SelectedFolderIdSessionName].ToString(), out folderId);
                }
                return folderId;
            }
            set
            {
                HttpContext.Current.Session[SelectedFolderIdSessionName] = value;
            }
        }

        #region User Session

        private const string UserSessionName = "DMS.UI.UserSession";

        private const string UserLogonTokenSessionName = "DMS.UI.UserLogonTokenSession";

        public static DmsUser LogonUser
        {
            get
            {
                if (HttpContext.Current.Session[UserSessionName] == null)
                {
                    return null;
                }
                return (DmsUser)HttpContext.Current.Session[UserSessionName];
            }
        }

        public static string LogonUserToken
        {
            get
            {
                if (HttpContext.Current.Session[UserLogonTokenSessionName] == null)
                {
                    return "";
                }
                return HttpContext.Current.Session[UserLogonTokenSessionName].ToString();
            }
        }

        public static FunctionReturnStatus CreateUserSession(DmsUser user, string logonToken)
        {
            try
            {
                if (user == null)
                {
                    return new FunctionReturnStatus(StatusType.Error, "Can not create session with empty user data");
                }
                if (string.IsNullOrEmpty(logonToken.Trim()))
                {
                    return new FunctionReturnStatus(StatusType.Error, "Can not create session with empty user logon token");
                }

                HttpContext.Current.Session[UserSessionName] = user;

                HttpContext.Current.Session[UserLogonTokenSessionName] = logonToken;

                return new FunctionReturnStatus(StatusType.Success, "User session created succesfully");
            }
            catch (Exception ex)
            {
                //logger.LogEvent(ex.ToString(), LogLevel.Error);
                throw;
            }
        }

        #endregion
    }
}