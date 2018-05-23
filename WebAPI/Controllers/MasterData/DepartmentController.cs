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
    [RoutePrefix("v1/Department/Department")]
    [AuthorizationRequired]
    public class DepartmentController : ApiController
    {
        private readonly IUserServices _userServices;
        private readonly ITokenServices _tokenServices;
        private readonly IRoleService _roleService;
        private readonly IDepartmentServices _departmentService;
        private readonly IDepartmentTypeServices _departmentTypeService;


        public DepartmentController(IUserServices userServices, ITokenServices tokenServices, IRoleService roleService,
            IDepartmentServices departmentService, IDepartmentTypeServices departmentTypeService)
        {
            _userServices = userServices;
            _tokenServices = tokenServices;
            _roleService = roleService;
            _departmentService = departmentService;
            _departmentTypeService = departmentTypeService;
        }
        // GET: api/Department
        public HttpResponseMessage Get()
        {
            //  return new string[] { "value1", "value2" };
            var departments = _departmentService.GetAllDepartments();
            var departmenEntities = departments as List<hmisDepartmentMaster> ?? departments.ToList();
            if (departmenEntities.Any())
                return Request.CreateResponse(HttpStatusCode.OK, departmenEntities);
            throw new ApiDataException(1000, "Department not found", HttpStatusCode.NotFound);
        }

        // GET: api/Department/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Department
        [POST("Create")]
        [POST("Department")]
        public HttpResponseMessage Post([FromBody]hmisDepartmentMaster departmentMsterEntity)
        {
            var createResult = _departmentService.CreateDepartment(departmentMsterEntity);
            if (createResult.Length > 0)
            {
                string[] returnData = createResult.ToString().Split(':');
                departmentMsterEntity.ID = new Guid(returnData[0].ToString());
            }
            return Request.CreateResponse(HttpStatusCode.OK, departmentMsterEntity.ID);
        }

        // PUT: api/Department/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Department/5
        public void Delete(int id)
        {
        }
    }
}
