using System;

namespace OrderFormAcceptanceTests.Domain
{
    public sealed class OrderItemRecipient
    {
        public DateTime? DeliveryDate { get; set; }

        public int Quantity { get; set; }

        public ServiceRecipient Recipient { get; set; }

        public decimal CalculateTotalCostPerYear(decimal price, TimeUnit? timePeriod)
        {
            return price * Quantity * (timePeriod?.AmountInYear() ?? 1);
        }
    }
}
