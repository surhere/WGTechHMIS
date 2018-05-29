using BusinessEntities;
using MvcToWebApi;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace WebMVCClient.Controllers
{
    public class DeceaseTypeController : Controller
    {
        // GET: DeceaseType
        public ActionResult Index()
        {
            var Token = "";
            if (Session != null)
            {
                if (Session["AuthUserToken"] != null)
                {
                    Token = Session["AuthUserToken"].ToString();
                }
            }


            GlobalVarriables.WebApiClient.DefaultRequestHeaders.Clear();
            GlobalVarriables.WebApiClient.DefaultRequestHeaders.Add("Token", Token);
            IEnumerable<hmisDeceaseTypeMaster> deceaseTypeList = null;
            List<hmisPatientBase> hmisPatientBase = new List<hmisPatientBase>();
            HttpResponseMessage response = GlobalVarriables.WebApiClient.GetAsync("DeceaseType").Result;
            //userList = response.Content.ReadAsByteArrayAsync<IEnumerable<hmisUserBase>>().Result;
            if (response.IsSuccessStatusCode)
            {
                //Storing the response details recieved from web api   
                var EmpResponse = response.Content.ReadAsStringAsync().Result;
                //Deserializing the response recieved from web api and storing into the Employee list  
                deceaseTypeList = JsonConvert.DeserializeObject<List<hmisDeceaseTypeMaster>>(EmpResponse);
                //Sorting    

                //return Json(new { data = patientList }, JsonRequestBehavior.AllowGet);
            }

            return View(deceaseTypeList);
        }

        // GET: User
        public ActionResult AddDeceaseType()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddDeceaseType(hmisDeceaseTypeMaster DeceaseTypeObject)
        {
            bool status = false;
            if (ModelState.IsValid)
            {
                var Token = "";
                if (Session != null)
                {
                    if (Session["AuthUserToken"] != null)
                    {
                        Token = Session["AuthUserToken"].ToString();
                    }
                }
                GlobalVarriables.WebApiClient.DefaultRequestHeaders.Clear();
                GlobalVarriables.WebApiClient.DefaultRequestHeaders.Add("Token", Token);
                HttpResponseMessage response1 = GlobalVarriables.WebApiClient.PostAsJsonAsync("DeceaseType", DeceaseTypeObject).Result;
                if (response1.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var EmpResponse = response1.Content.ReadAsStringAsync().Result;
                    var readTask = response1.Content.ReadAsAsync<IList<UserEntity>>();
                    // var Users = JsonConvert.DeserializeObject<List<UserEntity>>(EmpResponse);              
                    GlobalVarriables.WebApiClient.DefaultRequestHeaders.Add("Token", "1");
                }

            }
            return View();
        }
    }
}