using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MsTestProject
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod, TestCategory("Smoke")]
        public void TestMethod1()  // 5th , 6th,7
        {
            Console.WriteLine(" Test Method One");
        }

        [TestMethod]
        [Ignore]
        public void TestMethod2() // 2nd,  3rd, 4
        {
            Console.WriteLine(" Test Method Two ");
        }

        [TestInitialize] 
        public void Setup() // 1st , 4th  , 2 , 5th, 3,6
        {
            Console.WriteLine(" This is Setup ");
        }

        [TestCleanup]
        public void TearDown() // 3rd, 6th, 4th, 7th,5,8
        {
            Console.WriteLine(" This is Clean up");
        }

        [ClassInitialize]
        public static void ClassSetup(TestContext testContext) //1 ,2 
        {
            Console.WriteLine(" Class Set up ");
        }

        [ClassCleanup]
        public static void ClassTearDown() //8th,9
        {
            Console.WriteLine(" Class Tear Down ");
        }

        [AssemblyInitialize]
        public static void AssemblySetup(TestContext testContext) // 1
        {
            Console.WriteLine(" Assembly Setup ");
        }

        [AssemblyCleanup]
        public static void AssemblyTearDown() //10
        {
            Console.WriteLine(" Assembly Tear Down ");
        }

        
    }
}
