using AutoMapper;
using BusinessEntities;
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
    public class DepartmentServices : IDepartmentServices
    {
        private readonly UnitOfWork _unitOfWork;

        /// <summary>
        /// Public constructor.
        /// </summary>
        public DepartmentServices(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Creates a user in DBV
        /// </summary>
        /// <param name="userEntity"></param>
        /// <returns></returns>
        public string CreateDepartment(BusinessEntities.hmisDepartmentMaster departmentEntity)
        {
            //int lastNumber = _unitOfWork.PatientBaseRepository.GetLastIndex("index_number", "hmis_patient_base");

            using (var scope = new TransactionScope())
            {
                var departmentHMIS = new hmis_department_master
                {
                    ID = Guid.NewGuid(),
                    department_name = departmentEntity.department_name,
                    department_description = departmentEntity.department_description,
                    status = true,
                    departmenttype_id = departmentEntity.departmenttype_id,
                    created_on = System.DateTime.Now,
                    modified_on = DateTime.Now,
                    // change by sending from mvc controller using session user ID
                    created_by = new Guid("6418baab-9f1d-4917-9ff6-33bdfc5c49cc"),
                    modified_by = new Guid("6418baab-9f1d-4917-9ff6-33bdfc5c49cc")
                };
                _unitOfWork.DepartmentMasterRepository.Insert(departmentHMIS);
                _unitOfWork.Save();
                scope.Complete();
                return departmentHMIS.ID.ToString();
            }
        }

        /// <summary>
        /// Get All Department .
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public IEnumerable<hmisDepartmentMaster> GetAllDepartments()
        {
            var departments = _unitOfWork.DepartmentMasterRepository.GetAll().ToList();
            if (departments.Any())
            {
                Mapper.Reset();
                Mapper.Initialize(cfg =>
                {
                    cfg.CreateMap<hmis_department_master, hmisDepartmentMaster>();

                });
                // Mapper.CreateMap<hmis_patient_base, hmisPatientBase>();
                var patientModel = Mapper.Map<List<hmis_department_master>, List<hmisDepartmentMaster>>(departments);
                return patientModel;
            }
            return null;
        }
    }
}
