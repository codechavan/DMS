using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.Model;

namespace DMS.UI
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void BtnRegister_Click(object sender, EventArgs e)
        {
            NewDmsSystem newSys = new NewDmsSystem();
            newSys.SystemName = TxtSystemName.Value;

            newSys.Password = TxtPassword.Value;
            newSys.FullName = TxtUserId.Value;
            newSys.UserName = TxtUserId.Value;
            newSys.EmailId = TxtEmailId.Value;

            newSys.RoleDescription = TxtRoleName.Value;
            newSys.RoleName = TxtRoleName.Value;

            FunctionReturnStatus sts = APIMethods.CreateNewSystem(newSys);
        }
    }
}