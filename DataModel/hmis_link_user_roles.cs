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
    
    public partial class hmis_link_user_roles
    {
        public System.Guid user_id { get; set; }
        public System.Guid role_id { get; set; }
        public System.DateTime created_on { get; set; }
        public System.DateTime modified_on { get; set; }
    
        public virtual hmis_role_base hmis_role_base { get; set; }
        public virtual hmis_user_base hmis_user_base { get; set; }
    }
}
