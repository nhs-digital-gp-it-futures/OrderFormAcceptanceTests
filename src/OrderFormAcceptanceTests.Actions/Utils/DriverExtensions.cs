namespace OrderFormAcceptanceTests.Actions.Utils
{
    using OpenQA.Selenium;

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
