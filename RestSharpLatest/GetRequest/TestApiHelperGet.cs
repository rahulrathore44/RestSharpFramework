using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharpLatest.APIHelper;
using RestSharpLatest.APIHelper.Client;
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

        public void SetUp()
        {
            _client = new DefaultClient();
        }

        
        public void TearDown()
        {
            _client?.Dispose();
        }

    }
}
