using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebServiceAutomation.Helper.Request;
using WebServiceAutomation.Helper.Response;
using WebServiceAutomation.Model;
using WebServiceAutomation.Model.XmlModel;

namespace WebServiceAutomation
{
    [TestClass]
    public class UnitTest1
    {
        
        [TestMethod]
        public void TestMethod1()
        {
            // Step 1. To create Http Client
            HttpClient httpClient = new HttpClient();
            string getUrl = "https://laptopbag.herokuapp.com/laptop-bag/webapi/api/all";
            httpClient.Dispose(); // Close the connection and release the resource

        }

        //[TestMethod]
        public void TestDelayAsync()
        {
            executeAsync().GetAwaiter().GetResult();
        }

        private async Task executeAsync()
        {
            Console.WriteLine("Started..");
            string x = await GetDealy().ConfigureAwait(false);
            Console.WriteLine("Ended..");
            string y = await GetDealy1().ConfigureAwait(false);
        }

        private async Task<string> GetDealy()
        {

                HttpClient httpClient = new HttpClient();
                HttpResponseMessage response = await httpClient.GetAsync("https://laptopbag.herokuapp.com/laptop-bag/webapi/delay/all");
            response.EnsureSuccessStatusCode();
            string content = await response.Content.ReadAsStringAsync();
            return content;

        }

        private async Task executeAsync1()
        {
            Console.WriteLine("Started..");
            string x = await GetDealy1().ConfigureAwait(false);
            Console.WriteLine("Ended..");
        }

        private async Task<string> GetDealy1()
        {

            HttpClient httpClient = new HttpClient();
            HttpResponseMessage response = await httpClient.GetAsync("https://laptopbag.herokuapp.com/laptop-bag/webapi/delay/all");
            response.EnsureSuccessStatusCode();
            string content = await response.Content.ReadAsStringAsync();
            return content;

        }

        

        //[TestMethod]
        public void TestAsyncAwait()
        {
            Task t1 =  Method1();
            Task t2 = Method2();
            t1.Wait();
            t2.Wait();
        }

        public async Task Method1()
        {
            await Task.Run(() =>
            {
                HttpClientHelper.PerformGetRequest("https://laptopbag.herokuapp.com/laptop-bag/webapi/delay/all", null);
            });
        }


        public async Task Method2()
        {
            await Task.Run(() =>
            {
                HttpClientHelper.PerformGetRequest("https://laptopbag.herokuapp.com/laptop-bag/webapi/delay/all", null);
            });
        }


        //[TestMethod]
        public void TestCreatTask()
        {
           

            Task t1 = new Task(GetAction());
            t1.Start();

            Task t2 = new Task(GetAction());
            t2.Start();


            Task t3 = new Task(GetAction());
            t3.Start();

            Task t4 = new Task(GetAction());
            t4.Start();

            t1.Wait();
            t2.Wait();
            t3.Wait();
            t4.Wait();
        }

        private Action GetAction()
        {
            Dictionary<string, string> httpHeader = new Dictionary<string, string>
            {
                { "Accept", "application/json" }
            };
            return new Action(() =>
            {
                RestResponse restResponse = HttpClientHelper.PerformGetRequest("https://laptopbag.herokuapp.com/laptop-bag/webapi/delay/all", httpHeader);
                Assert.AreEqual(200, restResponse.StatusCode);
            });
        }

        
    }
}
