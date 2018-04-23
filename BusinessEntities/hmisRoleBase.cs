using DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessEntities
{
    public class hmisRoleBase
    {
        public hmisRoleBase()
        {
            this.hmis_link_role_persmissions = new HashSet<hmis_link_role_persmissions>();
            this.hmis_link_user_roles = new HashSet<hmis_link_user_roles>();
        }
        public System.Guid role_id { get; set; }
        public string role_name { get; set; }
        public string role_description { get; set; }
        public bool is_active { get; set; }
        public System.DateTime created_on { get; set; }
        public System.DateTime modified_on { get; set; }
        public string created_by { get; set; }
        public string modified_by { get; set; }

        public virtual ICollection<hmis_link_role_persmissions> hmis_link_role_persmissions { get; set; }
        public virtual ICollection<hmis_link_user_roles> hmis_link_user_roles { get; set; }


    }
}
