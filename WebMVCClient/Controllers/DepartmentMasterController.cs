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
    public class DepartmentMasterController : Controller
    {
        // GET: DepartmentMaster
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
            IEnumerable<hmisDepartmentTypeMaster> patientList = null;
            List<hmisPatientBase> hmisPatientBase = new List<hmisPatientBase>();
            HttpResponseMessage response = GlobalVarriables.WebApiClient.GetAsync("DepartmentType").Result;
            //userList = response.Content.ReadAsByteArrayAsync<IEnumerable<hmisUserBase>>().Result;
            if (response.IsSuccessStatusCode)
            {
                //Storing the response details recieved from web api   
                var EmpResponse = response.Content.ReadAsStringAsync().Result;
                //Deserializing the response recieved from web api and storing into the Employee list  
                patientList = JsonConvert.DeserializeObject<List<hmisDepartmentTypeMaster>>(EmpResponse);
                //Sorting    

                //return Json(new { data = patientList }, JsonRequestBehavior.AllowGet);
            }

            return View(patientList);
        }

        // GET: User
        public ActionResult AddDeparetmentType()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddDeparetmentType(hmisDepartmentTypeMaster DepartmentTypeObject)
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
                HttpResponseMessage response1 = GlobalVarriables.WebApiClient.PostAsJsonAsync("DepartmentType", DepartmentTypeObject).Result;
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

        // GET: User
        public ActionResult AddDeparetment()
        {
            List<SelectListItem> mySkills = new List<SelectListItem>() ;
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
            IEnumerable<hmisDepartmentTypeMaster> patientList = null;
            List<hmisPatientBase> hmisPatientBase = new List<hmisPatientBase>();
            HttpResponseMessage response = GlobalVarriables.WebApiClient.GetAsync("DepartmentType").Result;
            //userList = response.Content.ReadAsByteArrayAsync<IEnumerable<hmisUserBase>>().Result;
            if (response.IsSuccessStatusCode)
            {
                //Storing the response details recieved from web api   
                var EmpResponse = response.Content.ReadAsStringAsync().Result;
                //Deserializing the response recieved from web api and storing into the Employee list  
                patientList = JsonConvert.DeserializeObject<List<hmisDepartmentTypeMaster>>(EmpResponse);         
                foreach(var item in patientList)
                {
                    SelectListItem type = new SelectListItem();
                    type.Value = item.ID.ToString();
                    type.Text = item.department_type_name;
                    mySkills.Add(type);
                }
                ViewBag.MySkills = mySkills;
            }

            
            return View();
        }
        public List<SelectListItem> GetDepartmentTypes()
        {
            List<SelectListItem> departmentTypes = new List<SelectListItem>();
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
            IEnumerable<hmisDepartmentTypeMaster> patientList = null;
            List<hmisPatientBase> hmisPatientBase = new List<hmisPatientBase>();
            HttpResponseMessage response = GlobalVarriables.WebApiClient.GetAsync("DepartmentType").Result;
            //userList = response.Content.ReadAsByteArrayAsync<IEnumerable<hmisUserBase>>().Result;
            if (response.IsSuccessStatusCode)
            {
                //Storing the response details recieved from web api   
                var EmpResponse = response.Content.ReadAsStringAsync().Result;
                //Deserializing the response recieved from web api and storing into the Employee list  
                patientList = JsonConvert.DeserializeObject<List<hmisDepartmentTypeMaster>>(EmpResponse);
                foreach (var item in patientList)
                {
                    SelectListItem type = new SelectListItem();
                    type.Value = item.ID.ToString();
                    type.Text = item.department_type_name;
                    departmentTypes.Add(type);
                }
                
            }
            return departmentTypes;
        }

        [HttpPost]
        public ActionResult AddDeparetment(hmisDepartmentMaster DepartmentObject)
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
                string departmentType = Request.Form["DepatmentType"].ToString();
                DepartmentObject.departmenttype_id = new Guid(departmentType);
                GlobalVarriables.WebApiClient.DefaultRequestHeaders.Clear();
                GlobalVarriables.WebApiClient.DefaultRequestHeaders.Add("Token", Token);
                HttpResponseMessage response1 = GlobalVarriables.WebApiClient.PostAsJsonAsync("Department", DepartmentObject).Result;
                if (response1.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var EmpResponse = response1.Content.ReadAsStringAsync().Result;
                    var readTask = response1.Content.ReadAsAsync<IList<UserEntity>>();
                    // var Users = JsonConvert.DeserializeObject<List<UserEntity>>(EmpResponse);            
                    ViewBag.MySkills = GetDepartmentTypes();
                }

            }
            return View();
        }

        public ActionResult LoadDepartment()
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
            IEnumerable<hmisDepartmentMaster> patientList = null;
            List<hmisPatientBase> hmisPatientBase = new List<hmisPatientBase>();
            HttpResponseMessage response = GlobalVarriables.WebApiClient.GetAsync("Department").Result;
            //userList = response.Content.ReadAsByteArrayAsync<IEnumerable<hmisUserBase>>().Result;
            if (response.IsSuccessStatusCode)
            {
                //Storing the response details recieved from web api   
                var EmpResponse = response.Content.ReadAsStringAsync().Result;
                //Deserializing the response recieved from web api and storing into the Employee list  
                patientList = JsonConvert.DeserializeObject<List<hmisDepartmentMaster>>(EmpResponse);
                //Sorting    

                //return Json(new { data = patientList }, JsonRequestBehavior.AllowGet);
            }

            return View(patientList);
        }
    }
}