using AutoMapper;
using BusinessEntities;
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

        /// <summary>
        /// Creates a Admission Extesion Entity in DBV
        /// </summary>
        /// <param name="patientAdmissionEntity"></param>
        /// <returns></returns>
        public hmisPatientAdmissionBase PatientAdmissionAdditionalInfo(BusinessEntities.hmisPatientAdmissionBase patientAdmissionEntity)
        {
            List<DataModel.hmis_patient_admission_ext> listAdmissionAdditionalInfo = new List<hmis_patient_admission_ext>();
            Mapper.Reset();
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<hmis_user_base, hmisUserBase>();

            });
            using (var scope = new TransactionScope())
            {
                foreach (var additionalInfo in patientAdmissionEntity.hmis_patient_admission_ext)
                {
                    // context.Products.Add(product);
                    var admissionHMISExt = new DataModel.hmis_patient_admission_ext
                    {
                        ID = Guid.NewGuid(),
                        patient_admission_id = patientAdmissionEntity.ID,
                        attribute_name = additionalInfo.attribute_name,
                        attribute_value = additionalInfo.attribute_value

                    };
                    listAdmissionAdditionalInfo.Add(admissionHMISExt);
                };

                var bulkList = Mapper.Map<List<hmis_patient_admission_ext>>(listAdmissionAdditionalInfo);
                _unitOfWork.PatientAdmissionExtRepository.BulkInsert(listAdmissionAdditionalInfo);
                _unitOfWork.Save();
                scope.Complete();
                return patientAdmissionEntity;
            }
        }

        /// <summary>
        /// Get All users.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public IEnumerable<BusinessEntities.hmisPatientAdmissionBase> GetAllAdmittedPatients()
        {
            var patients = _unitOfWork.PatientAdmissionBaseRepository.GetAll().ToList();
            if (patients.Any())
            {
                Mapper.Reset();
                Mapper.Initialize(cfg =>
                {
                    cfg.CreateMap<hmis_patient_admission_base, hmisPatientBase>();

                });
                // Mapper.CreateMap<hmis_patient_base, hmisPatientBase>();
                var patientModel = Mapper.Map<List<hmis_patient_admission_base>, List<hmisPatientAdmissionBase>>(patients);
                return patientModel;
            }
            return null;
        }

        /// <summary>
        /// Get details of a users with base and extension.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public hmisPatientAdmissionBase GetAdmittedPatientById(Guid patientadmissionId)
        {
            var patient = _unitOfWork.PatientAdmissionBaseRepository.GetByID(patientadmissionId);

            string[] includes = { "hmis_patient_admission_ext" };
            var admission = _unitOfWork.PatientAdmissionBaseRepository.GetWithInclude(c => c.ID == patientadmissionId, includes).ToList();

            if (admission != null)
            {
                Mapper.Reset();
                Mapper.Initialize(cfg =>
                {
                    cfg.CreateMap<hmis_patient_admission_base, hmisPatientAdmissionBase>();
                    cfg.CreateMap<hmis_patient_admission_ext, hmisPatientAdmissionExt>()
                    .ForMember(dest => dest.hmis_patient_admission_base, opt => opt.Ignore());

                });
                // Mapper.CreateMap<hmis_patient_base, hmisPatientBase>();
                var patientModel = Mapper.Map<hmis_patient_admission_base, hmisPatientAdmissionBase>(patient);
                return patientModel;
            }
            return null;
        }
    }
}
