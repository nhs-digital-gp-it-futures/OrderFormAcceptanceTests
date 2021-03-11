namespace OrderFormAcceptanceTests.TestData.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using OrderFormAcceptanceTests.Domain;

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
    }
}
