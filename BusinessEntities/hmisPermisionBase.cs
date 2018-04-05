using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessEntities
{
    public class hmisPermisionBase
    {

        public System.Guid permissions_id { get; set; }
        public string permissions_descriptions { get; set; }
        public System.DateTime created_on { get; set; }
        public System.DateTime modified_on { get; set; }

        public virtual ICollection<hmisRolePermissions> hmis_link_role_persmissions { get; set; }
    }
}
