using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using RestSharpAutomation.Jwt_Token.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpAutomation.Jwt_Token
{
    [TestClass]
    public class TestJwtToken
    {
        /***
         1. Register the user with the enpoint https://jobapplicationjwt.herokuapp.com/users/sign-up
         2. Autheticate the user and generate the token https://jobapplicationjwt.herokuapp.com/users/authenticate
         3. Extract the token from the response
         4. Pass the token in the header for the Get Request https://jobapplicationjwt.herokuapp.com/auth/webapi/all
         
         */

        private string RegisterUrl = "https://jobapplicationjwt.herokuapp.com/users/sign-up";
        private string AuthenticateUrl = "https://jobapplicationjwt.herokuapp.com/users/authenticate";
        private string GetAllUrl = "https://jobapplicationjwt.herokuapp.com/auth/webapi/all";
        private IRestClient client;
        private IRestRequest request;
        private string token;
        private string user = "{ \"password\": \"Guns and Bikes\",  \"username\": \"John Wick\"}";


        [TestInitialize]
        public void SetUp()
        {
            client = new RestClient();
            // Registration
            request = new RestRequest()
            {
                Resource = RegisterUrl
            };
            request.AddJsonBody(user);
            var response = client.Post(request);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            // Generate the token

            request = new RestRequest()
            {
                Resource = AuthenticateUrl
            };
            request.AddJsonBody(user);
            var responseToken = client.Post<JwtToken>(request);
            Assert.AreEqual(HttpStatusCode.OK, responseToken.StatusCode);
            token = responseToken.Data.token; // JWT token
        }

        [TestMethod]
        public void TestGetWithJwt()
        {
            request = new RestRequest()
            {
                Resource = GetAllUrl
            };
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Authorization", "Bearer " + token);
            var response = client.Get(request);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

    }
}
