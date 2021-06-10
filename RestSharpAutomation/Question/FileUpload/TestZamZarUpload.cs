using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpAutomation.Question.FileUpload
{
    [TestClass]
    public class TestZamZarUpload
    {
        private string baseurl = "https://sandbox.zamzar.com/v1/jobs";

        [TestMethod]
        public void uploading()
        {

            IRestClient client = new RestClient();
            client.Authenticator = new HttpBasicAuthenticator("969bed863c84fea859fe48bc57ae13dbf1f28da1", " ");

            IRestRequest request = new RestRequest()
            {
                Resource = baseurl
            };

            request.AddHeader("Accept", "application/json");
            request.AddFile("source_file", @"C:\Data\log\2.gif", "multipart/form-data");
            request.AddParameter("target_format", "png", ParameterType.RequestBody);

            IRestResponse restResponse = client.Post(request);
            Assert.AreEqual(201, (int)restResponse.StatusCode);
        }
    }
}
