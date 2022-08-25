using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using RestSharpLatest.APIModel.JsonApiModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpLatest.PutRequest
{
    [TestClass]
    public class TestPutRequest
    {
        // POST - Create the entry in the test application
        // PUT - Update the created entry
        // GET - Fetch the entry and verify it

        private readonly string PostUrl = "http://localhost:8081/laptop-bag/webapi/api/add";
        private readonly string PutUrl = "http://localhost:8081/laptop-bag/webapi/api/update";
        private readonly string GetUrl = "http://localhost:8081/laptop-bag/webapi/api/find/";
        private readonly Random random = new Random();

        [TestMethod]
        public void TestPutRequestWithJson()
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

            var response = client.ExecutePost<JsonModel>(request);
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

            jsonData = "{" +
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
                                    "\"LaptopName\": \"Alienware SDK\"" +
                                "}";

            request = new RestRequest()
            {
                Resource = PutUrl,
                Method = Method.Put
            };

            request.AddStringBody(jsonData, DataFormat.Json);

            response = client.ExecutePut<JsonModel>(request);
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

            request = new RestRequest()
            {
                Resource = GetUrl + id,
                Method = Method.Get
            };

            response = client.ExecuteGet<JsonModel>(request);
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            response.Data.LaptopName.Should().Be("Alienware SDK");
            client?.Dispose();

        }
    }
}
