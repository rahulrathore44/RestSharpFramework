﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using WebServiceAutomation.Model.JsonModel;
using FluentAssertions;


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

        [TestMethod]
        public void GetRequestWithJsonAndDeserialize()
        {
            RestClient client = new RestClient();
            RestRequest getRequest = new RestRequest(getUrl);
            RestResponse<List<JsonRootObject>> response = client.ExecuteGet<List<JsonRootObject>>(getRequest);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            if (response.IsSuccessful)
            {
                // Status Code
                Debug.WriteLine($"Response Status Code - {response.StatusCode}");
                // Content
                Debug.WriteLine($"Response Content Size - {response.Data}");

                response.Data.ForEach((item) => {
                    Debug.WriteLine($"Response ID is - {item.Id}");
                });

                JsonRootObject jsonRootObject = response.Data.Find((item) => {
                    return item.Id == 1;
                });

                Assert.AreEqual("Alienware", jsonRootObject.BrandName);

                jsonRootObject.BrandName.Should().NotBeEmpty();
                jsonRootObject.BrandName.Should().Be("Alienware1");

                Assert.IsTrue(jsonRootObject.Features.Feature.Contains("Windows 10 Home 64-bit English"), "Element Not Found");

                jsonRootObject.Features.Feature.Should().NotBeEmpty();
                jsonRootObject.Features.Feature.Contains("Windows 10 Home 64-bit English");
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
