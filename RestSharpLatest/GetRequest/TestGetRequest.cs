using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using System.Diagnostics;


namespace RestSharpLatest.GetRequest
{
    [TestClass]
    public class TestGetRequest
    {
        private readonly string getUrl = "http://localhost:8081/laptop-bag/webapi/api/all";

        public TestContext TestContext { get; set; }

        [TestMethod]
        public void GetRequest()
        {
            RestClient client = new RestClient();
            RestRequest getRequest = new RestRequest(getUrl);
            // Get, later - ExecuteGet
            RestResponse response = client.ExecuteGet(getRequest);
            // Console.WriteLine()

            // Debug Class

            // Status Code
            Debug.WriteLine($"Response Status Code - {response.StatusCode}");

            // Error Message
            Debug.WriteLine($"Error Message - {response.ErrorMessage}");
            // Exception
            Debug.WriteLine($"Exception - {response.ErrorException}");

        }
    }
}
