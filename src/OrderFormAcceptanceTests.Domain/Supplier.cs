namespace OrderFormAcceptanceTests.Domain
{
    public sealed class Supplier
    {
        public string Id { get; init; }

        public string Name { get; set; }

        public int AddressId { get; set; }

        public Address Address { get; set; }
    }
}
