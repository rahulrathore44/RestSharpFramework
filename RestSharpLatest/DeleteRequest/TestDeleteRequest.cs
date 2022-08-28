using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using System;


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
        
    }
}
