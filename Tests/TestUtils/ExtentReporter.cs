using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;

namespace RSUdemyAppiumFramework.Tests.TestUtils
{
    public class ExtentReporter
    {
        public static ExtentReports Extent;
        public static ExtentTest TestLog;
        public static string? failedTestsScreenshotsPath;

        public static ExtentReports SetUpExtentReports()
        {
            string projectPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            string reportPath = Path.Combine(projectPath, "Reports");
            Directory.CreateDirectory(reportPath);
            failedTestsScreenshotsPath = reportPath + "\\Failed Tests Screenshots\\";

            ExtentSparkReporter reporter = new ExtentSparkReporter(reportPath + $"\\extentReport_{DateTime.Now:dd_MM_yyyy HH_mm_ss}.html");
            Extent = new ExtentReports();
            Extent.AttachReporter(reporter);
            reporter.Config.ReportName = "Web Automation Results";
            reporter.Config.DocumentTitle = "Test Results";

            Extent.AddSystemInfo("Environment", "QA");
            Extent.AddSystemInfo("Tester", "Andy Tilston");
            Extent.AddSystemInfo("Machine Name", Environment.MachineName);

            return Extent;
        }

        public static void StartExtentTest(string testsToStart)
        {
            TestLog = Extent.CreateTest(testsToStart);
        }

        public static void LoggingTestStatusExtentReport(AppiumDriver Driver)
        {
            try
            {
                var status = TestContext.CurrentContext.Result.Outcome.Status;
                var stackTrace = string.Empty + TestContext.CurrentContext.Result.StackTrace + string.Empty;
                var errorMessage = TestContext.CurrentContext.Result.Message;
                switch (status)
                {
                    case TestStatus.Failed:
                        TestLog.Log(Status.Fail, "Test ended with " + Status.Fail + " - " + errorMessage);
                        AddScreenshot(Driver);
                        break;
                    case TestStatus.Skipped:
                        TestLog.Log(Status.Skip, "Test ended with " + Status.Skip);
                        break;
                    default:
                        TestLog.Log(Status.Pass, "Test steps finished: " + TestContext.CurrentContext.Test.Name);
                        TestLog.Log(Status.Pass, "Test ended with " + Status.Pass);
                        break;
                }
            }
            catch (WebDriverException ex)
            {
                throw ex;
            }

        }

        private static void AddScreenshot(AppiumDriver Driver)
        {
            var screenshotFailed = ((ITakesScreenshot)Driver).GetScreenshot();
            screenshotFailed.SaveAsFile(failedTestsScreenshotsPath + $"{TestContext.CurrentContext.Test.Name}_{DateTime.Now:dd_MM_yyyy HH_mm_ss}.jpg");
            TestLog.AddScreenCaptureFromPath(failedTestsScreenshotsPath + $"{TestContext.CurrentContext.Test.Name}_{DateTime.Now:dd_MM_yyyy HH_mm_ss}.jpg");
        }
    }
}
