using DMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DMS.UI
{
    public partial class Dashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            gvDocList.AllowPaging = true;
            gvDocList.PageSize = 5;

            PagingDetails page = new PagingDetails();
            page.PageIndex = gvDocList.PageIndex;
            page.PageSize = gvDocList.PageSize;

            gvDocList.DataSource = GetDummyData(page);
            gvDocList.DataBind();
        }

        private List<Document> GetDummyData(PagingDetails paging)
        {
            List<Document> lstDoc = new List<Document>();
            Document doc = null;
            doc = new Document();
            doc.Name = "Document1";
            lstDoc.Add(doc);
            doc = new Document();
            doc.Name = "Document2";
            lstDoc.Add(doc);
            doc = new Document();
            doc.Name = "Document3";
            lstDoc.Add(doc);
            doc = new Document();
            doc.Name = "Document4";
            lstDoc.Add(doc);
            doc = new Document();
            doc.Name = "Document5";
            lstDoc.Add(doc);
            doc = new Document();
            doc.Name = "Document6";
            lstDoc.Add(doc);
            doc = new Document();
            doc.Name = "Document7";
            lstDoc.Add(doc);
            doc = new Document();
            doc.Name = "Document8";
            lstDoc.Add(doc);
            doc = new Document();
            doc.Name = "Document9";
            lstDoc.Add(doc);
            doc = new Document();
            doc.Name = "Document10";
            lstDoc.Add(doc);

            return lstDoc;

        }

        [System.Web.Services.WebMethod]
        public static IList<DocumentFolderTree> GetFolders()
        {
            if (SessionHelper.LogonUser == null)
            {
                return null;
            }
            return APIMethods.GetDocumentFolderTree(SessionHelper.LogonUser.SystemId);
        }

        protected void gvDocList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvDocList.PageIndex = e.NewPageIndex;
            PagingDetails page = new PagingDetails();
            page.PageIndex = e.NewPageIndex;
            page.PageSize = gvDocList.PageSize;

            gvDocList.DataSource = GetDummyData(page);
            gvDocList.DataBind();
        }
    }

}