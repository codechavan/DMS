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
    public class DMSConfigurationController : ApiController
    {
        Logger logger = null;

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentFileController" /> class.
        /// </summary>
        public DMSConfigurationController()
        {
            logger = new Logger("DMS.API.Controllers.DMSConfigurationController");
        }
        #endregion

        [HttpPost]
        public SysConfiguration GetConfiguration(string configurationCode)
        {
            using (ConfigurationBL sysBL = new ConfigurationBL(WebConstants.DMSConnectionStringName))
            {
                return sysBL.GetConfiguration(configurationCode);
            }
        }

        [HttpPost]
        public SysConfigurationSearchData GetConfigurations(ConfigurationSearchParameter searchParameters)
        {
            using (ConfigurationBL sysBL = new ConfigurationBL(WebConstants.DMSConnectionStringName))
            {
                return sysBL.GetConfiguration(searchParameters);
            }
        }

        [HttpPost]
        public FunctionReturnStatus UpdateConfigurationValue(SysConfiguration configuration)
        {
            using (ConfigurationBL sysBL = new ConfigurationBL(WebConstants.DMSConnectionStringName))
            {
                configuration.ModifiedBy = long.Parse(RequestContext.Principal.Identity.Name);
                return sysBL.UpdateConfigurationValue(configuration);
            }
        }

    }
}
