using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpAutomation.CustomReporter
{
    // Sigleton design pattern
    public class CustomeExtentRepoter
    {
        private readonly ExtentHtmlReporter extentHtmlReporter;
        private readonly ExtentReports extentReports;
        private static CustomeExtentRepoter customeExtentRepoter;

        private CustomeExtentRepoter()
        {
            extentHtmlReporter = new ExtentHtmlReporter(@"C:\Data\log\restsharp\");
            extentReports = new ExtentReports();
            extentReports.AttachReporter(extentHtmlReporter);
        }

        public static CustomeExtentRepoter GetInstance()
        {
            if(customeExtentRepoter == null)
            {
                customeExtentRepoter = new CustomeExtentRepoter();
            }

            return customeExtentRepoter;
        }

        public void AddToReport(string name, string description, UnitTestOutcome status, string error)
        {
            // name,
            // Descritpion
            // Outcome - Pass/Faile
            // Error info - Incase of failure
            switch (status)
            {
                case UnitTestOutcome.Passed:
                    extentReports.CreateTest(name, description).Pass("");
                    break;
                case UnitTestOutcome.Failed:
                    extentReports.CreateTest(name, description).Fail(error);
                    break;
                default:
                    extentReports.CreateTest(name, description).Skip("");
                    break;
            }
        }

        public void WriteToReport()
        {
            extentReports?.Flush();
        }
    }
}
