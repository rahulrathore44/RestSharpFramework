using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using RestSharp.Authenticators;
using RestSharpAutomation.HelperClass.Request;
using RestSharpAutomation.ReportAttribute;
using System;
using System.Collections.Generic;
using WebServiceAutomation.Model.XmlModel;

namespace RestSharpAutomation.RestPostEndpoint
{
    [TestClass]
    public class TestPostEndPoint
    {
        private string postUrl = "http://localhost:8080/laptop-bag/webapi/api/add";
        private Random random = new Random();

        [TestMethod]
        //[TestMethodWithReport]
        public void TestPostWithJsonData()
        {
            int id = random.Next(1000);
            string jsonData = "{" +
                                    "\"BrandName\": \"Alienware\"," +
                                    "\"Features\": {" +
                                    "\"Feature\": [" +
                                    "\"8th Generation Intel® Core™ i5-8300H\"," +
                                    "\"Windows 10 Home 64-bit English\"," +
                                    "\"NVIDIA® GeForce® GTX 1660 Ti 6GB GDDR6\"," +
                                    "\"8GB, 2x4GB, DDR4, 2666MHz\"" +
                                    "]" +
                                    "}," +
                                    "\"Id\": " + id + "," +
                                    "\"LaptopName\": \"Alienware M17\"" +
                                "}";
            IRestClient restClient = new RestClient();
            IRestRequest request = new RestRequest()
            {
                Resource = postUrl
            };

            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/xml");
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(jsonData);

            IRestResponse response = restClient.Post(request);
            Assert.AreEqual(200, (int)response.StatusCode);
            Console.WriteLine(response.Content);
        }

        private Laptop GetLaptopObject()
        {
            Laptop laptop = new Laptop();
            laptop.BrandName = "Sample Brand Name";
            laptop.LaptopName = "Sample Laptop Name";

            Features features = new Features();
            List<string> featureList = new List<string>()
            {
                ("sample feaure")
            };

            features.Feature = featureList;
            laptop.Id = "" + random.Next(1000);
            laptop.Features = features;

            return laptop;
        }

        [TestMethod]
        //[TestMethodWithReport]
        public void TestPostWithModelObject()
        {
            IRestClient restClient = new RestClient();
            IRestRequest request = new RestRequest()
            {
                Resource = postUrl
            };

            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/xml");
            request.RequestFormat = DataFormat.Json;
            request.AddBody(GetLaptopObject());

            IRestResponse response = restClient.Post(request);
            Assert.AreEqual(200, (int)response.StatusCode);
            Console.WriteLine(response.Content);
        }

        [TestMethod]
        //[TestMethodWithReport]
        public void TestPostWithModelObject_helperClass()
        {
            Dictionary<string, string> headers = new Dictionary<string, string>()
            {
                { "Content-Type", "application/json"},
                { "Accept", "application/xml"}
            };

            RestClientHelper restClientHelper = new RestClientHelper();
            IRestResponse<Laptop> restResponse = restClientHelper.PerformPostRequest<Laptop>(postUrl, headers, GetLaptopObject(), DataFormat.Json);

            Assert.AreEqual(200, (int)restResponse.StatusCode);
            Assert.IsNotNull(restResponse.Data, "Response Content is Null");
        }

        [TestMethod]
        //[TestMethodWithReport]
        public void TestPostwithxmlData()
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

            IRestClient client = new RestClient();
            IRestRequest request = new RestRequest()
            {
                Resource = postUrl
            };

            request.AddHeader("Content-Type", "application/xml");
            request.AddHeader("Accept", "application/xml");
            request.AddParameter("XmlBody", xmlData, ParameterType.RequestBody);

            IRestResponse<Laptop> response = client.Post<Laptop>(request);
            Assert.AreEqual(200, (int)response.StatusCode);
            Assert.IsNotNull(response.Data, "Response Content is Null");
        }

        [TestMethod]
        //[TestMethodWithReport]
        public void TestPostwithxmlData_ComplexPayload()
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

            IRestClient client = new RestClient();
            IRestRequest request = new RestRequest()
            {
                Resource = postUrl
            };

            request.AddHeader("Content-Type", "application/xml");
            request.AddHeader("Accept", "application/xml");
            request.RequestFormat = DataFormat.Xml;
            request.XmlSerializer = new RestSharp.Serializers.DotNetXmlSerializer();
            request.AddParameter("XmlBody", request.XmlSerializer.Serialize(xmlData), ParameterType.RequestBody);

            IRestResponse<Laptop> response = client.Post<Laptop>(request);
            Assert.AreEqual(200, (int)response.StatusCode);
            Assert.IsNotNull(response.Data, "Response Content is Null");
        }

        [TestMethod]
        //[TestMethodWithReport]
        public void TestPostWithXml_HelperClass()
        {
            Dictionary<string, string> headers = new Dictionary<string, string>()
            {
                { "Content-Type", "application/xml"},
                { "Accept", "application/xml"}
            };

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

            RestClientHelper helper = new RestClientHelper();
            // IRestResponse<Laptop> response = helper.PerformPostRequest<Laptop>(postUrl, headers, GetLaptopObject(), DataFormat.Xml);
            IRestResponse<Laptop> response = helper.PerformPostRequest<Laptop>(postUrl, headers, xmlData, DataFormat.Xml);

            Assert.AreEqual(200, (int)response.StatusCode);
            Assert.IsNotNull(response.Data, "Response Content is Null");
        }

        [TestMethod]
        //[TestMethodWithReport]
        public void TestVideoUpload()
        {
            IRestClient client = new RestClient();
            IRestRequest request = new RestRequest()
            {
                Resource = "https://www.googleapis.com/upload/youtube/v3/videos"
            };

            request.AddHeader("Content-Type", "video/*");
            request.AddHeader("client_id", "667364117050-vgsva9q8q7ktpai4209krrjqcr43eki8.apps.googleusercontent.com");
            request.AddHeader("client_secret", "7b-i7OurFnTlmogBqrnf3pbg");
            request.AddFile("video", @"C:\Users\rathr1\Downloads\upload.mp4", "video/*");

            request.AddParameter("snippet.title", "Testing", ParameterType.RequestBody);
            request.AddParameter("snippet.description", "Testing Description", ParameterType.RequestBody);
            request.AddParameter("snippet.categoryId", "22", ParameterType.RequestBody);
            request.AddParameter("status.privacyStatus", "public", ParameterType.RequestBody);

            IRestResponse restResponse =  client.Post(request);
            Console.WriteLine(restResponse.StatusCode);
            Console.WriteLine(restResponse.Content);
            Assert.AreEqual("OK", restResponse.StatusCode);
        }

    }
}
