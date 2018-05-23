using BusinessServices.MasterData.Interfaces;
using DataModel;
using DataModel.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace BusinessServices.MasterData
{
    public class DoctorServices : IDoctorServices
    {
        private readonly UnitOfWork _unitOfWork;

        /// <summary>
        /// Public constructor.
        /// </summary>
        public DoctorServices(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        /// <summary>
        /// Creates a user in DBV
        /// </summary>
        /// <param name="userEntity"></param>
        /// <returns></returns>
        public string CreateDoctor(BusinessEntities.hmisDoctorMaster doctorEntity)
        {
            //int lastNumber = _unitOfWork.PatientBaseRepository.GetLastIndex("index_number", "hmis_patient_base");

            using (var scope = new TransactionScope())
            {
                var doctorHMIS = new hmis_doctor_master
                {
                    ID = Guid.NewGuid(),
                    first_name = doctorEntity.first_name,
                    last_name = doctorEntity.last_name,
                    email_address = doctorEntity.email_address,
                    address = doctorEntity.address,
                    city = doctorEntity.city,
                    degree = doctorEntity.degree,
                    department_id = doctorEntity.department_id,
                    department_type = doctorEntity.department_type,
                    designation = doctorEntity.designation,
                    phone_number1 = doctorEntity.phone_number1,
                    phone_number2 = doctorEntity.phone_number2,
                    photo = doctorEntity.photo,
                    registration_number = doctorEntity.registration_number,
                    sex = doctorEntity.sex,
                    short_biography = doctorEntity.short_biography,
                    status = true,
                    created_on = System.DateTime.Now,
                    modified_on = DateTime.Now,
                    // change by sending from mvc controller using session user ID
                    created_by = new Guid("6418baab-9f1d-4917-9ff6-33bdfc5c49cc"),
                    modified_by = new Guid("6418baab-9f1d-4917-9ff6-33bdfc5c49cc")
                };
                _unitOfWork.DoctorMasterRepository.Insert(doctorHMIS);
                _unitOfWork.Save();
                scope.Complete();
                return doctorHMIS.ID.ToString();
            }
        }
    }
}
