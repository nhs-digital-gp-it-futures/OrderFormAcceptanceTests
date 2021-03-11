namespace OrderFormAcceptanceTests.TestData.Helpers
{
    using Bogus;
    using OrderFormAcceptanceTests.Domain;

    public static class ContactHelper
    {
        public static Contact Generate()
        {
            var contact = new Faker<Contact>("en_GB")
                .RuleFor(c => c.FirstName, f => f.Name.FirstName())
                .RuleFor(c => c.LastName, f => f.Name.LastName())
                .RuleFor(c => c.Email, (f, c) => f.Internet.Email(c.FirstName, c.LastName, "autotest.com"))
                .RuleFor(c => c.Phone, f => f.Phone.PhoneNumber())
                .Generate();

            return contact;
        }
    }
}
