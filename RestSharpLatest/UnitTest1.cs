using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;

namespace RestSharpLatest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            /**
             * 1. Create the client
             * 2. Create the request
             * 3. Send the request
             * 4. Capture the response
             * 5. Add the verification on the response.
             * **/

            RestClient client = new RestClient();
            RestRequest request = new RestRequest();

        }
    }
}
