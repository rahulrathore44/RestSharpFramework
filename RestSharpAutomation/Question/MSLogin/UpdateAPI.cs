using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpAutomation.Question.MSLogin
{
    [TestClass]
    public class UpdateAPI
    {
        [TestMethod]
        public void GetAccessToken()
        {
            Trace.WriteLine("whatever");
            Debug.WriteLine("Entering Main");
            RestClient client = new RestClient("https://login.microsoftonline.com");
            RestRequest request = new RestRequest("/85722e96-8e10-4794-9933-f05d211175ef/oauth2/token", Method.POST);

            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddHeader("Accept", "*/*");
            request.AddParameter("client_id", "9e6da7a9-93f9-4ca9-b724-0b4ce7e8242d");
            request.AddParameter("client_secret", "YHFw48kLl0R+rFeBAWYL3GVlqwfSyH/KQE+g0Hh7cHY=");
            request.AddParameter("grant_type", "client_credentials");
            request.AddParameter("resource", "https://nwsfdevuses0dmdhfhir.azurehealthcareapis.com");

            IRestResponse restResponse = client.Execute(request);
            string response = restResponse.Content;
            //int responseCode = (int)client.Execute(request).ResponseStatus;
            Debug.WriteLine(response);
            // Debug.WriteLine(responseCode);
            Trace.WriteLine(response);
            //System.Diagnostics.Trace.WriteLine(responseCode);
            Trace.WriteLine("END");

            var data = (JObject)JsonConvert.DeserializeObject(response);
            string accessTokenValue = data["access_token"].Value<string>();
            Debug.WriteLine(accessTokenValue);
            Trace.WriteLine("END22");

            RestClient clientget = new RestClient("https://nwsfdevuses0dmdhfhir.azurehealthcareapis.com");

            RestRequest requestget = new RestRequest("/CarePlan", Method.GET);
            requestget.AddHeader("Authorization", "Bearer " + accessTokenValue);
            requestget.AddHeader("Accept", "*/*");


            IRestResponse restGETResponse = clientget.Execute(requestget);
            int responseCode = (int)clientget.Execute(requestget).ResponseStatus;
            Debug.WriteLine(responseCode);

            string responseGET = restGETResponse.Content;
            Debug.WriteLine(responseGET);
            Debug.WriteLine("END77");
        }
    }
}
