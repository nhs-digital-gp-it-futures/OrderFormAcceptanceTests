namespace OrderFormAcceptanceTests.TestData.Helpers
{
    using System.Threading.Tasks;
    using Bogus;
    using OrderFormAcceptanceTests.Domain;
    using OrderFormAcceptanceTests.Persistence.Data;

    public static class AddressHelper
    {
        public static Address Generate()
        {
            var address = new Faker<Address>("en_GB")
                .RuleFor(a => a.Line1, f => f.Address.StreetAddress())
                .RuleFor(a => a.Town, f => f.Address.City())
                .RuleFor(a => a.Country, f => f.Address.County())
                .RuleFor(a => a.Postcode, f => f.Address.ZipCode())
                .RuleFor(a => a.Country, f => f.Address.Country())
                .Generate();

            return address;
        }

        public static async Task<Address> Create(Address address, OrderingDbContext context)
        {
            context.Add(address);
            await context.SaveChangesAsync();
            return address;
        }
    }
}
