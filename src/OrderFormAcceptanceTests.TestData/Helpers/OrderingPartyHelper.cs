namespace OrderFormAcceptanceTests.TestData.Helpers
{
    using System.Linq;
    using System.Threading.Tasks;
    using OrderFormAcceptanceTests.Domain;
    using OrderFormAcceptanceTests.TestData.Utils;

    public static class OrderingPartyHelper
    {
        public static async Task<OrderingParty> GetByOdsCode(this OrderingParty orderingParty, string connectionString)
        {
            var query = "SELECT * FROM OrderingParty WHERE OdsCode=@OdsCode";

            var results = await SqlExecutor.ExecuteAsync<OrderingParty>(connectionString, query, orderingParty);

            return results.SingleOrDefault();
        }

        public static async Task<OrderingParty> Create(this OrderingParty orderingParty, string connectionString)
        {
            var existing = await orderingParty.GetByOdsCode(connectionString);

            if (existing is not null)
            {
                return orderingParty;
            }

            var query = @"INSERT INTO OrderingParty
                            (
                              Id,
                              OdsCode,
                              [Name]
                              AddressId
                            )
                          VALUES (
                              @Id,
                              @OdsCode,
                              @Name
                              @AddressId
                            );";

            await SqlExecutor.ExecuteAsync<OrderingParty>(connectionString, query, orderingParty);

            return await orderingParty.GetByOdsCode(connectionString);
        }
    }
}
