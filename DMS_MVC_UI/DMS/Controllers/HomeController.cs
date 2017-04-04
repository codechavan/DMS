using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DMS.Model;
using System.Net.Http;
using Newtonsoft.Json;

namespace DMS.UI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        public async System.Threading.Tasks.Task<JsonResult> SubmitLogin(UserLoginParameter userLogin)
        {
            try
            {
                FunctionReturnStatus returnStat = new FunctionReturnStatus();
                HttpResponseMessage msg = RequestHelper.PostRequest(WebConstants.DMSAPIURL, WebConstants.LogonAPI, userLogin);
                if (msg.IsSuccessStatusCode)
                {
                    //return Json(msg.Content.ReadAsAsync<FunctionReturnStatus>(new[] { new System.Net.Http.Formatting.JsonMediaTypeFormatter() }));
                    returnStat = await msg.Content.ReadAsAsync<FunctionReturnStatus>(new[] { new System.Net.Http.Formatting.JsonMediaTypeFormatter() });

                    if (returnStat.StatusType == StatusType.Success)
                    {
                        FunctionReturnStatus fSession = new FunctionReturnStatus();
                        DmsUser usr = JsonConvert.DeserializeObject<DmsUser>(returnStat.Data.ToString());
                        fSession = SessionHelper.CreateUserSession(usr, usr.LogonToken);
                        if (fSession.StatusType != StatusType.Success)
                        {
                            return Json(fSession);
                        }
                    }

                    return Json(returnStat);
                }
                else
                {
                    return Json(new FunctionReturnStatus(StatusType.Error, "Exception while authorization, Kindly contact system administrator"));
                }
            }
            catch (Exception)
            {
                return Json(new FunctionReturnStatus(StatusType.Error, "Exception while authorization, Kindly contact system administrator"));
            }
        }

        public IList<DmsSystem> GetSystemDropdown()
        {
            //using (SystemBL sysBL = new SystemBL())
            //{

            //}
            List<DmsSystem> lstSys = new List<DmsSystem>();
            DmsSystem sys = new DmsSystem();
            sys.SystemName = "Test 1";
            sys.SystemID = 1;
            lstSys.Add(sys);
            sys = new DmsSystem();
            sys.SystemName = "Test 2";
            sys.SystemID = 2;
            lstSys.Add(sys);

            sys = new DmsSystem();
            sys.SystemName = "Test 3";
            sys.SystemID = 3;
            lstSys.Add(sys);

            return lstSys;
        }
    }
}