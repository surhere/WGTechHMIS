using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessEntities
{
    public class hmisUserExt
    {
        public System.Guid SID { get; set; }
        public string attribute_name { get; set; }

        public virtual hmisUserBase hmis_user_base { get; set; }
    }
}
