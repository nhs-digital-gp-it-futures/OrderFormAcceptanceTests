namespace OrderFormAcceptanceTests.TestData.Models
{
    public sealed class SupplierModel
    {
        public string Id { get; init; }

        public string Name { get; set; }

        public AddressModel Address { get; set; }
    }
}
