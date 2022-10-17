using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharpAutomation.CustomReporter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RestSharpAutomation.ReportAttribute
{
    /***
     1. Create a class which inherit from TestMethodAttribute
     2. Overirde the Execute method to provide the implementation
     3. Use the custom attribute with testmethod
     */
    [AttributeUsage(AttributeTargets.Method,AllowMultiple = false)]
    public class TestMethodWithReport : TestMethodAttribute
    {
        private readonly object syslock = new object();
        public override TestResult[] Execute(ITestMethod testMethod)
        {
            var name = testMethod.TestClassName + "." + testMethod.TestMethodName;
            var result = base.Execute(testMethod);
            var execution = result.FirstOrDefault();
            var status = execution.Outcome;
            var errormsg = execution?.TestFailureException?.Message;
            var trace = execution?.TestFailureException?.StackTrace;

            lock (syslock)
            {
                CustomeExtentRepoter.GetInstance().AddToReport(name, "", status, errormsg + "\n" + trace);
            }

            return result;
        }
    }
}
