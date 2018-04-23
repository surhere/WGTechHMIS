using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities
{
    public class hmisPatientBase
    {
        public System.Guid ID { get; set; }
        public string patient_registration_no { get; set; }
        public string patient_first_name { get; set; }
        public string patient_last_name { get; set; }
        public Nullable<System.DateTime> patient_dob { get; set; }
        public string patient_sex { get; set; }
        public string patient_blood_type { get; set; }
        public string patient_phone { get; set; }
        public string patient_notes { get; set; }
        public string patient_address { get; set; }
        public string patient_city { get; set; }
        public System.DateTime created_on { get; set; }
        public System.DateTime modified_on { get; set; }
        public System.Guid created_by { get; set; }
        public System.Guid modified_by { get; set; }
        public string additiona_info { get; set; }

        public virtual hmisUserBase hmisUserBase { get; set; }
        public virtual hmisUserBase hmisUserBase1 { get; set; }
        public virtual hmisPatientExt hmisPatientExt { get; set; }
    }
}
