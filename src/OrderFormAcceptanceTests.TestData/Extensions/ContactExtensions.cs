namespace OrderFormAcceptanceTests.TestData.Extensions
{
    using OrderFormAcceptanceTests.Domain;
    using OrderFormAcceptanceTests.TestData.Models;

    public static class ContactExtensions
    {
        public static ContactModel ToModel(this Contact contact)
        {
            if (contact is null)
            {
                return null;
            }

            return new ContactModel
            {
                FirstName = contact.FirstName,
                LastName = contact.LastName,
                EmailAddress = contact.Email,
                TelephoneNumber = contact.Phone,
            };
        }

        public static Contact ToDomain(this ContactModel model)
        {
            return model is null
                ? new Contact()
                : new Contact
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.EmailAddress,
                    Phone = model.TelephoneNumber,
                };
        }
    }
}
