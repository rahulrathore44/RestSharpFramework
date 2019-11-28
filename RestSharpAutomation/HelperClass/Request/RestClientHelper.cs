using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpAutomation.HelperClass.Request
{
    public class RestClientHelper
    {
        private IRestClient GetRestClient()
        {
            IRestClient restClient = new RestClient();
            return restClient;
                 
        }

        private IRestRequest GetRestRequest(string url, Dictionary<string,string> headers, Method method,object body,DataFormat dataFormat)
        {
            IRestRequest restRequest = new RestRequest()
            {
                Method = method,
                Resource = url
            };

            if(headers != null)
            {
                foreach (string key in headers.Keys)
                {
                    restRequest.AddHeader(key, headers[key]);
                }
            }

            if(body != null)
            {
                restRequest.RequestFormat = dataFormat;

                switch (dataFormat)
                {
                    case DataFormat.Json:
                        restRequest.AddBody(body);
                        break;
                    case DataFormat.Xml:
                        restRequest.XmlSerializer = new RestSharp.Serializers.DotNetXmlSerializer();
                        restRequest.AddParameter("xmlData", body.GetType().Equals(typeof(string)) ? body :
                            restRequest.XmlSerializer.Serialize(body), ParameterType.RequestBody);
                        break;
                }

                
            }
           
           return restRequest;

        }

        private IRestResponse SendRequest(IRestRequest restRequest)
        {
            IRestClient restClient = GetRestClient();
            IRestResponse restResponse =  restClient.Execute(restRequest);
            return restResponse;
        }

        private IRestResponse<T> SendRequest<T>(IRestRequest restRequest) where T : new()
        {
            IRestClient restClient = GetRestClient();
            IRestResponse<T> restResponse = restClient.Execute<T>(restRequest);

            if (restResponse.ContentType.Equals("application/xml"))
            {
                var deserializer = new RestSharp.Deserializers.DotNetXmlDeserializer();
                restResponse.Data = deserializer.Deserialize<T>(restResponse);
            }

            return restResponse;
        }

        public IRestResponse PerformGetRequest(string url,Dictionary<string,string> headers)
        {
            IRestRequest restRequest = GetRestRequest(url, headers, Method.GET,null,DataFormat.None);
            IRestResponse restResponse = SendRequest(restRequest);
            return restResponse;
        }

        public IRestResponse<T> PerformGetRequest<T>(string url, Dictionary<string, string> headers) where T : new()
        {
            IRestRequest restRequest = GetRestRequest(url, headers, Method.GET,null,DataFormat.None);
            IRestResponse<T> restResponse = SendRequest<T>(restRequest);
            return restResponse;
        }

       public IRestResponse<T> PerformPostRequest<T>(string url,Dictionary<string,string> headers,object body,DataFormat dataFormat) where T : new()
        {
            IRestRequest restRequest = GetRestRequest(url, headers, Method.POST, body, dataFormat);
            IRestResponse<T> restResponse = SendRequest<T>(restRequest);
            return restResponse;
        }

        public IRestResponse PerformPostRequest(string url, Dictionary<string, string> headers, object body, DataFormat dataFormat) 
        {
            IRestRequest restRequest = GetRestRequest(url, headers, Method.POST, body, dataFormat);
            IRestResponse restResponse = SendRequest(restRequest);
            return restResponse;
        }

        public IRestResponse<T> PerformPutRequest<T>(string url, Dictionary<string, string> headers, object body, DataFormat dataFormat) where T : new()
        {
            IRestRequest restRequest = GetRestRequest(url, headers, Method.PUT, body, dataFormat);
            IRestResponse<T> restResponse = SendRequest<T>(restRequest);
            return restResponse;
        }

        public IRestResponse PerformPutRequest(string url, Dictionary<string, string> headers, object body, DataFormat dataFormat)
        {
            IRestRequest restRequest = GetRestRequest(url, headers, Method.PUT, body, dataFormat);
            IRestResponse restResponse = SendRequest(restRequest);
            return restResponse;
        }

        public IRestResponse PerformDeleteRequest(string url, Dictionary<string, string> headers)
        {
            IRestRequest restRequest = GetRestRequest(url, headers, Method.DELETE, null, DataFormat.None);
            IRestResponse restResponse = SendRequest(restRequest);
            return restResponse;
        }

    }
}
