using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities
{
    public class hmisPatientOperation
    {

        public System.Guid ID { get; set; }
        public System.Guid patient_admission_id { get; set; }
        public Nullable<System.DateTime> date_of_operation { get; set; }
        public Nullable<int> number_of_nights { get; set; }
        public string proposed_surgery { get; set; }
        public Nullable<bool> consent_on_surgery { get; set; }
        public Nullable<bool> consent_on_admission { get; set; }
        public string notes_on_surgery { get; set; }
        public string consignee_name { get; set; }
        public string consignee_relationship { get; set; }
        public string special_instruction { get; set; }
        public string medical_notes { get; set; }
        public int index_nember { get; set; }

        public virtual hmisPatientAdmissionBase hmis_patient_admission_base { get; set; }
    }
}
