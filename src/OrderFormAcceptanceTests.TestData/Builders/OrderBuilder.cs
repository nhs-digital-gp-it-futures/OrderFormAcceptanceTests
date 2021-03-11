namespace OrderFormAcceptanceTests.TestData.Builders
{
    using System;
    using OrderFormAcceptanceTests.Domain;
    using OrderFormAcceptanceTests.Domain.Users;
    using OrderFormAcceptanceTests.TestData.Extensions;

    public sealed class OrderBuilder
    {
        private readonly Order order;

        public OrderBuilder(string description, User user, OrderingParty orderingParty)
        {
            order = new Order
            {
                Description = description,
                OrderingParty = orderingParty,
            };

            order.SetLastUpdatedBy(user.Id, $"{user.FirstName} {user.LastName}");
        }

        public OrderBuilder(Order existingOrder)
        {
            order = existingOrder;
        }

        public OrderBuilder WithCommencementDate(DateTime date)
        {
            order.CommencementDate = date;
            return this;
        }

        public OrderBuilder WithOrderingPartyContact(Contact contact)
        {
            order.OrderingPartyContact = contact;
            return this;
        }

        public OrderBuilder WithExistingSupplier(Supplier supplier)
        {
            order.Supplier = supplier;
            return this;
        }

        public OrderBuilder WithNewSupplier(SupplierDetails supplierDetails)
        {
            order.Supplier = new Supplier
            {
                Id = supplierDetails.SupplierId,
                Address = supplierDetails.AddressFromJson.ToDomain(),
                Name = supplierDetails.Name,
            };

            return this;
        }

        public OrderBuilder WithSupplierContact(Contact contact)
        {
            order.SupplierContact = contact;
            return this;
        }

        public OrderBuilder WithFundingSource(bool fs)
        {
            order.FundingSourceOnlyGms = fs;
            return this;
        }

        public Order Build()
        {
            return order;
        }
    }
}
