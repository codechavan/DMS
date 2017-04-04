using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.Model
{
    public class SystemParameterValueSearchData
    {
        public long RecordCount { get; set; }

        public PagingDetails PageDetail { get; set; }

        public IList<SystemParameterValue> LstData { get; set; }
    }
}
