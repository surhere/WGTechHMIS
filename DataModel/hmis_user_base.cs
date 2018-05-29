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
    
    public partial class hmis_user_base
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public hmis_user_base()
        {
            this.hmis_department_master = new HashSet<hmis_department_master>();
            this.hmis_department_master1 = new HashSet<hmis_department_master>();
            this.hmis_department_type_master = new HashSet<hmis_department_type_master>();
            this.hmis_department_type_master1 = new HashSet<hmis_department_type_master>();
            this.hmis_doctor_master = new HashSet<hmis_doctor_master>();
            this.hmis_doctor_master1 = new HashSet<hmis_doctor_master>();
            this.hmis_patient_admission_base = new HashSet<hmis_patient_admission_base>();
            this.hmis_patient_admission_base1 = new HashSet<hmis_patient_admission_base>();
            this.hmis_patient_base = new HashSet<hmis_patient_base>();
            this.hmis_patient_base1 = new HashSet<hmis_patient_base>();
            this.hmis_patient_operation = new HashSet<hmis_patient_operation>();
            this.hmis_patient_operation1 = new HashSet<hmis_patient_operation>();
            this.hmis_link_user_roles = new HashSet<hmis_link_user_roles>();
            this.hmis_user_ext = new HashSet<hmis_user_ext>();
            this.Tokens = new HashSet<Token>();
            this.hmis_decease_type_master = new HashSet<hmis_decease_type_master>();
            this.hmis_decease_type_master1 = new HashSet<hmis_decease_type_master>();
        }
    
        public System.Guid SID { get; set; }
        public string user_name { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public System.DateTime created_date { get; set; }
        public System.DateTime modified_date { get; set; }
        public string createdby { get; set; }
        public string modifiedby { get; set; }
        public string password { get; set; }
        public Nullable<bool> is_password_reset { get; set; }
        public Nullable<bool> active { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<hmis_department_master> hmis_department_master { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<hmis_department_master> hmis_department_master1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<hmis_department_type_master> hmis_department_type_master { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<hmis_department_type_master> hmis_department_type_master1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<hmis_doctor_master> hmis_doctor_master { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<hmis_doctor_master> hmis_doctor_master1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<hmis_patient_admission_base> hmis_patient_admission_base { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<hmis_patient_admission_base> hmis_patient_admission_base1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<hmis_patient_base> hmis_patient_base { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<hmis_patient_base> hmis_patient_base1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<hmis_patient_operation> hmis_patient_operation { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<hmis_patient_operation> hmis_patient_operation1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<hmis_link_user_roles> hmis_link_user_roles { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<hmis_user_ext> hmis_user_ext { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Token> Tokens { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<hmis_decease_type_master> hmis_decease_type_master { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<hmis_decease_type_master> hmis_decease_type_master1 { get; set; }
    }
}
