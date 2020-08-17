using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace OrderFormAcceptanceTests.Actions.Utils
{
    public sealed class EmailServerDriver
    {
        public static async Task<int> GetEmailCountAsync(string hostUrl, string emailToCheck = null)
        {
            var emailList = await FindAllEmailsAsync(hostUrl);
            if (emailToCheck != null)
            {
                emailList = emailList.Where(e => e.To.Equals(emailToCheck, StringComparison.OrdinalIgnoreCase));
            }

            return emailList.Count();
        }

        public static async Task<IEnumerable<Email>> FindAllEmailsAsync(string hostUrl, string emailToCheck = null)
        {
            using var client = NewHttpClient();
            var response = await client.GetAsync(GetAllEmailsUrl(hostUrl));
            var responseContent = JToken.Parse(await response.Content.ReadAsStringAsync());

            var emailList = responseContent.Select(x => new Email
            {
                Id = x.SelectToken("id").ToString().Trim(),
                PlainTextBody = x.SelectToken("text").ToString().Trim(),
                HtmlBody = x.SelectToken("html").ToString().Trim(),
                Subject = x.SelectToken("subject").ToString(),
                From = x.SelectToken("from").First().SelectToken("address").ToString(),
                To = x.SelectToken("to").First().SelectToken("address").ToString(),
            });

            if (emailToCheck != null)
            {
                emailList = emailList.Where(e => e.To.Equals(emailToCheck, StringComparison.OrdinalIgnoreCase));
            }
            return emailList;
        }

        public static async Task ClearAllEmailsAsync(string hostUrl)
        {
            using var client = NewHttpClient();
            await client.DeleteAsync(DeleteAllEmailsUrl(hostUrl));
        }
        public static async Task ClearEmailAsync(string hostUrl, string id)
        {
            using var client = NewHttpClient();
            await client.DeleteAsync(DeleteEmailUrl(hostUrl, id));
        }

        private static HttpClient NewHttpClient()
        {
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
            };
            return new HttpClient(handler);
        }

        private static string DowngradeHttps(string value)
        {
            return value.Replace("https", "http");
        }

        private static bool IsRunningLocal(string hostUrl)
        {
            return hostUrl.Contains("host", StringComparison.OrdinalIgnoreCase);
        }

        private static Uri GetAllEmailsUrl(string hostUrl)
        {
            if (IsRunningLocal(hostUrl))
            {
                return new Uri(DowngradeHttps($"{hostUrl}:1080/email/email"));
            }
            return new Uri($"{hostUrl}/email/email");
        }

        private static Uri DeleteAllEmailsUrl(string hostUrl)
        {
            if (IsRunningLocal(hostUrl))
            {
                return new Uri(DowngradeHttps($"{hostUrl}:1080/email/email/all"));
            }
            return new Uri($"{hostUrl}/email/email/all");
        }
        private static Uri DeleteEmailUrl(string hostUrl, string id)
        {
            if (IsRunningLocal(hostUrl))
            {
                return new Uri(DowngradeHttps($"{hostUrl}:1080/email/email/{id}"));
            }
            return new Uri($"{hostUrl}/email/email/{id}");
        }
    }
}
