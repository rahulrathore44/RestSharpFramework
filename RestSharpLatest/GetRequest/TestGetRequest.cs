using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using System.Diagnostics;
using RestSharp.Serializers.Xml;
using System.Collections.Generic;
using WebServiceAutomation.Model.JsonModel;

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

        [TestMethod]
        public void GetRequestPrintResponseContent()
        {
            RestClient client = new RestClient();
            RestRequest getRequest = new RestRequest(getUrl);
            RestResponse response = client.ExecuteGet(getRequest);

            if (response.IsSuccessful)
            {
                // Status Code
                Debug.WriteLine($"Response Status Code - {response.StatusCode}");
                // Content
                Debug.WriteLine($"Response Content - {response.Content}");
            }
            else
            {
                // Error Message
                Debug.WriteLine($"Error Message - {response.ErrorMessage}");
                // Exception
                Debug.WriteLine($"Exception - {response.ErrorException}");
            }
        }

        [TestMethod]
        public void GetRequestPrintResponseContentInXml()
        {
            RestClient client = new RestClient();
            RestRequest getRequest = new RestRequest(getUrl);

            // Add header
            //getRequest.AddHeader("Accept", "application/xml");

            client.UseXml();

            RestResponse response = client.ExecuteGet(getRequest);

            if (response.IsSuccessful)
            {
                // Status Code
                Debug.WriteLine($"Response Status Code - {response.StatusCode}");
                // Content
                Debug.WriteLine($"Response Content - {response.Content}");
            }
            else
            {
                // Error Message
                Debug.WriteLine($"Error Message - {response.ErrorMessage}");
                // Exception
                Debug.WriteLine($"Exception - {response.ErrorException}");
            }
        }


        [TestMethod]
        public void GetRequestPrintResponseContentInJson()
        {
            RestClient client = new RestClient();
            RestRequest getRequest = new RestRequest(getUrl);

            // Add header
            //getRequest.AddHeader("Accept", "application/json");

            client.UseJson(); // Default configuration

            RestResponse response = client.ExecuteGet(getRequest);

            if (response.IsSuccessful)
            {
                // Status Code
                Debug.WriteLine($"Response Status Code - {response.StatusCode}");
                // Content
                Debug.WriteLine($"Response Content - {response.Content}");
            }
            else
            {
                // Error Message
                Debug.WriteLine($"Error Message - {response.ErrorMessage}");
                // Exception
                Debug.WriteLine($"Exception - {response.ErrorException}");
            }
        }

      

    }
}
