using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.Model;

namespace DMS.UI
{
    public partial class MasterSite : System.Web.UI.MasterPage
    {

        #region Page Events
        protected void Page_Load(object sender, EventArgs e)
        {
            if (SessionHelper.LogonUser == null)
            {
                Session.Abandon();
                Response.Redirect("Login.aspx");
            }
        }
        #endregion

        #region Control Events
        protected void LnkBtnLogout_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("Login.aspx");
        }
        #endregion
    }
}