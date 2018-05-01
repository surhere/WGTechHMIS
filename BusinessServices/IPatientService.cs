using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessEntities;
using DataModel;

namespace BusinessServices
{
    public interface IPatientService
    {
        string CreatePatient(hmisPatientBase patientEntity);
        IEnumerable<hmisPatientBase> GetAllPatients();
        hmisPatientBase GetPatientById(Guid patientId);
        bool UpdatePatient(Guid patientId, BusinessEntities.hmisPatientBase patientEntity);
        hmisPatientBase CreatePatientAdditionalInfo(hmisPatientBase patientEntity);
    }
}
