using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using RestSharp.Authenticators;
using RestSharpLatest.DropBox.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpLatest.DropBox
{
    [TestClass]
    public class ListFilesAndFolder
    {
        public readonly string BaseUrl = "https://api.dropboxapi.com/2";
        public readonly string Token = "<your token>";

        [TestMethod]
        public void GetAllFilesAndFolder()
        {
            var contextPath = "/files/list_folder";

            var requestBody = "{\"include_deleted\":false,\"include_has_explicit_shared_members\":false,\"include_media_info\":false,\"include_mounted_folders\":true,\"include_non_downloadable_files\":true,\"path\":\"\",\"recursive\":false}";

            var client = new RestClient()
            {
                Authenticator = new JwtAuthenticator(Token)
            };

            var request = new RestRequest()
            {
                Resource = BaseUrl + contextPath,
                Method = Method.Post
            };

            request.AddStringBody(requestBody, DataFormat.Json);
            var response = client.ExecutePost<Root>(request);
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }
    }
}
