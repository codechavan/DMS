using DMS.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace DMS.UI
{
    public class APIMethods
    {
        private const string APIFailureMessage = "Error in communication to API";

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
                    throw new HttpException(APIFailureMessage);
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
                    throw new HttpException(APIFailureMessage);
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
                    FunctionReturnStatus sts = JsonConvert.DeserializeObject<FunctionReturnStatus>(responseMessage.Content.ReadAsStringAsync().Result);
                    if (sts.StatusType == StatusType.Success && sts.Data != null)
                    {
                        sts.Data = JsonConvert.DeserializeObject<DmsUser>(sts.Data.ToString());
                    }
                    return sts;
                }
                else
                {
                    throw new HttpException(APIFailureMessage);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static DmsUserSearchData GetUserList(DmsUserSearchParameter searchParameter)
        {
            try
            {
                HttpResponseMessage responseMessage = RequestHelper.PostRequest(WebConstants.DMSAPIURL, WebConstants.GetUserListAPI, searchParameter);
                if (responseMessage.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<DmsUserSearchData>(responseMessage.Content.ReadAsStringAsync().Result);
                }
                else
                {
                    throw new HttpException(APIFailureMessage);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static IList<DocumentFolderTree> GetDocumentFolderTree(DocumentFolderTreeSearchParameters searchParameters)
        {
            try
            {
                HttpResponseMessage responseMessage = RequestHelper.PostRequest(WebConstants.DMSAPIURL, WebConstants.GetDocumentFolderTreeAPI, searchParameters);
                if (responseMessage.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<IList<DocumentFolderTree>>(responseMessage.Content.ReadAsStringAsync().Result);
                }
                else
                {
                    throw new HttpException(APIFailureMessage);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static DocumentSearchData GetDocumentObjectList(DocumentSearchParameter searchParameters)
        {
            try
            {
                HttpResponseMessage responseMessage = RequestHelper.PostRequest(WebConstants.DMSAPIURL, WebConstants.GetDocumentObjectListAPI, searchParameters);
                if (responseMessage.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<DocumentSearchData>(responseMessage.Content.ReadAsStringAsync().Result);
                }
                else
                {
                    throw new HttpException(APIFailureMessage);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static FunctionReturnStatus CreateFolder(DocumentFolder folder)
        {
            try
            {
                HttpResponseMessage responseMessage = RequestHelper.PostRequest(WebConstants.DMSAPIURL, WebConstants.CreateFolderAPI, folder);
                if (responseMessage.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<FunctionReturnStatus>(responseMessage.Content.ReadAsStringAsync().Result);
                }
                else
                {
                    throw new HttpException(APIFailureMessage);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static SystemParameterValueSearchData GetSystemParameterList(SystemParameterSearchParameters searchParameters)
        {
            try
            {
                HttpResponseMessage responseMessage = RequestHelper.PostRequest(WebConstants.DMSAPIURL, WebConstants.GetSystemParameterValuesAPI, searchParameters);
                if (responseMessage.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<SystemParameterValueSearchData>(responseMessage.Content.ReadAsStringAsync().Result);
                }
                else
                {
                    throw new HttpException(APIFailureMessage);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static FunctionReturnStatus UploadFile(DocumentFile file)
        {
            try
            {
                HttpResponseMessage responseMessage = RequestHelper.PostRequest(WebConstants.DMSAPIURL, WebConstants.UploadFileAPI, file);
                if (responseMessage.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<FunctionReturnStatus>(responseMessage.Content.ReadAsStringAsync().Result);
                }
                else
                {
                    throw new HttpException(APIFailureMessage);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}