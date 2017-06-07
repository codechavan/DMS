using DMS.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace DMS.UI
{
    public class APIMethods
    {
        public static IList<DmsSystem> GetSystemDropdown()
        {
            try
            {
                HttpResponseMessage responseMessage = RequestHelper.PostRequest(WebConstants.DMSAPIURL, WebConstants.SystemDropDownAPI, null, false);
                if (responseMessage.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<IList<DmsSystem>>(responseMessage.Content.ReadAsStringAsync().Result);
                }
                else
                {
                    throw new HttpException("Error in communication to API");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static FunctionReturnStatus CreateNewSystem(NewDmsSystem newDmsSys)
        {
            try
            {
                HttpResponseMessage responseMessage = RequestHelper.PostRequest(WebConstants.DMSAPIURL, WebConstants.CreateDmsSystemAPI, newDmsSys, false);
                if (responseMessage.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<FunctionReturnStatus>(responseMessage.Content.ReadAsStringAsync().Result);
                }
                else
                {
                    throw new HttpException("Error in communication to API");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static FunctionReturnStatus Login(UserLoginParameter loginParameter)
        {
            try
            {
                HttpResponseMessage responseMessage = RequestHelper.PostRequest(WebConstants.DMSAPIURL, WebConstants.LogonAPI, loginParameter, false);
                if (responseMessage.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<FunctionReturnStatus>(responseMessage.Content.ReadAsStringAsync().Result);
                }
                else
                {
                    throw new HttpException("Error in communication to API");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}