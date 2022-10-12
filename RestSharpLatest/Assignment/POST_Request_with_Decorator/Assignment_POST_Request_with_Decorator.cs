using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharpLatest.APIHelper;
using RestSharpLatest.APIHelper.APIRequest;
using RestSharpLatest.APIHelper.Client;
using RestSharpLatest.APIHelper.Command;
using RestSharpLatest.APIModel.JsonApiModel;
using System;
using System.Collections.Generic;

namespace RestSharpLatest.Assignment.POST_Request_with_Decorator
{
    [TestClass]
    public class Assignment_POST_Request_with_Decorator
    {
        private readonly string SecurePostUrl = "http://localhost:8081/laptop-bag/webapi/secure/add";

        private static IClient authClient;
        private static RestApiExecutor apiExecutor;
        private readonly Random random = new Random();

        //ClassInitialize - a method that contains code that must be used before any of the tests in the test class have run
        [ClassInitialize]
        public static void Setup(TestContext testContext)
        {
            // Invoke the decorator with the tracer client impl
            authClient = new BasicAuthDecorator(new TracerClient());
            // Create the Executor
            apiExecutor = new RestApiExecutor();
        }

        //ClassCleanup - a method that contains code to be used after all the tests in the test class have run
        [ClassCleanup]
        public static void TearDown()
        {
            // Release the resource acquired by the client.
            authClient?.Dispose();
        }


        // Assignment - POST Request with BasicAuthDecorator using Framework API
        [TestMethod]
        public void TestSecurePostWithDecorator()
        {
            int id = random.Next(1000);

            // Create the request body.
            var payload = new JsonModelBuilder().WithId(id).WithBrandName("Alienware").WithLaptopName("Alienware M17").WithFeatures(new List<string>() { "8th Generation Intel® Core™ i5-8300H", "Windows 10 Home 64-bit English" }).Build();

            // Create the Post Request
            var request = new PostRequestBuilder().WithUrl(SecurePostUrl).WithBody<JsonModel>(payload, RequestBodyType.JSON);

            // Create the Command for the post request.
            var command = new RequestCommand(request, authClient);

            // Set the command for the RestApiExecutor.
            apiExecutor.SetCommand(command);

            // Send the POST request and De-Serialize the response to an object.
            var response = apiExecutor.ExecuteRequest<JsonModel>();

            // Validate the status code using Fluent API.
            response.GetHttpStatusCode().Should().Be(System.Net.HttpStatusCode.OK);

            // Validate the BrandName property.
            response.GetResponseData().BrandName.Should().NotBeNull();
            // Validate the LaptopName property.
            response.GetResponseData().LaptopName.Should().NotBeNull();
        }
    }
}
