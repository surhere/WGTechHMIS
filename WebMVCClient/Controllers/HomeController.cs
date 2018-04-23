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
using System.Json;

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
        public ActionResult Login(hmisUserBase objUser)
        {
            //HttpResponseMessage response = GlobalVarriables.WebApiClient.GetAsync("Authenticate/Authenticate").Result;
            //GlobalVarriables.WebApiClient.DefaultRequestHeaders.Add("Authorization Basic", "Basic admin" + ":" + "Test");
           string  username = "admin"; string password = "Test";
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(password);
            var converted =  System.Convert.ToBase64String(plainTextBytes).Replace('-','+');
            UserEntity usr = new UserEntity();
            usr.UserName = username;
            usr.Password = password;

            GlobalVarriables.WebApiClient.DefaultRequestHeaders.Clear();
            GlobalVarriables.WebApiClient.DefaultRequestHeaders.Add("Authorization", "Basic " + ("admin" + ":" + "Test"));

            HttpResponseMessage response1 = GlobalVarriables.WebApiClient.PostAsJsonAsync("Authenticate", objUser).Result;
            //userList = response.Content.ReadAsByteArrayAsync<IEnumerable<hmisUserBase>>().Result;
            if (response1.IsSuccessStatusCode)
            {
                //Storing the response details recieved from web api   
                var EmpResponse = response1.Content.ReadAsStringAsync().Result;
                string id = "";
                String[] parts = EmpResponse.Split(':');
                string Message = parts[0];
                string Token = parts[1];
                //  var readTask = response1.Content.ReadAsAsync<IList<UserEntity>>();
                var Users = JsonConvert.DeserializeObject<UserEntity>(EmpResponse);
                HttpHeaders headers = response1.Headers;
                IEnumerable<string> values;
                if (headers.TryGetValues("UserID", out values))
                {
                    id = values.First();
                }
                if (headers.TryGetValues("Token", out values))
                {
                    Token = values.First();
                }
                if (Session != null)
                {
                    if (Session["AuthUserToken"] == null)
                    {
                        Session["AuthUserToken"] = Token;
                    }
                }
                GlobalVarriables.WebApiClient.DefaultRequestHeaders.Clear();
                GlobalVarriables.WebApiClient.DefaultRequestHeaders.Add("Token", Token);             
  
                HttpResponseMessage response = GlobalVarriables.WebApiClient.GetAsync("admin" + "?id=" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    using (HttpContent content = response.Content)
                    {
                        // ... Read the string.
                        Task<string> result = content.ReadAsStringAsync();
                        var res = result.Result;
                        var userData = Json(result);
                        var userInfo = JsonConvert.DeserializeObject<hmisUserBase>(res);
                      
                        if (Session != null)
                        {
                            if (Session["UserInfo"] == null)
                            {
                                Session["UserInfo"] = userInfo;
                                Session["UserName"] = userInfo.last_name + " " + userInfo.first_name;
                            }
                        }
                    }
                }
                //Task<hmisUserBase> result1 = response.Content.ReadAsAsync<hmisUserBase>();
                ////client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", EmpResponse);
                //Deserializing the response recieved from web api and storing into the Employee list  


            }          
            return RedirectToAction("RegisterPatient", "User");
        }
        
    }
}