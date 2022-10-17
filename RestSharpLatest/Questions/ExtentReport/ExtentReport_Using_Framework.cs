using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharpAutomation.ReportAttribute;
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

namespace RestSharpLatest.Questions.ExtentReport
{
    [TestClass]
    public class ExtentReport_Using_Framework
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

        [TestMethodWithReport]
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
