using OpenQA.Selenium;
using RSUdemyAppiumFramework.Page_Objects.Android;
using RSUdemyAppiumFramework.Tests.Base;
using RSUdemyAppiumFramework.Utilities.Models;

namespace RSUdemyAppium.Tests
{
    [TestFixture]
    public class EcomFrameworkTests : AndroidBaseTest
    {
        [Test, Category ("Regression")]
        public void ValidateToastMessageDisplayedIfNameNotEnteredTest()
        {
            // Log into site
            formPage.SetGender("female");
            formPage.SetCountrySelection("Argentina");
            formPage.SubmitForm();
            string toastMessage = Driver.FindElement(By.XPath("(//android.widget.Toast)[1]")).GetAttribute("name");
            Assert.That(toastMessage, Is.EqualTo("Please enter your name"));
        }

        [Test, TestCaseSource(nameof(GetUserData)), Category("Smoke")]
        public void ScrollAndAddProductToCart(string name, string gender, string country)
        {

            // Log into site
            formPage.SetNameField(name);
            formPage.SetGender(gender);
            formPage.SetCountrySelection(country);
            ProductCatalogue productCatalogue = formPage.SubmitForm();

            // Scroll into view and find all elements in the view. If a product of Jordan 6 Rings exists, we'll click on it
            productCatalogue.AddItemToCartByProductName("Jordan 6 Rings");
            CartPage cartPage = productCatalogue.GoToCartPage();
            cartPage.AssertProductNameIsCorrect("Jordan 6 Rings");
        }

        [Test, TestCaseSource(nameof(GetJsonUserData))]
        public void EndToEndPurchaseTest(UserData userData)
        {
            // Log into site
            formPage.SetNameField(userData.Name);
            formPage.SetGender(userData.Gender);
            formPage.SetCountrySelection(userData.Country);
            ProductCatalogue productCatalogue = formPage.SubmitForm();

            // Add products to cart
            productCatalogue.AddItemToCartByIndex(0);
            productCatalogue.AddItemToCartByIndex(0);
            CartPage cartPage = productCatalogue.GoToCartPage();

            // Validate pricing
            double totalSum = cartPage.GetProductSum();
            double displayFormattedSum = cartPage.GetTotalAmountDisplayed();
            Assert.That(totalSum, Is.EqualTo(displayFormattedSum));

            // Long press on T's and C's and checkout
            cartPage.AcceptTermsAndConditions();
            cartPage.SubmitOrder();
        }
    }
}
