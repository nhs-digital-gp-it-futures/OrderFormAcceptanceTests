using OpenQA.Selenium;

namespace OrderFormAcceptanceTests.Actions.Utils
{
    internal static class DriverExtensions
    {
        internal static bool ElementVisible(this IWebDriver driver, By selector)
        {
            try
            {
                driver.FindElement(selector);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
