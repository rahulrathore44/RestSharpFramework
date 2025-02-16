using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace HttpInterceptor
{
    [TestClass]
    public class TestInterceptor
    {
        private readonly string GetUrl = "http://localhost:9191/secure/webapi/all";
        private readonly List<RestSharp.Interceptors.Interceptor> interceptors = new List<RestSharp.Interceptors.Interceptor>();

        [TestMethod]
        public void TestMethod1()
        {

            var interCeptor = new CustomInterceptor(KnownHeaders.Authorization, "Basic YWRtaW46d2VsY29tZQ==");
            interceptors.Add(interCeptor);

            // 1. Create the instance of RestClientOption
            // 2. Add the custom interceptor to the RestClientOption
            var option = new RestClientOptions()
            {
                Interceptors = interceptors
            };


            // 3. Create the Request

            var request = new RestRequest(GetUrl, Method.Get);

            // 4. Create the client using the RestCleintOption

            var client = new RestClient(option);

            // 5. Send the request
            var response = client.ExecuteGet(request);
            // 6. Print the response at the console.
            Console.WriteLine(response.Content);

        }
    }

    // 1. Intercept the request and add the auth header
    // 2. Intercept the response and encrypt the response content
    class CustomInterceptor : RestSharp.Interceptors.Interceptor
    {
        private readonly string headerName;
        private readonly string headerValue;

        public CustomInterceptor(string name, string value)
        {
            headerName = name;
            headerValue = value;
        }

        public override ValueTask BeforeRequest(RestRequest request, CancellationToken cancellationToken)
        {
            request.AddHeader(headerName, headerValue);
            return base.BeforeRequest(request, cancellationToken);
        }

        public override ValueTask AfterHttpRequest(HttpResponseMessage responseMessage, CancellationToken cancellationToken)
        {
            // 1. Get the response content

            var content = responseMessage.Content.ReadAsStringAsync().GetAwaiter().GetResult();

            // 2. Convert the response into byte array

            var contentInByteArray = System.Text.Encoding.UTF8.GetBytes(content);
            // 3. Entry the byte array

            var responseContent = Convert.ToBase64String(contentInByteArray);
            // 4. Add the new content to the response object
            responseMessage.Content = new StringContent(responseContent);

            return base.AfterHttpRequest(responseMessage, cancellationToken);
        }
    }
}
