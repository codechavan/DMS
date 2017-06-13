using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.Model
{
    public class DmsUserRoleSearchParameter
    {
        public long RoleId { get; set; }

        public long SystemId { get; set; }

        public string SystemName { get; set; }

        public string RoleName { get; set; }

        public string Description { get; set; }

        public PagingDetails PageDetail { get; set; }
    }
}
