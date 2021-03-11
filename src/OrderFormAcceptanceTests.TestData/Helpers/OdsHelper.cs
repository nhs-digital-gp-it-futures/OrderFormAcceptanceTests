namespace OrderFormAcceptanceTests.TestData.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Text.Json;
    using System.Threading.Tasks;
    using System.Web;
    using OrderFormAcceptanceTests.Domain;

    public sealed class OdsHelper
    {
        private readonly Uri odsUrl;
        private readonly HttpClient client;

        public OdsHelper(string odsUrl)
        {
            this.odsUrl = new Uri(odsUrl);
            client = new HttpClient();
        }

        public async Task<IEnumerable<ServiceRecipient>> GetServiceRecipientsByParentOdsCode(string odsCode)
        {
            List<ServiceRecipient> serviceRecipients = new();

            var uriBuilder = new UriBuilder(odsUrl);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            query["RelTypeId"] = "RE4";
            query["TargetOrgId"] = odsCode;
            query["RelStatus"] = "active";
            query["Limit"] = "100";
            uriBuilder.Query = query.ToString();

            var results = await client.GetStringAsync(uriBuilder.Uri);

            if (results is not null)
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                };

                var recipients = JsonSerializer.Deserialize<ServiceRecipientResponse>(results, options);
                foreach (var recipient in recipients.Organisations)
                {
                    serviceRecipients.Add(new ServiceRecipient(name: recipient.Name, odsCode: recipient.OrgId));
                }
            }

            return serviceRecipients;
        }

        internal sealed class ServiceRecipientResponse
        {
            public IEnumerable<ServiceRecipientResponseItem> Organisations { get; set; }
        }

        internal sealed class ServiceRecipientResponseItem
        {
            public string Name { get; set; }

            public string OrgId { get; set; }

            public string Status { get; set; }

            public string PrimaryRoleId { get; set; }
        }
    }
}
