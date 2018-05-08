using AttributeRouting.Web.Http;
using BusinessEntities;
using BusinessServices;
using BusinessServices.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPI.ActionFilters;

namespace WebAPI.Controllers.Patients
{
    [RoutePrefix("v1/PatientAdmissions/PatientAdmission")]
    [AuthorizationRequired] 
    public class PatientAdmissionController : ApiController
    {

        private readonly IUserServices _userServices;
        private readonly ITokenServices _tokenServices;
        private readonly IRoleService _roleService;
        private readonly IPatientService _patientService;
        private readonly IPatientAdmissionService _patientAdmissionService;


        public PatientAdmissionController(IUserServices userServices, ITokenServices tokenServices, IRoleService roleService, 
            IPatientService patientService, IPatientAdmissionService patientAdmissionService)
        {
            _userServices = userServices;
            _tokenServices = tokenServices;
            _roleService = roleService;
            _patientService = patientService;
            _patientAdmissionService = patientAdmissionService;
        }
        // GET: api/PatientAdmission
        [GET("allpatients")]
        [GET("all")]
        // [ClaimsAuthorizationRequired(ClaimType = "UserManagement", ClaimValue = "1")]
        public IEnumerable<string> Get()
        {
            //var patients = _patientAdmissionService.GetAllPatients();
            //var patientsEntities = patients as List<hmisPatientBase> ?? patients.ToList();
            //if (patientsEntities.Any())
            //    return Request.CreateResponse(HttpStatusCode.OK, patientsEntities);
            //throw new ApiDataException(1000, "Patient not found", HttpStatusCode.NotFound);

            return new string[] { "value1", "value2" };

        }

        // GET: api/PatientAdmission/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/PatientAdmission
        [POST("Create")]
        [POST("Admission")]
        public HttpResponseMessage Post([FromBody] hmisPatientAdmissionBase patientAdmissionEntity)
        {
            string name = ConfigurationManager.AppSettings["AdmissionNumFormat"];
            patientAdmissionEntity.admission_sequence = name;
            var createResult = _patientAdmissionService.AdmitPatient(patientAdmissionEntity);
            if (createResult.Length > 0)
            {
                string[] returnData = createResult.ToString().Split(':');
                patientAdmissionEntity.ID = new Guid(returnData[0].ToString());
                patientAdmissionEntity.admission_sequence = returnData[1].ToString();
                //var patientObject = _patientService.CreatePatientAdditionalInfo(patientEntity);
            }

            return Request.CreateResponse(HttpStatusCode.OK, patientAdmissionEntity.admission_sequence);
        }

        // PUT: api/PatientAdmission/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/PatientAdmission/5
        public void Delete(int id)
        {
        }
    }
}
