using BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices
{
    public interface IPatientAdmissionService
    {
        string AdmitPatient(hmisPatientAdmissionBase patientEntity);
       // IEnumerable<hmisPatientAdmissionBase> GetAllAdmittedPatients();
       // hmisPatientBase GetAdmittedPatientById(Guid patientId);
       // bool UpdateAdmisionPatient(Guid patientId, BusinessEntities.hmisPatientAdmissionBase patientEntity);
       // hmisPatientBase CreateAdmissionAdditionalInfo(hmisPatientAdmissionBase patientEntity);
    }
}
