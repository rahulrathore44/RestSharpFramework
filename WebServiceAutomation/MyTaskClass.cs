using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebServiceAutomation.Helper.Request;
using WebServiceAutomation.Helper.Response;
using WebServiceAutomation.Model;
using WebServiceAutomation.Model.XmlModel;

namespace WebServiceAutomation
{
    //[TestClass]
    public class MyTaskClass
    {
        private string postUrl = "http://localhost:8080/laptop-bag/webapi/api/add";
        private string xmlMediaType = "application/xml";
        private Dictionary<string, string> headers = new Dictionary<string, string>()
            {
                { "Accept","application/xml"}
            };

        [TestMethod]
        public void TestTask()
        {
            Task<RestResponse> t1 = Task.Factory.StartNew<RestResponse>(() =>
            {
                return HttpClientHelper.PerformGetRequest("https://laptopbag.herokuapp.com/laptop-bag/webapi/delay/all", headers);
            });

            var task = Task.Factory.StartNew<Laptop>(() =>
            {
                Random random = new Random();
                int id = random.Next(1000);
                Console.WriteLine("Start " + id);
                return AddRecord(id);
            }, TaskCreationOptions.LongRunning).ContinueWith((restResponse) =>
            {
                string id = restResponse.Result.Id;
                RestResponse rs = HttpClientHelper.PerformGetRequest("https://laptopbag.herokuapp.com/laptop-bag/webapi/delay/find/" + id, headers);
                Assert.AreEqual(200, rs.StatusCode);
                Console.WriteLine("Done");
            }, TaskContinuationOptions.OnlyOnRanToCompletion);

            task.Wait(50000);

            Console.WriteLine(t1.Result.StatusCode);

        }

        public Laptop AddRecord(int id)
        {
            string xmlData = "<Laptop>" +
                                     "<BrandName>Alienware</BrandName>" +
                                     "<Features>" +
                                        "<Feature>8th Generation Intel® Core™ i5 - 8300H</Feature>" +
                                        "<Feature>Windows 10 Home 64 - bit English</Feature>" +
                                        "<Feature>NVIDIA® GeForce® GTX 1660 Ti 6GB GDDR6</Feature>" +
                                        "<Feature>8GB, 2x4GB, DDR4, 2666MHz</Feature>" +
                                      "</Features>" +
                                   "<Id> " + id + "</Id>" +
                                   "<LaptopName>Alienware M17</LaptopName>" +
                                "</Laptop>";
            RestResponse restResponse = HttpClientHelper.PerformPostRequest(postUrl, xmlData, xmlMediaType, headers);
            Assert.AreEqual(200, restResponse.StatusCode);
            Laptop xmlDatat = ResponseDataHelper.DeserializeXmlResponse<Laptop>(restResponse.ResponseContent);
            return xmlDatat;
        }
    }
}
