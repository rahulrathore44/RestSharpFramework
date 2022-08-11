using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using System;
using FluentAssertions;
using System.Diagnostics;

namespace RestSharpLatest.PostRequest
{
    [TestClass]
    public class TestPostRequest
    {
        private string postUrl = "http://localhost:8081/laptop-bag/webapi/api/add";
        private Random random = new Random();

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
    }
}
