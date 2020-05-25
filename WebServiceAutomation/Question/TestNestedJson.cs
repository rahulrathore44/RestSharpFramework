using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WebServiceAutomation.Question
{
    [TestClass]
    public class TestNestedJson
    {
        [TestMethod]
        public void TestUsingStatement()
        {
            using (HttpClient httpClient = new HttpClient())
            {
                using (HttpRequestMessage httpRequestMessage = new HttpRequestMessage())
                {
                    httpRequestMessage.RequestUri = new Uri("http://dummy.restapiexample.com/api/v1/employees");
                    httpRequestMessage.Method = HttpMethod.Get;
                    httpRequestMessage.Headers.Add("Accept", "application/json");
                    Task<HttpResponseMessage> httpResponse = httpClient.SendAsync(httpRequestMessage);
                    using (HttpResponseMessage httpResponseMessage = httpResponse.Result)
                    {
                        HttpStatusCode status = httpResponseMessage.StatusCode;
                        using (HttpContent content = httpResponseMessage.Content)
                        {
                            using (Task<string> getResponse = content.ReadAsStringAsync())
                            {
                                string output = getResponse.Result;
                                Employee datum = JsonConvert.DeserializeObject<Employee>(output);
                                Console.WriteLine(datum.data[0].id);
                            }
                        }

                    }
                }
            }
        }
    }

    class Datum
    {
        public string id { get; set; }
        public string employee_name { get; set; }
        public string employee_salary { get; set; }
        public string employee_age { get; set; }
        public string profile_image { get; set; }
    }

    class Employee
    {
        public string status { get; set; }
        public IList<Datum> data { get; set; }
    }
}
