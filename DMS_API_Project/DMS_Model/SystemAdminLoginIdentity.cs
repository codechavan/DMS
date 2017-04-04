using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace DMS.Model
{
    public class SystemAdminLoginIdentity : GenericIdentity
    {
        public SystemAdminLoginIdentity(long adminId)
            : base(adminId.ToString(), "Basic")
        {
            this.AdminId = adminId;
        }

        public long AdminId { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public DateTime? LoginDate { get; set; }

        public bool IsSuccess { get; set; }

    }
}
