using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharpLatest.APIHelper;
using RestSharpLatest.APIHelper.APIRequest;
using RestSharpLatest.APIHelper.Client;
using RestSharpLatest.APIHelper.Command;
using FluentAssertions;
using System.Diagnostics;
using System.Collections.Generic;
using WebServiceAutomation.Model.JsonModel;

namespace RestSharpLatest.GetRequest
{
    [TestClass]
    public class TestGetWithFramework
    {
        private static IClient client;
        private static RestApiExecutor executor;
        private readonly string getUrl = "http://localhost:8081/laptop-bag/webapi/api/all";

        [ClassInitialize]
        public static void SetUp(TestContext testContext)
        {
            client = new DefaultClient();
            executor = new RestApiExecutor();
        }

        [TestMethod]
        public void GetRequest()
        {
            AbstractRequest request = new GetRequestBuilder().WithUrl(getUrl);
            ICommand getCommand = new RequestCommand(request, client);
            executor.SetCommand(getCommand);
            var response = executor.ExecuteRequest();
            response.GetHttpStatusCode().Should().Be(System.Net.HttpStatusCode.OK);
            Debug.WriteLine(response.GetResponseData());

            var typeResponse =  executor.ExecuteRequest<List<JsonRootObject>>();
            Debug.WriteLine("");
            Debug.WriteLine(typeResponse.GetResponseData());

            request = new GetRequestBuilder().WithUrl("http://www.google.com");
            getCommand = new RequestCommand(request, client);
            executor.SetCommand(getCommand);
            response = executor.ExecuteRequest();
            Debug.WriteLine("");
            Debug.WriteLine(response.GetResponseData());
        }

        [ClassCleanup]
        public static void TearDown()
        {
            client?.Dispose();
        }
    }
}
