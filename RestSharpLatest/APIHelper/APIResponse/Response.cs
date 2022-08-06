using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpLatest.APIHelper.APIResponse
{
    public class Response : AbstractResponse
    {
        private readonly RestResponse _restResponse;

        public Response(RestResponse restResponse) : base(restResponse)
        {
            _restResponse = restResponse;
        }

        public override string GetResponseData()
        {
            return _restResponse.Content;
        }
    }

    public class Response<T> : AbstractResponse<T>
    {
        private readonly RestResponse<T> _restResponse;

        public Response(RestResponse<T> restResponse) : base(restResponse)
        {
            _restResponse = restResponse;
        }

        public override T GetResponseData()
        {
            return _restResponse.Data;
        }
    }
}
