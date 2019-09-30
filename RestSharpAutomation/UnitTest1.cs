using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;

namespace RestSharpAutomation
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod111()
        {
            RestClient restClient = new RestClient();
            RestRequest restRequest = new RestRequest("http://localhost:8080/laptop-bag/webapi/api/all", Method.GET);
            IRestResponse restResponse =  restClient.Get(restRequest);
            Console.WriteLine(restResponse.Content);
        }
    }
}
