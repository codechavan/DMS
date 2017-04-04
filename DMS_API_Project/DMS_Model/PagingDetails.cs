using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.Model
{
    public class PagingDetails
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public string Where { get; set; }
        public string OrderBy { get; set; }
    }
}
