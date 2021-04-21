using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpAutomation.Question.LargeJson
{
    [TestClass]
    public class TestPostWithLargeJson
    {
        /* Compex JSON pay load */

        [TestMethod]
        public void Test_Post_With_Large_Json()
        {
            IRestClient restClient = new RestClient();
            IRestRequest request = new RestRequest()
            {
                Resource = ""
            };

            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/xml");
            request.RequestFormat = DataFormat.Json;
            var body = GetFileData(@"C:\abc\pqr.json");
            request.AddBody(body);

            IRestResponse response = restClient.Post(request);
            Assert.AreEqual(200, (int)response.StatusCode);
            Console.WriteLine(response.Content);
        }

        private string GetFileData(string fileLocation)
        {
            string data = "";
            FileInfo info = new FileInfo(fileLocation);
            using(FileStream stream = info.Open(FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using(StreamReader reader = new StreamReader(stream))
                {
                    data = reader.ReadToEnd();
                }
            }
            return data;
        }


    }
}
