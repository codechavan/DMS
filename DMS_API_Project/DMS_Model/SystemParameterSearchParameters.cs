using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.Model
{
    public class SystemParameterSearchParameters
    {
        public long SystemParameterId { get; set; }

        public long SystemId { get; set; }

        public string SystemParameterName { get; set; }
    }
}
