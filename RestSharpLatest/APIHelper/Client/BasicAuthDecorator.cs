using RestSharp;
using RestSharp.Authenticators;

namespace RestSharpLatest.APIHelper.Client
{
    public class BasicAuthDecorator : IClient
    {
        private readonly IClient _client;

        public BasicAuthDecorator(IClient client) // Default Client // Tracer Client
        {
            _client = client; 
        }

        public void Dispose()
        {
            _client.Dispose();
        }

        public RestClient GetClient()
        {
            //1. Invoke the GetClient API on the given client
            var newClient = _client.GetClient(); // Plain Client , // Rest Client with tracer
            //2. The set the authentication configuration
            newClient.Authenticator = new HttpBasicAuthenticator("admin", "welcome");
            //3. return the new client
            return newClient; // plain Client + Basic Auth info, // Rest Client with tracer + Basic Auth info
        }
    }
}
