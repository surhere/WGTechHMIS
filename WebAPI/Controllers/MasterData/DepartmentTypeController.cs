using AttributeRouting.Web.Http;
using BusinessEntities;
using BusinessServices;
using BusinessServices.MasterData.Interfaces;
using BusinessServices.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPI.ActionFilters;
using WebAPI.ErrorHelper;

namespace WebAPI.Controllers.MasterData
{
    [RoutePrefix("v1/DepartmentType/DepartmentType")]
    [AuthorizationRequired]
    public class DepartmentTypeController : ApiController
    {
        private readonly IUserServices _userServices;
        private readonly ITokenServices _tokenServices;
        private readonly IRoleService _roleService;
        private readonly IPatientService _patientService;
        private readonly IDepartmentTypeServices _departmentTypeService;


        public DepartmentTypeController(IUserServices userServices, ITokenServices tokenServices, IRoleService roleService,
            IPatientService patientService, IDepartmentTypeServices departmentTypeService)
        {
            _userServices = userServices;
            _tokenServices = tokenServices;
            _roleService = roleService;
            _patientService = patientService;
            _departmentTypeService = departmentTypeService;
        }
        // GET: api/DepartmentType
        public HttpResponseMessage Get()
        {
            //  return new string[] { "value1", "value2" };
            var patients = _departmentTypeService.GetAllDepartmentTypes();
            var patientsEntities = patients as List<hmisDepartmentTypeMaster> ?? patients.ToList();
            if (patientsEntities.Any())
                return Request.CreateResponse(HttpStatusCode.OK, patientsEntities);
            throw new ApiDataException(1000, "Patient not found", HttpStatusCode.NotFound);

        }

        // GET: api/DepartmentType/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/DepartmentType
        [POST("Create")]
        [POST("DepartmentType")]
        public HttpResponseMessage Post([FromBody] hmisDepartmentTypeMaster hmisDepartmentTypeMaster)
        {
            var createResult = _departmentTypeService.CreateDepartmentType(hmisDepartmentTypeMaster);
            if (createResult.Length > 0)
            {
                string[] returnData = createResult.ToString().Split(':');
                hmisDepartmentTypeMaster.ID = new Guid(returnData[0].ToString());
            }
            return Request.CreateResponse(HttpStatusCode.OK, hmisDepartmentTypeMaster.ID);
        }

        // PUT: api/DepartmentType/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/DepartmentType/5
        public void Delete(int id)
        {
        }
    }
}
