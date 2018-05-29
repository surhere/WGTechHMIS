using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities
{
   public class hmisDeceaseTypeMaster
    {

        public System.Guid ID { get; set; }
        public string decease_type_name { get; set; }
        public string description { get; set; }
        public System.DateTime created_on { get; set; }
        public System.DateTime modified_on { get; set; }
        public System.Guid created_by { get; set; }
        public System.Guid modified_by { get; set; }
        public bool status { get; set; }

        public virtual hmisUserBase hmis_user_base { get; set; }
        public virtual hmisUserBase hmis_user_base1 { get; set; }
    }
}
