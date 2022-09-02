using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using RestSharpLatest.APIModel.JsonApiModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpLatest.QueryParameters
{
    [TestClass]
    public class TestQueryParameters
    {
        private string PostUrl = "http://localhost:8081/laptop-bag/webapi/api/add";
        private string QueryUrl = "http://localhost:8081/laptop-bag/webapi/api/query";
        private Random random = new Random();

        // POST - Create an entry in the Test Application.
        // GET With query param - To fetch the created entry.

        [TestMethod]
        public void TestGetWithQueryParams()
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

            RestResponse response = client.ExecutePost(request);
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

            request = new RestRequest()
            {
                Resource = QueryUrl,
                Method = Method.Get
            };

            request.AddParameter("id", id);
            request.AddParameter("laptopName", "Alienware M17");

            //request.AddQueryParameter("id", id); / For PUT, POST

            var getresponse = client.ExecuteGet<JsonModel>(request);
            getresponse.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            getresponse.Data.Id.Should().Be(id);
            getresponse.Data.LaptopName.Should().Be("Alienware M17");
        }
    }
}
