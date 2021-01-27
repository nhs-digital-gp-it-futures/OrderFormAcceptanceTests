namespace OrderFormAcceptanceTests.Actions.Utils
{
    using System;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.UI;

    public static class ElementExtensions
    {
        public static bool ContainsElement(this IWebElement element, By message)
        {
            try
            {
                element.FindElement(message);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        ///     Method to set the value of an input field
        ///     Useful for entering a large number of characters instead of SendKeys which types each character individually
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="wait"></param>
        /// <param name="elementBy"></param>
        /// <param name="value">default is empty string</param>
        /// <param name="index">default is 0</param>
        public static void EnterTextViaJs(
            this IWebDriver driver,
            WebDriverWait wait,
            By elementBy,
            string value = "",
            int index = 0)
        {
            wait.Until(s => s.FindElements(elementBy)[index].Enabled);
            ((IJavaScriptExecutor)driver).ExecuteScript(
                "arguments[0].value=arguments[1];",
                driver.FindElements(elementBy)[index],
                value);
        }

        public static void ForJsToComplete(this WebDriverWait wait)
        {
            wait.Until(driver => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete"));
        }

        internal static Func<IWebDriver, bool> InvisibilityOfElement(By locator)
        {
            return driver =>
            {
                try
                {
                    var element = driver.FindElement(locator);
                    return !element.Displayed;
                }
                catch (NoSuchElementException)
                {
                    // Returns true because the element is not present in DOM.
                    // The try block checks if the element is present but is invisible.
                    return true;
                }
                catch (StaleElementReferenceException)
                {
                    // Returns true because stale element reference implies that element is no longer visible.
                    return true;
                }
            };
        }

        internal static Func<IWebDriver, IWebElement> ElementToBeClickable(By locator)
        {
            return driver =>
            {
                var element = driver.FindElement(locator).Displayed ? driver.FindElement(locator) : null;
                try
                {
                    if (element != null && element.Enabled)
                    {
                        return element;
                    }

                    return null;
                }
                catch (StaleElementReferenceException)
                {
                    return null;
                }
            };
        }
    }
}
