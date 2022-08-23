using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using RestSharpLatest.APIHelper;
using RestSharpLatest.APIHelper.APIRequest;
using RestSharpLatest.APIHelper.Client;
using RestSharpLatest.APIHelper.Command;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.Json;


namespace RestSharpLatest.PostRequest
{
    [TestClass]
    public class TestMockAbleIo
    {
        private readonly string PostUrl = "http://demo8650956.mockable.io";
        private static IClient _client;
        private static RestApiExecutor apiExecutor;

        [ClassInitialize]
        public static void SetUp(TestContext testContext)
        {
            _client = new TracerClient();
            apiExecutor = new RestApiExecutor();
        }

        [TestMethod]
        public void ParseAndValidateTheJson()
        {
            // Create the request
            var postRequest = new PostRequestBuilder().WithUrl(PostUrl);
            // Create the command
            var command = new RequestCommand(postRequest, _client);
            // Set the command
            apiExecutor.SetCommand(command);
            // Execute the request
            var response = apiExecutor.ExecuteRequest();
            // Capture the response

            response.GetHttpStatusCode().Should().Be(System.Net.HttpStatusCode.OK);
            var responseContent = response.GetResponseData();
            Debug.WriteLine(responseContent);


            // Parse the Given JSON document
            JObject jsonToValidate = JObject.Parse(responseContent);
            // Use the json path to query the json document
            // type caste the result of the query if needed
            var loggedOut = (bool)jsonToValidate.SelectToken("$.responseContext.mainAppWebResponseContext.loggedOut");

            loggedOut.Should().BeTrue();

            var headerJsonObject = jsonToValidate.SelectToken("$.header");
            Header headers = JsonSerializer.Deserialize<Header>(headerJsonObject.ToString());
            headers.feedTabbedHeaderRenderer.Should().NotBeNull();
        }

        [TestMethod]
        public void ParseAndValidateTheJson_Timing()
        {
            Stopwatch stopwatch = new Stopwatch();

            // Create the request
            var postRequest = new PostRequestBuilder().WithUrl(PostUrl);
            // Create the command
            var command = new RequestCommand(postRequest, _client);
            // Set the command
            apiExecutor.SetCommand(command);
            // Execute the request

            stopwatch.Start();
            var response = apiExecutor.ExecuteRequest();
            // Capture the response
            stopwatch.Stop();

            Debug.WriteLine($"Execute Request took {stopwatch.Elapsed.Milliseconds}");

            response.GetHttpStatusCode().Should().Be(System.Net.HttpStatusCode.OK);
            var responseContent = response.GetResponseData();
            Debug.WriteLine(responseContent);


            // Parse the Given JSON document
            JObject jsonToValidate = JObject.Parse(responseContent);
            // Use the json path to query the json document
            // type caste the result of the query if needed
            var loggedOut = (bool)jsonToValidate.SelectToken("$.responseContext.mainAppWebResponseContext.loggedOut");

            loggedOut.Should().BeTrue();

            var headerJsonObject = jsonToValidate.SelectToken("$.header");
            Header headers = JsonSerializer.Deserialize<Header>(headerJsonObject.ToString());
            headers.feedTabbedHeaderRenderer.Should().NotBeNull();
        }

        [ClassCleanup]
        public static void TearDown()
        {
            _client?.Dispose();
        }
    }

    // Model class for Sub Object
    class Runs
    {
        public string text { get; set; }

    }
    class Title
    {
        public IList<Runs> runs { get; set; }

    }
    class FeedTabbedHeaderRenderer
    {
        public Title title { get; set; }

    }

    class Header
    {
        public FeedTabbedHeaderRenderer feedTabbedHeaderRenderer { get; set; }

    }

    class Application
    {
        public Header header { get; set; }

    }
}
