using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using RestSharp.Authenticators;
using RestSharpLatest.APIHelper;
using RestSharpLatest.APIHelper.APIRequest;
using RestSharpLatest.APIHelper.Client;
using RestSharpLatest.APIHelper.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpLatest.GetRequest
{
    [TestClass]
    public class TestGetRequestSecure
    {
        private readonly string SecureGetUrl = "http://localhost:8081/laptop-bag/webapi/secure/all";

        private static IClient _client;
        private static IClient authClient;
        private static RestApiExecutor apiExecutor;

        [ClassInitialize]
        public static void Setup(TestContext testContext)
        {
            // Created the Tracer client
            _client = new TracerClient(); 
            // Invok the decorator with tracer client impl
            authClient = new BasicAuthDecorator(_client);
            apiExecutor = new RestApiExecutor();
        }

        [ClassCleanup]
        public static void TearDown()
        {
            _client?.Dispose();
        }

        [TestMethod]
        public void TestGetRequestWithBasicAuth()
        {
            // Create the Client
            // Create the Request
            // Using the client send the request

            var restClient = new RestClient()
            {
                Authenticator = new HttpBasicAuthenticator("admin", "welcome")
            };
            // restClient.Authenticator = new HttpBasicAuthenticator("admin", "welcome")
            var getRequest = new RestRequest()
            {
                Method = Method.Get,
                Resource = SecureGetUrl
            };

            var response = restClient.ExecuteGet(getRequest);
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

        }

        [TestMethod]
        public void TestSecureGetUsingDecorator()
        {
            //1. Create the request using Get Request builder class
            var getRequest = new GetRequestBuilder().WithUrl(SecureGetUrl);
            //2. Create the request command
            var command = new RequestCommand(getRequest, authClient);
            //3. Set the command on the API executor
            apiExecutor.SetCommand(command);
            //4. Execute the request
            var response = apiExecutor.ExecuteRequest();
            //5. Capture the response
            //6. Add the validation on the response status code.
            response.GetHttpStatusCode().Should().Be(System.Net.HttpStatusCode.OK);

        }
    }
}
