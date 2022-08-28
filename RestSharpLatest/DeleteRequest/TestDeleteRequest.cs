using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using RestSharpLatest.APIHelper;
using RestSharpLatest.APIHelper.APIRequest;
using RestSharpLatest.APIHelper.Client;
using RestSharpLatest.APIHelper.Command;
using System;
using System.Collections.Generic;
using WebServiceAutomation.Model.XmlModel;

namespace RestSharpLatest.DeleteRequest
{
    [TestClass]
    public class TestDeleteRequest
    {
        // POST - Create an entry in the test application
        // Delete - Delete the entry
        // GET - Fetch the entry, 404 should be returned

        private readonly string PostUrl = "http://localhost:8081/laptop-bag/webapi/api/add";
        private readonly string DeleteUrl = "http://localhost:8081/laptop-bag/webapi/api/delete/";
        private readonly string GetUrl = "http://localhost:8081/laptop-bag/webapi/api/find/";
        private readonly Random random = new Random();
        private static IClient Client;
        private static RestApiExecutor apiExecutor;

        [ClassInitialize]
        public static void Setup(TestContext testContext)
        {
            Client = new TracerClient();
            apiExecutor = new RestApiExecutor();
        }

        [ClassCleanup]
        public static void TearDown()
        {
            Client?.Dispose();
        }

        [TestMethod]
        public void TestDeleteRequestWithId()
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
                Resource = PostUrl,
                Method = Method.Post
            };
            // Add the body to the request

            request.AddStringBody(jsonData, DataFormat.Json);

            // Send the request

            var response = client.ExecutePost(request);
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

            request = new RestRequest()
            {
                Resource = DeleteUrl + id,
                Method = Method.Delete
            };

            request.AddHeader("Accept", "text/plain");

            response = client.Delete(request);
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

            request = new RestRequest()
            {
                Resource = GetUrl + id,
                Method = Method.Get
            };

            response = client.ExecuteGet(request);
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);

            client?.Dispose();
        }

        [TestMethod]
        public void TestDeleteRequestWithFramework()
        {
            int id = random.Next(1000);

            var xmlrequestBody = new XmlModelBuilder().WithId(id).WithBrandName("Test BrandName").WithLaptopName("Test LaptopName").WithFeatures(new List<string>() { "One", "Two" }).Build();

            var postrequest = new PostRequestBuilder().WithUrl(PostUrl).WithBody(xmlrequestBody, RequestBodyType.XML);

            var command = new RequestCommand(postrequest, Client);
            apiExecutor.SetCommand(command);
            var response = apiExecutor.ExecuteRequest();
            response.GetHttpStatusCode().Should().Be(System.Net.HttpStatusCode.OK);

            var deleterequest = new DeleteRequestBuilder().WithDefaultHeaders().WithUrl(DeleteUrl + id);

            command = new RequestCommand(deleterequest, Client);
            apiExecutor.SetCommand(command);
            response = apiExecutor.ExecuteRequest();
            response.GetHttpStatusCode().Should().Be(System.Net.HttpStatusCode.OK);

            var getrequest = new GetRequestBuilder().WithUrl(GetUrl + id);
            command = new RequestCommand(getrequest, Client);
            apiExecutor.SetCommand(command);
            response = apiExecutor.ExecuteRequest();
            response.GetHttpStatusCode().Should().Be(System.Net.HttpStatusCode.NotFound);


        }
        
    }
}
