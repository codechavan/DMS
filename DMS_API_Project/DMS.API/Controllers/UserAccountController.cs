using Chavan.Logger;
using DMS.BL;
using DMS.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DMS.API.Controllers
{
    public class UserAccountController : ApiController
    {
        Logger logger = null;

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="UserAccountController" /> class.
        /// </summary>
        public UserAccountController()
        {
            logger = new Logger("DMS.API.Controllers.UserAccountController");
        }
        #endregion

        #region Public Methods
        [HttpPost]
        public FunctionReturnStatus GetUserList(DmsUserSearchParameter searchParameter)
        {
            IList<DmsUser> lstUsers = null;
            FunctionReturnStatus result = null;
            try
            {

            }
            catch (Exception ex)
            {
                logger.LogEvent(ex.ToString(), LogLevel.Error);
                throw;
            }
            return result;
        }

        [AllowAnonymous]
        [HttpPost]
        public FunctionReturnStatus Login(UserLoginParameter loginParameter)
        {
            FunctionReturnStatus result = null;
            try
            {
                using (UserBL userBL = new UserBL(ConfigurationManager.ConnectionStrings[0].Name))
                {
                    result = userBL.UserLogin(loginParameter.SystemID, loginParameter.UserName, loginParameter.Password);
                }
            }
            catch (Exception ex)
            {
                logger.LogEvent(ex.ToString(), LogLevel.Error);
                throw;
            }
            return result;
        }

        #endregion

    }
}
