using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpAutomation.Question.PostFormData
{
    [TestClass]
    public class TestFormData
    {

        [TestMethod]
        public void Test_Post_of_Form_Data()
        {
            IRestClient client = new RestClient();
            IRestRequest request = new RestRequest()
            {
                Resource = "https://api.eventbaja.com/api/v2/registration"

            };
           
            request.AddParameter("role", "vendor", ParameterType.GetOrPost);
            request.AddParameter("user", "test@mailinator", ParameterType.GetOrPost);
            request.AddParameter("address", "89", ParameterType.GetOrPost);
            request.AddParameter("password", "qwertyui", ParameterType.GetOrPost);
            request.AddParameter("password_confirmation", "qwertyui", ParameterType.GetOrPost);
            request.AddParameter("otp", "526524", ParameterType.GetOrPost);
            request.AddParameter("confirm otp", "526524", ParameterType.GetOrPost);
            

            var response =  client.Post(request);
            Console.WriteLine(response.StatusCode);
        }
    }
}
