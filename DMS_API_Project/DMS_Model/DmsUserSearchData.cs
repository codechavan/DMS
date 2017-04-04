using System.Collections.Generic;

namespace DMS.Model
{
    public class DmsUserSearchData
    {
        public PagingDetails pageDetail { get; set; }

        public long RecordCount { get; set; }

        public IList<DmsUser> LstData { get; set; }
    }
}
