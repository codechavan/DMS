using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.Model
{
    public class DocumentSearchData
    {
        public long RecordCount { get; set; }
        
        public IList<Document> LstData { get; set; }

        public DocumentSearchParameter SearchParameter { get; set; }
    }
}
