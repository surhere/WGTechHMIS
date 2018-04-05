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
using System.Threading;
using System.Web.Http;
using WebApi.Filters;

namespace WebAPI.Controllers
{
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
        [AllowAnonymous]
        public string Get()
        {

            return "value";
        }

        // GET: api/User/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/User
        [HttpPost]
        public HttpResponseMessage Post(UserEntity ss)
        {
            try
            {
                //var user = _userServices.ValidateUser(ss.UserName, ss.Password);
                // _roleService.GetUserRoles(user.SID);
                //return GetUserInformation(ss.UserName,ss.Password);
                               
                }
            catch (Exception ex)
            {

            }

            //to be blocked after role selection. we will return a single user object having all roles in it
            return GetUserInformation(ss.UserName, ss.Password);
        }

        /// <summary>
        /// MVC Login Client
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public HttpResponseMessage GetUserInformation([FromUri]string uname, [FromUri]string pass)
        {
            var user = _userServices.ValidateUser(uname, pass);
            if (user.SID != Guid.Empty)
            {
                var basicAuthenticationIdentity = Thread.CurrentPrincipal.Identity as BasicAuthenticationIdentity;
                if (basicAuthenticationIdentity != null)
                    basicAuthenticationIdentity.UserId = user.SID;
                return GetAuthToken(user);
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
            var session = System.Web.HttpContext.Current.Session;
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
