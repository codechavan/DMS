using DMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace DMS.UI
{
    public partial class OpenFolder : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                pnlFileUpload.Visible = false;
                if (SystemParameterHelper.GetSystemParameterValue(SystemParameterHelper.SystemParameterName.CAN_UPLOAD_FILE_FROM_UI) == "1")
                {
                    pnlFileUpload.Visible = true;
                }

                SetFolderId();

                PagingDetails page = new PagingDetails();
                page.PageIndex = grdObjectList.PageIndex + 1;
                page.PageSize = grdObjectList.PageSize;
                LoadFolderDetail(page);
            }
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

        private void LoadFolderDetail(PagingDetails paging)
        {
            DocumentSearchParameter searchParameter = new DocumentSearchParameter();
            searchParameter.ParentFolderId = SessionHelper.SelectedFolderId;
            searchParameter.SystemId = SessionHelper.LogonUser.SystemId;
            searchParameter.PageDetail = paging;
            DocumentSearchData lst = APIMethods.GetDocumentObjectList(searchParameter);

            grdObjectList.VirtualItemCount = int.Parse(lst.RecordCount.ToString());

            grdObjectList.DataSource = lst.LstData;
            grdObjectList.DataBind();
        }

        protected void btnSaveFolder_Click(object sender, EventArgs e)
        {
            DocumentFolder folder = new DocumentFolder();
            folder.CreatedBy = SessionHelper.LogonUser.UserId;
            folder.SystemId = SessionHelper.LogonUser.SystemId;
            folder.FolderName = TxtFolderName.Text;
            folder.ParentFolderId = SessionHelper.SelectedFolderId;
            folder.ModifiedBy = SessionHelper.LogonUser.UserId;
            FunctionReturnStatus sts = APIMethods.CreateFolder(folder);
            if (sts.StatusType == StatusType.Success)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "ShowMessage('" + sts.Message + "');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "ShowMessage('" + sts.Message + "');$('#modalFolder').modal('show');", true);
            }
        }

        protected void btnSaveFile_Click(object sender, EventArgs e)
        {
            if (FileUpload.HasFile)
            {
                DocumentFile file = new DocumentFile();
                file.CreatedBy = SessionHelper.LogonUser.UserId;
                file.SystemId = SessionHelper.LogonUser.SystemId;
                file.FileName = FileUpload.FileName;
                file.FileData = FileUpload.FileBytes;
                file.FolderId = SessionHelper.SelectedFolderId;
                file.ModifiedBy = SessionHelper.LogonUser.UserId;

                FunctionReturnStatus sts = APIMethods.UploadFile(file);
                if (sts.StatusType == StatusType.Success)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "", "ShowMessage('" + sts.Message + "');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "", "ShowMessage('" + sts.Message + "');$('#modalFile').modal('show');", true);
                }
            }
        }

        protected void grdObjectList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Document doc = (Document)e.Row.DataItem;
                HtmlAnchor ach = (HtmlAnchor)e.Row.FindControl("LnkObjectName");
                if (doc.ObjectType == DocumentObjectType.Folder)
                {
                    ach.HRef = HttpRuntime.AppDomainAppVirtualPath + "OpenFolder.aspx?id=" + doc.ObjectId;
                    ach.InnerHtml = "<i class='fa fa-folder'></i>" + doc.Name;
                }
                else
                {
                    ach.HRef = "#";
                    ach.InnerHtml = "<i class='fa fa-file'></i>" + doc.Name;
                }
            }
        }

        protected void grdObjectList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdObjectList.PageIndex = e.NewPageIndex;
            PagingDetails page = new PagingDetails();
            page.PageIndex = e.NewPageIndex + 1;
            page.PageSize = grdObjectList.PageSize;
            
            LoadFolderDetail(page);
        }

    }
}