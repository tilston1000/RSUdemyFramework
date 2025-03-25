using OpenQA.Selenium.Appium;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Android;

namespace RSUdemyAppiumFramework.Utilities
{
    public class AndroidActions : AppiumUtils
    {
        AndroidDriver Driver;

        public AndroidActions(AndroidDriver Driver) 
        {
            this.Driver = Driver;
        }

        public void LongPressAction(WebElement element)
        {
            ((IJavaScriptExecutor)Driver).ExecuteScript("mobile: longClickGesture",
                new Dictionary<string, object>
                {
                    { "elementId", element },
                    { "duration", 2000 }
                });
        }

        public void SwipeAction(WebElement element, string direction)
        {
            ((IJavaScriptExecutor)Driver).ExecuteScript("mobile: swipeGesture",
                new Dictionary<string, object>
                {
                    { "elementId", element },
                    { "direction", direction },
                    { "percent", 0.25 }
                });
        }

        public void DragAndDrop(WebElement element, int xCoordinates, int yCoordinates)
        {
            ((IJavaScriptExecutor)Driver).ExecuteScript("mobile: dragGesture",
                new Dictionary<string, object>
                {
                                { "elementId", element },
                                { "endX", xCoordinates },
                                { "endY", yCoordinates }
                });
        }

        public void ScrollIntoViewSimple(string scrollableItemName)
        {
            Driver.FindElement(MobileBy.AndroidUIAutomator($"new UiScrollable(new UiSelector()).scrollIntoView(text(\"{scrollableItemName}\"))"));
        }

        public void ScrollToEnd()
        {
            bool canScrollMore;
            do
            {
                canScrollMore = (bool)((IJavaScriptExecutor)Driver).ExecuteScript("mobile: scrollGesture",
                    new Dictionary<string, object>
                    {
                    { "left", 100 },
                    { "top", 100 },
                    { "width", 200 },
                    { "height", 200 },
                    { "direction", "down" },
                    { "percent", 1.0 }
                    });
            } while (canScrollMore);
        }
    }
}
