using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Android;
using RSUdemyAppiumFramework.Utilities;

namespace RSUdemyAppiumFramework.Page_Objects.Android
{
    public class FormPage : AndroidActions
    {
        AndroidDriver Driver;

        public FormPage(AndroidDriver Driver) : base(Driver)
        {
            this.Driver = Driver;
        }

        private IWebElement NameField => Driver.FindElement(By.Id("com.androidsample.generalstore:id/nameField"));
        private IWebElement FemaleOption => Driver.FindElement(By.XPath("//android.widget.RadioButton[@text='Female']"));
        private IWebElement MaleOption => Driver.FindElement(By.XPath("//android.widget.RadioButton[@text='Male']"));
        private IWebElement CountrySelectionDropDown => Driver.FindElement(By.Id("android:id/text1"));
        private IWebElement ShopButton => Driver.FindElement(By.Id("com.androidsample.generalstore:id/btnLetsShop"));

        public void SetActivity()
        {
            Driver.StartActivity("com.androidsample.generalstore", "com.androidsample.generalstore.SplashActivity");
        }

        public void SetNameField(string name)
        {
            NameField.SendKeys(name);
        }

        public void SetGender(string gender)
        {
            if(gender.Contains("Female"))
                FemaleOption.Click();
            else
                MaleOption.Click();
        }

        public void SetCountrySelection(string country)
        {
            CountrySelectionDropDown.Click();
            ScrollIntoViewSimple(country);
            Driver.FindElement(By.XPath($"//android.widget.TextView[@text='{country}']")).Click();
        }

        public ProductCatalogue SubmitForm()
        {
            ShopButton.Click();
            return new ProductCatalogue(Driver);
        }
    }
}
