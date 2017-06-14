using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.Model
{
    public class DocumentFileSearchParameter
    {
        public long FileId { get; set; }
        public long FolderId { get; set; }
        public long SystemId { get; set; }
        public string FileName { get; set; }
        public string FolderName { get; set; }
        public bool? IsDeleted { get; set; }
        public PagingDetails PageDetail { get; set; }
    }
}
