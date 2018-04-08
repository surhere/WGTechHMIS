using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessEntities;
using MvcToWebApi;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using Newtonsoft.Json.Converters;


namespace WebMVCClient.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            IEnumerable<hmisUserBase> userList;
            List<hmisUserBase> EmpInfo = new List<hmisUserBase>();
            HttpResponseMessage response = GlobalVarriables.WebApiClient.GetAsync("User").Result;
            //userList = response.Content.ReadAsByteArrayAsync<IEnumerable<hmisUserBase>>().Result;
            if (response.IsSuccessStatusCode)
            {
                //Storing the response details recieved from web api   
                var EmpResponse = response.Content.ReadAsStringAsync().Result;
                //Deserializing the response recieved from web api and storing into the Employee list  
                EmpInfo = JsonConvert.DeserializeObject<List<hmisUserBase>>(EmpResponse);

            }
            return View(EmpInfo);
            //GlobalVarriables.WebApiClient.DefaultRequestHeaders.Add("Token", 1);
        }

        public ActionResult Register()
        {
            ViewBag.Message = "Your application Register page.";

            return View();
        }

        public ActionResult Contact()
        {
           // ViewBag.Message = "Your contact page.";

            return View();
        }

        /// <summary>
        /// calling api to validate current user login
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpPost]     
        public ActionResult Login(string username, string password)
        {
            //HttpResponseMessage response = GlobalVarriables.WebApiClient.GetAsync("Authenticate/Authenticate").Result;
            //GlobalVarriables.WebApiClient.DefaultRequestHeaders.Add("Authorization Basic", "Basic admin" + ":" + "Test");
            username = "admin";password = "Test";
            UserEntity usr = new UserEntity();
            usr.UserName = username;
            usr.Password = password;

            

            HttpResponseMessage response1 = GlobalVarriables.WebApiClient.PostAsJsonAsync("User",usr).Result;
            //userList = response.Content.ReadAsByteArrayAsync<IEnumerable<hmisUserBase>>().Result;
            //HttpResponseMessage response = GlobalVarriables.WebApiClient.GetAsync("User?name="+username+"&&pass="+ password).Result;
            //HttpResponseMessage response = GlobalVarriables.WebApiClient.GetAsync("User").Result;


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
                return View(Users);
            }

          
            return RedirectToAction("Index","User");
        }
        
    }
}