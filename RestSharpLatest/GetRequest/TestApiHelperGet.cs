using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharpLatest.APIHelper;
using RestSharpLatest.APIHelper.APIRequest;
using RestSharpLatest.APIHelper.Client;
using RestSharpLatest.APIHelper.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpLatest.GetRequest
{
    [TestClass]
    public class TestApiHelperGet
    {
        private IClient _client;
        private readonly string getUrl = "http://localhost:8081/laptop-bag/webapi/api/all";

        public void SetUp()
        {
            _client = new DefaultClient();
        }

        
        [TestMethod]
        public void GetRequestWithApiHelper()
        {
            var headers = new Dictionary<string, string>()
            {
                { "Accept", "application/json"}
            };
            AbstractRequest abstractRequest = new GetRequestBuilder().WithUrl(getUrl).WithHeaders(headers);
            ICommand getCommand = new RequestCommand(abstractRequest, _client);
        }

        public void TearDown()
        {
            _client?.Dispose();
        }

    }
}
