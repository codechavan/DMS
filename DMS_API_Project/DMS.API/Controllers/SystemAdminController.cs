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
    public class SystemAdminController : ApiController
    {
        Logger logger = null;

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="SystemAdminController" /> class.
        /// </summary>
        public SystemAdminController()
        {
            logger = new Logger("DMS.API.Controllers.SystemAdminController");
        }
        #endregion

        [HttpPost]
        public FunctionReturnStatus Login(string username, string password)
        {
            using (SystemAdminBL sysBL = new SystemAdminBL(WebConstants.DMSConnectionStringName))
            {
                return sysBL.UserLogin(username,password);
            }
        }

        [HttpPost]
        public FunctionReturnStatus CreateUser(SystemAdmin sysAdmin)
        {
            using (SystemAdminBL sysBL = new SystemAdminBL(WebConstants.DMSConnectionStringName))
            {
                sysAdmin.CreatedBy = long.Parse(RequestContext.Principal.Identity.Name);
                return sysBL.CreateUser(sysAdmin);
            }
        }

        [HttpPost]
        public FunctionReturnStatus UpdateUser(SystemAdmin sysAdmin)
        {
            using (SystemAdminBL sysBL = new SystemAdminBL(WebConstants.DMSConnectionStringName))
            {
                sysAdmin.ModifiedBy = long.Parse(RequestContext.Principal.Identity.Name);
                return sysBL.UpdateUser(sysAdmin);
            }
        }

        [HttpPost]
        public FunctionReturnStatus ChangePasword(SystemAdmin sysAdmin)
        {
            using (SystemAdminBL sysBL = new SystemAdminBL(WebConstants.DMSConnectionStringName))
            {
                sysAdmin.ModifiedBy = long.Parse(RequestContext.Principal.Identity.Name);
                return sysBL.ChangePasword(sysAdmin.AdminId,sysAdmin.Password,sysAdmin.Password,sysAdmin.ModifiedBy);
            }
        }

        [HttpPost]
        public SystemAdmin GetSystemAdmin(long adminId)
        {
            using (SystemAdminBL sysBL = new SystemAdminBL(WebConstants.DMSConnectionStringName))
            {
                return sysBL.GetSystemAdmin(adminId);
            }
        }

        [HttpPost]
        public SystemAdminSearchData GetSystemAdmins(SystemAdminSearchParameter searchParameters)
        {
            using (SystemAdminBL sysBL = new SystemAdminBL(WebConstants.DMSConnectionStringName))
            {
                return sysBL.GetSystemAdmin(searchParameters);
            }
        }

    }
}
