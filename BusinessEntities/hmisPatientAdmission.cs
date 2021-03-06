﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities
{
    public class hmisPatientAdmissionBase
    {

        //------------------------------------------------------------------------------
        // <auto-generated>
        //     This code was generated from a template.
        //
        //     Manual changes to this file may cause unexpected behavior in your application.
        //     Manual changes to this file will be overwritten if the code is regenerated.
        // </auto-generated>
        //------------------------------------------------------------------------------

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public hmisPatientAdmissionBase()
        {
            this.hmis_patient_admission_ext = new HashSet<hmisPatientAdmissionExt>();
            this.hmis_patient_operation = new HashSet<hmisPatientOperation>();
        }

        public System.Guid ID { get; set; }
        public System.Guid patient_id { get; set; }
        public string admission_type { get; set; }
        public string diagonosed_in { get; set; }
        public string progressive_in_year { get; set; }
        public string from_health_unit { get; set; }
        public Nullable<System.DateTime> discharge_date { get; set; }
        public string discharge_type { get; set; }
        public string discharge_instruction { get; set; }
        public string admission_notes { get; set; }
        public Nullable<bool> Is_allergic { get; set; }
        public Nullable<int> bed_days { get; set; }
        public Nullable<bool> Is_malnutritious { get; set; }
        public string ward_number { get; set; }
        public int index_number { get; set; }
        public string admission_sequence { get; set; }
        public System.DateTime created_on { get; set; }
        public System.DateTime modified_on { get; set; }
        public System.Guid created_by { get; set; }
        public System.Guid modified_by { get; set; }
        public virtual hmisPatientBase hmis_patient_base { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<hmisPatientAdmissionExt> hmis_patient_admission_ext { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<hmisPatientOperation> hmis_patient_operation { get; set; }
    }

    

}
