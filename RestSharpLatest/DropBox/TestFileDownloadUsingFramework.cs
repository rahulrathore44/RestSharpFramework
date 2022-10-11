using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp.Authenticators;
using RestSharpLatest.APIHelper;
using RestSharpLatest.APIHelper.APIRequest;
using RestSharpLatest.APIHelper.Client;
using RestSharpLatest.APIHelper.Command;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpLatest.DropBox
{
    [TestClass]
    public class TestFileDownloadUsingFramework
    {
        private static RestApiExecutor restApiExecutor;
        private static IClient client;
        private static IClient authClient;
        private static string Token = "<Your Token>";
        private readonly string BasePath = "https://content.dropboxapi.com/2";

        [ClassInitialize]
        public static void Setup(TestContext testContext)
        {
            client = new TracerClient();
            authClient = new AuthenticationDecorato(client, new JwtAuthenticator(Token));
            restApiExecutor = new RestApiExecutor();

        }

        [ClassCleanup]
        public static void TearDown()
        {
            authClient?.Dispose();
        }

        [TestMethod]
        public void DownloadFile_UsingFramework()
        {
            var fileName = "Video.mp4";
            var contextPath = "/files/download";
            var location = "{\"path\":\"/" + fileName + "\"}";

            var postRequest = new PostRequestBuilder().WithUrl(BasePath + contextPath).WithHeaders(new Dictionary<string, string>() { { "Dropbox-API-Arg", location } });

            var command = new DownloadRequestCommand(postRequest, authClient);
            restApiExecutor.SetCommand(command);
            var data = restApiExecutor.DownloadData();
            File.WriteAllBytes("New_" + fileName, data);
            File.Exists("New_" + fileName).Should().BeTrue();
        }

    }
}
