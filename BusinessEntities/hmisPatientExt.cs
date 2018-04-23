using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities
{
    public class hmisPatientExt
    {
        public System.Guid patient_id { get; set; }
        public string attribute_name { get; set; }
        public string attribute_value { get; set; }

        public virtual hmisPatientBase hmisPatientBase { get; set; }
    }
}
