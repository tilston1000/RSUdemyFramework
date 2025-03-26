#pragma warning disable NUnit1032 // An IDisposable field/property should be Disposed in a TearDown method

using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Service;
using RSUdemyAppiumFramework.Page_Objects.Android;
using RSUdemyAppiumFramework.Utilities;
using RSUdemyAppiumFramework.Resources;
using Microsoft.Extensions.Configuration;
using RSUdemyAppiumFramework.Tests.TestUtils; 

namespace RSUdemyAppiumFramework.Tests.Base
{
    public class AndroidBaseTest : AppiumUtils
    {
        public AndroidDriver Driver;
        public static AppiumLocalService AppiumLocalService;
        public static AppiumOptions AppiumOptions;
        public FormPage formPage;
        public static GlobalVariablesData GlobalVariablesData;
        public static IConfigurationRoot GlobalVariablesFileSetup;

        [OneTimeSetUp]
        public void SetupAppiumServerAndDriver()
        {
            ExtentReporter.SetUpExtentReports();
            SetupGlobalVariablesFromJsonFile();

            var ipAddress = SetUpIPAddress();

            AppiumLocalService = StartAppiumServer(ipAddress, int.Parse(GlobalVariablesData.port));

            AppiumOptions = new AppiumOptions()
            {
                AutomationName = AutomationName.AndroidUIAutomator2,
                DeviceName = "Rahulemulator",
                PlatformName = "Android",
                //PlatformVersion = "13",
                //App = "C:\\source\\Training\\Udemy\\RSUdemyAppium\\RSUdemyAppium\\Resources\\General-Store.apk",
            };
            AppiumOptions.AddAdditionalAppiumOption("chromedriverExecutable", "C:\\source\\Training\\Udemy\\RSUdemyAppium\\RSUdemyAppium\\Resources\\chromedriver131.exe");

            Driver = new AndroidDriver(AppiumLocalService.ServiceUrl, AppiumOptions, TimeSpan.FromSeconds(45));
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        [SetUp]
        public void TestSetup()
        {
            ExtentReporter.StartExtentTest(TestContext.CurrentContext.Test.Name);
            formPage = new FormPage(Driver);
            formPage.SetActivity();
        }

        [TearDown]
        public void TestTearDown() 
        {
            ExtentReporter.LoggingTestStatusExtentReport(Driver);
        }

        [OneTimeTearDown]
        public void CloseAppiumServerAndDriver()
        {
            ExtentReporter.Extent.Flush();
            Driver.Dispose();
            AppiumLocalService.Dispose();
        }

        private GlobalVariablesData SetupGlobalVariablesFromJsonFile()
        {
            GlobalVariablesFileSetup = new ConfigurationBuilder()
                .AddJsonFile("appconfig.json").Build();

            GlobalVariablesData = new GlobalVariablesData();
            GlobalVariablesFileSetup.Bind(GlobalVariablesData);

            return GlobalVariablesData;
        }

        private string SetUpIPAddress()
        {
            // IP Address can be passed in as a parameter. If it's not passed in we use the default
            // in the data.json file
            var ipAddress = TestContext.Parameters["ipAddress"];
            if (ipAddress == null)
            {
                ipAddress = GlobalVariablesData.ipAddress;
            }

            return ipAddress;
        }
    }
}
