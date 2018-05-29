using BusinessServices.MasterData.Interfaces;
using DataModel.UnitOfWork;
using DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using BusinessEntities;
using AutoMapper;

namespace BusinessServices.MasterData
{
    public class DeceaseTypeServices :IDeceaseTypeServices
    {
        private readonly UnitOfWork _unitOfWork;

        /// <summary>
        /// Public constructor.
        /// </summary>
        public DeceaseTypeServices(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Creates a user in DBV
        /// </summary>
        /// <param name="userEntity"></param>
        /// <returns></returns>
        public string CreateDeceaseType(BusinessEntities.hmisDeceaseTypeMaster departmentTypeEntity)
        {
            using (var scope = new TransactionScope())
            {
                var deceaseTypeHMIS = new hmis_decease_type_master
                {
                    ID = Guid.NewGuid(),
                    decease_type_name = departmentTypeEntity.decease_type_name,
                    description = departmentTypeEntity.description,
                    status = true,
                    created_on = System.DateTime.Now,
                    modified_on = DateTime.Now,
                    // change by sending from mvc controller using session user ID
                    created_by = new Guid("6418baab-9f1d-4917-9ff6-33bdfc5c49cc"),
                    modified_by = new Guid("6418baab-9f1d-4917-9ff6-33bdfc5c49cc")
                };
                _unitOfWork.DeceaseTypeMasterRepository.Insert(deceaseTypeHMIS);
                _unitOfWork.Save();
                scope.Complete();
                return deceaseTypeHMIS.ID.ToString();
            }
        }

        /// <summary>
        /// Get All Department Types.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public IEnumerable<hmisDeceaseTypeMaster> GetAllDeceaseTypes()
        {
            var deceaseTypes = _unitOfWork.DeceaseTypeMasterRepository.GetAll().ToList();
            if (deceaseTypes.Any())
            {
                Mapper.Reset();
                Mapper.Initialize(cfg =>
                {
                    cfg.CreateMap<hmis_decease_type_master, hmisDeceaseTypeMaster>();

                });
                // Mapper.CreateMap<hmis_patient_base, hmisPatientBase>();
                var patientModel = Mapper.Map<List<hmis_decease_type_master>, List<hmisDeceaseTypeMaster>>(deceaseTypes);
                return patientModel;
            }
            return null;
        }
    }   
}
