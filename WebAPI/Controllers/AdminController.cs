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
    [RoutePrefix("v1/Users/User")]
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
        [GET("allusers")]
        [GET("all")]
       // [ClaimsAuthorizationRequired(ClaimType = "UserManagement", ClaimValue = "1")]
        public HttpResponseMessage Get()
        {
            var users = _userServices.GetAllUsers();
            var productEntities = users as List<hmisUserBase> ?? users.ToList();
            if (productEntities.Any())
                return Request.CreateResponse(HttpStatusCode.OK, productEntities);
            throw new ApiDataException(1000, "Users not found", HttpStatusCode.NotFound);
        }

        // GET: api/Admin/5
        [GET("userid/{id?}")]
        [GET("particularuser/{id?}")]
        [GET("myuser/{id:range(1, 3)}")]
        public HttpResponseMessage Get(Guid id)
        {
            if (id != null)
            {
                var user = _userServices.GetUserById(id);
                if (user != null)
                    return Request.CreateResponse(HttpStatusCode.OK, user);

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
        [PUT("Update/userid/{id}")]
        [PUT("Modify/userid/{id}")]
        public bool Put(Guid id, [FromBody] hmisUserBase userEntity)
        {
            if (id != Guid.Empty)
            {
                return _userServices.UpdateUser(id, userEntity);
            }
            return false;
        }

        // DELETE: api/Admin/5
        [DELETE("remove/userid/{id}")]
        [DELETE("clear/userid/{id}")]
        [PUT("delete/userid/{id}")]
        public bool Delete(Guid id)
        {
            if (id != null && id != Guid.Empty)
            {
                var isSuccess = _userServices.DeleteUser(id);
                if (isSuccess)
                {
                    return isSuccess;
                }
                throw new ApiDataException(1002, "User is already deleted or not exist in system.", HttpStatusCode.NoContent);
            }
            throw new ApiException() { ErrorCode = (int)HttpStatusCode.BadRequest, ErrorDescription = "Bad Request..." };
        }
    }
}
