using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using RestSharpAutomation.CustomSerializer;
using RestSharpAutomation.Question.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebServiceAutomation.Model.XmlModel;


namespace RestSharpAutomation.Question.Post
{
    [TestClass]
    public class PostRequest
    {
        private string postUrl = "http://localhost:8080/laptop-bag/webapi/api/add";
        private Random random = new Random();

        [TestMethod]
        public void TestPostWithModelObject()
        {
            IRestClient restClient = new RestClient();
            IRestRequest request = new RestRequest()
            {
                Resource = postUrl
            };

            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/xml");
            request.RequestFormat = DataFormat.Json;
            request.JsonSerializer = new CustomJsonSerializer();
            request.AddBody(GetProfile());

            IRestResponse response = restClient.Post(request);
            Assert.AreEqual(200, (int)response.StatusCode);
            Console.WriteLine(response.Content);
        }

        private MarginProfile GetProfile()
        {
            MileMarkups range1 = new MileMarkups(1, 100);
            MileMarkups range2 = new MileMarkups(101, 200);
            MileMarkups range3 = new MileMarkups(251, 250);
            List<MileMarkups> ranges = new List<MileMarkups>()
            {
               range1,
               range2,
               range3
            };


            MarginProfile Profile = new MarginProfile()
            {
                customerId = 683746,
                trailerGroup = "Van",
                fuelSurcharge = 3,
                overrideDefaultFuelSurcharge = true,
                overrideDefaultLDIMarkup = true,
                truckPayAdjustment = 1,
                overrideDefaultTruckPayAdjustment = true,
                overrideDefaultMileMarkups = true,
                mileMarkups = ranges
            };
            return Profile;
        }

    }
}
