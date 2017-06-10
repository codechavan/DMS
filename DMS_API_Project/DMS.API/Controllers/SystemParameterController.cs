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
    public class SystemParameterController : ApiController
    {
        Logger logger = null;

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="SystemParameterController" /> class.
        /// </summary>
        public SystemParameterController()
        {
            logger = new Logger("DMS.API.Controllers.SystemParameterController");
        }
        #endregion

        [HttpPost]
        public SystemParameterValueSearchData GetSystemParameterValues(SystemParameterSearchParameters searchParameters)
        {
            using (SystemParameterBL sysBL = new SystemParameterBL(WebConstants.DMSConnectionStringName))
            {
                return sysBL.GetSystemParameterValues(searchParameters);
            }
        }

        [HttpPost]
        public SystemParameterValue GetSystemParameterValue(long systemId, string parameterName)
        {
            SystemParameterSearchParameters searchParameters = new SystemParameterSearchParameters();
            searchParameters.SystemParameterName = parameterName;
            searchParameters.SystemId = systemId;
            using (SystemParameterBL sysBL = new SystemParameterBL(WebConstants.DMSConnectionStringName))
            {
                SystemParameterValueSearchData lst = sysBL.GetSystemParameterValues(searchParameters);
                if (lst.RecordCount == 1)
                {
                    return lst.LstData[0];
                }
                return null;
            }
        }
    }
}
