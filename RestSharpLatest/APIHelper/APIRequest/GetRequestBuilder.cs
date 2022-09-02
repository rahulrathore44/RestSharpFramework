using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpLatest.APIHelper.APIRequest
{
    public class GetRequestBuilder : AbstractRequest
    {
        private readonly RestRequest _restRequest;

        public GetRequestBuilder()
        {
            _restRequest = new RestRequest()
            {
                Method = Method.Get
            };
        }

        public override RestRequest Build()
        {
            return _restRequest;
        }

        // URL

        public GetRequestBuilder WithUrl(string url)
        {
            WithUrl(url, _restRequest);
            return this;
        }

        // Req headers

        public GetRequestBuilder WithHeaders(Dictionary<string,string> headers)
        {
            WithHeaders(headers, _restRequest);
            return this;
        }

        //QueryParameter
        public GetRequestBuilder WithQueryParameters(Dictionary<string, string> parameters)
        {
            WithQueryParameters(parameters, _restRequest);
            return this;
        }

    }
}
