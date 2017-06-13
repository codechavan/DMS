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
    public class SystemController : ApiController
    {
        Logger logger = null;

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="SystemController" /> class.
        /// </summary>
        public SystemController()
        {
            logger = new Logger("DMS.API.Controllers.SystemController");
        }
        #endregion

        [HttpPost]
        public IList<DmsSystem> GetSystemDropdown()
        {
            using (SystemBL sysBL = new SystemBL(WebConstants.DMSConnectionStringName))
            {
                return sysBL.GetSystem(null).LstData;
            }
        }

        [HttpPost]
        public FunctionReturnStatus CreateDmsSystem(NewDmsSystem newDmsSys)
        {
            if (newDmsSys == null)
            {
                throw new ArgumentNullException("newDmsSys");
            }
            DmsSystem system = new DmsSystem();
            DmsUser dmsUser = new DmsUser();
            DmsUserRole userRole = new DmsUserRole();

            system.SystemName = newDmsSys.SystemName;

            dmsUser.UserName = newDmsSys.UserName;
            dmsUser.FullName = newDmsSys.FullName;
            dmsUser.Password = newDmsSys.Password;
            dmsUser.EmailId = newDmsSys.EmailId;

            userRole.RoleName = newDmsSys.RoleName;
            userRole.Description = newDmsSys.RoleDescription;

            FunctionReturnStatus result = null;
            try
            {
                using (SystemBL systemBL = new SystemBL(WebConstants.DMSConnectionStringName))
                {
                    result = systemBL.AddDmsSystem(system, dmsUser, userRole);
                }
            }
            catch (Exception ex)
            {
                logger.LogEvent(ex.ToString(), LogLevel.Error);
                throw;
            }
            return result;
        }

        [HttpPost]
        public FunctionReturnStatus UpdateSystem(DmsSystem system)
        {
            if (system == null)
            {
                throw new ArgumentNullException("system");
            }
            system.ModifiedBy =long.Parse( RequestContext.Principal.Identity.Name);
            FunctionReturnStatus result = null;
            try
            {
                using (SystemBL systemBL = new SystemBL(WebConstants.DMSConnectionStringName))
                {
                    result = systemBL.UpdateDmsSystem(system);
                }
            }
            catch (Exception ex)
            {
                logger.LogEvent(ex.ToString(), LogLevel.Error);
                throw;
            }
            return result;
        }

        [HttpPost]
        public DmsSystemSearchData GetSystems(DmsSystemSearchParameters searchParameters)
        {
            if (searchParameters == null)
            {
                throw new ArgumentNullException("searchParameters");
            }
            try
            {
                using (SystemBL systemBL = new SystemBL(WebConstants.DMSConnectionStringName))
                {
                    return systemBL.GetSystem(searchParameters);
                }
            }
            catch (Exception ex)
            {
                logger.LogEvent(ex.ToString(), LogLevel.Error);
                throw;
            }
        }

        [HttpPost]
        public DmsSystem GetSystem(long systemId)
        {
            try
            {
                using (SystemBL systemBL = new SystemBL(WebConstants.DMSConnectionStringName))
                {
                    return systemBL.GetSystem(systemId);
                }
            }
            catch (Exception ex)
            {
                logger.LogEvent(ex.ToString(), LogLevel.Error);
                throw;
            }
        }

    }
}
