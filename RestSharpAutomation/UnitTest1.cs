using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;

namespace RestSharpAutomation
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {

            /**
             1. Create the Client
             2. Create the Request
             3. Send the request using the client
             4. Capture the respose
             */

            IRestClient restClient = new RestClient();
            IRestRequest restRequest = new RestRequest();
        }

        [TestMethod]
        public void TestYouTubeDataApi()
        {

            /**
             1. Create the Client
             2. Create the Request
             3. Send the request using the client
             4. Capture the respose
             */
            IRestClient client = new RestClient();
            IRestRequest request = new RestRequest();
            request.Resource = "https://accounts.google.com/o/oauth2/v2/auth";
            request.AddParameter("scope", "https://www.googleapis.com/auth/youtube", ParameterType.QueryString);
            request.AddParameter("access_type", "online",ParameterType.QueryString);
            request.AddParameter("include_granted_scopes","true", ParameterType.QueryString);
            request.AddParameter("state", "state_parameter_passthrough_value", ParameterType.QueryString);
            request.AddParameter("redirect_uri", "https://localhost.com", ParameterType.QueryString);
            request.AddParameter("response_type", "code", ParameterType.QueryString);
            request.AddParameter("client_id", "654769406751-6ekohb9nkv7lomiegoan80756pr8iq74.apps.googleusercontent.com", ParameterType.QueryString);
            IRestResponse response = client.Get(request);
            Console.WriteLine(response.Content);

        }

        [TestMethod]
        public void TestDropBox()
        {
            string body = "{\"path\": \"/Homework\",\"autorename\": true}";
            string uploadHeader = "{\"path\": \"/Homework/upload.mp4\",\"mode\": \"add\",\"autorename\": true,\"mute\": false,\"strict_conflict\": false}";
            string downloadHeader = "{\"path\": \"/Homework/Document.docx\"}";
            IRestClient client = new RestClient();
            IRestRequest request = new RestRequest()
            {
                Resource = "https://content.dropboxapi.com/2/files/download",
                Method = Method.POST
                
            };
            request.AddHeader("Authorization", "Bearer 6nPT8TAQ09kAAAAAAAAAk-q6hpQpQKBVK5qLAuXxUbAtPGJ4KJY1IYiaZ1-1_bVD");
            //request.AddHeader("Content-Type", "application/octet-stream");
            request.AddHeader("Dropbox-API-Arg", downloadHeader);
            //request.AddFile("upload.mp4", "C:\\Users\\rathr1\\Downloads\\upload.mp4", "video/mp4");
            //request.RequestFormat = DataFormat.Json;
            //request.AddBody(body);
            //IRestResponse response = client.Post(request);
            var response = client.DownloadData(request);
            //Console.WriteLine(response.Content);
            Console.WriteLine(response.Length);
            File.WriteAllBytes("down.docx", response);
        }
    }
}
