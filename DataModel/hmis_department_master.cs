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
    
    public partial class hmis_department_master
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public hmis_department_master()
        {
            this.hmis_doctor_master = new HashSet<hmis_doctor_master>();
        }
    
        public System.Guid ID { get; set; }
        public string department_name { get; set; }
        public string department_description { get; set; }
        public System.Guid created_by { get; set; }
        public System.Guid modified_by { get; set; }
        public System.DateTime created_on { get; set; }
        public System.DateTime modified_on { get; set; }
        public bool status { get; set; }
        public System.Guid departmenttype_id { get; set; }
    
        public virtual hmis_department_type_master hmis_department_type_master { get; set; }
        public virtual hmis_user_base hmis_user_base { get; set; }
        public virtual hmis_user_base hmis_user_base1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<hmis_doctor_master> hmis_doctor_master { get; set; }
    }
}
