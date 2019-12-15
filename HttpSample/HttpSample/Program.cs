using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using RestSharp;

namespace HttpSample
{
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

        static void ExecuteByWebClient(string url)
        {
            WebClient wc = new WebClient();
            var result =wc.DownloadString(url);
            Console.WriteLine(result);
        }

        static void Main(string[] args)
        {
            Console.WriteLine(HttpGet("https://github.com") );
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
}
