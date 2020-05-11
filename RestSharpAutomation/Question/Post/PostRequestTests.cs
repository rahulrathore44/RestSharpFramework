using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpAutomation.Question.Post
{
    [TestClass]
    public class PostRequestTests
    {
        Random generateId = new Random();

        private string postUrl = "http://localhost:8080/laptop-bag/webapi/api/add";

        private RestResponse restResponse;

        private string jsonMediaType = "application/json";

        [TestMethod]

        public void PostRequestTest()

        {

            List<string> ola = new List<string>();

            ola.Add("8th Generation Intel® Core™ i5 - 8300H");

            ola.Add("Windows 10 Home 64-bit English");

            ola.Add("NVIDIA® GeForce® GTX 1660 Ti 6GB GDDR6");

            ola.Add("8GB, 2x4GB, DDR4, 2666MHz");


            HttpClient client = new HttpClient();

            //Setting data into class property to get posted.

            LaptopDetails details = new LaptopDetails()

            {

                BrandName = "Hp Dell Laptop",

                Features = new LapTopFeatures()

                {

                    Feature = ola

                },

                LaptopName = "Pentium 5",

                Id = generateId.Next(1000),

            };


            Console.WriteLine($ "My data to post is - {details.ToString()}");

            HttpContent body = new StringContent(details.ToString(), Encoding.UTF8, jsonMediaType); // Creating the request to fire.



            //Fire request....

            Task<HttpResponseMessage> postResponse = client.PostAsync(postUrl, body);

            HttpStatusCode statusCode = postResponse.Result.StatusCode; // Receive bad request.

            HttpContent postResponseContent = postResponse.Result.Content;

            string postResponseData = postResponseContent.ReadAsStringAsync().Result;



            restResponse = new RestResponse();

            restResponse.statusCode = (int)statusCode;

            restResponse.responseData = postResponseData;

            Console.WriteLine($"This is my status code and response data - { restResponse.ToString()}");

        }
    }

    
}
