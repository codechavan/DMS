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
                if (file.FileId < 1)
                {
                    file.CreatedBy = long.Parse(RequestContext.Principal.Identity.Name);
                }
                else
                {
                    file.ModifiedBy = long.Parse(RequestContext.Principal.Identity.Name);
                }
                return sysBL.UploadFile(file);
            }
        }

        [HttpPost]
        public DocumentFileSearchData GetFiles(DocumentFileSearchParameter searchParameters)
        {
            using (DocumentFileBL sysBL = new DocumentFileBL(WebConstants.DMSConnectionStringName))
            {
                return sysBL.GetFiles(searchParameters);
            }
        }

        [HttpPost]
        public DocumentFile GetFile(long documentId)
        {
            using (DocumentFileBL sysBL = new DocumentFileBL(WebConstants.DMSConnectionStringName))
            {
                return sysBL.GetFiles(documentId);
            }
        }

        [HttpPost]
        public DocumentProperties GetDocumentFileProperties(long documentId)
        {
            using (DocumentFileBL sysBL = new DocumentFileBL(WebConstants.DMSConnectionStringName))
            {
                return sysBL.GetDocumentFileProperties(documentId);
            }
        }

        [HttpPost]
        public IList<DocumentFileHistory> GetFileHistory(long documentId)
        {
            using (DocumentFileBL sysBL = new DocumentFileBL(WebConstants.DMSConnectionStringName))
            {
                return sysBL.GetFileHistory(documentId);
            }
        }

    }
}
