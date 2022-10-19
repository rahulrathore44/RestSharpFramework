using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WebServiceAutomation.Model.XmlModel;

namespace RestSharpLatest.Assignment.Assignment___GET_Request
{
    [TestClass]
    public class Assignment_Get_Request
    {
        // GET end point URL
        private readonly string getUrl = "http://localhost:8081/laptop-bag/webapi/api/all";

        [TestMethod]
        public void SendRequestWithExecuteAPI()
        {
            // Create the Client.
            RestClient client = new RestClient();

            // Sets the RestClient to only use XML.
            client.UseXml();

            // Create the Request.
            RestRequest getRequest = new RestRequest()
            {
                // Set the Http Method.
                Method = Method.Get,
                // Set the Get end-point url.
                Resource = getUrl
            };

            // Send the GET request and de-serialize the response.
            var response = client.Execute<LaptopDetailss>(getRequest);

            // Validate the status code.
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            // Extract the List of objects.
            var content = response.Data.Laptop;

            // Extract a single object.
            var xmlObject = content.Find((item) => {
                return "1".Equals(item.Id, StringComparison.OrdinalIgnoreCase);
            });

            // Validate the BrandName property.
            xmlObject.BrandName.Should().NotBeNull();
            // Validate the LaptopName property.
            xmlObject.LaptopName.Should().NotBeNull();

            // Release the resource acquired by the client.
            client?.Dispose();

        }
    }
}
