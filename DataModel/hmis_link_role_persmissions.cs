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
    
    public partial class hmis_link_role_persmissions
    {
        public System.Guid role_id { get; set; }
        public System.Guid permission_id { get; set; }
        public System.DateTime created_on { get; set; }
        public System.DateTime modified_on { get; set; }
    
        public virtual hmis_permission_base hmis_permission_base { get; set; }
        public virtual hsmis_role_base hsmis_role_base { get; set; }
    }
}