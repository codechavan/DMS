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
    public class DocumentFolderController : ApiController
    {
        Logger logger = null;

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentFolderController" /> class.
        /// </summary>
        public DocumentFolderController()
        {
            logger = new Logger("DMS.API.Controllers.DocumentFolderController");
        }
        #endregion

        [HttpPost]
        public IList<DocumentFolderTree> GetDocumentFolderTree()
        {
            long systemId = 1;
            using (DocumentFolderBL sysBL = new DocumentFolderBL(WebConstants.DMSConnectionStringName))
            {
                return sysBL.GetDocumentFolderTree(systemId);
            }
        }

    }
}
