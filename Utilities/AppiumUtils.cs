using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Service;
using OpenQA.Selenium.Support.UI;
using RSUdemyAppiumFramework.Utilities.Models;
using SeleniumExtras.WaitHelpers;

namespace RSUdemyAppiumFramework.Utilities
{
    public abstract class AppiumUtils
    {
        public static AppiumServiceBuilder AppiumServiceBuilder;
        public static AppiumLocalService AppiumLocalService;

        public double GetFormattedAmount(string amount)
        {
            double price = double.Parse(amount.Substring(1));
            return price;
        }

        public void WaitForElementToAppear(IWebElement element, AppiumDriver Driver)
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(5));
            wait.Until(ExpectedConditions.TextToBePresentInElement(element, "Cart"));
        }

        public AppiumLocalService StartAppiumServer(string ipAddress, int port)
        {
            // Build Appium Service
            AppiumServiceBuilder = new AppiumServiceBuilder();
            AppiumServiceBuilder.WithIPAddress(ipAddress);
            AppiumServiceBuilder.UsingPort(port);

            // Start Appium server
            AppiumLocalService = AppiumServiceBuilder.Build();
            AppiumLocalService.Start();

            return AppiumLocalService;
        }

        // TEST DATA
        public static IEnumerable<TestCaseData> GetJsonUserData()
        {
            var jsonContent = File.ReadAllText("Tests/Test Data/shoppingData.json");
            var usersList = JsonConvert.DeserializeObject<List<UserData>>(jsonContent);

            foreach (var data in usersList)
            {
                yield return new TestCaseData(data)
                    .SetName("End To End Test With Different Users: " + data.Name);
            }
        }

        public static IEnumerable<TestCaseData> GetUserData()
        {
            yield return new TestCaseData("Andy Tilston", "female", "Argentina")
                .SetName("Andy Tilston-Shopping Data");
            yield return new TestCaseData("Rahul Shetty", "male", "Argentina")
                .SetName("Rahul Shetty-Shopping Data");
        }
    }
}
