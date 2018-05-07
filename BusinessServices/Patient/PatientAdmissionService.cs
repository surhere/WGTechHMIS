using DataModel;
using DataModel.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace BusinessServices
{
    public class PatientAdmissionService : IPatientAdmissionService
    {
        private readonly UnitOfWork _unitOfWork;

        /// <summary>
        /// Public constructor.
        /// </summary>
        public PatientAdmissionService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Creates a user in DBV
        /// </summary>
        /// <param name="userEntity"></param>
        /// <returns></returns>
        public string AdmitPatient(BusinessEntities.hmisPatientAdmissionBase patientAdmissionEntity)
        {
            int lastNumber = _unitOfWork.PatientBaseRepository.GetLastIndex("index_number", "[dbo].[hmis_patient_admission_base]");

            using (var scope = new TransactionScope())
            {
                var patientAdmissionHMIS = new hmis_patient_admission_base
                {
                    ID = Guid.NewGuid(),
                    admission_sequence  = patientAdmissionEntity.admission_sequence + (lastNumber + 1),
                    admission_type = patientAdmissionEntity.admission_type,
                    admission_notes = patientAdmissionEntity.admission_notes,
                    patient_id = patientAdmissionEntity.patient_id,
                    ward_number = patientAdmissionEntity.ward_number,
                    diagonosed_in = patientAdmissionEntity.diagonosed_in,
                    progressive_in_year = patientAdmissionEntity.progressive_in_year,
                    from_health_unit = patientAdmissionEntity.from_health_unit,
                    discharge_date = patientAdmissionEntity.discharge_date,
                    discharge_type = patientAdmissionEntity.discharge_type,
                    discharge_instruction = patientAdmissionEntity.discharge_instruction,
                    created_on = System.DateTime.Now,
                    modified_on = DateTime.Now,

                    // change by sending from mvc controller using session user ID
                    created_by = new Guid("6418baab-9f1d-4917-9ff6-33bdfc5c49cc"),
                    modified_by = new Guid("6418baab-9f1d-4917-9ff6-33bdfc5c49cc")


                };
                _unitOfWork.PatientAdmissionBaseRepository.Insert(patientAdmissionHMIS);
                _unitOfWork.Save();
                scope.Complete();
                return patientAdmissionHMIS.ID + ":" + patientAdmissionHMIS.admission_sequence;
            }
        }
    }
}
