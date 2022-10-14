using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharpLatest.APIHelper;
using RestSharpLatest.APIHelper.APIRequest;
using RestSharpLatest.APIHelper.Client;
using RestSharpLatest.APIHelper.Command;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebServiceAutomation.Model.JsonModel;

namespace RestSharpLatest.Assignment.GET_Request_with_De_serialization_using_FrameworkAPI
{
    [TestClass]
    public class Assignment_Get_Request_Framework
    {
        private static IClient client;
        private static RestApiExecutor executor;
        private readonly string getUrl = "http://localhost:8081/laptop-bag/webapi/api/all";

        //ClassInitialize - a method that contains code that must be used before any of the tests in the test class have run
        [ClassInitialize]
        public static void SetUp(TestContext testContext)
        {
            // Create the Default Client
            client = new DefaultClient();
            // Create the Executor
            executor = new RestApiExecutor();
        }

        // Assignment - GET Request with De-serialization using Framework API
        [TestMethod]
        public void GetRequest()
        {
            // Create the GET request.
            AbstractRequest request = new GetRequestBuilder().WithUrl(getUrl);

            // Create the Command for the GET request.
            ICommand getCommand = new RequestCommand(request, client);

            // Set the command for the RestApiExecutor.
            executor.SetCommand(getCommand);

            // Send the GET request and De-Serialize the response to an object.
            var response = executor.ExecuteRequest<List<JsonRootObject>>();

            // Validate the status code using Fluent API.
            response.GetHttpStatusCode().Should().Be(System.Net.HttpStatusCode.OK);

            // Extract a single object.
            var entry = response.GetResponseData().Find((item) => { return item.Id == 1; });

            // Validate the BrandName property.
            entry.BrandName.Should().NotBeNull();
            // Validate the LaptopName property.
            entry.LaptopName.Should().NotBeNull();

        }

        //ClassCleanup - a method that contains code to be used after all the tests in the test class have run
        [ClassCleanup]
        public static void TearDown()
        {
            // Release the resource acquired by the client.
            client?.Dispose();
        }
    }
}
