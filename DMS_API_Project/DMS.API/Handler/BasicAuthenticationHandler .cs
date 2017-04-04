using DMS.BL;
using DMS.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace DMS.API
{
    public class BasicAuthenticationHandler : DelegatingHandler
    {
        private const string WWWAuthenticateHeader = "WWW-Authenticate";

        UserBL userBL = null;

        public BasicAuthenticationHandler()
        {
            userBL = new UserBL(ConfigurationManager.ConnectionStrings[0].Name);
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
                                                               CancellationToken cancellationToken)
        {
            if (
                request.RequestUri.ToString().Contains("api/System/GetSystemDropdown") ||
                request.RequestUri.ToString().Contains("api/UserAccount/Login")
                )
            {
                return base.SendAsync(request, cancellationToken); ;
            }

            BasicAuthenticationIdentity credentials = ParseAuthorizationHeader(request);


            if (credentials == null)
            {
                var response = request.CreateErrorResponse(HttpStatusCode.Unauthorized, new Exception("Request is not autheticated"));
                var tsc = new TaskCompletionSource<HttpResponseMessage>();
                tsc.SetResult(response);
                return tsc.Task;
            }
            if (credentials.IsSuccess == false)
            {
                var response = request.CreateErrorResponse(HttpStatusCode.Unauthorized, new Exception("Request is not autheticated"));
                var tsc = new TaskCompletionSource<HttpResponseMessage>();
                tsc.SetResult(response);
                return tsc.Task;
            }

            if (userBL.IsAuthenticateUser(credentials.SystemID, credentials.UserName, credentials.Password) == false)
            {
                var response = request.CreateErrorResponse(HttpStatusCode.Unauthorized, new Exception("Request is not autheticated"));
                var tsc = new TaskCompletionSource<HttpResponseMessage>();
                tsc.SetResult(response);
                return tsc.Task;
            }

            if (credentials != null)
            {
                if (credentials.IsSuccess == true)
                {
                    var principal = new GenericPrincipal(credentials, null);

                    Thread.CurrentPrincipal = principal;
                    if (HttpContext.Current != null)
                        HttpContext.Current.User = principal;
                }
            }

            return base.SendAsync(request, cancellationToken)
                .ContinueWith(task =>
                {
                    var response = task.Result;
                    if (credentials == null && response.StatusCode == HttpStatusCode.Unauthorized)
                        Challenge(request, response);


                    return response;
                });
        }



        /// <summary>
        /// Parses the Authorization header and creates user credentials
        /// </summary>
        /// <param name="actionContext"></param>
        protected virtual BasicAuthenticationIdentity ParseAuthorizationHeader(HttpRequestMessage request)
        {
            string authHeader = null;
            var auth = request.Headers.Authorization;
            if (auth != null && auth.Scheme == "Basic")
                authHeader = auth.Parameter;

            if (string.IsNullOrEmpty(authHeader))
                return null;

            authHeader = Encoding.Default.GetString(Convert.FromBase64String(authHeader));

            //// find first : as password allows for :
            //int idx = authHeader.IndexOf(':');
            //if (idx < 0)
            //    return null;

            //string username = authHeader.Substring(0, idx);
            //string password = authHeader.Substring(idx + 1);

            return userBL.AuthenticateLogonToken(authHeader);
        }

        /// <summary>
        /// Send the Authentication Challenge request
        /// </summary>
        /// <param name="message"></param>
        /// <param name="actionContext"></param>
        void Challenge(HttpRequestMessage request, HttpResponseMessage response)
        {
            var host = request.RequestUri.DnsSafeHost;
            response.Headers.Add(WWWAuthenticateHeader, string.Format("Basic realm=\"{0}\"", host));
        }

    }

}