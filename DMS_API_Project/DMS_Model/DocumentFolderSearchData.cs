using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.Model
{
    public class DocumentFolderSearchData
    {
        public long RecordCount { get; set; }
        
        public IList<DocumentFolder> LstData { get; set; }

        public PagingDetails PageDetail { get; set; }
    }
}
