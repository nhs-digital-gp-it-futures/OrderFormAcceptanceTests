namespace OrderFormAcceptanceTests.TestData
{
    using System.Globalization;
    using System.Linq;
    using System.Threading.Tasks;
    using Bogus;
    using OrderFormAcceptanceTests.TestData.Utils;

    internal sealed class OrderHelpers
    {
        private const string DefaultOrderId = "C010000-01";

        public static async Task<string> GetLatestOrderId(string connectionString)
        {
            return await GetIncrementedOrderId(connectionString);
        }

        private static async Task<string> GetIncrementedOrderId(string connectionString)
        {
            var resultOrderId = DefaultOrderId;
            var latestOrderId = await GetLatestOrderIdByCreationDateAsync(connectionString);

            if (string.IsNullOrEmpty(latestOrderId))
            {
                return resultOrderId;
            }

            var numberSection = latestOrderId.Substring(1, 6);
            var orderNumber = int.Parse(numberSection, CultureInfo.InvariantCulture);

            resultOrderId = $"C{orderNumber + 1:D6}-{new Faker().Random.Number(1, 99):D2}";

            return resultOrderId;
        }

        private static async Task<string> GetLatestOrderIdByCreationDateAsync(string connectionString)
        {
            var query = "SELECT TOP 1 OrderId FROM dbo.[Order] ORDER BY Created DESC";
            var result = await SqlExecutor.ExecuteAsync<string>(connectionString, query, null);
            return result.FirstOrDefault();
        }
    }
}
