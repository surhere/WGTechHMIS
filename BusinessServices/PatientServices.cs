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
    public class PatientServices : IPatientService
    {
        private readonly UnitOfWork _unitOfWork;

        /// <summary>
        /// Public constructor.
        /// </summary>
        public PatientServices(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        /// <summary>
        /// Creates a user in DBV
        /// </summary>
        /// <param name="userEntity"></param>
        /// <returns></returns>
        public string CreatePatient(BusinessEntities.hmisPatientBase patientEntity)
        {
           int lastNumber = _unitOfWork.PatientBaseRepository.GetLastIndex("index_number", "hmis_patient_base");

            using (var scope = new TransactionScope())
            {
                var patientHMIS = new hmis_patient_base
                {
                    ID = Guid.NewGuid(),
                    patient_registration_no = patientEntity.patient_registration_no + (lastNumber+1),
                    patient_first_name = patientEntity.patient_first_name,
                    patient_last_name = patientEntity.patient_last_name,
                    patient_dob = patientEntity.patient_dob,
                    patient_sex = patientEntity.patient_sex,
                    patient_blood_type = patientEntity.patient_blood_type,
                    patient_phone = patientEntity.patient_phone,
                    patient_notes = patientEntity.patient_notes,
                    patient_address = patientEntity.patient_address,
                    patient_city = patientEntity.patient_city,
                    additiona_info = patientEntity.additiona_info,
                    created_on = System.DateTime.Now,
                    modified_on = DateTime.Now,

                    // change by sending from mvc controller using session user ID
                    created_by = new Guid("6418baab-9f1d-4917-9ff6-33bdfc5c49cc") ,
                    modified_by = new Guid("6418baab-9f1d-4917-9ff6-33bdfc5c49cc")


                };
                _unitOfWork.PatientBaseRepository.Insert(patientHMIS);
                _unitOfWork.Save();              
                scope.Complete();
                return patientHMIS.ID +":"+ patientHMIS.patient_registration_no;
            }
        }

        /// <summary>
        /// Creates a patient Entity in DBV
        /// </summary>
        /// <param name="patientEntity"></param>
        /// <returns></returns>
        public hmisPatientBase CreatePatientAdditionalInfo(BusinessEntities.hmisPatientBase patientEntity)
        {
            //ICollection<DataModel.hmis_patient_ext> listPatientAdditionalInfo = 
            List<DataModel.hmis_patient_ext> listPatientAdditionalInfo = new List<hmis_patient_ext>();
            Mapper.Reset();
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<hmis_user_base, hmisUserBase>();

            });
            using (var scope = new TransactionScope())
            {               
                foreach (var additionalInfo in patientEntity.hmis_patient_ext)
                {
                    // context.Products.Add(product);
                    var patientHMISExt = new DataModel.hmis_patient_ext
                    {
                        ID = Guid.NewGuid(),
                        patient_id = patientEntity.ID,
                        attribute_name = additionalInfo.attribute_name,
                        attribute_value = additionalInfo.attribute_value

                    };
                   
                     listPatientAdditionalInfo.Add(patientHMISExt);
                };
               
                var bulkList = Mapper.Map<List<hmis_patient_ext>>(listPatientAdditionalInfo);
                //var books = new List<hmis_patient_ext> {
                //            new hmis_patient_ext { patient_id = patientEntity.ID, attribute_name = "Carrie",attribute_value="author",ID=Guid.NewGuid() },
                //            new hmis_patient_ext { patient_id = patientEntity.ID, attribute_name = "Carrie2",attribute_value="author2",ID=Guid.NewGuid() },
                //            new hmis_patient_ext { patient_id = patientEntity.ID, attribute_name = "Carrie3",attribute_value="author3",ID=Guid.NewGuid() }
                //        };
                _unitOfWork.PatientExtRepository.BulkInsert(listPatientAdditionalInfo);
                _unitOfWork.Save();
                scope.Complete();
                return patientEntity;
            }
        }
        /// <summary>
        /// Get All users.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public IEnumerable<BusinessEntities.hmisPatientBase> GetAllPatients()
        {
            var patients = _unitOfWork.PatientBaseRepository.GetAll().ToList();
            if (patients.Any())
            {
                Mapper.Reset();
                Mapper.Initialize(cfg =>
                {
                    cfg.CreateMap<hmis_patient_base, hmisPatientBase>();

                });
                // Mapper.CreateMap<hmis_patient_base, hmisPatientBase>();
                var patientModel = Mapper.Map<List<hmis_patient_base>, List<hmisPatientBase>>(patients);
                //foreach(var patientModel in patients)
                //{

                //}
                 return patientModel;
            }
            return null;
        }

        /// <summary>
        /// Get All users.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public hmisPatientBase GetPatientById(Guid patientId)
        {
            var patient = _unitOfWork.PatientBaseRepository.GetByID(patientId);
            if (patient!=null)
            {
                Mapper.Reset();
                Mapper.Initialize(cfg =>
                {
                    cfg.CreateMap<hmis_patient_base, hmisPatientBase>();

                });
                // Mapper.CreateMap<hmis_patient_base, hmisPatientBase>();
                var patientModel = Mapper.Map<hmis_patient_base, hmisPatientBase>(patient);
                //foreach(var patientModel in patients)
                //{

                //}
                return patientModel;
            }
            return null;
        }


        /// <summary>
        /// Updates a patient
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="patientEntity"></param>
        /// <returns></returns>
        public bool UpdatePatient(Guid patientId, BusinessEntities.hmisPatientBase patientEntity)
        {
            var success = false;
            if (patientEntity != null)
            {
                using (var scope = new TransactionScope())
                {
                    var patient = _unitOfWork.PatientBaseRepository.GetByID(patientId);
                    if (patient != null)
                    {
                        patient.ID = patientEntity.ID;
                        _unitOfWork.PatientBaseRepository.Update(patient);
                        _unitOfWork.Save();
                        scope.Complete();
                        success = true;
                    }
                }
            }
            return success;
        }
    }
}
