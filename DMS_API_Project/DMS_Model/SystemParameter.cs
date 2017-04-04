using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.Model
{
    public class SystemParameter
    {
        public long SystemParameterId { get; set; }
        public string SystemParameterName { get; set; }
        public string ParameterDescription { get; set; }
        public string ParameterDefaultValue { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
