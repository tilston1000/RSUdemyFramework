using OpenQA.Selenium.Appium;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Android;
using RSUdemyAppiumFramework.Utilities;

namespace RSUdemyAppiumFramework.Page_Objects.Android
{
    public class CartPage : AndroidActions
    {
        AndroidDriver Driver;

        public CartPage(AndroidDriver Driver) : base(Driver)
        {
            this.Driver = Driver;
        }

        private IList<AppiumElement> productPrices => Driver.FindElements(By.Id("com.androidsample.generalstore:id/productPrice"));
        private IWebElement DisplaySum => Driver.FindElement(By.Id("com.androidsample.generalstore:id/totalAmountLbl"));
        private WebElement TermsAndConditionsLink => Driver.FindElement(By.Id("com.androidsample.generalstore:id/termsButton"));
        private IWebElement AcceptButton => Driver.FindElement(By.Id("android:id/button1"));
        private IWebElement ProductName => Driver.FindElement(By.Id("com.androidsample.generalstore:id/productName"));
        private IWebElement Checkbox => Driver.FindElement(MobileBy.ClassName("android.widget.CheckBox"));
        private IWebElement ProceedButton => Driver.FindElement(By.Id("com.androidsample.generalstore:id/btnProceed"));

        public double GetProductSum()
        {
            int productCount = productPrices.Count();
            double totalSum = 0;
            for (int i = 0; i < productCount; i++)
            {
                string amountString = productPrices[i].Text;
                double price = GetFormattedAmount(amountString);
                totalSum = totalSum + price;
            }
            return totalSum;
        }

        public double GetTotalAmountDisplayed()
        {
            string displaySum = DisplaySum.Text;

            return GetFormattedAmount(displaySum);
        }

        public void AcceptTermsAndConditions()
        {
            LongPressAction(TermsAndConditionsLink);
            AcceptButton.Click();
        }

        public void SubmitOrder()
        {
            Checkbox.Click();
            ProceedButton.Click();
        }

        // ASSERTIONS
        public void AssertProductNameIsCorrect(string productName)
        {
            Assert.That(ProductName.Text, Is.EqualTo(productName));
        }

    }
}
