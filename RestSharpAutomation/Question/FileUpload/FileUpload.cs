using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using RestSharp.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Text;
using System.Threading.Tasks;

namespace RestSharpAutomation.Question.FileUpload
{
    [TestClass]
    public class FileUpload
    {
        [TestMethod]
        public void Test_File_Upload_With_File_From_Device()
        {
            IRestClient client = new RestClient();
            IRestRequest request = new RestRequest()
            {
                Resource = "http://localhost:9191/normal/webapi/upload",
                Method = Method.POST
            };
            request.AddHeader("Accept", "application/json");
            request.AddFile("file", @"C:\Users\rathr1\Downloads\rathr1.pem", "multipart/form-data");

            var response = client.Post(request);
            Console.WriteLine(response.StatusCode);
            Console.WriteLine(response.Content);

        }
    }
}
