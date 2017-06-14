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
    public class DocumentAccessController : ApiController
    {
        Logger logger = null;

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentAccessController" /> class.
        /// </summary>
        public DocumentAccessController()
        {
            logger = new Logger("DMS.API.Controllers.DocumentAccessController");
        }
        #endregion

        [HttpPost]
        public FunctionReturnStatus UpdateConfigurationValue(DocumentAccess documentAccess)
        {
            using (DocumentAccessBL sysBL = new DocumentAccessBL(WebConstants.DMSConnectionStringName))
            {
                if (documentAccess.ObjectId == 0)
                {
                    documentAccess.CreatedBy = long.Parse(RequestContext.Principal.Identity.Name);
                }
                else
                {
                    documentAccess.ModifiedBy = long.Parse(RequestContext.Principal.Identity.Name);
                }
                return sysBL.UpdateConfigurationValue(documentAccess);
            }
        }

        [HttpPost]
        public FunctionReturnStatus RemoveDocumentAccess(DocumentAccess documentAccess)
        {
            using (DocumentAccessBL sysBL = new DocumentAccessBL(WebConstants.DMSConnectionStringName))
            {
                documentAccess.ModifiedBy = long.Parse(RequestContext.Principal.Identity.Name);
                return sysBL.RemoveDocumentAccess(documentAccess);
            }
        }

        [HttpPost]
        public FunctionReturnStatus GetDocumentAccess(DocumentAccessSearchParameter searchParameter)
        {
            using (DocumentAccessBL sysBL = new DocumentAccessBL(WebConstants.DMSConnectionStringName))
            {
                return sysBL.GetDocumentAccess(searchParameter);
            }
        }

    }
}
