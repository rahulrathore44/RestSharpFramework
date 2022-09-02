using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpLatest.APIHelper.APIRequest
{
    public class DeleteRequestBuilder : AbstractRequest
    {
        private readonly RestRequest _restRequest;

        public DeleteRequestBuilder()
        {
            _restRequest = new RestRequest()
            {
                Method = Method.Delete
            };
        }

        public override RestRequest Build()
        {
            return _restRequest;
        }

        // URL

        public DeleteRequestBuilder WithUrl(string url)
        {
            WithUrl(url, _restRequest);
            return this;
        }

        // Req headers

        public DeleteRequestBuilder WithDefaultHeaders()
        {
            WithHeaders(null, _restRequest);
            return this;
        }

        protected override void WithHeaders(Dictionary<string, string> header, RestRequest restRequest)
        {
            restRequest.AddOrUpdateHeader("Accept", "text/plain");
        }

        //QueryParameter
        public DeleteRequestBuilder WithQueryParameters(Dictionary<string, string> parameters)
        {
            WithQueryParameters(parameters, _restRequest);
            return this;
        }
    }
}
