using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DMS.Model;

namespace DMS.UI.Controllers
{
    public class DataProviderController : ApiController
    {
        public List<DmsSystem> GetSystemDropdown()
        {
            List<DmsSystem> lstSys = new List<DmsSystem>();
            DmsSystem sys = new DmsSystem();
            sys.SystemName = "Test 1";
            sys.SystemId = 1;
            lstSys.Add(sys);
            sys = new DmsSystem();
            sys.SystemName = "Test 2";
            sys.SystemId = 2;
            lstSys.Add(sys);

            sys = new DmsSystem();
            sys.SystemName = "Test 3";
            sys.SystemId = 3;
            lstSys.Add(sys);

            return lstSys;
            //return Json(lstSys, JsonRequestBehavior.AllowGet);
        }
    }
}
