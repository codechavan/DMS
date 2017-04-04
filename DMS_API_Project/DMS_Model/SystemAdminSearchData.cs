using System;
using System.Collections.Generic;

namespace DMS.Model
{
    public class SystemAdminSearchData
    {
        public long RecordCount { get; set; }

        public PagingDetails PageDetail { get; set; }

        public IList<SystemAdmin> LstData { get; set; }
    }
}
