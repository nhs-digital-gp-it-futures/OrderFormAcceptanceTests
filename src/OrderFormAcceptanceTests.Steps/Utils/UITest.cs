using NHSD.BuyingCatalogue.EmailClient.IntegrationTesting.Drivers;
using OpenQA.Selenium;
using OrderFormAcceptanceTests.Actions;
using OrderFormAcceptanceTests.Actions.Collections;

namespace OrderFormAcceptanceTests.Steps.Utils
{
    public sealed class UITest
    {
        internal string ConnectionString;
        internal string BapiConnectionString;
        internal string IsapiConnectionString;
        internal IWebDriver Driver;
        internal EmailServerDriver EmailServerDriver;
        internal PageActionCollection Pages;
        internal readonly string Url;

        public UITest()
        {
            ConnectionString = EnvironmentVariables.DbConnectionString();
            BapiConnectionString = EnvironmentVariables.BapiDbConnectionString();
            IsapiConnectionString = EnvironmentVariables.IsapiDbConnectionString();

            Driver = new BrowserFactory().Driver;
            Pages = new PageActions(Driver).PageActionCollection;
            Url = EnvironmentVariables.Url();

            EmailServerDriver = InstatiateEmailServerDriver(Url);

            GoToUrl();
        }

        public void GoToUrl()
        {
            Driver.Navigate().GoToUrl(Url);
        }

        private EmailServerDriver InstatiateEmailServerDriver(string Url)
        {
            var emailUrl = EmailUtils.GetEmailUrl(Url);
            var emailDriverSettings = new EmailServerDriverSettings(emailUrl);
            return new EmailServerDriver(emailDriverSettings);
        }
    }
}
