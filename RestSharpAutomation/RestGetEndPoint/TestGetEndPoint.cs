using JsonAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using RestSharp.Authenticators;
using RestSharpAutomation.HelperClass.Request;
using RestSharpAutomation.ReportAttribute;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebServiceAutomation.Model.JsonModel;
using WebServiceAutomation.Model.XmlModel;

namespace RestSharpAutomation.RestGetEndPoint
{
    [TestClass]
    public class TestGetEndPoint
    {
        private string getUrl = "http://localhost:8080/laptop-bag/webapi/api/all";
        private string secureGet = "http://localhost:8080/laptop-bag/webapi/secure/all";

        [TestMethod]
        //[TestMethodWithReport]
        public void TestGetUsingRestSharp()
        {
            IRestClient restClient = new RestClient();
            IRestRequest restRequest = new RestRequest(getUrl);
            IRestResponse restResponse =  restClient.Get(restRequest);
            /*Console.WriteLine(restResponse.IsSuccessful);
            Console.WriteLine(restResponse.StatusCode);
            Console.WriteLine(restResponse.ErrorMessage);
            Console.WriteLine(restResponse.ErrorException);*/

            if (restResponse.IsSuccessful)
            {
                Console.WriteLine("Status Code " + restResponse.StatusCode);
                Console.WriteLine("Response Content " + restResponse.Content);
            }

        }

        [TestMethod]
        //[TestMethodWithReport]
        public void TestGetInXmlFormat()
        {
            IRestClient restClient = new RestClient();
            IRestRequest restRequest = new RestRequest(getUrl);
            restRequest.AddHeader("Accept", "application/xml");
            IRestResponse restResponse = restClient.Get(restRequest);

            if (restResponse.IsSuccessful)
            {
                Console.WriteLine("Status Code " + restResponse.StatusCode);
                Console.WriteLine("Response Content " + restResponse.Content);
            }

        }

        [TestMethod]
        //[TestMethodWithReport]
        public void TestGetInJsonFormat()
        {
            IRestClient restClient = new RestClient();
            IRestRequest restRequest = new RestRequest(getUrl);
            restRequest.AddHeader("Accept", "application/json");
            IRestResponse restResponse = restClient.Get(restRequest);

            if (restResponse.IsSuccessful)
            {
                Console.WriteLine("Status Code " + restResponse.StatusCode);
                Console.WriteLine("Response Content " + restResponse.Content);
            }

        }

        [TestMethod]
        //[TestMethodWithReport]
        public void TestGetWithJson_Deserialize()
       {
            IRestClient restClient = new RestClient();
            IRestRequest restRequest = new RestRequest(getUrl);
            restRequest.AddHeader("Accept", "application/json");
            IRestResponse<List<JsonRootObject>> restResponse =  restClient.Get<List<JsonRootObject>>(restRequest);

            if (restResponse.IsSuccessful)
            {
                Console.WriteLine("Status Code " + restResponse.StatusCode);
                Assert.AreEqual(200, (int)restResponse.StatusCode);
                Console.WriteLine("Size of List " + restResponse.Data.Count);
                List<JsonRootObject> data = restResponse.Data;

                JsonRootObject jsonRootObject =  data.Find((x) =>
                {
                    return x.Id == 1;
                });

                Assert.AreEqual("Alienware M17", jsonRootObject?.LaptopName);
                Assert.IsTrue(jsonRootObject.Features.Feature.Contains("Windows 10 Home 64 - bit English"), "Element is not Present");

            }
            else
            {
                Console.WriteLine("Error Msg " + restResponse.ErrorMessage);
                Console.WriteLine("Stack Trace " + restResponse.ErrorException);
            }

        }

        [TestMethod]
        //[TestMethodWithReport]
        public void TestGetWithXml_Deserialize()
        {
            IRestClient restClient = new RestClient();
            IRestRequest restRequest = new RestRequest(getUrl);
            restRequest.AddHeader("Accept", "application/xml");

           var dotNetXmlDeserializer = new RestSharp.Deserializers.DotNetXmlDeserializer();

            //IRestResponse<LaptopDetailss> restResponse = restClient.Get<LaptopDetailss>(restRequest);
            IRestResponse restResponse = restClient.Get(restRequest);

            if (restResponse.IsSuccessful)
            {
                Console.WriteLine("Status Code " + restResponse.StatusCode);
                Assert.AreEqual(200, (int)restResponse.StatusCode);
                
                LaptopDetailss data = dotNetXmlDeserializer.Deserialize<LaptopDetailss>(restResponse);
                Console.WriteLine("Size of List " + data.Laptop.Count);

                Laptop laptop = data.Laptop.Find((x) =>
                {
                    return x.Id.Equals("1", StringComparison.OrdinalIgnoreCase);
                });

                Assert.AreEqual("Alienware M17", laptop?.LaptopName);
                Assert.IsTrue(laptop.Features.Feature.Contains("Windows 10 Home 64 - bit English"), "Element is not Present");
            }
            else
            {
                Console.WriteLine("Error Msg " + restResponse.ErrorMessage);
                Console.WriteLine("Stack Trace " + restResponse.ErrorException);
            }
        }

        [TestMethod]
        //[TestMethodWithReport]
        public void TestGetWithExecute()
        {
            IRestClient restClient = new RestClient();
            IRestRequest restRequest = new RestRequest()
            {
                Method = Method.GET,
                Resource = getUrl
            };

            restRequest.AddHeader("Accept", "application/json");

            IRestResponse<List<Laptop>> restResponse =  restClient.Execute<List<Laptop>>(restRequest);

            Assert.AreEqual(200, (int)restResponse.StatusCode);
            Assert.IsNotNull(restResponse.Data, "Response is null");
        }

        [TestMethod]
        //[TestMethodWithReport]
        public void TestGetWithXMLUsingHelperClass()
        {
            Dictionary<string, string> headers = new Dictionary<string, string>()
            {
                {"Accept", "application/xml" }
            };

            RestClientHelper restClientHelper = new RestClientHelper();
            IRestResponse restResponse = restClientHelper.PerformGetRequest(getUrl, headers);
            Assert.AreEqual(200, (int)restResponse.StatusCode);
            Assert.IsNotNull(restResponse.Content, "Content is Null/Empty");

            IRestResponse<LaptopDetailss>  restResponse1 = restClientHelper.PerformGetRequest<LaptopDetailss>(getUrl, headers);
            Assert.AreEqual(200, (int)restResponse.StatusCode);
            Assert.IsNotNull(restResponse1.Data, "Content is Null/Empty");

        }

        [TestMethod]
        //[TestMethodWithReport]
        public void TestGetWithJsonUsingHelperClass()
        {
            Dictionary<string, string> headers = new Dictionary<string, string>()
            {
                {"Accept", "application/json" }
            };

            RestClientHelper restClientHelper = new RestClientHelper();
            IRestResponse restResponse = restClientHelper.PerformGetRequest(getUrl, headers);
            Assert.AreEqual(200, (int)restResponse.StatusCode);
            Assert.IsNotNull(restResponse.Content, "Content is Null/Empty");

            IRestResponse<List<Laptop>> restResponse1 = restClientHelper.PerformGetRequest<List<Laptop>>(getUrl, headers);
            Assert.AreEqual(200, (int)restResponse.StatusCode);
            Assert.IsNotNull(restResponse1.Data, "Content is Null/Empty");

        }

        [TestMethod]
        //[TestMethodWithReport]
        public void TestGetAndAssertLongJson()
        {
            Dictionary<string, string> headers = new Dictionary<string, string>()
            {
                {"Accept", "application/json" }
            };

            RestClientHelper restClientHelper = new RestClientHelper();
            string getUrl = "http://localhost:1082/laptop-bag/webapi/api/all";
            IRestResponse restResponse = restClientHelper.PerformGetRequest(getUrl, headers);
            Assert.AreEqual(200, (int)restResponse.StatusCode);
            Assert.IsNotNull(restResponse.Content, "Content is Null/Empty");

            IRestResponse<List<Laptop>> restResponse1 = restClientHelper.PerformGetRequest<List<Laptop>>(getUrl, headers);
            Assert.AreEqual(200, (int)restResponse.StatusCode);
            Assert.IsNotNull(restResponse1.Data, "Content is Null/Empty");

            var actual = File.ReadAllText(Directory.GetCurrentDirectory() + "/Resource/Get/getall.json");

            AssertJson.AreEquals(restResponse.Content, actual);

        }

        [TestMethod]
        //[TestMethodWithReport]
        public void TestSecureGet()
        {
            IRestClient client = new RestClient();
            client.Authenticator = new HttpBasicAuthenticator("admin", "welcome");
            IRestRequest request = new RestRequest()
            {
                Resource = secureGet
            };

            IRestResponse response = client.Get(request);
            Assert.AreEqual(200, (int)response.StatusCode);
        }
    }
}
