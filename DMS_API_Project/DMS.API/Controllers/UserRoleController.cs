using Chavan.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DMS.Model;
using DMS.BL;
using System.Configuration;

namespace DMS.API.Controllers
{
    public class UserRoleController : ApiController
    {
        Logger logger = null;

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="UserRoleController" /> class.
        /// </summary>
        public UserRoleController()
        {
            logger = new Logger("DMS.API.Controllers.UserRoleController");
        }
        #endregion

        [HttpPost]
        public FunctionReturnStatus CreateUserRole(DmsUserRole role)
        {
            using (UserRoleBL sysBL = new UserRoleBL(WebConstants.DMSConnectionStringName))
            {
                role.CreatedBy = long.Parse(RequestContext.Principal.Identity.Name);
                return sysBL.CreateUserRole(role);
            }
        }

        [HttpPost]
        public FunctionReturnStatus UpdateUserRole(DmsUserRole role)
        {
            using (UserRoleBL sysBL = new UserRoleBL(WebConstants.DMSConnectionStringName))
            {
                role.ModifiedBy = long.Parse(RequestContext.Principal.Identity.Name);
                return sysBL.UpdateUserRole(role);
            }
        }

        [HttpPost]
        public DmsUserRole GetUserRole(long userRoleId)
        {
            using (UserRoleBL sysBL = new UserRoleBL(WebConstants.DMSConnectionStringName))
            {
                return sysBL.GetUserRole(userRoleId);
            }
        }

        [HttpPost]
        public DmsUserRoleSearchData GetUserRoles(DmsUserRoleSearchParameter searchParameters)
        {
            using (UserRoleBL sysBL = new UserRoleBL(WebConstants.DMSConnectionStringName))
            {
                return sysBL.GetUserRole(searchParameters);
            }
        }

    }
}
