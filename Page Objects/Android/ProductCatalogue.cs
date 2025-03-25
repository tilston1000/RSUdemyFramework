using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using RSUdemyAppiumFramework.Utilities;

namespace RSUdemyAppiumFramework.Page_Objects.Android
{
    public class ProductCatalogue : AndroidActions
    {
        AndroidDriver Driver;

        public ProductCatalogue(AndroidDriver Driver) : base(Driver)
        {
            this.Driver = Driver;
        }

        private IList<AppiumElement> ProductName => Driver.FindElements(By.Id("com.androidsample.generalstore:id/productName"));
        private IList<AppiumElement> ProductAddCartButton => Driver.FindElements(By.XPath("//android.widget.TextView[@text='ADD TO CART']"));
        private IWebElement CartIcon => Driver.FindElement(By.Id("com.androidsample.generalstore:id/appbar_btn_cart"));
        private IWebElement CartTitle => Driver.FindElement(By.Id("com.androidsample.generalstore:id/toolbar_title"));

        public void AddItemToCartByProductName(string product)
        {
            ScrollIntoViewSimple(product);
            int productCount = ProductName.Count();
            for (int i = 0; i < productCount; i++)
            {
                string productName = ProductName[i].Text;

                if (productName.Equals(product, StringComparison.OrdinalIgnoreCase))
                {
                    ProductAddCartButton[i].Click();
                }
            }
        }

        public void AddItemToCartByIndex(int index)
        {
            ProductAddCartButton[index].Click();
        }

        public CartPage GoToCartPage()
        {
            CartIcon.Click();
            WaitForElementToAppear(CartTitle, Driver);
            return new CartPage(Driver);
        }

    }
}
