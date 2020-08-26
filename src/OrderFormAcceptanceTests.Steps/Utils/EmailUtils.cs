using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NHSD.BuyingCatalogue.EmailClient.IntegrationTesting.Drivers;
using TechTalk.SpecFlow;

namespace OrderFormAcceptanceTests.Steps
{
    [Binding]
    class EmailUtils
    {

        private readonly EmailServerDriver _emailServerDriver;

        public EmailUtils(EmailServerDriver emailServerDriver)
        {
            _emailServerDriver = emailServerDriver ?? throw new ArgumentNullException(nameof(emailServerDriver));
        }

        private static string DowngradeHttps(string value)
        {
            return value.Replace("https", "http");
        }

        private static bool IsRunningLocal(string hostUrl)
        {
            return hostUrl.Contains("host", StringComparison.OrdinalIgnoreCase);
        }

        public static Uri GetEmailUrl(string hostUrl)
        {
            if (IsRunningLocal(hostUrl))
            {
                return new Uri(DowngradeHttps($"{hostUrl}:1080/email"));
            }
            return new Uri($"{hostUrl}/email");
        }
    }
}
