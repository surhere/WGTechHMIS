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
using WebAPI.ErrorHelper;

namespace WebAPI.Controllers
{
    [RoutePrefix("v1/Patients/Patient")]
    [AuthorizationRequired]
    public class PatientController : ApiController
    {
        private readonly IUserServices _userServices;
        private readonly ITokenServices _tokenServices;
        private readonly IRoleService _roleService;
        private readonly IDeceaseTypeService _patientService;

        public PatientController(IUserServices userServices, ITokenServices tokenServices, IRoleService roleService, IDeceaseTypeService patientService)
        {
            _userServices = userServices;
            _tokenServices = tokenServices;
            _roleService = roleService;
            _patientService = patientService;
        }
        // GET: api/Patient
        [GET("allpatients")]
        [GET("all")]
        // [ClaimsAuthorizationRequired(ClaimType = "UserManagement", ClaimValue = "1")]
        public HttpResponseMessage Get()
        {
            var patients = _patientService.GetAllPatients();
            var patientsEntities = patients as List<hmisPatientBase> ?? patients.ToList();
            if (patientsEntities.Any())
                return Request.CreateResponse(HttpStatusCode.OK, patientsEntities);
            throw new ApiDataException(1000, "Patient not found", HttpStatusCode.NotFound);
        }
        // GET: api/patient/5
        [GET("patient/{id?}")]
        [GET("particularuser/{id?}")]
        [GET("myuser/{id:range(1, 3)}")]
        public HttpResponseMessage Get(Guid id)
        {
            if (id != null)
            {
                var patient = _patientService.GetPatientById(id);
                if (patient != null)
                    return Request.CreateResponse(HttpStatusCode.OK, patient);

                throw new ErrorHelper.ApiDataException(1001, "No product found for this id.", HttpStatusCode.NotFound);
            }
            throw new ApiException() { ErrorCode = (int)HttpStatusCode.BadRequest, ErrorDescription = "Bad Request..." };
        }
        // POST api/Patient
        [POST("Create")]
        [POST("Register")]
        public HttpResponseMessage Post([FromBody] hmisPatientBase patientEntity)
        {
            string name = ConfigurationManager.AppSettings["ResgistrationNumFormat"];
            patientEntity.patient_registration_no = name;
            var createResult = _patientService.CreatePatient(patientEntity);
            if (createResult.Length > 0)
            {
                string[] returnData = createResult.ToString().Split(':');
                patientEntity.ID = new Guid(returnData[0].ToString());
                patientEntity.patient_registration_no = returnData[1].ToString();
             //   _patientService.CreatePatientAdditionalInfo(patientEntity);
                var patientObject = _patientService.CreatePatientAdditionalInfo(patientEntity);
            }

            return Request.CreateResponse(HttpStatusCode.OK, patientEntity.patient_registration_no);
        }

        // PUT: api/Admin/5
        [PUT("Update/patientid/{id}")]
        [PUT("Modify/patientid/{id}")]
        public bool Put([FromBody] hmisPatientBase patientEntity)
        {
            if (patientEntity.ID != Guid.Empty)
            {
                return _patientService.UpdatePatient(patientEntity.ID, patientEntity);
            }
            return false;
        }
    }
}
