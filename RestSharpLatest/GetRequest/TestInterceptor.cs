using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RestSharpLatest.GetRequest
{
    class TestInterceptor : RestSharp.Interceptors.Interceptor
    {
        public override ValueTask BeforeRequest(RestRequest request, CancellationToken cancellationToken)
        {
            return base.BeforeRequest(request, cancellationToken);
        }
    }

}
