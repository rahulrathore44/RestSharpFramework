
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpAutomation.Mocks
{
    [TestClass]
    public class TestServiceWithMock
    {
        [TestMethod]
        public void TestGetOfProductCompositService()
        {
            IRestClient client = new RestClient("http://localhost:9090");
            IRestRequest request = new RestRequest()
            {
                Method = Method.GET,
                Resource = "/productinfo/123"
            };
            request.AddHeader("Accept", "application/json");
            var response = client.Get(request);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
