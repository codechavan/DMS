using System;

namespace DMS.Model
{
    public class SystemAdmin
    {
        public long AdminId { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string FullName { get; set; }

        public string EmailId { get; set; }

        public DateTime? LastLogin { get; set; }

        public long LastPasswordChangedBy { get; set; }

        public DateTime? LastPasswordChangedOn { get; set; }

        public long CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public long ModifiedBy { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public string LogonToken { get; set; }
    }
}
