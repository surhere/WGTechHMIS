using BusinessEntities;
using MvcToWebApi;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace WebMVCClient.Controllers
{
    
    public class PatientClientController : Controller
    {

        public ActionResult LoadPatient()
        {
            var Token = "";
            if (Session != null)
            {
                if (Session["AuthUserToken"] != null)
                {
                    Token = Session["AuthUserToken"].ToString();
                }
            }

            //var draw = Request.Form.GetValues("draw").FirstOrDefault();
            //var start = Request.Form.GetValues("start").FirstOrDefault();
            //var length = Request.Form.GetValues("length").FirstOrDefault();
            //var sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
            //var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();
            //var searchValue = Request.Form.GetValues("search[value]").FirstOrDefault();


            ////Paging Size (10,20,50,100)    
            //int pageSize = length != null ? Convert.ToInt32(length) : 0;
            //int skip = start != null ? Convert.ToInt32(start) : 0;
            //int recordsTotal = 0;
            GlobalVarriables.WebApiClient.DefaultRequestHeaders.Clear();
            GlobalVarriables.WebApiClient.DefaultRequestHeaders.Add("Token", Token);
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
                //Sorting    
                //if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
                //{
                //   // patientList = patientList.OrderBy((sortColumn + " " + sortColumnDir);
                //}
                ////Search    
                //if (!string.IsNullOrEmpty(searchValue))
                //{
                //    patientList = patientList.Where(m => m.patient_last_name == searchValue);
                //}

                ////total number of rows count     
                //recordsTotal = patientList.Count();
                ////Paging     
                //var data = patientList.Skip(skip).Take(pageSize).ToList();
                ////Returning Json Data    
                //return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });
                // return Json(new { data = patientList }, JsonRequestBehavior.AllowGet);
                return Json(new { data = patientList }, JsonRequestBehavior.AllowGet);
            }

            return null;

        }
        // GET: PatientClient
        public ActionResult Index()
        {           
            return View();
        }

        // GET: User
        public ActionResult RegisterPatient()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Save(Guid id)
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
            HttpResponseMessage response = GlobalVarriables.WebApiClient.GetAsync("Patient" + "?id=" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                using (HttpContent content = response.Content)
                {
                    // ... Read the string.
                    Task<string> result = content.ReadAsStringAsync();
                    var res = result.Result;
                    var userData = Json(result);
                    JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
                    dynamic patientInfo = jsonSerializer.Deserialize<dynamic>(res);                
                    return View(PopulateData(patientInfo));
                }
            }
            return HttpNotFound();
        }

        public hmisPatientBase PopulateData(dynamic patientInfo)
        {
            var patientUnit = new hmisPatientBase();
            patientUnit = new hmisPatientBase
            {
                ID = patientInfo["ID"] == null ? Guid.Empty : new Guid(patientInfo["ID"].ToString()),
                patient_city = patientInfo["patient_city"] == null ? "" : patientInfo["patient_city"].ToString() ?? "" ,
                patient_first_name = patientInfo["patient_first_name"] == null ? "" : patientInfo["patient_first_name"].ToString() ?? "",
                patient_last_name = patientInfo["patient_last_name"] == null ? "" : patientInfo["patient_last_name"].ToString() ?? "",
                patient_registration_no = patientInfo["patient_registration_no"] == null ? "" : patientInfo["patient_registration_no"].ToString() ?? "",
                patient_address = patientInfo["patient_address"] == null ? "" : patientInfo["patient_address"].ToString() ?? "",
                patient_dob = patientInfo["patient_dob"] == null ? null : Convert.ToDateTime(patientInfo["patient_dob"].ToString()),
                patient_blood_type = patientInfo["patient_blood_type"] == null ? "" : patientInfo["patient_blood_type"].ToString() ?? "",
                patient_sex = patientInfo["patient_sex"] == null ? "" : patientInfo["patient_sex"].ToString() ?? "",
                patient_phone = patientInfo["patient_phone"] == null ? "" : patientInfo["patient_phone"].ToString() ?? "",
                Reffered_Doctor = patientInfo["Reffered_Doctor"] == null ? "" : patientInfo["Reffered_Doctor"].ToString() ?? "",
                patient_notes = patientInfo["patient_notes"] == null ? false : patientInfo["patient_notes"].ToString(),
                Is_Bpl_holder = patientInfo["Is_Bpl_holder"] == null ? false : patientInfo["Is_Bpl_holder"].ToString(),
                Is_Consent_Signed = patientInfo["Is_Bpl_holder"] == null ? false : patientInfo["Is_Bpl_holder"].ToString()
            };
            return patientUnit;

        }

        [HttpPost]
        public ActionResult Save(hmisPatientBase patientObject)
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
                var Name = Request.Form.Get("RefferedDoctor");
                Dictionary<string, string> getAllAdditionalInfo = new Dictionary<string, string>();
                foreach (string key in Request.Form.AllKeys)
                {
                    if (key.Contains("RefferedDoctor") && key.Equals("RefferedDoctor"))
                    {
                        getAllAdditionalInfo.Add("RefferedDoctor", Request.Form[key]);
                        var patientHMISExt = new hmisPatientExt
                        {
                            attribute_name = "RefferedDoctor",
                            attribute_value = Request.Form[key]
                        };
                        patientObject.hmis_patient_ext.Add(patientHMISExt);
                    }

                    if (key.Contains("MothersName") && key.Equals("MothersName"))
                    {
                        getAllAdditionalInfo.Add("MothersName", Request.Form[key]);
                        var patientHMISExt = new hmisPatientExt
                        {
                            attribute_name = "MothersName",
                            attribute_value = Request.Form[key]
                        };
                        patientObject.hmis_patient_ext.Add(patientHMISExt);
                    }
                    if (key.Contains("FathersName") && key.Equals("FathersName"))
                    {
                        getAllAdditionalInfo.Add("FathersName", Request.Form[key]);
                        var patientHMISExt = new hmisPatientExt
                        {
                            attribute_name = "FathersName",
                            attribute_value = Request.Form[key]
                        };
                        patientObject.hmis_patient_ext.Add(patientHMISExt);
                    }
                    if (key.Contains("bpl_card_holder") && key.Equals("bpl_card_holder"))
                    {
                        getAllAdditionalInfo.Add("BPLCardHolder", Request.Form[key]);
                        var patientHMISExt = new hmisPatientExt
                        {
                            attribute_name = "BPLCardHolder",
                            attribute_value = Request.Form[key]
                        };
                        patientObject.hmis_patient_ext.Add(patientHMISExt);
                    }
                }

                patientObject.patient_registration_no = "CAL-MED-20992";
                //HttpResponseMessage response1 = GlobalVarriables.WebApiClient.PostAsJsonAsync("Patient", patientObject).Result;
                var response1 = GlobalVarriables.WebApiClient.PutAsJsonAsync("Patient", patientObject).Result;
                if (response1.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var EmpResponse = response1.Content.ReadAsStringAsync().Result;
                    var readTask = response1.Content.ReadAsAsync<IList<UserEntity>>();
                    // var Users = JsonConvert.DeserializeObject<List<UserEntity>>(EmpResponse);              
                    GlobalVarriables.WebApiClient.DefaultRequestHeaders.Add("Token", "1");
                }
                status = true;
                
            }
            return new JsonResult { Data = new { status = status } };
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
            var Token = "";
            if (Session != null)
            {
                if (Session["AuthUserToken"] == null)
                {
                    Token = Session["AuthUserToken"].ToString();
                }
            }
            var Name = Request.Form.Get("RefferedDoctor");
            Dictionary<string, string> getAllAdditionalInfo = new Dictionary<string, string>();
            foreach (string key in Request.Form.AllKeys)
            {
                if (key.Contains("RefferedDoctor") && key.Equals("RefferedDoctor"))
                {
                    getAllAdditionalInfo.Add("RefferedDoctor", Request.Form[key]);
                    var patientHMISExt = new hmisPatientExt
                    {
                        attribute_name = "RefferedDoctor",
                        attribute_value = Request.Form[key]
                    };
                    patientObject.hmis_patient_ext.Add(patientHMISExt);
                }

                if (key.Contains("MothersName") && key.Equals("MothersName"))
                {
                    getAllAdditionalInfo.Add("MothersName", Request.Form[key]);
                    var patientHMISExt = new hmisPatientExt
                    {
                        attribute_name = "MothersName",
                        attribute_value = Request.Form[key]
                    };
                    patientObject.hmis_patient_ext.Add(patientHMISExt);
                }
                if (key.Contains("FathersName") && key.Equals("FathersName"))
                {
                    getAllAdditionalInfo.Add("FathersName", Request.Form[key]);
                    var patientHMISExt = new hmisPatientExt
                    {
                        attribute_name = "FathersName",
                        attribute_value = Request.Form[key]
                    };
                    patientObject.hmis_patient_ext.Add(patientHMISExt);
                }
                if (key.Contains("bpl_card_holder") && key.Equals("bpl_card_holder"))
                {
                    getAllAdditionalInfo.Add("BPLCardHolder", Request.Form[key]);
                    var patientHMISExt = new hmisPatientExt
                    {
                        attribute_name = "BPLCardHolder",
                        attribute_value = Request.Form[key]
                    };
                    patientObject.hmis_patient_ext.Add(patientHMISExt);
                }
            }

            patientObject.patient_registration_no = "CAL-MED-20992";
            HttpResponseMessage response1 = GlobalVarriables.WebApiClient.PostAsJsonAsync("Patient", patientObject).Result;
            if (response1.IsSuccessStatusCode)
            {
                //Storing the response details recieved from web api   
                var EmpResponse = response1.Content.ReadAsStringAsync().Result;
                var readTask = response1.Content.ReadAsAsync<IList<UserEntity>>();
               // var Users = JsonConvert.DeserializeObject<List<UserEntity>>(EmpResponse);              
                GlobalVarriables.WebApiClient.DefaultRequestHeaders.Add("Token", "1");             
            }
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
        public ActionResult AdmitPatient(hmisPatientAdmissionBase patientAdmissionObject)
        {
            //HttpResponseMessage response1 = GlobalVarriables.WebApiClient.PostAsJsonAsync("Admin", user).Result;
            var Token = "";
            if (Session != null)
            {
                if (Session["AuthUserToken"] == null)
                {
                    Token = Session["AuthUserToken"].ToString();
                }
            }
            var Name = Request.Form.Get("RefferedDoctor");
            Dictionary<string, string> getAllAdditionalInfo = new Dictionary<string, string>();
            //foreach (string key in Request.Form.AllKeys)
            //{
            //    if (key.Contains("RefferedDoctor") && key.Equals("RefferedDoctor"))
            //    {
            //        getAllAdditionalInfo.Add("RefferedDoctor", Request.Form[key]);
            //        var patientHMISExt = new hmisPatientExt
            //        {
            //            attribute_name = "RefferedDoctor",
            //            attribute_value = Request.Form[key]
            //        };
            //        patientObject.hmis_patient_ext.Add(patientHMISExt);
            //    }

            //    if (key.Contains("MothersName") && key.Equals("MothersName"))
            //    {
            //        getAllAdditionalInfo.Add("MothersName", Request.Form[key]);
            //        var patientHMISExt = new hmisPatientExt
            //        {
            //            attribute_name = "MothersName",
            //            attribute_value = Request.Form[key]
            //        };
            //        patientObject.hmis_patient_ext.Add(patientHMISExt);
            //    }
            //    if (key.Contains("FathersName") && key.Equals("FathersName"))
            //    {
            //        getAllAdditionalInfo.Add("FathersName", Request.Form[key]);
            //        var patientHMISExt = new hmisPatientExt
            //        {
            //            attribute_name = "FathersName",
            //            attribute_value = Request.Form[key]
            //        };
            //        patientObject.hmis_patient_ext.Add(patientHMISExt);
            //    }
            //    if (key.Contains("bpl_card_holder") && key.Equals("bpl_card_holder"))
            //    {
            //        getAllAdditionalInfo.Add("BPLCardHolder", Request.Form[key]);
            //        var patientHMISExt = new hmisPatientExt
            //        {
            //            attribute_name = "BPLCardHolder",
            //            attribute_value = Request.Form[key]
            //        };
            //        patientObject.hmis_patient_ext.Add(patientHMISExt);
            //    }
            //}

            HttpResponseMessage response1 = GlobalVarriables.WebApiClient.PostAsJsonAsync("PatientAdmission", patientAdmissionObject).Result;
            if (response1.IsSuccessStatusCode)
            {
                //Storing the response details recieved from web api   
                var EmpResponse = response1.Content.ReadAsStringAsync().Result;
                var readTask = response1.Content.ReadAsAsync<IList<UserEntity>>();
                // var Users = JsonConvert.DeserializeObject<List<UserEntity>>(EmpResponse);              
            }
            return View();
        }

        // GET: User
        [HttpGet]
        public ActionResult AdmitPatient()
        {
            return View();
        }
    }
}