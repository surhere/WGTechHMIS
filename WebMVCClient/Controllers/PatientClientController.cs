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
    
    public class PatientClientController : Controller
    {
        // GET: PatientClient
        public ActionResult Index()
        {
            IEnumerable<hmisPatientBase> patientList = null;
            List<hmisPatientBase> hmisPatientBase = new List<hmisPatientBase>();
            HttpResponseMessage response = GlobalVarriables.WebApiClient.GetAsync("Patient").Result;
            //userList = response.Content.ReadAsByteArrayAsync<IEnumerable<hmisUserBase>>().Result;
            if (response.IsSuccessStatusCode)
            {
                //Storing the response details recieved from web api   
                var EmpResponse = response.Content.ReadAsStringAsync().Result;
                //Deserializing the response recieved from web api and storing into the Employee list  
                patientList = JsonConvert.DeserializeObject<List<hmisPatientBase>>(EmpResponse);

            }
            return View(patientList);
        }

        // GET: User
        public ActionResult RegisterPatient()
        {
            return View();
        }

        // GET: User
        /// <summary>
        /// calling api to validate current user login
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult RegisterPatient(hmisPatientBase patientObject)
        {
            //HttpResponseMessage response1 = GlobalVarriables.WebApiClient.PostAsJsonAsync("Admin", user).Result;
            patientObject.patient_registration_no = "CAL-MED-20981";
            HttpResponseMessage response1 = GlobalVarriables.WebApiClient.PostAsJsonAsync("Patient", patientObject).Result;
            if (response1.IsSuccessStatusCode)
            {
                //Storing the response details recieved from web api   
                var EmpResponse = response1.Content.ReadAsStringAsync().Result;

                var readTask = response1.Content.ReadAsAsync<IList<UserEntity>>();
                var Users = JsonConvert.DeserializeObject<List<UserEntity>>(EmpResponse);

                if (Session != null)
                {
                    if (Session["AuthUser"] == null)
                    {
                        Session["AuthUser"] = EmpResponse;
                    }
                }
                GlobalVarriables.WebApiClient.DefaultRequestHeaders.Add("Token", "1");
                ////client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", EmpResponse);
                //Deserializing the response recieved from web api and storing into the Employee list  
                // EmpInfo =  JsonConvert.DeserializeObject<List<hmisUserBase>>(EmpResponse);

            }
            return View();
        }
    }
}