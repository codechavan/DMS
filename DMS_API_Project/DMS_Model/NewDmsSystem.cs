using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.Model
{
    public class NewDmsSystem
    {
        public string SystemName { get; set; }

        public string UserName { get; set; }
        public string EmailId { get; set; }
        public string FullName { get; set; }
        public string Password { get; set; }
        
        public string RoleName { get; set; }
        public string RoleDescription { get; set; }
    }
}
