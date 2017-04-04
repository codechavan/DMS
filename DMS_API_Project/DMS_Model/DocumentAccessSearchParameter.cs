using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.Model
{
    public class DocumentAccessSearchParameter
    {
        public long SystemId { get; set; }
        public DocuementObjectType ObjectType { get; set; }
        public long ObjectId { get; set; }
        public long UserRoleId { get; set; }
    }
}
