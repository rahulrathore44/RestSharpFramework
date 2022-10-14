using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp.Authenticators;
using RestSharpLatest.APIHelper;
using RestSharpLatest.APIHelper.APIRequest;
using RestSharpLatest.APIHelper.Client;
using RestSharpLatest.APIHelper.Command;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpLatest.Assignment.File_Download_using_Framework
{
    [TestClass]
    public class Assignment_POST_File_Download_Parallel
    {
        private static RestApiExecutor restApiExecutor;
        private static IClient client;
        private static IClient authClient;
        private static readonly string Token = "<Your Token>";
        private readonly string BasePath = "https://content.dropboxapi.com/2";

        //ClassInitialize - a method that contains code that must be used before any of the tests in the test class have run
        [ClassInitialize]
        public static void Setup(TestContext testContext)
        {
            // Create the Tracer Client
            client = new TracerClient();
            // Invoke the decorator with the tracer client impl to associate the token information.
            authClient = new AuthenticationDecorato(client, new JwtAuthenticator(Token));
            // Create the Executor
            restApiExecutor = new RestApiExecutor();

        }

        //ClassCleanup - a method that contains code to be used after all the tests in the test class have run
        [ClassCleanup]
        public static void TearDown()
        {
            // Release the resource acquired by the client.
            authClient?.Dispose();
        }

        // Assignment - Parallel Download of Multiple Files using Framework API
        [TestMethod]
        public void Download_File_Parallel_UsingFramework()
        {
            var contextPath = "/files/download";
            
            // Meta data for the first file
            var fileNameOne = "Video.mp4";
            var locationOne = "{\"path\":\"/" + fileNameOne + "\"}";

            // Meta data for the second file
            var fileNameTwo = "Part1.mp3";
            var locationTwo = "{\"path\":\"/" + fileNameTwo + "\"}";

            // Create the Post Request for first file
            var postRequestforVideoFile = new PostRequestBuilder().WithUrl(BasePath + contextPath).WithHeaders(new Dictionary<string, string>() { { "Dropbox-API-Arg", locationOne } });

            // Create the Post Request for second file
            var postRequestforAudioFile = new PostRequestBuilder().WithUrl(BasePath + contextPath).WithHeaders(new Dictionary<string, string>() { { "Dropbox-API-Arg", locationTwo } });

            // Create the DownloadRequestCommand for the first post request.
            var command = new DownloadRequestCommand(postRequestforVideoFile, authClient);

            // Set the command for the RestApiExecutor.
            restApiExecutor.SetCommand(command);

            // Call DownloadDataAsync method to download first file.
            var dataOne = restApiExecutor.DownloadDataAsync();

            // Create the DownloadRequestCommand for the second post request.
            command = new DownloadRequestCommand(postRequestforAudioFile, authClient);

            // Set the command for the RestApiExecutor.
            restApiExecutor.SetCommand(command);

            // Call DownloadDataAsync method to download second file.
            var dataTwo = restApiExecutor.DownloadDataAsync();

            // Wait for download task
            Task.WaitAll(dataOne, dataTwo);

            // Save the first file on the local file system
            File.WriteAllBytes("New_" + fileNameOne, dataOne.Result);

            // Verify that the files are downloaded successfully.
            File.Exists("New_" + fileNameOne).Should().BeTrue();

            // Save the second file on the local file system
            File.WriteAllBytes("New_" + fileNameTwo, dataTwo.Result);

            // Verify that the files are downloaded successfully.
            File.Exists("New_" + fileNameTwo).Should().BeTrue();
        }
    }
}
