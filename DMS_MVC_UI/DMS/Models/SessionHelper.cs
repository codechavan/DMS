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

        private const string UserSessionName = "DMS.API.UserSession";

        private const string UserLogonTokenSessionName = "DMS.API.UserLogonTokenSession";

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

        public SessionHelper()
        {
            //logger = new Logger("DMS.API.HelperClasses.SessionHelper");
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
    }
}