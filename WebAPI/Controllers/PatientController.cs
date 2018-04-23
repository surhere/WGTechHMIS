using AttributeRouting.Web.Http;
using BusinessEntities;
using BusinessServices;
using BusinessServices.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPI.ErrorHelper;

namespace WebAPI.Controllers
{
    [RoutePrefix("v1/Patients/Patient")]
    public class PatientController : ApiController
    {
        private readonly IUserServices _userServices;
        private readonly ITokenServices _tokenServices;
        private readonly IRoleService _roleService;
        private readonly IPatientService _patientService;

        public PatientController(IUserServices userServices, ITokenServices tokenServices, IRoleService roleService, IPatientService patientService)
        {
            _userServices = userServices;
            _tokenServices = tokenServices;
            _roleService = roleService;
            _patientService = patientService;
        }
        // GET: api/Admin
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
        // POST api/Patient
        [POST("Create")]
        [POST("Register")]
        public Guid Post([FromBody] hmisPatientBase patientEntity)
        {
            return _patientService.CreatePatient(patientEntity);
        }
    }
}
