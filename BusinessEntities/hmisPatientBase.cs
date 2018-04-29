using DataModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities
{
    public class hmisPatientBase
    {
        public hmisPatientBase()
        {
            this.hmis_patient_ext = new HashSet<hmisPatientExt>();
        }

        public System.Guid ID { get; set; }
        [Display(Name = "Registration Number")]
        public string patient_registration_no { get; set; }
        [Display(Name = "First Name")]
        public string patient_first_name { get; set; }
        [Display(Name = "Last Name")]
        public string patient_last_name { get; set; }
        [Display(Name = "DOB")]
        public Nullable<System.DateTime> patient_dob { get; set; }
        [Display(Name = "Sex")]
        public string patient_sex { get; set; }
        [Display(Name = "Blood Type")]
        public string patient_blood_type { get; set; }
        [Display(Name = "Phone Number")]
        public string patient_phone { get; set; }
        [Display(Name = "Notes")]
        public string patient_notes { get; set; }
        [Display(Name = "Address")]
        public string patient_address { get; set; }
        [Display(Name = "City")]
        public string patient_city { get; set; }
        public System.DateTime created_on { get; set; }
        public System.DateTime modified_on { get; set; }
        public System.Guid created_by { get; set; }
        public System.Guid modified_by { get; set; }
        [Display(Name = "Additional Information")]

        public string additiona_info { get; set; }
        [Display(Name = "BPL Card Holder")]

        public Nullable<bool> Is_Bpl_holder { get; set; }
        [Display(Name = "Medicine After Effect")]
        public Nullable<bool> Is_Medicine_Adverse_Effect { get; set; }
        [Display(Name = "Past History")]

        public Nullable<bool> Is_Past_History { get; set; }
        [Display(Name = "Refferd By")]

        public Nullable<bool> Is_Reffered_By { get; set; }
        [Display(Name = "Refferd By Doctor")]
        public string Reffered_Doctor { get; set; }
        [Display(Name = "Patient COnsent to registeration")]
        public Nullable<bool> Is_Consent_Signed { get; set; }

        public virtual hmis_user_base hmis_user_base { get; set; }
        public virtual hmis_user_base hmis_user_base1 { get; set; }
        public virtual ICollection<hmisPatientExt> hmis_patient_ext { get; set; }
    }
}
