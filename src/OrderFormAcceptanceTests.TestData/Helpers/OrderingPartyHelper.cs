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
    }
}
