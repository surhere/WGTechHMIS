using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities
{
    public class UserRoles
    {
        public System.Guid userid { get; set; }
        public string rolename { get; set; }
        public bool isactive { get; set; }
        public string roledescription { get; set; }
    }
}
