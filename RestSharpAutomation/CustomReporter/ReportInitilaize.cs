using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpAutomation.CustomReporter
{
    [TestClass]
    public class ReportInitilaize
    {
        [AssemblyCleanup]
        public static void CleanUp()
        {
            CustomeExtentRepoter.GetInstance().WriteToReport();
        }
    }
}
