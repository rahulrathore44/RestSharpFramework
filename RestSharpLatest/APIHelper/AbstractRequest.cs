using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpLatest.APIHelper
{
    public abstract class AbstractRequest
    {
        public abstract RestRequest Build();

        // For URL

        protected virtual void WithUrl(string url, RestRequest restRequest)
        {
            restRequest.Resource = url;
        }

        protected virtual void WithHeaders(Dictionary<string, string> header, RestRequest restRequest)
        {
            foreach (string key in header.Keys)
            {
                restRequest.AddOrUpdateHeader(key, header[key]);
            }
        }

        //
        //QueryParameter

        protected virtual void WithQueryParameters(Dictionary<string, string> parameters, RestRequest restRequest)
        {
            foreach (string key in parameters.Keys)
            {
                restRequest.AddParameter(key, parameters[key]);
            }
        }
        // URL Segments
    }
}
