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

    [RoutePrefix("v1/DeceaseType/DeceaseType")]
    [AuthorizationRequired]
    public class DeceaseTypeController : ApiController
    {
        private readonly IUserServices _userServices;
        private readonly ITokenServices _tokenServices;
        private readonly IRoleService _roleService;

        private readonly IDeceaseTypeServices _deceaseTypeService;


        public DeceaseTypeController(IUserServices userServices, ITokenServices tokenServices, IRoleService roleService,
            IDeceaseTypeServices deceaseTypeService)
        {
            _userServices = userServices;
            _tokenServices = tokenServices;
            _roleService = roleService;
            _deceaseTypeService = deceaseTypeService;
        }
        // GET: api/DeceaseType
        public HttpResponseMessage Get()
        {
            //  return new string[] { "value1", "value2" };
            var patients = _deceaseTypeService.GetAllDeceaseTypes();
            var patientsEntities = patients as List<hmisDeceaseTypeMaster> ?? patients.ToList();
            if (patientsEntities.Any())
                return Request.CreateResponse(HttpStatusCode.OK, patientsEntities);
            throw new ApiDataException(1000, "Patient not found", HttpStatusCode.NotFound);
        }

        // GET: api/DeceaseType/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/DeceaseType
        [POST("Create")]
        [POST("DeseaseType")]
        public HttpResponseMessage Post([FromBody] hmisDeceaseTypeMaster hmisDeseaseTypeMaster)
        {
            var createResult = _deceaseTypeService.CreateDeceaseType(hmisDeseaseTypeMaster);
            if (createResult.Length > 0)
            {
                string[] returnData = createResult.ToString().Split(':');
                hmisDeseaseTypeMaster.ID = new Guid(returnData[0].ToString());
            }
            return Request.CreateResponse(HttpStatusCode.OK, hmisDeseaseTypeMaster.ID);
        }

        // PUT: api/DeceaseType/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/DeceaseType/5
        public void Delete(int id)
        {
        }
    }
}
