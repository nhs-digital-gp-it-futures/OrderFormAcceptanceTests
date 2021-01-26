namespace OrderFormAcceptanceTests.Steps
{
    using System;
    using TechTalk.SpecFlow;

    [Binding]
    internal static class EmailUtils
    {
        public static Uri GetEmailUrl(string hostUrl)
        {
            Uri uri;
            if (IsRunningLocal(hostUrl))
            {
                uri = new Uri(DowngradeHttps($"{hostUrl}:1080/email"));
            }
            else
            {
                uri = new Uri($"{hostUrl}/email");
            }

            return uri;
        }

        private static string DowngradeHttps(string value)
        {
            return value.Replace("https", "http");
        }

        private static bool IsRunningLocal(string hostUrl)
        {
            return hostUrl.Contains("host", StringComparison.OrdinalIgnoreCase);
        }
    }
}
