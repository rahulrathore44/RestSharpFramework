using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using System;
using WebServiceAutomation.Model.XmlModel;

namespace RestSharpLatest.Assignment.PUT_Request_with_XML_body
{
    [TestClass]
    public class Assignment_PUT_Request_with_XML_body
    {
        // POST - Create the entry in the test application
        // PUT - Update the created entry
        // GET - Fetch the entry and verify it

        private readonly string PostUrl = "http://localhost:8081/laptop-bag/webapi/api/add";
        private readonly string PutUrl = "http://localhost:8081/laptop-bag/webapi/api/update";
        private readonly string GetUrl = "http://localhost:8081/laptop-bag/webapi/api/find/";
        private readonly Random random = new Random();

        [TestMethod]
        public void TestPutRequestWithXML()
        {
            int id = random.Next(1000);
            // XML Request Body for POST
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
                Resource = PostUrl,
                Method = Method.Post
            };

            // Add the xml request body to the request
            request.AddStringBody(xmlData, DataFormat.Xml);

            // Execute the POST request and de-serialize the XML response body to an object
            var response = client.ExecutePost<Laptop>(request);
            // Validate the status code.
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

            // XML Request Body for PUT request
            string putreqbody = "<Laptop>" +
                                    "<BrandName>Alienware</BrandName>" +
                                    "<Features>" +
                                       "<Feature>8th Generation Intel® Core™ i5 - 8300H</Feature>" +
                                       "<Feature>Windows 10 Home 64 - bit English</Feature>" +
                                       "<Feature>NVIDIA® GeForce® GTX 1660 Ti 6GB GDDR6</Feature>" +
                                       "<Feature>8GB, 2x4GB, DDR4, 2666MHz</Feature>" +
                                       // Feature updated
                                   "<Feature>New Feature</Feature>" +
                                     "</Features>" +
                                  "<Id> " + id + "</Id>" +
                                  "<LaptopName>Alienware M17</LaptopName>" +
                               "</Laptop>";

            // Create the Request
            request = new RestRequest()
            {
                Resource = PutUrl,
                Method = Method.Put
            };

            // Add the xml request body to the request
            request.AddStringBody(putreqbody, DataFormat.Xml);

            // Execute the PUT request and de-serialize the XML response body to an object
            response = client.ExecutePut<Laptop>(request);
            // Validate the status code.
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

            // Create the Request
            request = new RestRequest()
            {
                Resource = GetUrl + id,
                Method = Method.Get
            };

            // Add the request header to accept the response in XML
            request.AddHeader("Accept", "application/xml");

            // Execute the GET request and de-serialize the XML response body to an object
            response = client.ExecuteGet<Laptop>(request);
            // Validate the status code.
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

            // Validate the BrandName property.
            response.Data.BrandName.Should().NotBeNull();
            // Validate the LaptopName property.
            response.Data.LaptopName.Should().NotBeNull();
            // Validate the new value in Feature property.
            response.Data.Features.Feature.Contains("New Feature");
            // Release the resource acquired by the client.
            client?.Dispose();

        }
    }
}
