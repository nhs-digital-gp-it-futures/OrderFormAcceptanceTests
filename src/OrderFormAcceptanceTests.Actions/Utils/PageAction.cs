using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OrderFormAcceptanceTests.Objects;
using OrderFormAcceptanceTests.Objects.Collections;
using System;


namespace OrderFormAcceptanceTests.Actions.Utils
{
    public abstract class PageAction
    {
        internal readonly IWebDriver Driver;
        internal readonly WebDriverWait Wait;
        internal PageCollection Pages;

        protected PageAction(IWebDriver driver)
        {
            this.Driver = driver;

            // Initialize a WebDriverWait that can be reutilized by all that inherit from this class
            // Polls every 0.1 seconds for 10 seconds maximum
            Wait = new WebDriverWait(new SystemClock(), this.Driver, TimeSpan.FromSeconds(10),
                TimeSpan.FromMilliseconds(100));

            // Initialize the page objects
            Pages = new PageObjects().Pages;
        }
    }
}
