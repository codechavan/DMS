using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.Model
{
    public class DmsSystemSearchParameters
    {
        public long SystemId { get; set; }

        public string SystemName { get; set; }

        public bool? IsActive { get; set; }

        public PagingDetails PageDetail { get; set; }
    }
}
