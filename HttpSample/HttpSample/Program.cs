using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace HttpSample
{
    public class EBMModel
    {
        public string RID { get; set; }

        public string ChartNo { get; set; }

        public string Status { get; set; }
    }

    public class PersonModel
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string PassWord{ get; set; }
    }
    public class PatientRoundModel
    {
        public string ChartNo { get; set; }
        public string Date { get; set; }
    }

    class Program
    {
        

        /// <summary>
        /// POST傳輸
        /// </summary>
        /// <param name="posturl"></param>
        /// <param name="postData"></param>
        /// <returns></returns>
        static string HttpGet(string posturl)
        {
            ServicePointManager.ServerCertificateValidationCallback
               = (sender, certificate, chain, errors) => true ;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | 
                                                  SecurityProtocolType.Tls11 |
                                                  SecurityProtocolType.Tls12 | 
                                                  SecurityProtocolType.Ssl3;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(posturl);
            request.Method = "GET";
            //byte[] postcontentsArray = Encoding.UTF8.GetBytes(postData);
            //request.ContentType = "application/x-www-form-urlencoded";
            //request.ContentLength = postcontentsArray.Length;

            //using (Stream requestStream = request.GetRequestStream())
            //{
            //    requestStream.Write(postcontentsArray, 0, postcontentsArray.Length);
            WebResponse response = request.GetResponse();
            using (Stream responseStream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(responseStream))
            {
                return reader.ReadToEnd();
            }
            //}
        }

        static async void ExecuteByHttpClient(string url)
        {
            using (HttpClient httpClient= new HttpClient())
            {
                HttpResponseMessage response = await httpClient.GetAsync(new Uri(url));
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine(responseBody);
            }
        }

        static string ExecuteByHttpClient(string url,string json)
        {
            using (HttpClient httpClient= new HttpClient())
            {
                
                StringContent data = new StringContent(json,Encoding.UTF8,"application/json");
                HttpResponseMessage response = httpClient.PostAsync(new Uri(url),data).Result;
                response.EnsureSuccessStatusCode();
                return response.Content.ReadAsStringAsync().Result;
            }
        }

        static void ExecuteByWebClient(string url)
        {
            WebClient wc = new WebClient();
            //wc.Headers
            var result =wc.DownloadString(url);
            Console.WriteLine(result);
        }

        static void Main(string[] args)
        {
            ApiService apiService = new ApiService();
            //第一次請求API
            var data = JsonConvert.SerializeObject(new { RID = apiService.GetRID()});
            var patientData = ExecuteByHttpClient("http://localhost:1209/api/Patient/GetPatientRounds", data);

          
            var chartNo = JsonConvert.DeserializeObject< ApiReturnViewModel<IEnumerable<PatientRoundModel>>>(patientData).Data.Select(x=>x.ChartNo);
            var chartNoJson = JsonConvert.SerializeObject(new {ChartNo = chartNo});
            var EBMJson = ExecuteByHttpClient("http://localhost:1209/api/Patient/GetPatients", chartNoJson);
            var EBMList = JsonConvert.DeserializeObject<ApiReturnViewModel<IEnumerable<EBMModel>>>(EBMJson).Data;

            apiService.InsertEBM(EBMList);
            Console.ReadKey();
        }

        private static void RestRequest()
        {
            var client = new RestClient("https://github.com");

            var request = new RestRequest("/restsharp/RestSharp", Method.GET);
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.DefaultConnectionLimit = 9999;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | 
                                                   SecurityProtocolType.Tls11 |
                                                   SecurityProtocolType.Tls12 | 
                                                   SecurityProtocolType.Ssl3;

            //X509Certificate2 certificates = new X509Certificate2();
            //certificates.Import();

            //client.ClientCertificates = new X509CertificateCollection()
            //{
            //    certificates
            //};

            // easily add HTTP Headers
            request.AddHeader("header", "value");

            // execute the request
            IRestResponse response = client.Execute(request);
            var content = response.Content; // raw content as string

            IRestResponse<object> response2 = client.Execute<object>(request);
            //var name = response2.Data.Name;
        }
    }

    public class ApiReturnViewModel<T>
    {
        public string Status { get; set; }
        public T Data { get; set; }
        public string ErrorMessage { get; set; }
    }
}
