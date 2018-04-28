using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Net.Http.Headers;

namespace MvcToWebApi
{
    public static class GlobalVarriables
    {
        public static HttpClient WebApiClient = new HttpClient();

        static GlobalVarriables()
        {
            WebApiClient.BaseAddress = new Uri("http://localhost:63947/api/");
            //WebApiClient.BaseAddress = new Uri("http://webapihmis.azurewebsites.net/api/");
            WebApiClient.DefaultRequestHeaders.Clear();
            WebApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}