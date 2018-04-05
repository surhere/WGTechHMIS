using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessEntities
{
    public class hmisRoleBase
    {

        public System.Guid role_id { get; set; }
        public string role_name { get; set; }
        public string role_description { get; set; }
        public bool is_active { get; set; }
        public System.DateTime created_on { get; set; }
        public System.DateTime modified_on { get; set; }
        public string created_by { get; set; }
        public string modified_by { get; set; }

        public virtual ICollection<hmisRolePermissions> hmis_link_role_persmissions { get; set; }
        public virtual ICollection<hmisUserRoles> hmis_link_user_roles { get; set; }
    }
}
