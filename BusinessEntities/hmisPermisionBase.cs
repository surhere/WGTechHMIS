using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessEntities
{
    public class hmisPermisionBase
    {
        public hmisPermisionBase()
        {
            this.hmisRolePermissions = new HashSet<hmisRolePermissions>();
        }

        public System.Guid permissions_id { get; set; }
        public string permissions_descriptions { get; set; }
        public System.DateTime created_on { get; set; }
        public System.DateTime modified_on { get; set; }
        
        public string access_area { get; set; }
        public bool can_read { get; set; }
        public bool can_update { get; set; }
        public bool can_create { get; set; }
        public bool can_delete { get; set; }
        public bool is_active { get; set; }
        public virtual ICollection<hmisRolePermissions> hmisRolePermissions { get; set; }
    }
}
