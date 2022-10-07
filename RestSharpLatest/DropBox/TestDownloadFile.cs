using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using RestSharp.Authenticators;
using System.IO;

namespace RestSharpLatest.DropBox
{
    [TestClass]
    public class TestDownloadFile
    {
        private readonly string Token = "<Your Token>";

        private readonly string BasePath = "https://content.dropboxapi.com/2";

        [TestMethod]
        public void DownloadFile()
        {
            var fileName = "Video.mp4";
            var contextPath = "/files/download";
            var location = "{\"path\":\"/" + fileName + "\"}";

            var client = new RestClient()
            {
                Authenticator = new JwtAuthenticator(Token)
            };

            var request = new RestRequest()
            {
                Resource = BasePath + contextPath,
                Method = Method.Post
            };

            request.AddHeader("Dropbox-API-Arg", location);
            var data = client.DownloadData(request);
            File.WriteAllBytes(fileName, data);
            client.Dispose();
        }
    }
}
