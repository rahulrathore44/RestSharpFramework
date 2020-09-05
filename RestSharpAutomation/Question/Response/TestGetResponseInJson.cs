using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpAutomation.Question.Response
{
    [TestClass]
    public class TestGetResponseInJson
    {
        private string getUrl = "https://reqres.in/api/user";

        [TestMethod]
        public void Test_XMLDeserialization()
        {
            IRestClient restClient = new RestClient();
            IRestRequest restRequest = new RestRequest(getUrl);
            restClient.AddDefaultHeader("Accept", "application/json");
            IRestResponse response = restClient.Get(restRequest);
            Console.WriteLine(response.Content);
        }

    }
}
