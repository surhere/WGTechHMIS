﻿using BusinessEntities;
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
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
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
        public ActionResult Register(hmisUserBase user)
        {
            UserEntity usr = new UserEntity();

            HttpResponseMessage response1 = GlobalVarriables.WebApiClient.PostAsJsonAsync("User", usr).Result;
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
            return View();
        }


    }
}