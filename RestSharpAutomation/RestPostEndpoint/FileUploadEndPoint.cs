using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpAutomation.RestPostEndpoint
{
    [TestClass]
    public class FileUploadEndPoint
    {
        [TestMethod]
        public void Test_Upload_Of_File()
        {
            IRestClient client = new RestClient();
            IRestRequest request = new RestRequest()
            {
                Resource = "https://jobportalkarate.herokuapp.com/normal/webapi/upload"
            };

            request.AddFile("file", @"C:\Users\rathr1\Desktop\PipeLine-Command.txt", "multipart/form-data");
            var response = client.Post(request);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

        }
    }
}
