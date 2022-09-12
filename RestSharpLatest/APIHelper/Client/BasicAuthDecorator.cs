using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpLatest.APIHelper.Client
{
    public class BasicAuthDecorator : IClient
    {
        private readonly IClient _client;

        public BasicAuthDecorator(IClient client) // Default Client //Tracer Client
        {
            _client = client;
        }
        public void Dispose()
        {
            _client.Dispose();
        }

        public RestClient GetClient()
        {
            //1. Invoke _client.GetClient() API

            var newClient = _client.GetClient(); // Plain RestClient // Rest Client + Tracer

            //2. Add the auth configuration
            newClient.Authenticator = new HttpBasicAuthenticator("admin", "welcome");
            //3. return the new client
            return newClient; // Plain RestClient + Auth Config 
            //  Rest Client + Tracer + Auth Config
        }
    }
}
