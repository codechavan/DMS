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
        private long FileId
        {
            get
            {
                long fileId = 0;
                long.TryParse(ViewState["Selected_File_Id"].ToString(), out fileId);
                return fileId;
            }
        }

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
                LoadDocumentProperties(0);
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

        private void LoadDocumentProperties(long documentId)
        {
            if (documentId < 1)
            {
                pnlProperties.Visible = false;
                return;
            }
            DocumentPropertiesSearchParameter searchParameters = new DocumentPropertiesSearchParameter();
            searchParameters.DocumentFileId = documentId;
            DocumentProperties prop = APIMethods.GetDocumentProperties(searchParameters);
            int propertyCount = int.Parse(SystemParameterHelper.GetSystemParameterValue(SystemParameterHelper.SystemParameterName.NUM_OF_FILE_PROPERTIES));

            pnlProperties.Visible = false;

            if (propertyCount > 0)
            {
                pnlProperties.Visible = true;
                pnlProperty1.Visible = false;
                pnlProperty2.Visible = false;
                pnlProperty3.Visible = false;
                pnlProperty4.Visible = false;
                pnlProperty5.Visible = false;
                pnlProperty6.Visible = false;
                pnlProperty7.Visible = false;
                pnlProperty8.Visible = false;
                pnlProperty9.Visible = false;
                pnlProperty10.Visible = false;
                if (propertyCount > 0)
                {
                    pnlProperty1.Visible = true;
                    lblPropertyName1.Text = prop.Propertynames.Field1Name;
                    txtPropertyValue1.Text = prop.Field1Value;
                }
                if (propertyCount > 1)
                {
                    pnlProperty2.Visible = true;
                    lblPropertyName2.Text = prop.Propertynames.Field2Name;
                    txtPropertyValue2.Text = prop.Field2Value;
                }
                if (propertyCount > 2)
                {
                    pnlProperty3.Visible = true;
                    lblPropertyName3.Text = prop.Propertynames.Field3Name;
                    txtPropertyValue3.Text = prop.Field3Value;
                }
                if (propertyCount > 3)
                {
                    pnlProperty4.Visible = true;
                    lblPropertyName4.Text = prop.Propertynames.Field4Name;
                    txtPropertyValue4.Text = prop.Field4Value;
                }
                if (propertyCount > 4)
                {
                    pnlProperty5.Visible = true;
                    lblPropertyName5.Text = prop.Propertynames.Field5Name;
                    txtPropertyValue5.Text = prop.Field5Value;
                }
                if (propertyCount > 5)
                {
                    pnlProperty6.Visible = true;
                    lblPropertyName6.Text = prop.Propertynames.Field6Name;
                    txtPropertyValue6.Text = prop.Field6Value;
                }
                if (propertyCount > 6)
                {
                    pnlProperty7.Visible = true;
                    lblPropertyName7.Text = prop.Propertynames.Field7Name;
                    txtPropertyValue7.Text = prop.Field7Value;
                }
                if (propertyCount > 7)
                {
                    pnlProperty8.Visible = true;
                    lblPropertyName8.Text = prop.Propertynames.Field8Name;
                    txtPropertyValue8.Text = prop.Field8Value;
                }
                if (propertyCount > 8)
                {
                    pnlProperty9.Visible = true;
                    lblPropertyName9.Text = prop.Propertynames.Field9Name;
                    txtPropertyValue9.Text = prop.Field9Value;
                }
                if (propertyCount > 9)
                {
                    pnlProperty10.Visible = true;
                    lblPropertyName10.Text = prop.Propertynames.Field10Name;
                    txtPropertyValue10.Text = prop.Field10Value;
                }
            }
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
                LinkButton Lnk = (LinkButton)e.Row.FindControl("LnkFileName");

                if (doc.ObjectType == DocumentObjectType.Folder)
                {
                    ach.HRef = HttpRuntime.AppDomainAppVirtualPath + "OpenFolder.aspx?id=" + doc.ObjectId;
                    ach.InnerHtml = "<i class='fa fa-folder'></i>" + doc.Name;
                    Lnk.Visible = false;
                }
                else
                {
                    ach.Visible = false;
                    Lnk.Text = "<i class='fa fa-file'></i>" + doc.Name;
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

        protected void grdObjectList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "OpenFile")
            {
                GridViewRow gvr = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                long ID = long.Parse(grdObjectList.DataKeys[gvr.RowIndex].Value.ToString());
                ViewState["Selected_File_Id"] = ID;
                LoadDocumentProperties(ID);
            }
        }

        protected void btnUpdateFileProperties_ServerClick(object sender, EventArgs e)
        {
            DocumentFolder folder = new DocumentFolder();
            folder.CreatedBy = SessionHelper.LogonUser.UserId;
            folder.SystemId = SessionHelper.LogonUser.SystemId;
            folder.FolderName = TxtFolderName.Text;
            folder.ParentFolderId = SessionHelper.SelectedFolderId;
            folder.ModifiedBy = SessionHelper.LogonUser.UserId;

            DocumentProperties docProp = new DocumentProperties();

            docProp.DocumentFileId = FileId;
            docProp.PropertyValueModifiedBy = SessionHelper.LogonUser.UserId;

            docProp.Field1Value = txtPropertyValue1.Text;
            docProp.Field2Value = txtPropertyValue2.Text;
            docProp.Field3Value = txtPropertyValue3.Text;
            docProp.Field4Value = txtPropertyValue4.Text;
            docProp.Field5Value = txtPropertyValue5.Text;
            docProp.Field6Value = txtPropertyValue6.Text;
            docProp.Field7Value = txtPropertyValue7.Text;
            docProp.Field8Value = txtPropertyValue8.Text;
            docProp.Field9Value = txtPropertyValue9.Text;
            docProp.Field10Value = txtPropertyValue10.Text;

            FunctionReturnStatus sts = APIMethods.UpdateDocumentProperties(docProp);

            ScriptManager.RegisterStartupScript(this, this.GetType(), "", "ShowMessage('" + sts.Message + "');", true);
        }

    }
}