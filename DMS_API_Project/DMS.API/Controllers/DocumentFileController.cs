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
    public class DocumentFileController : ApiController
    {
        Logger logger = null;

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentFileController" /> class.
        /// </summary>
        public DocumentFileController()
        {
            logger = new Logger("DMS.API.Controllers.DocumentFileController");
        }
        #endregion

        [HttpPost]
        public FunctionReturnStatus UploadFile(DocumentFile file)
        {
            using (DocumentFileBL sysBL = new DocumentFileBL(WebConstants.DMSConnectionStringName))
            {
                return sysBL.UploadFile(file);
            }
        }

    }
}
