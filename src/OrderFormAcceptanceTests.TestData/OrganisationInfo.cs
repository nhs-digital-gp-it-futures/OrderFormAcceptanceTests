using OrderFormAcceptanceTests.TestData.Utils;

namespace OrderFormAcceptanceTests.TestData
{
    public static class OrganisationInfo
    {
        public static void DeleteContactsForOrdersNoLongerInDb(string connectionString)
        {
            var query = "DELETE FROM dbo.Contact " +
                "WHERE ContactId NOT IN (SELECT OrganisationContactId FROM dbo.[Order])" +
                "OR ContactId NOT IN (SELECT SupplierContactId FROM dbo.[Order]";

            SqlExecutor.Execute<string>(connectionString, query, null);
        }
    }
}
