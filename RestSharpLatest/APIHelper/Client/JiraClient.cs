using FluentAssertions;
using RestSharp;
using RestSharpLatest.SessionBasedAuth.JiraApplication.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RestSharpLatest.APIHelper.Client
{
    public class JiraClient : IJiraClient
    {
        private RestClient _restClient;
        private readonly RestClientOptions _restClientOptions;

        public JiraClient(string baseUrl)
        {
            _restClientOptions = new RestClientOptions(baseUrl);
            _restClient = new RestClient(_restClientOptions);
        }

        public void Dispose()
        {
            _restClient?.Dispose();
            _restClient = null;
        }

        public RestClient GetClient()
        {
            return _restClient;
        }

        public void Login(IJiraUser user)
        {
            // Send the post request to login inside the jira application
            var response = _restClient.PostJson<IJiraUser, LoginResponse>("rest/auth/1/session", user);
            // Add the validation on session information present in the response
            response.session.name.Should().NotBeNullOrEmpty();
            response.session.value.Should().NotBeNullOrEmpty();
            // Add the session information in the form of cookie to the client
            _restClient.AddCookie(response.session.name, response.session.value, "/", "localhost");
        }

        public void Logout()
        {
            // Create a Rest Request
            var request = new RestRequest()
            {
                Resource = "/rest/auth/1/session"
            };
            // Send the delete request
            var response = _restClient.Delete(request);
            // Add the validation on the response status code.
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.NoContent);
        }
    }

}

