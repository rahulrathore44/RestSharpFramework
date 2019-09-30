using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebServiceAutomation.Model;

namespace WebServiceAutomation.Helper.Request
{
    public class HttpClientAsyncHelper
    {
        private HttpClient httpClient;

        private HttpClient AddHeadersAndCreateHttpClient(Dictionary<string, string> httpHeader)
        {
            HttpClient httpClient = new HttpClient();

            if (null != httpHeader)
            {
                foreach (string key in httpHeader.Keys)
                {
                    httpClient.DefaultRequestHeaders.Add(key, httpHeader[key]);

                }
            }
            return httpClient;

        }

        /* private HttpRequestMessage CreateHttpRequestMessage(string requestUrl, HttpMethod httpMethod, HttpContent httpContent)
         {
             HttpRequestMessage httpRequestMessage = new HttpRequestMessage(httpMethod, requestUrl);
             if (!((httpMethod == HttpMethod.Get) || (httpMethod == HttpMethod.Delete)))
                 httpRequestMessage.Content = httpContent;
             return httpRequestMessage;
         }*/

        /*private RestResponse SendRequest(string requestUrl, HttpMethod httpMethod, HttpContent httpContent, Dictionary<string, string> httpHeader)
        {
            httpClient = AddHeadersAndCreateHttpClient(httpHeader);
            httpRequestMessage = CreateHttpRequestMessage(requestUrl, httpMethod, httpContent);

            try
            {
                Task<HttpResponseMessage> httpResponseMessage = httpClient.SendAsync(httpRequestMessage);
                restResponse = new RestResponse((int)httpResponseMessage.Result.StatusCode, httpResponseMessage.Result.Content.ReadAsStringAsync().Result);
            }
            catch (Exception err)
            {
                restResponse = new RestResponse(500, err.Message + "\n" + err.InnerException);
                Console.WriteLine(err.StackTrace);
                Console.WriteLine(err.InnerException);
            }
            finally
            {
                httpRequestMessage?.Dispose();
                httpClient?.Dispose();
            }

            return restResponse;
        }*/

        public async Task<RestResponse> PerformGetRequest(string requestUrl, Dictionary<string, string> httpHeader)
        {
            httpClient = AddHeadersAndCreateHttpClient(httpHeader);
            HttpResponseMessage httpResponseMessage = await httpClient.GetAsync(requestUrl,HttpCompletionOption.ResponseContentRead);
            int statusCode = (int)httpResponseMessage.StatusCode;
            var responseData = await httpResponseMessage.Content.ReadAsStringAsync();
            return new RestResponse(statusCode, responseData);
        }

        public async Task<RestResponse> PerformPostRequest(string requestUrl, HttpContent httpContent, Dictionary<string, string> httpHeader)
        {
            httpClient = AddHeadersAndCreateHttpClient(httpHeader);
            HttpResponseMessage httpResponseMessage =  await httpClient.PostAsync(requestUrl, httpContent, CancellationToken.None);
            int statusCode = (int)httpResponseMessage.StatusCode;
            var responseData = await httpResponseMessage.Content.ReadAsStringAsync();
            return new RestResponse(statusCode, responseData);
        }

        public async Task<RestResponse> PerformPostRequest(string requestUrl, string data, string mediaType, Dictionary<string, string> httpHeader)
        {
            httpClient = AddHeadersAndCreateHttpClient(httpHeader);
            HttpContent httpContent = new StringContent(data, Encoding.UTF8, mediaType);
            HttpResponseMessage httpResponseMessage = await httpClient.PostAsync(requestUrl, httpContent, CancellationToken.None);
            int statusCode = (int)httpResponseMessage.StatusCode;
            var responseData = await httpResponseMessage.Content.ReadAsStringAsync();
            return new RestResponse(statusCode, responseData);
        }

        public async Task<RestResponse> PerformPutRequest(string requestUrl, string content, string mediaType, Dictionary<string, string> httpHeader)
        {
            httpClient = AddHeadersAndCreateHttpClient(httpHeader);
            HttpContent httpContent = new StringContent(content, Encoding.UTF8, mediaType);
            HttpResponseMessage httpResponseMessage = await httpClient.PutAsync(requestUrl, httpContent, CancellationToken.None);
            int statusCode = (int)httpResponseMessage.StatusCode;
            var responseData = await httpResponseMessage.Content.ReadAsStringAsync();
            return new RestResponse(statusCode, responseData);
        }

        public async Task<RestResponse> PerformPutRequest(string requestUrl, HttpContent httpContent, Dictionary<string, string> httpHeader)
        {
           httpClient = AddHeadersAndCreateHttpClient(httpHeader);
            HttpResponseMessage httpResponseMessage = await httpClient.PutAsync(requestUrl, httpContent, CancellationToken.None);
            int statusCode = (int)httpResponseMessage.StatusCode;
            var responseData = await httpResponseMessage.Content.ReadAsStringAsync();
            return new RestResponse(statusCode, responseData);
        }

        public async Task<RestResponse> PerformDeleteRequest(string requestUrl)
        {
            httpClient = AddHeadersAndCreateHttpClient(null);
            HttpResponseMessage httpResponseMessage = await httpClient.DeleteAsync(requestUrl, CancellationToken.None);
            int statusCode = (int)httpResponseMessage.StatusCode;
            var responseData = await httpResponseMessage.Content.ReadAsStringAsync();
            return new RestResponse(statusCode, responseData);
        }

        //overloaded version 
        public async Task<RestResponse> PerformDeleteRequest(string requestUrl, Dictionary<string, string> httpHeader)
        {
            httpClient = AddHeadersAndCreateHttpClient(httpHeader);
            HttpResponseMessage httpResponseMessage = await httpClient.DeleteAsync(requestUrl, CancellationToken.None);
            int statusCode = (int)httpResponseMessage.StatusCode;
            var responseData = await httpResponseMessage.Content.ReadAsStringAsync();
            return new RestResponse(statusCode, responseData);
        }
    }
}
