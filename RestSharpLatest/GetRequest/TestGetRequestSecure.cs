using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using RestSharp.Authenticators;
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
    }
}
