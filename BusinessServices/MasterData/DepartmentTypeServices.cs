using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessServices.MasterData.Interfaces;
using DataModel.UnitOfWork;
using System.Transactions;
using DataModel;
using BusinessEntities;
using AutoMapper;

namespace BusinessServices.MasterData
{
    public class DepartmentTypeServices : IDepartmentTypeServices
    {
        private readonly UnitOfWork _unitOfWork;

        /// <summary>
        /// Public constructor.
        /// </summary>
        public DepartmentTypeServices(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Creates a user in DBV
        /// </summary>
        /// <param name="userEntity"></param>
        /// <returns></returns>
        public string CreateDepartmentType(BusinessEntities.hmisDepartmentTypeMaster departmentTypeEntity)
        {
            //int lastNumber = _unitOfWork.PatientBaseRepository.GetLastIndex("index_number", "hmis_patient_base");

            using (var scope = new TransactionScope())
            {
                var departmentTypeHMIS = new hmis_department_type_master
                {
                    ID = Guid.NewGuid(),
                    department_type_name = departmentTypeEntity.department_type_name,
                    description = departmentTypeEntity.description,
                    status =true,
                    created_on = System.DateTime.Now,
                    modified_on = DateTime.Now,               
                    // change by sending from mvc controller using session user ID
                    created_by = new Guid("6418baab-9f1d-4917-9ff6-33bdfc5c49cc"),
                    modified_by = new Guid("6418baab-9f1d-4917-9ff6-33bdfc5c49cc")
                };
                _unitOfWork.DepartmentTypeMasterRepository.Insert(departmentTypeHMIS);
                _unitOfWork.Save();
                scope.Complete();
                return departmentTypeHMIS.ID.ToString();
            }
        }

        /// <summary>
        /// Get All Department Types.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public IEnumerable<hmisDepartmentTypeMaster> GetAllDepartmentTypes()
        {
            var departments = _unitOfWork.DepartmentTypeMasterRepository.GetAll().ToList();
            if (departments.Any())
            {
                Mapper.Reset();
                Mapper.Initialize(cfg =>
                {
                    cfg.CreateMap<hmis_department_type_master, hmisDepartmentTypeMaster>();

                });
                // Mapper.CreateMap<hmis_patient_base, hmisPatientBase>();
                var patientModel = Mapper.Map<List<hmis_department_type_master>, List<hmisDepartmentTypeMaster>>(departments);
                return patientModel;
            }
            return null;
        }
    }
}
