using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AttributeRouting.Web.Http;
using WebApi.Filters;
using BusinessServices.Services;
using System;
using BusinessEntities;
using System.Web;

namespace WebApi.Controllers
{
    [ApiAuthenticationFilter]
    public class AuthenticateController : ApiController
    {
        #region Private variable.

        private readonly ITokenServices _tokenServices;

        #endregion

        #region Public Constructor

        /// <summary>
        /// Public constructor to initialize product service instance
        /// </summary>
        public AuthenticateController(ITokenServices tokenServices)
        {
            _tokenServices = tokenServices;
        }

        #endregion

       /// <summary>
       /// Authenticates user and returns token with expiry.
       /// </summary>
       /// <returns></returns>
        [POST("login")]
        [POST("authenticate")]
        [POST("get/token")]
        public HttpResponseMessage Authenticate()
        {
            if (System.Threading.Thread.CurrentPrincipal!=null && System.Threading.Thread.CurrentPrincipal.Identity.IsAuthenticated)
            {
                var basicAuthenticationIdentity = System.Threading.Thread.CurrentPrincipal.Identity as BasicAuthenticationIdentity;
                if (basicAuthenticationIdentity != null)
                {
                    var userId = basicAuthenticationIdentity.UserId;
                    return GetAuthToken(userId);
                }
            }
           return null;
        }
               
        /// <summary>
        /// Returns auth token for the validated user.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        private HttpResponseMessage GetAuthToken(Guid userId)
        {
            LoginResponseObject obj = new LoginResponseObject();
          

            var token = _tokenServices.GenerateToken(userId);
            obj.Authorized = "Authorized:";
            obj.access_token = token.AuthToken;
            obj.userName = userId.ToString();
            var response = Request.CreateResponse(HttpStatusCode.OK, obj);
            response.Headers.Add("Token", token.AuthToken);
            response.Headers.Add("UserID", userId.ToString());
            response.Headers.Add("TokenExpiry", ConfigurationManager.AppSettings["AuthTokenExpiry"]);
            response.Headers.Add("Access-Control-Expose-Headers", "Token,TokenExpiry" );
            //response.Content.Headers.Add("access_token", token.AuthToken);
            //response.Content.Headers.Add("userName", userId.ToString());
            var session = HttpContext.Current.Session; 
            //if(session!=null)
            //{
            //    if(session["AuthUser"]==null)
            //    {
            //        session["AuthUser"] = token;
            //    }
            //}
            return response;
        }
        public class LoginResponseObject
        {
            public string userName { get; set; }
            public string Authorized { get; set; }
            public string access_token { get; set; }
            hmisUserBase SessionUserData { get; set; }
        }
    }
}
