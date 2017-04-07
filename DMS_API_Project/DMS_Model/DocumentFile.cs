using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.Model
{
    public class DocumentFile
    {
        public long FileId { get; set; }
        public long FolderId { get; set; }
        public long SystemId { get; set; }
        public string FileName { get; set; }
        public byte[] FileData { get; set; }
        public string FolderName { get; set; }
        public bool IsDeleted { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public long ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string CreatedByUserName { get; set; }
        public string CreatedByUserFullName { get; set; }
        public string ModifiedByUserName { get; set; }
        public string ModifiedByUserFullName { get; set; }
    }
}
