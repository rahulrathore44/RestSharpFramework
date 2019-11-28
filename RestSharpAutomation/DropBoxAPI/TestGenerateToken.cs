using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpAutomation.DropBoxAPI
{
    [TestClass]
    public class TestGenerateToken
    {
        private const string AuthorizeUrl = "https://www.dropbox.com/oauth2/authorize";
        private const string GenerateTokenUrl = "https://api.dropboxapi.com/oauth2/token";
        private const string OAuth1 = "https://api.dropboxapi.com/2/auth/token/from_oauth1";

        [TestMethod]
        public void TestGetToken()
        {
            string body = "{\"oauth1_token\": \"keg8sdb86l4kdlp\",\"oauth1_token_secret\": \"kusfz7e4twb60k4\"}";
            IRestClient client = new RestClient();
            client.Authenticator = new HttpBasicAuthenticator("keg8sdb86l4kdlp", "kusfz7e4twb60k4");
            IRestRequest request = new RestRequest()
            {
                Resource = GenerateTokenUrl
            };
            
            request.AddParameter("code", "authorization_code", ParameterType.QueryString);
            request.AddParameter("grant_type", "authorization_code", ParameterType.QueryString);
            //request.AddHeader("Content-Type", "application/json");
            //request.RequestFormat = DataFormat.Json;
            //request.AddBody(body);
            var response = client.Post(request);
            Assert.IsNotNull(response.StatusCode);


        }

    }
}
