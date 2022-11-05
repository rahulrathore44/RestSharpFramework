using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using RestSharpLatest.APIHelper;
using RestSharpLatest.APIHelper.APIRequest;
using RestSharpLatest.APIHelper.Client;
using RestSharpLatest.APIHelper.Command;
using RestSharpLatest.SessionBasedAuth.JiraApplication.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpLatest.SessionBasedAuth.JiraApplication
{
    [TestClass]
    public class TestCreateProject
    {
        private static IJiraClient _jiraClient;
        private static string BaseUrl = "http://localhost:9191/";
        private static RestApiExecutor _apiExecutor;
        
        [ClassInitialize]
        public static void Setup(TestContext testContext)
        {
            _jiraClient = new JiraClient(BaseUrl);
            _apiExecutor = new RestApiExecutor();
            _jiraClient.Login(new AdminJiraUser("rahul", "admin@1234#"));
        }

        [ClassCleanup]
        public static void TearDown()
        {
            _jiraClient.Logout();
            _jiraClient.Dispose();
        }

        [TestMethod]
        public void VerifyCreateProject()
        {
            // Create the request body using the model clas
            var requestBody = new CreateProjectPayload();

            // Create the post request using PostRequestBuilder class
            var postRequest = new PostRequestBuilder().WithUrl("rest/api/2/project").WithBody(requestBody, RequestBodyType.JSON);
            
            // Create the Request Command and set the command on the Api executor
            var command = new RequestCommand(postRequest, _jiraClient);
            _apiExecutor.SetCommand(command);
            var response = _apiExecutor.ExecuteRequest();

            // Execute the request & add the validation on response status code.
            response.GetHttpStatusCode().Should().Be(System.Net.HttpStatusCode.Created);
            
            // Extract the project key from the response.
            var responseObject = JObject.Parse(response.GetResponseData());
            var projectKey = (string)responseObject.SelectToken("$.key");
            
            // Create the Get request using GetRequestBuilder Class
            var getRequest = new GetRequestBuilder().WithUrl($"/rest/api/2/project/{projectKey}");
            
            // Create the Request Command and set the command on the Api executor
            command = new RequestCommand(getRequest, _jiraClient);
            _apiExecutor.SetCommand(command);
            response = _apiExecutor.ExecuteRequest();
            
            // Execute the request & add the validation on response status code.
            response.GetHttpStatusCode().Should().Be(System.Net.HttpStatusCode.OK);
        }

        [TestMethod]
        public void VerifyCreateProjectSecondTime()
        {
            // Create the request body using the model clas

            var requestBody = new CreateProjectPayload();

            // Create the post request using PostRequestBuilder class

            var postRequest = new PostRequestBuilder().WithUrl("rest/api/2/project").WithBody(requestBody, RequestBodyType.JSON);
            // Create the Request Command and set the command on the Api executor
            var command = new RequestCommand(postRequest, _jiraClient);
            _apiExecutor.SetCommand(command);
            var response = _apiExecutor.ExecuteRequest();

            // Execute the request & add the validation on response status code.
            response.GetHttpStatusCode().Should().Be(System.Net.HttpStatusCode.Created);
            // Extract the project key from the response.
            var responseObject = JObject.Parse(response.GetResponseData());
            var projectKey = (string)responseObject.SelectToken("$.key");
            // Create the Get request using GetRequestBuilder Class

            var getRequest = new GetRequestBuilder().WithUrl($"/rest/api/2/project/{projectKey}");
            // Create the Request Command and set the command on the Api executor
            command = new RequestCommand(getRequest, _jiraClient);
            _apiExecutor.SetCommand(command);
            response = _apiExecutor.ExecuteRequest();
            // Execute the request & add the validation on response status code.
            response.GetHttpStatusCode().Should().Be(System.Net.HttpStatusCode.OK);
        }
    }
}
