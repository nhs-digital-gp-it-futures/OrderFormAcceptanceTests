namespace OrderFormAcceptanceTests.TestData.Models
{
    using System;

    public sealed class CreateOrderModel
    {
        public string Description { get; init; }

        public Guid OrganisationId { get; set; }
    }
}
