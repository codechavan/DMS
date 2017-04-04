using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.Model
{
    public class SystemParameterValue
    {
        public long SystemId { get; set; }
        public long ParameterId { get; set; }
        public string SystemName { get; set; }
        public string ParameterName { get; set; }
        public string Description { get; set; }
        public string DefaultValue { get; set; }
        public string ParameterValue { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public long ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
