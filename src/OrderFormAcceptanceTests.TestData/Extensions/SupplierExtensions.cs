namespace OrderFormAcceptanceTests.TestData.Extensions
{
    using OrderFormAcceptanceTests.Domain;

    public static class SupplierExtensions
    {
        public static Supplier ToDomain(this SupplierDetails model)
        {
            return model is null
                ? null
                : new Supplier
                {
                    Id = model.SupplierId,
                    Name = model.Name,
                    Address = model.AddressFromJson.ToDomain(),
                };
        }
    }
}
