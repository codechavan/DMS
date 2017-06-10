using DMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DMS.UI
{
    public class SystemParameterHelper
    {
        public SystemParameterHelper()
        {
        }

        private const string SystemParametersSessionName = "DMS.UI.SystemParameters";
        private static IList<SystemParameterValue> SystemParameterValues
        {
            get
            {
                if (HttpContext.Current.Session[SystemParametersSessionName] == null)
                {
                    SystemParameterSearchParameters searchParameters = new SystemParameterSearchParameters();
                    searchParameters.SystemId = SessionHelper.LogonUser.SystemId;
                    SystemParameterValueSearchData data = APIMethods.GetSystemParameterList(searchParameters);
                    if (data.LstData != null)
                    {
                        HttpContext.Current.Session[SystemParametersSessionName] = data.LstData;
                    }
                    else
                    {
                        return null;
                    }
                }
                return (IList<SystemParameterValue>)HttpContext.Current.Session[SystemParametersSessionName];
            }
        }

        public static string GetSystemParameterValue(SystemParameterName parameterName)
        {
            IList<SystemParameterValue> lst = SystemParameterValues.Where(pr => pr.ParameterName == parameterName.ToString()).ToList();
            if (lst.Count == 1)
            {
                return lst[0].ParameterValue;
            }
            return null;
        }

        public enum SystemParameterName
        {
            CAN_UPLOAD_FILE_FROM_UI,
            NUM_OF_FILE_PROPERTIES
        }
    }
}