using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using System;
using WebServiceAutomation.Model.XmlModel;

namespace RestSharpLatest.Assignment.POST_Request_with_De_serialization_of_XML_response_body
{
    [TestClass]
    public class Assignment_POST_Request_with_De_serialization_XML_body
    {
        private Random random = new Random();
        private string postUrl = "http://localhost:8081/laptop-bag/webapi/api/add";

        [TestMethod]
        public void SendPostRequestWithXMLBody()
        {
            int id = random.Next(1000);

            // XML Request Body
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

            // Add the xml request body to the request
            request.AddStringBody(xmlData, DataFormat.Xml);

            // Add the request header to accept the response in XML
            request.AddHeader("Accept", "application/xml");

            // Execute the POST request and de-serialize the XML response body to an object
            var response = client.ExecutePost<Laptop>(request);

            // Validate the status code.
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            
            // Validate the BrandName property.
            response.Data.BrandName.Should().NotBeNull();
            // Validate the LaptopName property.
            response.Data.LaptopName.Should().NotBeNull();

            // Release the resource acquired by the client.
            client.Dispose();
        }
    }
}
