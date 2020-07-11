using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WebServiceAutomation.JwtToken
{
    [TestClass]
    public class HandlingJwtToken
    {
        private readonly string registerurl = "https://jobapplicationjwt.herokuapp.com/users/sign-up";
        private readonly string authurl = "https://jobapplicationjwt.herokuapp.com/users/authenticate";
        private readonly string geturl = "https://jobapplicationjwt.herokuapp.com/auth/webapi/all";
        private string token = "";
        private readonly string body = "{  \"password\": \"Guns and Bikes\",  \"username\": \"John Wick\"}";
        private HttpClient client;
        private HttpRequestMessage request;

        private void RegisterUser()
        {
            request = new HttpRequestMessage(HttpMethod.Post, registerurl)
            {
                Content = new StringContent(body, Encoding.UTF8, "application/json")
            };
            Task<HttpResponseMessage> responseMessage = client.SendAsync(request);
            Assert.AreEqual(HttpStatusCode.OK, responseMessage.Result.StatusCode);
        }

        private void GetToken()
        {
            request = new HttpRequestMessage(HttpMethod.Post, authurl)
            {
                Content = new StringContent(body, Encoding.UTF8, "application/json")
            };
            Task<HttpResponseMessage> responseMessage = client.SendAsync(request);
            Assert.AreEqual(HttpStatusCode.OK, responseMessage.Result.StatusCode);
            var content = responseMessage.Result.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<RootNode>(content.Result);
            token = response.token;
        }
        
        [TestInitialize]
        public void Setup()
        {
            client = new HttpClient();
            RegisterUser();
            GetToken();
        }

        [TestMethod]
        public void SendRequstWithJwt()
        {
            request = new HttpRequestMessage(HttpMethod.Get, geturl);
            request.Headers.Add("Accept", "application/json");
            request.Headers.Add("Authorization", "Bearer " + token);
            Task<HttpResponseMessage> responseMessage = client.SendAsync(request);
            Assert.AreEqual(HttpStatusCode.OK, responseMessage.Result.StatusCode);
        }
     


    }
}
