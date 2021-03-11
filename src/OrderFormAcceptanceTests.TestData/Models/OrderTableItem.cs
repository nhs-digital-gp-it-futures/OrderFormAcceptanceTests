namespace OrderFormAcceptanceTests.TestData.Models
{
    using System;

    public sealed class OrderTableItem
    {
        public string Id { get; set; }

        public string Description { get; set; }

        public string LastUpdatedBy { get; set; }

        public DateTime Created { get; set; }

        public DateTime? Completed { get; set; }

        public DateTime? LastUpdated { get; set; }

        public bool? AutomaticallyProcessed { get; set; }
    }
}
