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
    public class DocumentFilePropertiesController : ApiController
    {
        Logger logger = null;

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentFilePropertiesController" /> class.
        /// </summary>
        public DocumentFilePropertiesController()
        {
            logger = new Logger("DMS.API.Controllers.DocumentFilePropertiesController");
        }
        #endregion

        [HttpPost]
        public DocumentPropertiesNames GetDocumentPropertiesNames(DocumentPropertiesNamesSearchParamater searchParameters)
        {
            using (DocumentPropertiesNamesBL sysBL = new DocumentPropertiesNamesBL(WebConstants.DMSConnectionStringName))
            {
                return sysBL.GetDocumentPropertiesNames(searchParameters);
            }
        }

        [HttpPost]
        public DocumentProperties GetDocumentProperties(DocumentPropertiesSearchParameter searchParameters)
        {
            using (DocumentPropertiesBL sysBL = new DocumentPropertiesBL(WebConstants.DMSConnectionStringName))
            {
                return sysBL.GetDocumentProperties(searchParameters.DocumentFileId);
            }
        }

        [HttpPost]
        public FunctionReturnStatus UpdateDocumentProperties(DocumentProperties paramValue)
        {
            using (DocumentPropertiesBL sysBL = new DocumentPropertiesBL(WebConstants.DMSConnectionStringName))
            {
                return sysBL.UpdateDocumentProperties(paramValue);
            }
        }
    }
}
