namespace OrderFormAcceptanceTests.TestData.Services
{
    using OrderFormAcceptanceTests.Domain;
    using OrderFormAcceptanceTests.Persistence.Data;
    using OrderFormAcceptanceTests.TestData.Extensions;
    using OrderFormAcceptanceTests.TestData.Models;

    public sealed class ContactDetailsService
    {
        private readonly OrderingDbContext context;

        public ContactDetailsService(OrderingDbContext context)
        {
            this.context = context;
        }

        public Address AddOrUpdateAddress(Address existingAddress, AddressModel newOrUpdatedAddress)
        {
            if (existingAddress is null)
            {
                return newOrUpdatedAddress.ToDomain();
            }

            context.Entry(existingAddress).CurrentValues.SetValues(newOrUpdatedAddress);
            return existingAddress;
        }

        public Contact AddOrUpdatePrimaryContact(Contact existingContact, ContactModel newOrUpdatedContact)
        {
            if (existingContact is null)
            {
                return newOrUpdatedContact.ToDomain();
            }

            context.Entry(existingContact).CurrentValues.SetValues(newOrUpdatedContact);
            return existingContact;
        }
    }
}
