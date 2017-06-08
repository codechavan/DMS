using DMS.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DMS.UI
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DdlSystems.Items.Clear();
                try
                {
                    foreach (var item in APIMethods.GetSystemDropdown())
                    {
                        DdlSystems.Items.Add(new ListItem(item.SystemName, item.SystemId.ToString()));
                    }
                }
                catch (Exception)
                {
                    
                }
            }
        }

        protected void BtnLogin_Click(object sender, EventArgs e)
        {
            UserLoginParameter prm = new UserLoginParameter();
            prm.Password = TxtPassword.Text;
            prm.UserName = TxtUserName.Text;
            prm.SystemID = long.Parse(DdlSystems.SelectedValue);
            FunctionReturnStatus sts = APIMethods.Login(prm);

            if (sts.StatusType == StatusType.Success)
            {
                DmsUser usr = (DmsUser)sts.Data;
                FunctionReturnStatus snStatus = SessionHelper.CreateUserSession(usr, usr.LogonToken);
                if (snStatus.StatusType == StatusType.Success)
                {
                    Response.Redirect("Dashboard.aspx");
                }
            }
        }

    }
}