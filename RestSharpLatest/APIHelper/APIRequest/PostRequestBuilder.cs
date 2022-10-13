using RestSharp;
using RestSharp.Serializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpLatest.APIHelper.APIRequest
{
    public class PostRequestBuilder : AbstractRequest
    {
        private readonly RestRequest _restRequest;

        public PostRequestBuilder()
        {
            _restRequest = new RestRequest()
            {
                Method = Method.Post
            };
        }

        public override RestRequest Build()
        {
            return _restRequest;
        }

        //URL

        public PostRequestBuilder WithUrl(string url)
        {
            WithUrl(url, _restRequest);
            return this;
        }

        // Headers
        public PostRequestBuilder WithHeaders(Dictionary<string, string> headers)
        {
            WithHeaders(headers, _restRequest);
            return this;
        }

        // Body

        public PostRequestBuilder WithBody<T>(T body, RequestBodyType bodyType, string contentType = ContentType.Json) where T : class
        {
            // String
            // Object 

            switch (bodyType)
            {
                case RequestBodyType.STRING:
                    _restRequest.AddStringBody(body.ToString(), contentType);
                    break;
                case RequestBodyType.JSON:
                    _restRequest.AddJsonBody<T>(body);
                    break;
                case RequestBodyType.XML:
                    _restRequest.AddXmlBody<T>(body);
                    break;
            }
            return this;
        }

        //QueryParameter
        public PostRequestBuilder WithQueryParameters(Dictionary<string, string> parameters)
        {
            WithQueryParameters(parameters, _restRequest);
            return this;
        }

        //File Upload
        public PostRequestBuilder WithFileUpload(string paramName, byte[] content, string fileName)
        {
            _restRequest.AddFile(paramName, content, fileName, "multipart/form-data");
            return this;
        }

        protected override void WithQueryParameters(Dictionary<string, string> parameters, RestRequest restRequest)
        {
            foreach (string key in parameters.Keys)
            {
                restRequest.AddQueryParameter(key, parameters[key]);
            }
        }

    }

    public enum RequestBodyType
    {
        STRING, // For the String body
        JSON, // Serialize the object in to JSON
        XML // Serialize the object in to XML
    }
}
