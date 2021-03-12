namespace OrderFormAcceptanceTests.TestData
{
    using System.Threading.Tasks;
    using OrderFormAcceptanceTests.TestData.Utils;

    public static class OrganisationInfo
    {
        public static async Task DeleteContactsForOrdersNoLongerInDb(string connectionString)
        {
            var query = "DELETE FROM dbo.Contact " +
                "WHERE ContactId NOT IN (SELECT OrganisationContactId FROM dbo.[Order])" +
                "OR ContactId NOT IN (SELECT SupplierContactId FROM dbo.[Order];";

            await SqlExecutor.ExecuteAsync<string>(connectionString, query, null);
        }
    }
}
