using NHSD.BuyingCatalogue.EmailClient.IntegrationTesting.Drivers;
using OpenQA.Selenium;
using OrderFormAcceptanceTests.Actions;
using OrderFormAcceptanceTests.Actions.Collections;

namespace OrderFormAcceptanceTests.Steps.Utils
{
    public sealed class UITest
    {
        internal string OrdapiConnectionString;
        internal string BapiConnectionString;
        internal string IsapiConnectionString;
        internal IWebDriver Driver;
        internal EmailServerDriver EmailServerDriver;
        internal PageActionCollection Pages;
        internal readonly string Url;

        public UITest(Settings settings)
        {
            OrdapiConnectionString = settings.GetDbString("ordapi-db");
            BapiConnectionString = settings.GetDbString("bapi-db");
            IsapiConnectionString = settings.GetDbString("isapi-db");

            Driver = new BrowserFactory(settings.Browser, settings.HubUrl).Driver;
            Pages = new PageActions(Driver).PageActionCollection;
            Url = settings.PublicBrowseUrl;
            EmailServerDriver = InstantiateEmailServerDriver(Url);

            GoToUrl();
        }

        public void GoToUrl()
        {
            Driver.Navigate().GoToUrl(Url);
        }

        private EmailServerDriver InstantiateEmailServerDriver(string Url)
        {
            var emailUrl = EmailUtils.GetEmailUrl(Url);
            var emailDriverSettings = new EmailServerDriverSettings(emailUrl);
            return new EmailServerDriver(emailDriverSettings);
        }
    }
}
