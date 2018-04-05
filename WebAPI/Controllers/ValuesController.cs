using AttributeRouting.Web.Http;
using BusinessEntities;
using BusinessServices.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.ActionFilters;

namespace WebAPI.Controllers
{
    [AuthorizationRequired]
    [RoutePrefix("v1/Products/Product")]
    public class ValuesController : ApiController
    {
        private readonly IUserAuthenticationService _userServices;
        #region Public Constructor

        /// <summary>
        /// Public constructor to initialize product service instance
        /// </summary>
        public ValuesController(IUserAuthenticationService userServices)
        {
            _userServices = userServices;
        }

        #endregion
       // GET api/values
       [GET("allproducts")]
       [GET("all")]
        public HttpResponseMessage Get()
        {
            var products = _userServices.GetAllUsers();
            var productEntities = products as List<hmisUserBase> ?? products.ToList();
            if (productEntities.Any())
                return Request.CreateResponse(HttpStatusCode.OK, productEntities);
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Products not found");
        }
      

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
