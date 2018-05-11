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
    public class AdmissionClientController : Controller
    {
        // GET: AdmissionClient
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
            IEnumerable<hmisPatientAdmissionBase> patientList = null;
            List<hmisPatientAdmissionBase> hmisPatientBase = new List<hmisPatientAdmissionBase>();
            HttpResponseMessage response = GlobalVarriables.WebApiClient.GetAsync("PatientAdmission").Result;
            //userList = response.Content.ReadAsByteArrayAsync<IEnumerable<hmisUserBase>>().Result;
            if (response.IsSuccessStatusCode)
            {
                //Storing the response details recieved from web api   
                var EmpResponse = response.Content.ReadAsStringAsync().Result;
                //Deserializing the response recieved from web api and storing into the Employee list  
                patientList = JsonConvert.DeserializeObject<List<hmisPatientAdmissionBase>>(EmpResponse);
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
            foreach (string key in Request.Form.AllKeys)
            {
                if (key.Contains("patientheight") && key.Equals("patientheight"))
                {
                    getAllAdditionalInfo.Add("patientheight", Request.Form[key]);
                    var admissionHMISExt = new hmisPatientAdmissionExt
                    {
                        attribute_name = "patientheight",
                        attribute_value = Request.Form[key]
                    };
                    patientAdmissionObject.hmis_patient_admission_ext.Add(admissionHMISExt);
                }

                if (key.Contains("patientwaight") && key.Equals("patientwaight"))
                {
                    getAllAdditionalInfo.Add("patientwaight", Request.Form[key]);
                    var admissionHMISExt = new hmisPatientAdmissionExt
                    {
                        attribute_name = "patientwaight",
                        attribute_value = Request.Form[key]
                    };
                    patientAdmissionObject.hmis_patient_admission_ext.Add(admissionHMISExt);
                }
                if (key.Contains("lowpressure") && key.Equals("lowpressure"))
                {
                    getAllAdditionalInfo.Add("lowpressure", Request.Form[key]);
                    var patientHMISExt = new hmisPatientAdmissionExt
                    {
                        attribute_name = "lowpressure",
                        attribute_value = Request.Form[key]
                    };
                    patientAdmissionObject.hmis_patient_admission_ext.Add(patientHMISExt);
                }
                if (key.Contains("highpressure") && key.Equals("highpressure"))
                {
                    getAllAdditionalInfo.Add("highpressure", Request.Form[key]);
                    var admissionHMISExt = new hmisPatientAdmissionExt
                    {
                        attribute_name = "highpressure",
                        attribute_value = Request.Form[key]
                    };
                    patientAdmissionObject.hmis_patient_admission_ext.Add(admissionHMISExt);
                }
                if (key.Contains("heartrate") && key.Equals("heartrate"))
                {
                    getAllAdditionalInfo.Add("heartrate", Request.Form[key]);
                    var admissionHMISExt = new hmisPatientAdmissionExt
                    {
                        attribute_name = "heartrate",
                        attribute_value = Request.Form[key]
                    };
                    patientAdmissionObject.hmis_patient_admission_ext.Add(admissionHMISExt);
                }
                if (key.Contains("temparature") && key.Equals("temparature"))
                {
                    getAllAdditionalInfo.Add("temparature", Request.Form[key]);
                    var admissionHMISExt = new hmisPatientAdmissionExt
                    {
                        attribute_name = "temparature",
                        attribute_value = Request.Form[key]
                    };
                    patientAdmissionObject.hmis_patient_admission_ext.Add(admissionHMISExt);
                }
                if (key.Contains("saturation") && key.Equals("saturation"))
                {
                    getAllAdditionalInfo.Add("saturation", Request.Form[key]);
                    var admissionHMISExt = new hmisPatientAdmissionExt
                    {
                        attribute_name = "saturation",
                        attribute_value = Request.Form[key]
                    };
                    patientAdmissionObject.hmis_patient_admission_ext.Add(admissionHMISExt);
                }
                if (key.Contains("contacttype") && key.Equals("contacttype"))
                {
                    getAllAdditionalInfo.Add("contacttype", Request.Form[key]);
                    var admissionHMISExt = new hmisPatientAdmissionExt
                    {
                        attribute_name = "contacttype",
                        attribute_value = Request.Form[key]
                    };
                    patientAdmissionObject.hmis_patient_admission_ext.Add(admissionHMISExt);
                }
                if (key.Contains("contactfirstanme") && key.Equals("contactfirstanme"))
                {
                    getAllAdditionalInfo.Add("contactfirstanme", Request.Form[key]);
                    var admissionHMISExt = new hmisPatientAdmissionExt
                    {
                        attribute_name = "contactfirstanme",
                        attribute_value = Request.Form[key]
                    };
                    patientAdmissionObject.hmis_patient_admission_ext.Add(admissionHMISExt);
                }
                if (key.Contains("contactlastanme") && key.Equals("contactlastanme"))
                {
                    getAllAdditionalInfo.Add("contactlastanme", Request.Form[key]);
                    var admissionHMISExt = new hmisPatientAdmissionExt
                    {
                        attribute_name = "contactlastanme",
                        attribute_value = Request.Form[key]
                    };
                    patientAdmissionObject.hmis_patient_admission_ext.Add(admissionHMISExt);
                }
                if (key.Contains("contactphone") && key.Equals("contactphone"))
                {
                    getAllAdditionalInfo.Add("contactphone", Request.Form[key]);
                    var admissionHMISExt = new hmisPatientAdmissionExt
                    {
                        attribute_name = "contactphone",
                        attribute_value = Request.Form[key]
                    };
                    patientAdmissionObject.hmis_patient_admission_ext.Add(admissionHMISExt);
                }
                if (key.Contains("additionalphone") && key.Equals("additionalphone"))
                {
                    getAllAdditionalInfo.Add("additionalphone", Request.Form[key]);
                    var admissionHMISExt = new hmisPatientAdmissionExt
                    {
                        attribute_name = "additionalphone",
                        attribute_value = Request.Form[key]
                    };
                    patientAdmissionObject.hmis_patient_admission_ext.Add(admissionHMISExt);
                }
            }

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
        public ActionResult AdmitPatient(string ID)
        {
            hmisPatientAdmissionBase objModel = new hmisPatientAdmissionBase();
            objModel.patient_id = new Guid(ID);
            return View(objModel);
        }

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
            HttpResponseMessage response = GlobalVarriables.WebApiClient.GetAsync("PatientAdmission" + "?id=" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                using (HttpContent content = response.Content)
                {
                    // ... Read the string.
                    Task<string> result = content.ReadAsStringAsync();
                    var res = result.Result;
                    var userData = Json(result);
                    JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
                    dynamic admissionInfo = jsonSerializer.Deserialize<dynamic>(res);
                    return View(PopulateData(admissionInfo));
                }
            }
            return HttpNotFound();
        }

        public hmisPatientAdmissionBase PopulateData(dynamic admissionInfo)
        {
            //var patientUnit = new hmisPatientBase();
            var admissionUnit = new hmisPatientAdmissionBase
            {
                ID = admissionInfo["ID"] == null ? Guid.Empty : new Guid(admissionInfo["ID"].ToString()),
                admission_type = admissionInfo["admission_type"] == null ? "" : admissionInfo["admission_type"].ToString() ?? "",
                ward_number = admissionInfo["ward_number"] == null ? "" : admissionInfo["ward_number"].ToString() ?? "",
                discharge_date = admissionInfo["discharge_date"] == null ? "" : admissionInfo["discharge_date"].ToString() ?? "",
                diagonosed_in = admissionInfo["diagonosed_in"] == null ? "" : admissionInfo["diagonosed_in"].ToString() ?? "",
                discharge_type = admissionInfo["discharge_type"] == null ? "" : admissionInfo["discharge_type"].ToString() ?? "",
                from_health_unit = admissionInfo["from_health_unit"] == null ? null : admissionInfo["from_health_unit"].ToString(),
                admission_sequence = admissionInfo["admission_sequence"] == null ? "" : admissionInfo["admission_sequence"].ToString() ?? "",
                bed_days = admissionInfo["bed_days"] == null ? "" : admissionInfo["bed_days"].ToString() ?? "",
                progressive_in_year = admissionInfo["progressive_in_year"] == null ? "" : admissionInfo["progressive_in_year"].ToString() ?? "",
                discharge_instruction = admissionInfo["discharge_instruction"] == null ? "" : admissionInfo["discharge_instruction"].ToString() ?? "",
                Is_allergic = admissionInfo["Is_allergic"] == null ? false : admissionInfo["Is_allergic"].ToString(),
                Is_malnutritious = admissionInfo["Is_malnutritious"] == null ? false : admissionInfo["Is_malnutritious"].ToString(),
                admission_notes = admissionInfo["admission_notes"] == null ? false : admissionInfo["admission_notes"].ToString()
            };
            return admissionUnit;

        }
    }
}