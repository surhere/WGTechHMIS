using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessEntities
{
    public class hmisRolePermissions
    {
        public System.Guid role_id { get; set; }
        public System.Guid permission_id { get; set; }
        public System.DateTime created_on { get; set; }
        public System.DateTime modified_on { get; set; }

        public virtual hmisPermisionBase hmis_permission_base { get; set; }
        public virtual hmisRoleBase hsmis_role_base { get; set; }

    }
}
