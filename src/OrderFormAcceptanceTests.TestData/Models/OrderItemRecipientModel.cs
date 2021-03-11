namespace OrderFormAcceptanceTests.TestData.Models
{
    using System;

    public class OrderItemRecipientModel
    {
        public DateTime? DeliveryDate { get; set; }

        public string Name { get; set; }

        public string OdsCode { get; set; }

        public int? Quantity { get; set; }
    }
}
