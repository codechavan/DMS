using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.Model
{
    public class DmsUserRoleSearchData
    {
        public PagingDetails PageDetail { get; set; }

        public long RecordCount { get; set; }

        public IList<DmsUserRole> LstData { get; set; }
    }
}
