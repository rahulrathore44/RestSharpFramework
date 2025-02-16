using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsTestProject
{
    [TestClass]
    public class TestContextClass
    {
        private TestContext testContextInstance;

        private delegate void TestImplDelegate();
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        [TestInitialize]
        public void BeforeMethod()
        {
            Console.WriteLine("Before Test");
        }

        [TestMethod]
        public void TestMethodOne()
        {
            throw new Exception("Exception");
        }

        [TestCleanup]
        public void AfterMethod()
        {
            /*
            In NUNIT We have below code to achieve my goal..Do we have anything in MSTest to achieve the same?
            if (TestContext.CurrentContext.Result.Outcome.Status == NUnit.Framework.Interfaces.TestStatus.Failed)
                Console.WriteLine(TestContext.CurrentContext.Result.Message);
            */

            if (testContextInstance.CurrentTestOutcome == UnitTestOutcome.Failed)
            {
            }

        }
    }
}
