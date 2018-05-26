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
    public class DoctorMasterController : Controller
    {
        // GET: DoctorMaster
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
            IEnumerable<hmisDoctorMaster> doctorList = null;
            HttpResponseMessage response = GlobalVarriables.WebApiClient.GetAsync("Doctor").Result;
            if (response.IsSuccessStatusCode)
            {
                //Storing the response details recieved from web api   
                var EmpResponse = response.Content.ReadAsStringAsync().Result;
                //Deserializing the response recieved from web api and storing into the Employee list  
                doctorList = JsonConvert.DeserializeObject<List<hmisDoctorMaster>>(EmpResponse);
                //Sorting    

                //return Json(new { data = patientList }, JsonRequestBehavior.AllowGet);
            }

            return View(doctorList);
        }
        // GET: User
        public ActionResult AddDoctor()
        {
            ViewData["Department"] = GetDepartment();
            ViewData["DepartmentType"] = GetDepartmentTypes();
            //ViewBag.DepartmentType = GetDepartmentTypes();
  
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

        public List<SelectListItem> GetDepartment()
        {
            List<SelectListItem> department = new List<SelectListItem>();
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
                foreach (var item in patientList)
                {
                    SelectListItem type = new SelectListItem();
                    type.Value = item.ID.ToString();
                    type.Text = item.department_name;
                    department.Add(type);
                }

            }
            return department;
        }

        [HttpPost]
        public ActionResult AddDoctor(hmisDoctorMaster doctorObject)
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
                string departmentType = Request.Form["DepartmentType"].ToString();
                string department = Request.Form["Department"].ToString();

                doctorObject.department_id = new Guid(department);
                doctorObject.department_type = new Guid(departmentType);

                GlobalVarriables.WebApiClient.DefaultRequestHeaders.Clear();
                GlobalVarriables.WebApiClient.DefaultRequestHeaders.Add("Token", Token);
                HttpResponseMessage response1 = GlobalVarriables.WebApiClient.PostAsJsonAsync("Doctor", doctorObject).Result;
                if (response1.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var EmpResponse = response1.Content.ReadAsStringAsync().Result;
                    var readTask = response1.Content.ReadAsAsync<IList<UserEntity>>();
                    // var Users = JsonConvert.DeserializeObject<List<UserEntity>>(EmpResponse);              

                    ViewBag.department = GetDepartment();
                    ViewBag.departmentType = GetDepartmentTypes();
                }

            }
            return View();
        }

    }
}