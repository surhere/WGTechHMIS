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
    
    public partial class hsmis_role_base
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public hsmis_role_base()
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
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<hmis_link_role_persmissions> hmis_link_role_persmissions { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<hmis_link_user_roles> hmis_link_user_roles { get; set; }
    }
}
