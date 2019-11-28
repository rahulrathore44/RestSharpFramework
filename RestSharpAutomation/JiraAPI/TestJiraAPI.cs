using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using RestSharpAutomation.JiraAPI.Request;
using RestSharpAutomation.JiraAPI.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpAutomation.JiraAPI
{
    [TestClass]
    public class TestJiraAPI
    {
        private const string LoginEndpoint = "/rest/auth/1/session";

        [TestMethod]
        public void TestJiraLogin()
        {
            JiraLogin jiraLogin = new JiraLogin()
            {
                username = "rahul",
                password = "admin@1234#"
            };

            IRestClient client = new RestClient()
            {
                BaseUrl = new Uri("http://localhost:9191")
            };

            IRestRequest request = new RestRequest()
            {
                Resource = LoginEndpoint
            };
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(jiraLogin);
            request.AddHeader("Contenet-Type", "application/json");

            var response = client.Post<LoginResponse>(request);
            Console.WriteLine(response.Data);

           
        }
    }
}
