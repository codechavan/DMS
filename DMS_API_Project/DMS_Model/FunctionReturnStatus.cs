using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.Model
{
    public class FunctionReturnStatus
    {
        public FunctionReturnStatus() { }

        public FunctionReturnStatus(StatusType statusType)
        {
            this.StatusType = statusType;
        }

        public FunctionReturnStatus(StatusType statusType, string message)
        {
            this.StatusType = statusType;
            this.Message = message;
        }

        public FunctionReturnStatus(StatusType statusType, string message, object data)
        {
            this.StatusType = statusType;
            this.Message = message;
            this.Data = data;
        }

        public StatusType StatusType { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }
}
