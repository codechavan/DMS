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
            using (SystemBL sysBL = new SystemBL(ConfigurationManager.ConnectionStrings[0].Name))
            {
                return sysBL.GetSystem(null, null).LstData;
            }
            //List<DmsSystem> lstSys = new List<DmsSystem>();
            //DmsSystem sys = new DmsSystem();
            //sys.SystemName = "Test 1";
            //sys.SystemId = 1;
            //lstSys.Add(sys);
            //sys = new DmsSystem();
            //sys.SystemName = "Test 2";
            //sys.SystemId = 2;
            //lstSys.Add(sys);

            //sys = new DmsSystem();
            //sys.SystemName = "Test 3";
            //sys.SystemId = 3;
            //lstSys.Add(sys);

            //return lstSys;
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
                using (SystemBL systemBL = new SystemBL(ConfigurationManager.ConnectionStrings[0].Name))
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

    }
}
