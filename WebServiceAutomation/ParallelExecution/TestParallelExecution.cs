using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebServiceAutomation.Helper.Request;
using WebServiceAutomation.Helper.Response;
using WebServiceAutomation.Model;
using WebServiceAutomation.Model.JsonModel;
using WebServiceAutomation.Model.XmlModel;

namespace WebServiceAutomation.ParallelExecution
{
    [TestClass]
    public class TestParallelExecution
    {
        private string delayGetUrl = "http://localhost:8080/laptop-bag/webapi/delay/all";
        private string delayGetWithId = "http://localhost:8080/laptop-bag/webapi/delay/find/";
        private string delayPostUrl = "http://localhost:8080/laptop-bag/webapi/delay/add";
        private string delayPutUrl = "http://localhost:8080/laptop-bag/webapi/delay/update";
        private string xmlMediaType = "application/xml";
        private Random random = new Random();
        private HttpClientAsyncHelper httpClientAsyncHelper = new HttpClientAsyncHelper();

        private void SendGetRequest()
        {
            Dictionary<string, string> httpHeader = new Dictionary<string, string>();
            httpHeader.Add("Accept", "application/json");

            RestResponse restResponse = httpClientAsyncHelper.PerformGetRequest(delayGetUrl, httpHeader).GetAwaiter().GetResult();

            //List<JsonRootObject> jsonRootObject = JsonConvert.DeserializeObject<List<JsonRootObject>>(restResponse.ResponseContent);
            //Console.WriteLine(jsonRootObject[0].ToString());
            Assert.AreEqual(200, restResponse.StatusCode);

            List<JsonRootObject> jsonData = ResponseDataHelper.DeserializeJsonResponse<List<JsonRootObject>>(restResponse.ResponseContent);

            Console.WriteLine(jsonData[0].ToString());
        }

        private void SendPostRequest()
        {
            int id = random.Next(1000);
            string xmlData = "<Laptop>" +
                                     "<BrandName>Alienware</BrandName>" +
                                     "<Features>" +
                                        "<Feature>8th Generation Intel® Core™ i5 - 8300H</Feature>" +
                                        "<Feature>Windows 10 Home 64 - bit English</Feature>" +
                                        "<Feature>NVIDIA® GeForce® GTX 1660 Ti 6GB GDDR6</Feature>" +
                                        "<Feature>8GB, 2x4GB, DDR4, 2666MHz</Feature>" +
                                      "</Features>" +
                                   "<Id> " + id + "</Id>" +
                                   "<LaptopName>Alienware M17</LaptopName>" +
                                "</Laptop>";


            Dictionary<string, string> headers = new Dictionary<string, string>()
            {
                { "Accept","application/xml"}
            };

            RestResponse restResponse = httpClientAsyncHelper.PerformPostRequest(delayPostUrl, xmlData, xmlMediaType, headers).GetAwaiter().GetResult();

            //HttpContent httpContent = new StringContent(xmlData, Encoding.UTF8, xmlMediaType) ;
            //HttpClientHelper.PerformPostRequest(postUrl, httpContent, headers);

            Assert.AreEqual(200, restResponse.StatusCode);

            Console.WriteLine(">>>>>>>>>>" + restResponse.ResponseContent);

            Laptop xmlDatat = ResponseDataHelper.DeserializeXmlResponse<Laptop>(restResponse.ResponseContent);
            Console.WriteLine(xmlDatat.ToString());
        }

        private void SendPutRequest()
        {
            int id = random.Next(1000);

            string xmlData = "<Laptop>" +
                                    "<BrandName>Alienware</BrandName>" +
                                    "<Features>" +
                                       "<Feature>8th Generation Intel® Core™ i5 - 8300H</Feature>" +
                                       "<Feature>Windows 10 Home 64 - bit English</Feature>" +
                                       "<Feature>NVIDIA® GeForce® GTX 1660 Ti 6GB GDDR6</Feature>" +
                                       "<Feature>8GB, 2x4GB, DDR4, 2666MHz</Feature>" +
                                     "</Features>" +
                                  "<Id> " + id + "</Id>" +
                                  "<LaptopName>Alienware M17</LaptopName>" +
                               "</Laptop>";

            Dictionary<string, string> headers = new Dictionary<string, string>()
            {
                { "Accept","application/xml"}
            };

            RestResponse restResponse = httpClientAsyncHelper.PerformPostRequest(delayPostUrl, xmlData, xmlMediaType, headers).GetAwaiter().GetResult();
            Assert.AreEqual(200, restResponse.StatusCode);

            xmlData = "<Laptop>" +
                                    "<BrandName>Alienware</BrandName>" +
                                    "<Features>" +
                                       "<Feature>8th Generation Intel® Core™ i5 - 8300H</Feature>" +
                                       "<Feature>Windows 10 Home 64 - bit English</Feature>" +
                                       "<Feature>NVIDIA® GeForce® GTX 1660 Ti 6GB GDDR6</Feature>" +
                                       "<Feature>8GB, 2x4GB, DDR4, 2666MHz</Feature>" +
                                       "<Feature>1 TB of SSD</Feature>" +
                                     "</Features>" +
                                  "<Id> " + id + "</Id>" +
                                  "<LaptopName>Alienware M17</LaptopName>" +
                               "</Laptop>";

            restResponse = httpClientAsyncHelper.PerformPutRequest(delayPutUrl, xmlData, xmlMediaType, headers).GetAwaiter().GetResult();
            Assert.AreEqual(200, restResponse.StatusCode);

            restResponse = httpClientAsyncHelper.PerformGetRequest(delayGetWithId + id, headers).GetAwaiter().GetResult();
            Assert.AreEqual(200, restResponse.StatusCode);

            Laptop xmlObj = ResponseDataHelper.DeserializeXmlResponse<Laptop>(restResponse.ResponseContent);
            Assert.IsTrue(xmlObj.Features.Feature.Contains("1 TB of SSD"), "Item Not found");
        }

        [TestMethod]
        public void TestTask()
        {
            Task get = new Task(() =>
            {
                SendGetRequest();
            });
            get.Start();

            Task post = new Task(() =>
            {
                SendPostRequest();
            });
            post.Start();

            Task put = new Task(() =>
            {
                SendPutRequest();
            });
            put.Start();

            get.Wait();
            put.Wait();
            post.Wait();
        }

        [TestMethod]
        public void TestTaskUsingTaskFactory()
        {
            var getTask = Task.Factory.StartNew(() =>
            {
                SendGetRequest();
            });

            var postTask = Task.Factory.StartNew(() =>
            {
                SendPostRequest();
            });

            var putTask = Task.Factory.StartNew(() =>
            {
                SendPutRequest();
            });

            getTask.Wait();
            postTask.Wait();
            putTask.Wait();

        }

        [TestMethod]
        public void TestTaskUsingTaskFactoryWithResponse()
        {
            Task<RestResponse> getTask = Task.Factory.StartNew<RestResponse>(() =>
            {
                return SendGetRequestWithResponse();
            });

            Task<RestResponse> postTask = Task.Factory.StartNew<RestResponse>(() =>
            {
                return SendPostRequestWithResponse();
            });

            Console.WriteLine(getTask.Result.ResponseContent);
            Console.WriteLine(postTask.Result.ResponseContent);

        }

        private RestResponse SendGetRequestWithResponse()
        {
            Dictionary<string, string> httpHeader = new Dictionary<string, string>();
            httpHeader.Add("Accept", "application/json");

            RestResponse restResponse = httpClientAsyncHelper.PerformGetRequest(delayGetUrl, httpHeader).GetAwaiter().GetResult();

            //List<JsonRootObject> jsonRootObject = JsonConvert.DeserializeObject<List<JsonRootObject>>(restResponse.ResponseContent);
            //Console.WriteLine(jsonRootObject[0].ToString());
            Assert.AreEqual(200, restResponse.StatusCode);

            //List<JsonRootObject> jsonData = ResponseDataHelper.DeserializeJsonResponse<List<JsonRootObject>>(restResponse.ResponseContent);

            //Console.WriteLine(jsonData[0].ToString());
            return restResponse;
        }

        private RestResponse SendPostRequestWithResponse()
        {
            int id = random.Next(1000);
            string xmlData = "<Laptop>" +
                                     "<BrandName>Alienware</BrandName>" +
                                     "<Features>" +
                                        "<Feature>8th Generation Intel® Core™ i5 - 8300H</Feature>" +
                                        "<Feature>Windows 10 Home 64 - bit English</Feature>" +
                                        "<Feature>NVIDIA® GeForce® GTX 1660 Ti 6GB GDDR6</Feature>" +
                                        "<Feature>8GB, 2x4GB, DDR4, 2666MHz</Feature>" +
                                      "</Features>" +
                                   "<Id> " + id + "</Id>" +
                                   "<LaptopName>Alienware M17</LaptopName>" +
                                "</Laptop>";


            Dictionary<string, string> headers = new Dictionary<string, string>()
            {
                { "Accept","application/xml"}
            };

            RestResponse restResponse = httpClientAsyncHelper.PerformPostRequest(delayPostUrl, xmlData, xmlMediaType, headers).GetAwaiter().GetResult();

            //HttpContent httpContent = new StringContent(xmlData, Encoding.UTF8, xmlMediaType) ;
            //HttpClientHelper.PerformPostRequest(postUrl, httpContent, headers);

            Assert.AreEqual(200, restResponse.StatusCode);

            Console.WriteLine(">>>>>>>>>>" + restResponse.ResponseContent);

            //Laptop xmlDatat = ResponseDataHelper.DeserializeXmlResponse<Laptop>(restResponse.ResponseContent);
            //Console.WriteLine(xmlDatat.ToString());
            return restResponse;
        }
    }
}
