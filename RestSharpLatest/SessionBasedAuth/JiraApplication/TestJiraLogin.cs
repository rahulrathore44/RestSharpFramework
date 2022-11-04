using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using RestSharpLatest.SessionBasedAuth.JiraApplication.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpLatest.SessionBasedAuth.JiraApplication
{
    [TestClass]
    public class TestJiraLogin
    {
        private readonly string BaseUrl = "http://localhost:9191/";

        [TestMethod]
        public void LoginIntoJira()
        {
            var client = new RestClient(BaseUrl);
            var request = new RestRequest()
            {
                Resource = "/rest/auth/1/session",
                Method = Method.Post
            };

            request.AddJsonBody(new {
                username = "rahul",
                password = "admin@1234#"
            });

            var response = client.Execute<LoginResponse>(request);
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }
    }
}
