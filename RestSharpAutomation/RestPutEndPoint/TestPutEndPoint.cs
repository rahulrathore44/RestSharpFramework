using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using RestSharpAutomation.HelperClass.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebServiceAutomation.Model.JsonModel;
using WebServiceAutomation.Model.XmlModel;

namespace RestSharpAutomation.RestPutEndPoint
{
    [TestClass]
    public class TestPutEndPoint
    {
        private string postUrl = "http://localhost:8080/laptop-bag/webapi/api/add";
        private string getUrl = "http://localhost:8080/laptop-bag/webapi/api/find/";
        private string putUrl = "http://localhost:8080/laptop-bag/webapi/api/update";
        private Random random = new Random();

        [TestMethod]
        public void TestPutWithJsonData()
        {
            // Post to create a record
            // Put to update the record
            // Get using the Id fetch the record and add the validation

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

            Dictionary<string, string> headers = new Dictionary<string, string>()
            {
                {"Content-Type", "application/json" },
                {"Accept", "application/json" }
            };

            RestClientHelper restClientHelper = new RestClientHelper();
            IRestResponse restResponse = restClientHelper.PerformPostRequest(postUrl, headers, jsonData, RestSharp.DataFormat.Json);
            Assert.AreEqual(200, (int)restResponse.StatusCode);

            jsonData = "{" +
                                    "\"BrandName\": \"Alienware\"," +
                                    "\"Features\": {" +
                                    "\"Feature\": [" +
                                    "\"8th Generation Intel® Core™ i5-8300H\"," +
                                    "\"Windows 10 Home 64-bit English\"," +
                                    "\"NVIDIA® GeForce® GTX 1660 Ti 6GB GDDR6\"," +
                                    "\"8GB, 2x4GB, DDR4, 2666MHz\"," +
                                    "\"New Feature\"" +
                                    "]" +
                                    "}," +
                                    "\"Id\": " + id + "," +
                                    "\"LaptopName\": \"Alienware M17\"" +
                                "}";

            IRestClient restClient = new RestClient();
            IRestRequest restRequest = new RestRequest()
            {
                Resource = putUrl
            };

            restRequest.AddHeader("Content-Type", "application/json");
            restRequest.AddHeader("Accept", "application/json");
            restRequest.RequestFormat = DataFormat.Json;
            restRequest.AddJsonBody(jsonData);

            IRestResponse<JsonRootObject> restResponse1 = restClient.Put<JsonRootObject>(restRequest);
            Assert.IsTrue(restResponse1.Data.Features.Feature.Contains("New Feature"), "Feature did not got updated");

            headers = new Dictionary<string, string>()
            {
                {"Accept", "application/json" }
            };

            restResponse1 = restClientHelper.PerformGetRequest<JsonRootObject>(getUrl + id, headers);
            Assert.AreEqual(200, (int)restResponse1.StatusCode);
            Assert.IsTrue(restResponse1.Data.Features.Feature.Contains("New Feature"),"Feature did not got updated");
        }

        [TestMethod]
        public void TestPutWithXmlData()
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
                {"Content-Type", "application/xml" },
                {"Accept", "application/xml" }
            };

            RestClientHelper restClientHelper = new RestClientHelper();
            IRestResponse restResponse = restClientHelper.PerformPostRequest(postUrl, headers, xmlData, RestSharp.DataFormat.Xml);
            Assert.AreEqual(200, (int)restResponse.StatusCode);

            xmlData = "<Laptop>" +
                                    "<BrandName>Alienware</BrandName>" +
                                    "<Features>" +
                                       "<Feature>8th Generation Intel® Core™ i5 - 8300H</Feature>" +
                                       "<Feature>Windows 10 Home 64 - bit English</Feature>" +
                                       "<Feature>NVIDIA® GeForce® GTX 1660 Ti 6GB GDDR6</Feature>" +
                                       "<Feature>8GB, 2x4GB, DDR4, 2666MHz</Feature>" +
                                       "<Feature>Updated Feature</Feature>" +
                                     "</Features>" +
                                  "<Id> " + id + "</Id>" +
                                  "<LaptopName>Alienware M17</LaptopName>" +
                               "</Laptop>";

            IRestClient restClient = new RestClient();
            IRestRequest restRequest = new RestRequest()
            {
                Resource = putUrl
            };

            restRequest.AddHeader("Content-Type", "application/xml");
            restRequest.AddHeader("Accept", "application/xml");
            restRequest.RequestFormat = DataFormat.Xml;
            restRequest.AddParameter("xmlBody", xmlData, ParameterType.RequestBody);
            IRestResponse restResponse1 = restClient.Put(restRequest);
            var deserializer = new RestSharp.Deserializers.DotNetXmlDeserializer();

            var laptop =  deserializer.Deserialize<Laptop>(restResponse1);
            Assert.IsTrue(laptop.Features.Feature.Contains("Updated Feature"), "Feature did not got updated");

            headers = new Dictionary<string, string>()
            {
                {"Accept", "application/xml" }
            };

            var restResponse2 = restClientHelper.PerformGetRequest<Laptop>(getUrl + id, headers);
            Assert.AreEqual(200, (int)restResponse2.StatusCode);
            Assert.IsTrue(restResponse2.Data.Features.Feature.Contains("Updated Feature"), "Feature did not got updated");
        }

        [TestMethod]
        public void TestPutWithXmlData_HelperClass()
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
                {"Content-Type", "application/xml" },
                {"Accept", "application/xml" }
            };

            RestClientHelper restClientHelper = new RestClientHelper();
            IRestResponse restResponse = restClientHelper.PerformPostRequest(postUrl, headers, xmlData, RestSharp.DataFormat.Xml);
            Assert.AreEqual(200, (int)restResponse.StatusCode);

            xmlData = "<Laptop>" +
                                    "<BrandName>Alienware</BrandName>" +
                                    "<Features>" +
                                       "<Feature>8th Generation Intel® Core™ i5 - 8300H</Feature>" +
                                       "<Feature>Windows 10 Home 64 - bit English</Feature>" +
                                       "<Feature>NVIDIA® GeForce® GTX 1660 Ti 6GB GDDR6</Feature>" +
                                       "<Feature>8GB, 2x4GB, DDR4, 2666MHz</Feature>" +
                                       "<Feature>Updated Feature</Feature>" +
                                     "</Features>" +
                                  "<Id> " + id + "</Id>" +
                                  "<LaptopName>Alienware M17</LaptopName>" +
                               "</Laptop>";

            var restResponse3 = restClientHelper.PerformPutRequest<Laptop>(putUrl, headers, xmlData, DataFormat.Xml);
            Assert.IsTrue(restResponse3.Data.Features.Feature.Contains("Updated Feature"), "Feature did not got updated");

            headers = new Dictionary<string, string>()
            {
                {"Accept", "application/xml" }
            };

            var restResponse2 = restClientHelper.PerformGetRequest<Laptop>(getUrl + id, headers);
            Assert.AreEqual(200, (int)restResponse2.StatusCode);
            Assert.IsTrue(restResponse2.Data.Features.Feature.Contains("Updated Feature"), "Feature did not got updated");
        }

    }
}
