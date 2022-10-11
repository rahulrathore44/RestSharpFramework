using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.IO;
using System.Threading.Tasks;

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

        [TestMethod]
        public void DownloadData_Parallel()
        {
            var contextPath = "/files/download";

            var fileNameOne = "Video.mp4";
            var locationOne = "{\"path\":\"/" + fileNameOne + "\"}";

            var fileNameTwo = "Part1.mp3";
            var locationTwo = "{\"path\":\"/" + fileNameTwo + "\"}";

            var client = new RestClient()
            {
                Authenticator = new JwtAuthenticator(Token)
            };

            var audioDownload = new RestRequest()
            {
                Resource = BasePath + contextPath,
                Method = Method.Post
            };

            audioDownload.AddHeader("Dropbox-API-Arg", locationTwo);

            var videoDownload = new RestRequest()
            {
                Resource = BasePath + contextPath,
                Method = Method.Post
            };
            videoDownload.AddHeader("Dropbox-API-Arg", locationOne);

            var task1 = client.DownloadDataAsync(videoDownload);
            var task2 = client.DownloadDataAsync(audioDownload);

            Task.WaitAll(task1, task2);

            File.WriteAllBytes(fileNameOne, task1.Result);
            File.WriteAllBytes(fileNameTwo, task2.Result);

            File.Exists(fileNameOne).Should().BeTrue();
            File.Exists(fileNameTwo).Should().BeTrue();

        }
    }
}
