using DMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DMS.UI
{
    public partial class OpenFolder : System.Web.UI.Page
    {
        private long FolderId
        {
            get
            {
                long result = 0;
                long.TryParse(Chavan.Common.EncryptionHelper.Instance.DecryptValue(hdnFolderId.Value), out result);
                return result;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            SetFolderId();
            LoadFolderDetail();
        }

        private void SetFolderId()
        {
            if (!IsPostBack)
            {
                long folderId = 0;
                long.TryParse(Request.QueryString["id"], out folderId);
                hdnFolderId.Value = Chavan.Common.EncryptionHelper.Instance.EncryptValue(folderId.ToString());
                SessionHelper.SelectedFolderId = folderId;
            }
        }

        private void LoadFolderDetail()
        {
            DocumentSearchParameter searchParameter = new DocumentSearchParameter();
            searchParameter.ParentFolderId = FolderId;
            searchParameter.SystemId = SessionHelper.LogonUser.SystemId;
            DocumentSearchData lst = APIMethods.GetDocumentObjectList(searchParameter);
        }

        protected void btnSaveFolder_Click(object sender, EventArgs e)
        {
            DocumentFolder folder = new DocumentFolder();
            folder.CreatedBy = SessionHelper.LogonUser.UserId;
            folder.SystemId = SessionHelper.LogonUser.SystemId;
            folder.FolderName = TxtFolderName.Text;
            folder.ParentFolderId = FolderId;
            folder.ModifiedBy = SessionHelper.LogonUser.UserId;
            FunctionReturnStatus sts = APIMethods.CreateFolder(folder);
        }

    }
}