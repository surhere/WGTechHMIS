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
    
    public partial class hmis_doctor_master
    {
        public System.Guid ID { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string salutation { get; set; }
        public System.Guid created_by { get; set; }
        public System.Guid modified_by { get; set; }
        public System.DateTime created_on { get; set; }
        public System.DateTime modified_on { get; set; }
        public int phone_number1 { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public string photo { get; set; }
        public string registration_number { get; set; }
        public System.Guid department_id { get; set; }
        public string designation { get; set; }
        public string degree { get; set; }
        public Nullable<int> phone_number2 { get; set; }
        public string email_address { get; set; }
        public Nullable<bool> status { get; set; }
        public string short_biography { get; set; }
        public string sex { get; set; }
        public System.Guid department_type { get; set; }
    
        public virtual hmis_department_master hmis_department_master { get; set; }
        public virtual hmis_department_type_master hmis_department_type_master { get; set; }
        public virtual hmis_user_base hmis_user_base { get; set; }
        public virtual hmis_user_base hmis_user_base1 { get; set; }
    }
}
