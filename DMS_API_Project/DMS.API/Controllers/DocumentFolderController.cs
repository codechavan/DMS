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
        public IList<DocumentFolderTree> GetDocumentFolderTree(DocumentFolderTreeSearchParameters searchParameters)
        {
            using (DocumentFolderBL sysBL = new DocumentFolderBL(WebConstants.DMSConnectionStringName))
            {
                return sysBL.GetDocumentFolderTree(searchParameters);
            }
        }

        [HttpPost]
        public DocumentSearchData GetDocumentObjectList(DocumentSearchParameter searchParameters)
        {
            using (DocumentFolderBL sysBL = new DocumentFolderBL(WebConstants.DMSConnectionStringName))
            {
                return sysBL.GetDocumentObjectList(searchParameters);
            }
        }

        [HttpPost]
        public FunctionReturnStatus CreateFolder(DocumentFolder folder)
        {
            using (DocumentFolderBL sysBL = new DocumentFolderBL(WebConstants.DMSConnectionStringName))
            {
                folder.CreatedBy = long.Parse(RequestContext.Principal.Identity.Name);
                return sysBL.CreateFolder(folder);
            }
        }

        [HttpPost]
        public FunctionReturnStatus UpdateFolder(DocumentFolder folder)
        {
            using (DocumentFolderBL sysBL = new DocumentFolderBL(WebConstants.DMSConnectionStringName))
            {
                folder.ModifiedBy = long.Parse(RequestContext.Principal.Identity.Name);
                return sysBL.UpdateFolder(folder);
            }
        }

        [HttpPost]
        public DocumentFolderSearchData GetFolder(long parentFolderId)
        {
            using (DocumentFolderBL sysBL = new DocumentFolderBL(WebConstants.DMSConnectionStringName))
            {
                return sysBL.GetFolder(parentFolderId);
            }
        }

        [HttpPost]
        public DocumentFolderSearchData GetFolders(DocumentFolderSearchParameter searchParameter)
        {
            using (DocumentFolderBL sysBL = new DocumentFolderBL(WebConstants.DMSConnectionStringName))
            {
                return sysBL.GetFolders(searchParameter);
            }
        }

    }
}
