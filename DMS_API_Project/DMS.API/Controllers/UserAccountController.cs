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
        public DmsUserSearchData GetUsers(DmsUserSearchParameter searchParameter)
        {
            try
            {
                using (UserBL userBL = new UserBL(WebConstants.DMSConnectionStringName))
                {
                    return userBL.GetUser(searchParameter);
                }
            }
            catch (Exception ex)
            {
                logger.LogEvent(ex.ToString(), LogLevel.Error);
                throw;
            }
        }

        [HttpPost]
        public DmsUser GetUser(long userId)
        {
            try
            {
                using (UserBL userBL = new UserBL(WebConstants.DMSConnectionStringName))
                {
                    return userBL.GetUser(userId);
                }
            }
            catch (Exception ex)
            {
                logger.LogEvent(ex.ToString(), LogLevel.Error);
                throw;
            }
        }

        [HttpPost]
        public FunctionReturnStatus CreateUser(DmsUser user)
        {
            try
            {
                using (UserBL userBL = new UserBL(WebConstants.DMSConnectionStringName))
                {
                    user.CreatedBy = long.Parse(RequestContext.Principal.Identity.Name);
                    return userBL.CreateUser(user);
                }
            }
            catch (Exception ex)
            {
                logger.LogEvent(ex.ToString(), LogLevel.Error);
                throw;
            }
        }

        [HttpPost]
        public FunctionReturnStatus UpdateUser(DmsUser user)
        {
            try
            {
                using (UserBL userBL = new UserBL(WebConstants.DMSConnectionStringName))
                {
                    user.ModifiedBy = long.Parse(RequestContext.Principal.Identity.Name);
                    return userBL.UpdateUser(user);
                }
            }
            catch (Exception ex)
            {
                logger.LogEvent(ex.ToString(), LogLevel.Error);
                throw;
            }
        }

        [HttpPost]
        public FunctionReturnStatus UnlockUser(DmsUser user)
        {
            try
            {
                using (UserBL userBL = new UserBL(WebConstants.DMSConnectionStringName))
                {
                    user.ModifiedBy = long.Parse(RequestContext.Principal.Identity.Name);
                    return userBL.UnlockUser(user.SystemId, user.UserId, user.ModifiedBy);
                }
            }
            catch (Exception ex)
            {
                logger.LogEvent(ex.ToString(), LogLevel.Error);
                throw;
            }
        }

        [HttpPost]
        public FunctionReturnStatus ChangePasword(DmsUser user)
        {
            try
            {
                using (UserBL userBL = new UserBL(WebConstants.DMSConnectionStringName))
                {
                    user.ModifiedBy = long.Parse(RequestContext.Principal.Identity.Name);
                    return userBL.ChangePasword(user.SystemId, user.UserId, user.Password, user.Password, user.ModifiedBy);
                }
            }
            catch (Exception ex)
            {
                logger.LogEvent(ex.ToString(), LogLevel.Error);
                throw;
            }
        }

        [AllowAnonymous]
        [HttpPost]
        public FunctionReturnStatus Login(UserLoginParameter loginParameter)
        {
            FunctionReturnStatus result = null;
            try
            {
                using (UserBL userBL = new UserBL(WebConstants.DMSConnectionStringName))
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
