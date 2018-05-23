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

namespace WebAPI.Controllers.MasterData
{
    [RoutePrefix("v1/Department/Department")]
    [AuthorizationRequired]
    public class DoctorController : ApiController
    {

        private readonly IUserServices _userServices;
        private readonly ITokenServices _tokenServices;
        private readonly IRoleService _roleService;
        private readonly IDoctorServices _doctorService;
        private readonly IDepartmentTypeServices _departmentTypeService;


        public DoctorController(IUserServices userServices, ITokenServices tokenServices, IRoleService roleService,
            IDoctorServices doctorervice, IDepartmentTypeServices departmentTypeService)
        {
            _userServices = userServices;
            _tokenServices = tokenServices;
            _roleService = roleService;
            _doctorService = doctorervice;
            _departmentTypeService = departmentTypeService;
        }
        // GET: api/Doctor

        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Doctor/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Doctor
        public HttpResponseMessage Post([FromBody]hmisDoctorMaster doctorEntity)
        {
            var createResult = _doctorService.CreateDoctor(doctorEntity);
            if (createResult.Length > 0)
            {
                string[] returnData = createResult.ToString().Split(':');
                doctorEntity.ID = new Guid(returnData[0].ToString());
            }
            return Request.CreateResponse(HttpStatusCode.OK, doctorEntity.ID);
        }

        // PUT: api/Doctor/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Doctor/5
        public void Delete(int id)
        {
        }
    }
}
