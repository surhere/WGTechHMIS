using AttributeRouting.Web.Http;
using BusinessEntities;
using BusinessServices;
using BusinessServices.Service.Interfaces;
using BusinessServices.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using WebApi.Filters;
using WebAPI.ErrorHelper;

namespace WebAPI.Controllers
{
    [ApiAuthenticationFilter]
    public class UserController : ApiController
    {
        private readonly IUserServices _userServices;
        private readonly ITokenServices _tokenServices;
        private readonly IRoleService _roleService;


        public UserController(IUserServices userServices, ITokenServices tokenServices, IRoleService roleService)
        {
            _userServices = userServices;
            _tokenServices = tokenServices;
            _roleService = roleService;
        }
        // GET: api/User
        [HttpGet]
        public IEnumerable<hmisUserBase> Get()
        {
            return _userServices.GetAllUsers(); 
        }

        // GET: api/User/5
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


        // POST: api/User

        /// <summary>
        /// Code for Client Session with a Tocken to be passed on success.
        /// Over all login logic with token generation goes here.
        /// ValidateUser Checks userid and password with DB abd returns user object.
        /// Password hash will be applied from utility. Custom encryption with salt
        /// </summary>
        /// <param name="ss"></param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage Post(hmisUserBase userObject)
        {
            try
            {
                userObject.user_name = "admin";
                var user = _userServices.ValidateUser(userObject.user_name, userObject.password);
                if (user.SID != Guid.Empty)
                {
                    var urole = _roleService.GetUserRoles(user.SID);
                    //foreach (var roles in urole.ToList())
                    //{
                    //    ///Add all roles to the specified user authenticated at the top validatre method
                    //    ///user.hmis_link_user_roles.Add(roles.na)
                    //}

                }
                /// will return user object in Json via IActionresult 
                //return GetUserInformation(ss.UserName,ss.Password);                
            }
            catch (Exception ex)
            {
                //Write Application Log
            }

            //to be blocked after role selection. we will return a single user object having all roles in it
            return GetUserInformation(userObject.user_name, userObject.password);
        }

        //[HttpGet]
        //public async Task<IHttpActionResult> AsyncGet200MsDelay()
        //{
        //    // simulate a delay - could be a database query or another service request
        //    await Task.Delay(200);
        //    //call ValidateUser method asynchronously
        //    //hmisUserBase hmisUserBase = await Task.Run(() => _userServices.ValidateUser("admin", "Test"));
        //    hmisUserBase hmisUserBase =  _userServices.ValidateUser("admin", "Test");
        //    return await Task.FromResult(Ok(hmisUserBase));
            
        //}

        /// <summary>
        /// MVC Login Client
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public HttpResponseMessage GetUserInformation([FromUri]string uname, [FromUri]string pass)
        {
            try
            {
                var user = _userServices.ValidateUser(uname, pass);
                if (user.SID != Guid.Empty)
                {
                    var basicAuthenticationIdentity = Thread.CurrentPrincipal.Identity as BasicAuthenticationIdentity;
                    if (basicAuthenticationIdentity != null)
                        basicAuthenticationIdentity.UserId = user.SID;
                    return GetAuthToken(user);
                }
            }
            catch(Exception ex)
            {
                // Write Application Log
            }
            
            return null;
        }

        /// <summary>
        /// Returns auth token for the validated user.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet] // Or [AcceptVerbs("GET", "POST")]
        private HttpResponseMessage GetAuthToken(hmisUserBase user)
        {
            var token = _tokenServices.GenerateToken(user.SID);
            var response = Request.CreateResponse(HttpStatusCode.OK, "Authorized:" + token.AuthToken + "User"+" :"+ user);
            response.Headers.Add("Token", token.AuthToken);
            response.Headers.Add("TokenExpiry", ConfigurationManager.AppSettings["AuthTokenExpiry"]);
            response.Headers.Add("Access-Control-Expose-Headers", "Token,TokenExpiry");
            //response.Content = new StringContent("hello Subhamay"+Json(user), Encoding.Unicode);
            //var session = System.Web.HttpContext.Current.Session;
            //if(session!=null)
            //{
            //    if(session["AuthUser"]==null)
            //    {
            //        session["AuthUser"] = token;
            //    }
            //}
            return response;
        }
        // PUT: api/User/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/User/5
        public void Delete(int id)
        {
        }
    }
}
