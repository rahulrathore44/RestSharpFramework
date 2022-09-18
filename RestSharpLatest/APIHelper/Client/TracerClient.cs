using HttpTracer;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpLatest.APIHelper.Client
{
    public class TracerClient : IClient
    {
        private readonly RestClientOptions _restClientOptions;
        private RestClient _restClient;

        public TracerClient()
        {
            _restClientOptions = new RestClientOptions();

        }

        public void Dispose()
        {
            _restClient?.Dispose();
        }

        public RestClient GetClient()
        {
            //_restClientOptions.ConfigureMessageHandler = TraceConfig;
            /*_restClientOptions.ConfigureMessageHandler = (handler) =>
            {
                var tracer = new HttpTracerHandler(handler, HttpMessageParts.All);
                return tracer;
            };*/

            _restClientOptions.ConfigureMessageHandler = (handler) =>
            {
                return new HttpTracerHandler(handler, HttpMessageParts.All);
            };

            _restClientOptions.ThrowOnDeserializationError = true;
            // To avoide creating one RestClient per request 
            if (_restClient == null)
                _restClient = new RestClient(_restClientOptions);

            return _restClient;
        }

        private HttpMessageHandler TraceConfig(HttpMessageHandler handler)
        {
            var tracer = new HttpTracerHandler(handler, HttpMessageParts.All);
            return tracer;
        }
    }
}
