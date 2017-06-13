using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.Model
{
    public class DmsUserSearchParameter
    {
        public long UserId { get; set; }

        public long UserRoleId { get; set; }

        public long SystemId { get; set; }

        public string UserName { get; set; }

        public string FullName { get; set; }

        public PagingDetails PageDetail { get; set; }
    }
}
