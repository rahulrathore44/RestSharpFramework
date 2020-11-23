using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpAutomation.Question.Attribute
{
    [AttributeUsage(AttributeTargets.Method,AllowMultiple = false)]
    public class TestMethodWithRetry : TestMethodAttribute
    {
        public override TestResult[] Execute(ITestMethod testMethod)
        {
            var result =  base.Execute(testMethod);
            /**
             * Check if there is a failed test then rerun it
             */
            if(result.Any(test => test.Outcome == UnitTestOutcome.Failed))
            {
                result = base.Execute(testMethod);
            }

            return result;
        }
    }
}
