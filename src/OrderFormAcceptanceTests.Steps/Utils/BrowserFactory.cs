using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using System;
using System.Diagnostics;
using System.IO;

namespace OrderFormAcceptanceTests.Steps.Utils
{
    public sealed class BrowserFactory
    {
        public BrowserFactory(string browser, string hubUrl)
        {
            Driver = Browser(browser, hubUrl);
        }

        public IWebDriver Driver { get; }

        private IWebDriver Browser(string browser, string hubUrl)
        {
            Enum.TryParse(browser, out BrowserTypes browserType);

            if (Debugger.IsAttached)
                browserType = BrowserTypes.ChromeLocal;

            return browserType switch
            {
                BrowserTypes.Chrome => ChromeDriver(hubUrl),
                BrowserTypes.Firefox => FirefoxDriver(hubUrl),
                BrowserTypes.ChromeLocal => LocalChromeDriver(),
                _ => LocalChromeDriver()
            };
        }

        private static IWebDriver LocalChromeDriver()
        {
            var options = DefaultChromeOptions(false);

            return new ChromeDriver(Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory), options);
        }

        private static IWebDriver ChromeDriver(string hubUrl)
        {
            var options = DefaultChromeOptions(true);

            return RemoteDriver(new Uri(hubUrl), options);
        }

        private static IWebDriver FirefoxDriver(string hubUrl)
        {
            var options = new FirefoxOptions();
            options.AddArguments("headless", "window-size=1920,1080", "no-sandbox", "acceptInsecureCerts");

            return RemoteDriver(new Uri(hubUrl), options);
        }

        private static IWebDriver RemoteDriver(Uri uri, DriverOptions options)
        {
            IWebDriver driver = null;

            Policies.GetPolicy().Execute(() => { driver = new RemoteWebDriver(uri, options); });

            return driver;
        }

        private static ChromeOptions DefaultChromeOptions(bool headless)
        {
            var options = new ChromeOptions();
            options.AddArguments("no-sandbox", "disable-dev-shm-usage", "ignore-certificate-errors");
            if (headless)
                options.AddArguments("headless", "window-size=1920,1080");
            else
                options.AddArguments("start-maximized", "auto-open-devtools-for-tabs");

            return options;
        }
    }

    internal enum BrowserTypes
    {
        Chrome,
        Firefox,
        ChromeLocal
    }
}
