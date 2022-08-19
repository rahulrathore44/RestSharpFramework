using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using System;
using FluentAssertions;
using System.Diagnostics;
using WebServiceAutomation.Model.JsonModel;
using RestSharpLatest.APIModel.JsonApiModel;
using RestSharpLatest.APIHelper;
using RestSharpLatest.APIHelper.Client;
using RestSharpLatest.APIHelper.APIRequest;
using RestSharpLatest.APIHelper.Command;
using WebServiceAutomation.Model.XmlModel;
using System.Collections.Generic;
using RestSharp.Serializers;

namespace RestSharpLatest.PostRequest
{
    [TestClass]
    public class TestPostWithTracerClient
    {
        private string postUrl = "http://localhost:8081/laptop-bag/webapi/api/add";
        private Random random = new Random();
        private static IClient _client;
        private static RestApiExecutor _executor;

        [ClassInitialize]
        public static void Setup(TestContext testContext)
        {
            _client = new TracerClient();
            _executor = new RestApiExecutor();
        }

        [ClassCleanup]
        public static void TearDown()
        {
            _client?.Dispose();
        }

        [TestMethod]
        public void TestPostRequestWithStringBody()
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
            // Create the Client
            RestClient client = new RestClient();
            // Create the Request
            RestRequest request = new RestRequest()
            {
                Resource = postUrl,
                Method = Method.Post
            };
            // Add the body to the request

            request.AddStringBody(jsonData, DataFormat.Json);
            // Send the request

            RestResponse response = client.ExecutePost(request);
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            Debug.WriteLine(response.Content);

        }

        [TestMethod]
        public void TestPostRequestWithJsonObject()
        {
            int id = random.Next(1000);

            // Serialize the object into the JSON or XML representation.
            // Add the Content - Type header.
            //Add the serialized object to the request.

            // ------ Old Model--
            /*var payload = new JsonRootObjectBuilder().WithId(id).WithBrandName("Test BrandName").WithLaptopName("Test LaptopName").WithFeatures(new System.Collections.Generic.List<string>() { "Feature1", "Feature2" }).Build();*/


            var payload = new JsonModelBuilder().WithId(id).WithBrandName("Test BrandName").WithLaptopName("Test LaptopName").WithFeatures(new System.Collections.Generic.List<string>() { "Feature1", "Feature2" }).Build();

            // Create the Client
            RestClient client = new RestClient();
            // Create the Request
            RestRequest request = new RestRequest()
            {
                Resource = postUrl,
                Method = Method.Post
            };

            request.AddJsonBody(payload);
            var response = client.ExecutePost(request);
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            Debug.WriteLine(response.Content);
        }

        [TestMethod]
        public void TestPostRequestWithFramework_Json()
        {
            int id = random.Next(1000);
            var payload = new JsonModelBuilder().WithId(id).WithBrandName("Test BrandName").WithLaptopName("Test LaptopName").WithFeatures(new System.Collections.Generic.List<string>() { "Feature1", "Feature2" }).Build();

            // Post Request

            var request = new PostRequestBuilder().WithUrl(postUrl).WithBody<JsonModel>(payload, RequestBodyType.JSON);

            // Command

            var command = new RequestCommand(request, _client);
            // SetCommand

            _executor.SetCommand(command);
            // Execute the request
            var response = _executor.ExecuteRequest();
            response.GetHttpStatusCode().Should().Be(System.Net.HttpStatusCode.OK);
            response.GetResponseData().Should().Contain("Test LaptopName");
        }


        [TestMethod]
        public void TestPostRequestWithFramework_String()
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

            // Post Request

            var request = new PostRequestBuilder().WithUrl(postUrl).WithBody<string>(jsonData, RequestBodyType.STRING);

            // Command

            var command = new RequestCommand(request, _client);
            // SetCommand

            _executor.SetCommand(command);
            // Execute the request
            var response = _executor.ExecuteRequest();
            response.GetHttpStatusCode().Should().Be(System.Net.HttpStatusCode.OK);
            response.GetResponseData().Should().Contain("8th Generation Intel® Core™ i5-8300H");

            var responseType = _executor.ExecuteRequest<JsonModel>();
            responseType.GetHttpStatusCode().Should().Be(System.Net.HttpStatusCode.OK);
            var responseData = responseType.GetResponseData();
            responseData.Features.Feature.Should().Contain("8th Generation Intel® Core™ i5-8300H");

        }


        [TestMethod]
        public void TestPostRequestWithXML_String()
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


            // Create the Client
            RestClient client = new RestClient();
            // Create the Request
            RestRequest request = new RestRequest()
            {
                Resource = postUrl,
                Method = Method.Post
            };

            request.AddStringBody(xmlData, DataFormat.Xml);
            request.AddHeader("Accept", "application/xml");

            var response = client.ExecutePost(request);
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            Debug.WriteLine(response.Content);
            client.Dispose();
        }

        [TestMethod]
        public void TestPostRequestWithFramework_XML()
        {
            int id = random.Next(1000);
            var xmlPayLoad = new XmlModelBuilder().WithId(id).WithBrandName("Test Brand Name").WithFeatures(new List<string>() { "Feature1", "Feature2" }).WithLaptopName("Test Laptop Name").Build();

            // Post Request

            var request = new PostRequestBuilder().WithUrl(postUrl).WithBody<Laptop>(xmlPayLoad, RequestBodyType.XML).WithHeaders(new Dictionary<string, string>() { { "Accept", "application/xml" } });

            // Command

            var command = new RequestCommand(request, _client);
            // SetCommand

            _executor.SetCommand(command);
            // Execute the request
            var response = _executor.ExecuteRequest();
            response.GetHttpStatusCode().Should().Be(System.Net.HttpStatusCode.OK);
            response.GetResponseData().Should().Contain("Test Laptop Name");
        }

        [TestMethod]
        public void TestPostRequestWithFrameworkXML_String()
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

            // Post Request

            var request = new PostRequestBuilder().WithUrl(postUrl).WithBody<string>(xmlData, RequestBodyType.STRING, ContentType.Xml).WithHeaders(new Dictionary<string, string>() { { "Accept", "application/xml" } });

            // Command

            var command = new RequestCommand(request, _client);
            // SetCommand

            _executor.SetCommand(command);
            // Execute the request
            var response = _executor.ExecuteRequest();
            response.GetHttpStatusCode().Should().Be(System.Net.HttpStatusCode.OK);
            response.GetResponseData().Should().Contain("8th Generation Intel® Core™ i5 - 8300H");

            var responseType = _executor.ExecuteRequest<Laptop>();
            responseType.GetHttpStatusCode().Should().Be(System.Net.HttpStatusCode.OK);
            var responseData = responseType.GetResponseData();
            responseData.Features.Feature.Should().Contain("8th Generation Intel® Core™ i5 - 8300H");

        }


    }
}
