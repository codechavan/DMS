using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.Model
{
    public class DocumentAccess
    {
        public long SystemId { get; set; }
        public DocumentObjectType ObjectType { get; set; }
        public long ObjectId { get; set; }
        public long UserRoleId { get; set; }

        public bool CanRead { get; set; }
        public bool CanWrite { get; set; }
        public bool CanDelete { get; set; }

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
