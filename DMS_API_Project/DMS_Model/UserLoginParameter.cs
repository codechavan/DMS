using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.Model
{
    public class UserLoginParameter
    {
        public long SystemID { get; set; }

        public string SystemName { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }
    }
}
