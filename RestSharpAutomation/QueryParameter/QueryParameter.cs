using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebServiceAutomation.Model.XmlModel;

namespace RestSharpAutomation.QueryParameter
{
    [TestClass]
    public class QueryParameter
    {
        private string searchUrl = "http://localhost:8080/laptop-bag/webapi/api/query";

        [TestMethod]
        public void TestQueryParameter()
        {
            IRestClient client = new RestClient();
            IRestRequest request = new RestRequest()
            {
                Resource = searchUrl
            };

            request.AddHeader("Accept", "application/xml");
            // 1st apporach 
            //request.AddParameter("id", "1", ParameterType.QueryString);
            request.AddQueryParameter("id", "1");
            request.AddQueryParameter("laptopName", "Alienware M17");

            var response = client.Get<Laptop>(request);

            Assert.AreEqual(System.Net.HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual("Alienware", response.Data.BrandName);

        }
    }
}
