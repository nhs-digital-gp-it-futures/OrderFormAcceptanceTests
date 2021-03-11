namespace OrderFormAcceptanceTests.TestData
{
    using System.Text.Json;
    using OrderFormAcceptanceTests.TestData.Models;

    public sealed class SupplierDetails
    {
        public string SupplierId { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public AddressModel AddressFromJson
        {
            get
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                };
                return JsonSerializer.Deserialize<AddressModel>(Address, options);
            }
        }
    }
}
