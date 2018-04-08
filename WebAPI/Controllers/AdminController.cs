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
using WebAPI.ActionFilters;
using WebAPI.ErrorHelper;

namespace WebAPI.Controllers
{
    [AuthorizationRequired]
    [RoutePrefix("v1/Products/Product")]
    public class AdminController : ApiController
    {

        private readonly IUserServices _userServices;
        private readonly ITokenServices _tokenServices;
        private readonly IRoleService _roleService;


        public AdminController(IUserServices userServices, ITokenServices tokenServices, IRoleService roleService)
        {
            _userServices = userServices;
            _tokenServices = tokenServices;
            _roleService = roleService;
        }
        // GET: api/Admin
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Admin/5
        [GET("userid/{id?}")]
        [GET("particularuser/{id?}")]
        [GET("myuser/{id:range(1, 3)}")]
        public HttpResponseMessage Get(Guid id)
        {
            if (id != null)
            {
                var product = _userServices.GetUserById(id);
                if (product != null)
                    return Request.CreateResponse(HttpStatusCode.OK, product);

                throw new ErrorHelper.ApiDataException(1001, "No product found for this id.", HttpStatusCode.NotFound);
            }
            throw new ApiException() { ErrorCode = (int)HttpStatusCode.BadRequest, ErrorDescription = "Bad Request..." };
        }

        // POST api/Admin
        [POST("Create")]
        [POST("Register")]
        public Guid Post([FromBody] hmisUserBase userEntity)
        {
            return _userServices.CreateUser(userEntity);
        }


        // PUT: api/Admin/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Admin/5
        public void Delete(int id)
        {
        }
    }
}
