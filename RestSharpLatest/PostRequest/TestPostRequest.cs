﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using System;
using FluentAssertions;
using System.Diagnostics;
using WebServiceAutomation.Model.JsonModel;
using RestSharpLatest.APIModel.JsonApiModel;

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
    }
}
