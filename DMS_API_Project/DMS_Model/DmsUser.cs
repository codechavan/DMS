using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.Model
{
    public class DmsUser
    {
        public long UserId { get; set; }

        public long SystemId { get; set; }

        public string SystemName { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string FullName { get; set; }

        public string EmailId { get; set; }

        public long RoleID { get; set; }

        public string RoleName { get; set; }

        public string UserRoleDescription { get; set; }

        public bool IsAdmin { get; set; }

        public bool IsActive { get; set; }

        public bool IsLock { get; set; }

        public DateTime? LastLoginDate { get; set; }

        public long LastPasswordChangedBy { get; set; }

        public DateTime? LastPasswordChangedOn { get; set; }

        public long LastUnblockBy { get; set; }

        public int LoginFailCount { get; set; }

        public long CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public long ModifiedBy { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public string LogonToken { get; set; }
    }
}
