using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace DMS.Model
{
    public class BasicAuthenticationIdentity : GenericIdentity
    {
        public BasicAuthenticationIdentity(long UserId)
            : base(UserId.ToString(), "Basic")
        {
            this.UserId = UserId;
        }

        public long UserId { get; set; }

        public long SystemID { get; set; }

        public string SystemName { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public DateTime? LoginDate { get; set; }

        public bool IsSuccess { get; set; }

    }
}
