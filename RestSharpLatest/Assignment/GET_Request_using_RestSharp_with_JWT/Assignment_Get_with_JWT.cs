using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using RestSharpLatest.JsonWebToken;
using RestSharpLatest.JsonWebToken.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpLatest.Assignment.GET_Request_using_RestSharp_with_JWT
{
    [TestClass]
    public class Assignment_Get_with_JWT
    {
        private static readonly string BaseUrl = "http://localhost:9191/";

        // Assignment - Use the JSON Web Token Authenticator with RestSharp Client
        [TestMethod]
        public void Secure_Get_With_Jwt_RestClient()
        {
            // Create the Client
            var client = new RestClient()
            {
                // Set the custom authenticator
                Authenticator = new JsonWebTokenAuthenticator(BaseUrl, new User()
                {
                    Id = 3,
                    Username = "James_2",
                    Password = "Testing@"
                })
            };

            // Create the Request
            var request = new RestRequest()
            {
                // Set the Get end-point url.
                Resource = BaseUrl + "auth/webapi/all",
                // Set the Http Method.
                Method = Method.Get
            };

            // Send the GET request
            var response = client.Execute(request);

            // Validate the status code.
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

            // Send the GET request
            response = client.Execute(request);

            // Validate the status code.
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

            // Release the resource acquired by the client.
            client?.Dispose();
        }
    }
}
