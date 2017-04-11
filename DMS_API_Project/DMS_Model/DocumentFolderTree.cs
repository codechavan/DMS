using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.Model
{
    public class DocumentFolderTree
    {
        public long FolderId { get; set; }
        public string FolderName { get; set; }
        public long ParentFolderId { get; set; }
        public List<DocumentFolderTree> ChildFolders { get; set; }
    }
}
