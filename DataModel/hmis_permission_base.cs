//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class hmis_permission_base
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public hmis_permission_base()
        {
            this.hmis_link_role_persmissions = new HashSet<hmis_link_role_persmissions>();
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
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<hmis_link_role_persmissions> hmis_link_role_persmissions { get; set; }
    }
}
