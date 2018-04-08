﻿using DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessEntities
{
    public class hmisUserBase
    {

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
        public virtual ICollection<hmis_link_user_roles> hmis_link_user_roles { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<hmis_user_ext> hmis_user_ext { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Token> Tokens { get; set; }
    }
}
