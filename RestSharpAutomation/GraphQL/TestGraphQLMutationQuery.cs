using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpAutomation.GraphQL
{
    [TestClass]
    public class TestGraphQLMutationQuery
    {
        private IRestClient client;
        private IRestRequest request;
        private string PostUrl = "http://localhost:4000/";

        [TestMethod]
        public void TestMutationQuery()
        {
            var QueryOne = @"query {
                                allBooks {
                                    id,
                                    author,
                                    title,
                                    isbn,
                                    url
                                }
                            }";

            client = new RestClient();
            request = new RestRequest()
            {
                Method = Method.POST,
                Resource = PostUrl
            };

            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(new
            {
                query = QueryOne,
                variables = new { _id = 17 }
            });
            var response = client.Post(request);
            Assert.AreEqual(200, (int)response.StatusCode);
            Console.WriteLine(response.Content);
        }
    }
}
