using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.Model
{
    public class DocumentSearchParameter
    {
        public long SystemId { get; set; }
        public long ParentFolderId { get; set; }
        public string ObjectName { get; set; }
    }
}
