using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpLatest.PostRequest
{
    [TestClass]
    public class TestPostRequest
    {
        private string postUrl = "http://localhost:8081/laptop-bag/webapi/api/add";
        private Random random = new Random();

        [TestMethod]
        public void PostRequestWithJsonPayload()
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
            // Create the Request
            // Add the Request body
            // Send the Request

            RestClient client = new RestClient();

            RestRequest request = new RestRequest()
            {
                Resource = postUrl,
                Method = Method.Post
            };

            request.AddStringBody(jsonData, DataFormat.Json);

            RestResponse response = client.Post(request);
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

            Debug.WriteLine(response.Content);
        }
    }
}
