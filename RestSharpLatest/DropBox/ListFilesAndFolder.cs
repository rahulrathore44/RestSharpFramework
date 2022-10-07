using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using RestSharp.Authenticators;
using RestSharpLatest.APIHelper;
using RestSharpLatest.APIHelper.APIRequest;
using RestSharpLatest.APIHelper.Client;
using RestSharpLatest.APIHelper.Command;
using RestSharpLatest.DropBox.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpLatest.DropBox
{
    [TestClass]
    public class ListFilesAndFolder
    {
        private readonly string BaseUrl = "https://api.dropboxapi.com/2";
        private static readonly string Token = "<Your Token>";

        private static IClient client;
        private static IClient authClient;
        private static RestApiExecutor apiExecutor;

        [ClassInitialize]
        public static void SetUp(TestContext testContext)
        {
            client = new TracerClient();
            authClient = new AuthenticationDecorato(client, new JwtAuthenticator(Token));
            //authClient = new AuthenticationDecorato(client, new HttpBasicAuthenticator("","")); // For Basic Auth
            apiExecutor = new RestApiExecutor();

        }

        [ClassCleanup]
        public static void TearDown()
        {
            authClient?.Dispose();
        }

        [TestMethod]
        public void GetAllFilesAndFolder()
        {
            var contextPath = "/files/list_folder";

            var requestBody = "{\"include_deleted\":false,\"include_has_explicit_shared_members\":false,\"include_media_info\":false,\"include_mounted_folders\":true,\"include_non_downloadable_files\":true,\"path\":\"\",\"recursive\":false}";

            var client = new RestClient()
            {
                Authenticator = new JwtAuthenticator(Token)
            };

            var request = new RestRequest()
            {
                Resource = BaseUrl + contextPath,
                Method = Method.Post
            };
            request.AddStringBody(requestBody, DataFormat.Json);
            var response = client.ExecutePost<Root>(request);
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            client.Dispose();
        }

        [TestMethod]
        public void GetAllFilesAndFolder_with_Framework() {
            // request body
            var contextPath = "/files/list_folder";

            var requestBody = "{\"include_deleted\":false,\"include_has_explicit_shared_members\":false,\"include_media_info\":false,\"include_mounted_folders\":true,\"include_non_downloadable_files\":true,\"path\":\"\",\"recursive\":false}";
            // Post request
            var postrequest = new PostRequestBuilder().WithUrl(BaseUrl + contextPath).WithBody(requestBody, RequestBodyType.STRING);
            // Request command
            var command = new RequestCommand(postrequest, authClient);
            // set the command on api executor
            apiExecutor.SetCommand(command);
            // execute the request
            var response = apiExecutor.ExecuteRequest<Root>();
            // validate the response status
            response.GetHttpStatusCode().Should().Be(System.Net.HttpStatusCode.OK);

        }
        
        [TestMethod]
        public void CreateFolder_with_framework()
        {
            // request body
            var contextPath = "/files/create_folder_v2";

            var requestBody = "{\"autorename\":true,\"path\":\"/TestFolder\"}";
            // Post request
            var postrequest = new PostRequestBuilder().WithUrl(BaseUrl + contextPath).WithBody(requestBody, RequestBodyType.STRING);
            // Request command
            var command = new RequestCommand(postrequest, authClient);
            // set the command on api executor
            apiExecutor.SetCommand(command);
            // execute the request
            var response = apiExecutor.ExecuteRequest<Root>();
            // validate the response status
            response.GetHttpStatusCode().Should().Be(System.Net.HttpStatusCode.OK);
        }
    }
}
