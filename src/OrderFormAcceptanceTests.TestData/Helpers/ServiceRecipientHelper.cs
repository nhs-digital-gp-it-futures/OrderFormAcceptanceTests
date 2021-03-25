namespace OrderFormAcceptanceTests.TestData.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using OrderFormAcceptanceTests.Domain;
    using OrderFormAcceptanceTests.Persistence.Data;

    public static class ServiceRecipientHelper
    {
        public static async Task<IEnumerable<OrderItemRecipient>> Generate(string odsCode, string odsUrl, int numRecipients = 1)
        {
            List<OrderItemRecipient> recipients = new();
            Random rng = new();

            var allRecipients = await new OdsHelper(odsUrl).GetServiceRecipientsByParentOdsCode(odsCode);

            var recipientList = allRecipients.ToList();

            for (int i = 0; i < numRecipients; i++)
            {
                var recipient = new OrderItemRecipient()
                {
                    Recipient = recipientList[i],
                    DeliveryDate = DateTime.Today,
                    Quantity = rng.Next(1, 101),
                };

                recipients.Add(recipient);
            }

            return recipients;
        }

        public static async Task<IReadOnlyDictionary<string, ServiceRecipient>> AddOrUpdateRecipients(IEnumerable<ServiceRecipient> recipients, OrderingDbContext context)
        {
            var requestRecipients = recipients
                .Select(r => new ServiceRecipient(r.OdsCode, r.Name))
                .ToDictionary(r => r.OdsCode);

            var existingServiceRecipients = await context.ServiceRecipient
                .Where(s => requestRecipients.Keys.Contains(s.OdsCode))
                .ToListAsync();

            foreach (var recipient in existingServiceRecipients)
            {
                recipient.Name = requestRecipients[recipient.OdsCode].Name;
            }

            var newServiceRecipients = requestRecipients.Values.Except(existingServiceRecipients).ToList();

            context.ServiceRecipient.AddRange(newServiceRecipients);

            return existingServiceRecipients.Union(newServiceRecipients).ToDictionary(r => r.OdsCode);
        }
    }
}
