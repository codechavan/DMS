using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.Model
{
    public class DmsUserRole
    {
        public long RoleId { get; set; }

        public long SystemId { get; set; }

        public string SystemName { get; set; }

        public string RoleName { get; set; }

        public string Description { get; set; }

        public long CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public long ModifiedBy { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}
