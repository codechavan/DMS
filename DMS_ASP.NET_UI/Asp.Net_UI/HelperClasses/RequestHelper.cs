using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using System.Net;

namespace DMS.UI
{
    public static class RequestHelper
    {
        /// <summary>
        /// Posts the request.
        /// </summary>
        /// <param name="tenantId">The tenant identifier.</param>
        /// <param name="baseURL">The base URL.</param>
        /// <param name="actionPath">The action path.</param>
        /// <param name="requestData">The request data.</param>
        /// <returns>Response fot the post request</returns>
        public static HttpResponseMessage PostRequest(string baseURL, string actionPath, object requestData = null, bool isAuthenticationRequired = true)
        {
            try
            {
                HttpClient client = GetHttpClient();
                client.BaseAddress = new Uri(baseURL);
                if (isAuthenticationRequired)
                {
                    client.DefaultRequestHeaders.Add(WebConstants.SessionTokenIdIdentifier, DateTime.Now.ToString("ddMMyyyyhhmmss"));
                    client.DefaultRequestHeaders.Add(WebConstants.SessionAuthorizationIdentifier,"Basic " + SessionHelper.LogonUserToken);
                }

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.PostAsJsonAsync(baseURL + actionPath, requestData).Result;
                return response;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Gets the authetication information.
        /// </summary>
        /// <returns>Json object for Authetication Inforamtion as Html string</returns>
        public static IHtmlString GetAutheticationInfo()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("{");
            sb.Append("'" + WebConstants.SessionAuthorizationIdentifier + "':'Basic " + SessionHelper.LogonUserToken + "'");
            sb.Append(", '" + WebConstants.SessionTokenIdIdentifier + "':'" + DateTime.Now.ToString("ddMMyyyyhhmmss") + "'");
            sb.Append(", '" + WebConstants.SessionAllowCredentials + "':true");
            
            sb.Append("}");

            return new HtmlString(sb.ToString());
        }

        /// <summary>
        /// Represents method to get Http client object.
        /// </summary>
        /// <returns>The Http Client</returns>
        private static HttpClient GetHttpClient()
        {
            WebRequestHandler handler = new WebRequestHandler();
            //handler.CookieContainer = new System.Net.CookieContainer();
            //handler.UseCookies = true;
            //handler.UseDefaultCredentials = true;

            //// Get Cookie name for FAW 
            //string fawSessionCookieName = System.Configuration.ConfigurationManager.AppSettings["FAWSessionCookieName"];
            ////// Create cookieCaontainer taking values from request context cookie
            //foreach (string cookieKey in HttpContext.Current.Request.Cookies.AllKeys)
            //{
            //    HttpCookie httpcookie = HttpContext.Current.Request.Cookies[cookieKey];
            //    /// Do not send FAW Session Cookie as it will envoke SecuriySession module Begin Request

            //    if (httpcookie.Name.ToLower() != fawSessionCookieName.ToLower())
            //    {
            //        Cookie cookie = new Cookie(httpcookie.Name, httpcookie.Value, httpcookie.Path);
            //        cookie.Domain = HttpContext.Current.Request.Url.Host;
            //        handler.CookieContainer.Add(cookie);
            //    }
            //}
            //// The above step is required to pass authetication information to post url.
            
            HttpClient client = new HttpClient(handler);

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return client;
        }
    }
}