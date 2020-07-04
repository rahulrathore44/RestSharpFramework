using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpAutomation.ReportAttribute
{
    [TestClass]
    public class TestCustomAttribue
    {
        [TestMethodWithReport]
        public void FirstMethod()
        {

        }

        [TestMethodWithReport]
        public void SecondMethod()
        {
            Assert.Fail("I failed the test method");
        }

    }
}
