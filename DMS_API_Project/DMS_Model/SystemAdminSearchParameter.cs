using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.Model
{
    public class SystemAdminSearchParameter
    {
        public long AdminId { get; set; }

        public string UserName { get; set; }

        public string FullName { get; set; }

        public string EmailId { get; set; }
    }
}
