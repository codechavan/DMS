using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.Model
{
    public class DocumentAccessDetail
    {
        public long SystemId { get; set; }
        public DocumentObjectType ObjectType { get; set; }
        public long ObjectId { get; set; }
        public long UserId { get; set; }

        public bool CanRead { get; set; }
        public bool CanWrite { get; set; }
        public bool CanDelete { get; set; }

        public bool IsInhereted { get; set; }
        public long InheretedFolderId { get; set; }
        public string InheretedFolderName{ get; set; }
        
    }
}
