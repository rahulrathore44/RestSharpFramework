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
    public class TestJiraEndToEndFlow
    {
        private const string LoginEndPoint = "/rest/auth/1/session"; // POST
        private const string LogoutEndPoint = "/rest/auth/1/session"; // DELETE
        private const string CreateProjectEndPoint = "/rest/api/2/project"; // POST
        private static IRestClient client;
        private static IRestResponse<LoginResponse> LoginResponse;

        /**
         * 1 Login into JIRS --> Class Initialize
         * 2. Create the Project
         * 3. Logout from JIRA --> Class Cleanup 
         * 
         * **/

        [ClassInitialize]
        public static void Login(TestContext context)
        {
            client = new RestClient()
            {
                BaseUrl = new Uri("http://localhost:9191")
            };
            IRestRequest request = new RestRequest()
            {
                Resource = LoginEndPoint
            };
            JiraLogin jiraLogin = new JiraLogin()
            {
                username = "rahul",
                password = "admin@1234#"
            };
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(jiraLogin);
            request.AddHeader("Content-Type", "application/json");
            LoginResponse = client.Post<LoginResponse>(request);
            Assert.AreEqual(200, (int)LoginResponse.StatusCode);
        }

        [ClassCleanup]
        public static void Logout()
        {
            IRestRequest request = new RestRequest()
            {
                Resource = LogoutEndPoint
            };
            request.AddCookie(LoginResponse.Data.session.name, LoginResponse.Data.session.value);
            var response = client.Delete(request);
            Assert.AreEqual(204, (int)response.StatusCode);
        }

        [TestMethod]
        public void CreateProject()
        {
            CreateProjectPayload createProjectPayload = new CreateProjectPayload();
            IRestRequest restRequest = new RestRequest()
            {
                Resource = CreateProjectEndPoint
            };
            restRequest.RequestFormat = DataFormat.Json;
            restRequest.AddBody(createProjectPayload);
            restRequest.AddHeader("Content-Type", "application/json");
            restRequest.AddCookie(LoginResponse.Data.session.name, LoginResponse.Data.session.value);
            var response = client.Post<CreateProjectResponse>(restRequest);
            Assert.AreEqual(201, (int)response.StatusCode);
        }

    }
}
