using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities
{
    public class hmisDepartmentMaster
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public hmisDepartmentMaster()
        {
            this.hmis_doctor_master = new HashSet<hmisDoctorMaster>();
        }

        public System.Guid ID { get; set; }
        public string department_name { get; set; }
        public string department_description { get; set; }
        public System.Guid created_by { get; set; }
        public System.Guid modified_by { get; set; }
        public System.DateTime created_on { get; set; }
        public System.DateTime modified_on { get; set; }
        public bool status { get; set; }

        public virtual hmisUserBase hmis_user_base { get; set; }
        public virtual hmisUserBase hmis_user_base1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<hmisDoctorMaster> hmis_doctor_master { get; set; }
    }
}
