namespace OrderFormAcceptanceTests.Steps.Utils
{
    using NHSD.BuyingCatalogue.EmailClient.IntegrationTesting.Drivers;
    using OpenQA.Selenium;
    using OrderFormAcceptanceTests.Actions;
    using OrderFormAcceptanceTests.Actions.Collections;

    public sealed class UITest
    {
        public UITest(Settings settings)
        {
            OrdapiConnectionString = settings.GetDbString("ordapidb");
            BapiConnectionString = settings.GetDbString("bapidb");
            IsapiConnectionString = settings.GetDbString("isapidb");

            Driver = new BrowserFactory(settings.Browser, settings.HubUrl).Driver;
            Pages = new PageActions(Driver).PageActionCollection;
            Url = settings.OrderFormUrl;
            PbUrl = settings.PbUrl;
            EmailServerDriver = InstantiateEmailServerDriver();
        }

        internal string OrdapiConnectionString { get; }

        internal string BapiConnectionString { get; }

        internal string IsapiConnectionString { get; }

        internal IWebDriver Driver { get; }

        internal EmailServerDriver EmailServerDriver { get; }

        internal PageActionCollection Pages { get; }

        internal string Url { get; }

        internal string PbUrl { get; }

        public void GoToUrl()
        {
            Driver.Navigate().GoToUrl(Url);
        }

        private EmailServerDriver InstantiateEmailServerDriver()
        {
            var emailUrl = EmailUtils.GetEmailUrl(PbUrl);
            var emailDriverSettings = new EmailServerDriverSettings(emailUrl);
            return new EmailServerDriver(emailDriverSettings);
        }
    }
}
