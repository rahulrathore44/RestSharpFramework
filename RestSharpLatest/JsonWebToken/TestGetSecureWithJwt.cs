using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using RestSharpLatest.APIHelper;
using RestSharpLatest.APIHelper.APIRequest;
using RestSharpLatest.APIHelper.Client;
using RestSharpLatest.APIHelper.Command;
using RestSharpLatest.JsonWebToken.Model;


namespace RestSharpLatest.JsonWebToken
{
    [TestClass]
    public class TestGetSecureWithJwt
    {
        private static readonly string BaseUrl = "http://localhost:9191/";
        private static RestApiExecutor apiExecutor;
        private static IClient client;

        [ClassInitialize]
        public static void SetUp(TestContext testContext)
        {
            client = new TracerClient();
            client = new AuthenticationDecorato(client, new JsonWebTokenAuthenticator(BaseUrl, new User()
            {
                Id = 1,
                Username = "John",
                Password = "Admin@1234#"
            }));

            apiExecutor = new RestApiExecutor();
        }

        [ClassCleanup]
        public static void TearDown()
        {
            client?.Dispose();
        }

        [TestMethod]
        public void Secure_Get_With_Jwt()
        {
            var getRequest = new GetRequestBuilder().WithUrl(BaseUrl + "auth/webapi/all");
            var command = new RequestCommand(getRequest, client);
            apiExecutor.SetCommand(command);

            var response = apiExecutor.ExecuteRequest();
            response.GetHttpStatusCode().Should().Be(System.Net.HttpStatusCode.OK);

            response = apiExecutor.ExecuteRequest();
            response.GetHttpStatusCode().Should().Be(System.Net.HttpStatusCode.OK);
        }

        [TestMethod]
        public void Secure_Get_With_Jwt_RestClient()
        {
            var client = new RestClient()
            {
                Authenticator = new JsonWebTokenAuthenticator(BaseUrl , new User()
                {
                    Id = 3,
                    Username = "James_2",
                    Password = "Testing@"
                })
            };

            var request = new RestRequest()
            {
                Resource = BaseUrl + "auth/webapi/all",
                Method = Method.Get
            };

            var response  = client.Execute(request);
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

            response = client.Execute(request);
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }

    }
}
