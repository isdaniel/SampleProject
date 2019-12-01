using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace HttpSample
{
    class Program
    {
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

        static  void ExecuteByWebClient(string url)
        {
            WebClient wc = new WebClient();
            var result =wc.DownloadString(url);
            Console.WriteLine(result);
        }

        static void Main(string[] args)
        {
            ExecuteByWebClient("https://marcus116.blogspot.com/2018/02/c-web-api-httpclient.html");
            Console.ReadKey();
        }
    }
}
