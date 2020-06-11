using OpenQA.Selenium;
using OrderFormAcceptanceTests.Actions;
using OrderFormAcceptanceTests.Actions.Collections;

namespace OrderFormAcceptanceTests.Steps.Utils
{
    public sealed class UITest
    {
        internal string ConnectionString;
        internal string BapiConnectionString;
        internal IWebDriver Driver;
        internal PageActionCollection Pages;
        internal readonly string Url;

        public UITest()
        {
            ConnectionString = EnvironmentVariables.DbConnectionString();
            BapiConnectionString = EnvironmentVariables.BapiDbConnectionString();

            Driver = new BrowserFactory().Driver;
            Pages = new PageActions(Driver).PageActionCollection;
            Url = EnvironmentVariables.Url();

            GoToUrl();
        }

        public void GoToUrl()
        {
            Driver.Navigate().GoToUrl(Url);
        }
    }
}
