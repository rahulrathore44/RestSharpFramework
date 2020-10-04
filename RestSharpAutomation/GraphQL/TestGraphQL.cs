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
    public class TestGraphQL
    {
        private IRestClient client;
        private IRestRequest request;
        private string PostUrl = "http://localhost:4000/";

        [TestMethod]
        public void TestGraphQLQuery()
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

            var QueryTwo = @"query {
                                allBooks {
                                    id,
                                    author
                                }
                            }";

            var QueryThree = @"query{
                                allBooks(filter:{
                                    id:17
                                }) {
                                    isbn
                                }
                            }";

            var QueryFour = @"query findById($_id: ID){
                                allBooks(filter:{
                                    id:$_id
                                }) {
                                    isbn
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
                query = QueryFour,
                variables = new { _id = 17}
            });
            var response = client.Post(request);
            Assert.AreEqual(200, (int)response.StatusCode);
            Console.WriteLine(response.Content);
        }
    }
}
