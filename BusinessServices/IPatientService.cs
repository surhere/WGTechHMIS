using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessEntities;

namespace BusinessServices
{
    public interface IPatientService
    {
        Guid CreatePatient(hmisPatientBase patientEntity);
        IEnumerable<hmisPatientBase> GetAllPatients();

        Guid CreatePatientAdditionalInfo(hmisPatientBase patientEntity);
    }
}
