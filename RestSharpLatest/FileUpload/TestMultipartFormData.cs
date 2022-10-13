using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using RestSharpLatest.APIHelper;
using RestSharpLatest.APIHelper.APIRequest;
using RestSharpLatest.APIHelper.Client;
using RestSharpLatest.APIHelper.Command;
using System.IO;

namespace RestSharpLatest.FileUpload
{
    [TestClass]
    public class TestMultipartFormData
    {
        private readonly string BasePath = "http://localhost:9191/";
        private static RestApiExecutor apiExecutor;
        private static IClient client;

        [ClassInitialize]
        public static void Setup(TestContext testContext)
        {
            client = new TracerClient();
            apiExecutor = new RestApiExecutor();
        }

        [ClassCleanup]
        public static void TearDown()
        {
            client?.Dispose();
        }

        [TestMethod]
        public void File_Upload()
        {
            // Create the client
            var client = new RestClient(BasePath);
            // Create the Request
            var fileUploadRequest = new RestRequest()
            {
                Resource = "/normal/webapi/upload",
                Method = Method.Post
            };
            // Read and store the file content in a byte array
            var fileContent = File.ReadAllBytes(@"C:\Data\log\TestData.xlsx");
            // call the Add file api and pass the byte array
            fileUploadRequest.AddFile("file", fileContent, "TestData.xlsx", "multipart/form-data");
            // send the request
            var resposne = client.Execute(fileUploadRequest);
            // verify the response status code.
            resposne.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            client.Dispose();
        }

        [TestMethod]
        public void File_Upload_Using_Framework()
        {
            var fileContent = File.ReadAllBytes(@"C:\Data\log\TestData.xlsx");
            var fileUploadRequest = new PostRequestBuilder().WithUrl(BasePath + "normal/webapi/upload").WithFileUpload("file", fileContent, "TestData.xlsx");
            var command = new RequestCommand(fileUploadRequest, client);
            apiExecutor.SetCommand(command);
            var response = apiExecutor.ExecuteRequest();
            response.GetHttpStatusCode().Should().Be(System.Net.HttpStatusCode.OK);
        }

    }
}
