using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebServiceAutomation.Model;

namespace WebServiceAutomation.Helper.Request
{
    public class HttpClientHelper
    {
        private static HttpClient httpClient;
        private static HttpRequestMessage httpRequestMessage;
        private static RestResponse restResponse;

        private static HttpClient AddHeadersAndCreateHttpClient(Dictionary<string,string> httpHeader)
        {
            HttpClient httpClient = new HttpClient();

            if(null != httpHeader)
            {
                foreach (string key in httpHeader.Keys)
                {
                    httpClient.DefaultRequestHeaders.Add(key, httpHeader[key]);
                    
                }
            }
            return httpClient;

        }

        private static HttpRequestMessage CreateHttpRequestMessage(string requestUrl,HttpMethod httpMethod,HttpContent httpContent)
        {
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage(httpMethod, requestUrl);
            if(!((httpMethod == HttpMethod.Get) || (httpMethod == HttpMethod.Delete)))
                httpRequestMessage.Content = httpContent;
            return httpRequestMessage;
        }

        private static RestResponse SendRequest(string requestUrl, HttpMethod httpMethod, HttpContent httpContent, Dictionary<string, string> httpHeader)
        {
            httpClient = AddHeadersAndCreateHttpClient(httpHeader);
            httpRequestMessage = CreateHttpRequestMessage(requestUrl, httpMethod, httpContent);

            try
            {
                Task<HttpResponseMessage> httpResponseMessage = httpClient.SendAsync(httpRequestMessage);
                restResponse = new RestResponse((int)httpResponseMessage.Result.StatusCode, httpResponseMessage.Result.Content.ReadAsStringAsync().Result);
            }catch(Exception err)
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
        }

        public static RestResponse PerformGetRequest(string requestUrl, Dictionary<string, string> httpHeader)
        {
            return SendRequest(requestUrl, HttpMethod.Get, null, httpHeader);
        }

       public static RestResponse PerformPostRequest(string requestUrl,HttpContent httpContent,Dictionary<string,string> httpHeaders)
        {
            return SendRequest(requestUrl, HttpMethod.Post, httpContent, httpHeaders);
        }

        public static RestResponse PerformPostRequest(string requestUrl, string data, string mediaType,Dictionary<string, string> httpHeaders)
        {
            HttpContent httpContent = new StringContent(data, Encoding.UTF8, mediaType);
            return PerformPostRequest(requestUrl, httpContent, httpHeaders);
        }

       public static RestResponse PerformPutRequest(string requestUrl,string content,string mediaType,Dictionary<string,string> headers)
        {
            HttpContent httpContent = new StringContent(content, Encoding.UTF8, mediaType);
            return SendRequest(requestUrl, HttpMethod.Put, httpContent, headers);
        }

        public static RestResponse PerformPutRequest(string requestUrl, HttpContent httpContent, Dictionary<string, string> headers)
        {
            return SendRequest(requestUrl, HttpMethod.Put, httpContent, headers);
        }

       public static RestResponse PerformDeleteRequest(string requestUrl)
        {
            return SendRequest(requestUrl, HttpMethod.Delete, null, null);
        }

        //overloaded version 
        public static RestResponse PerformDeleteRequest(string requestUrl, Dictionary<string, string> headers)
        {
            return SendRequest(requestUrl, HttpMethod.Delete, null, headers);
        }

    }
}
