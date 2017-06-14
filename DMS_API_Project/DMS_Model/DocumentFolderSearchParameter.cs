using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.Model
{
    public class DocumentFolderSearchParameter
    {
        public long FolderId { get; set; }
        public long SystemId { get; set; }
        public string FolderName { get; set; }
        public long ParentFolderId { get; set; }
        public bool IsDelete { get; set; }
        public PagingDetails PageDetail { get; set; }
    }
}
