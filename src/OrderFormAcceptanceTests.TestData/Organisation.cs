namespace OrderFormAcceptanceTests.TestData
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Bogus;
    using OrderFormAcceptanceTests.TestData.Utils;

    public sealed class Organisation
    {
        public Guid OrganisationId { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string OdsCode { get; set; }

        public string PrimaryRoleId { get; set; }

        public int CatalogueAgreementSigned { get; set; } = 0;

        public DateTime LastUpdated { get; set; }

        public static async Task<Organisation> GetByODSCode(string odsCode, string connectionString)
        {
            var query = "SELECT * FROM Organisations WHERE OdsCode = @odsCode;";
            var result = await SqlExecutor.ExecuteAsync<Organisation>(connectionString, query, new { odsCode });
            return result.Single();
        }

        public Organisation RetrieveRandomOrganisation(string connectionString)
        {
            var query = "SELECT * FROM dbo.Organisations;";
            var listOfItems = SqlExecutor.Execute<Organisation>(connectionString, query, this);
            return listOfItems.ElementAt(new Faker().Random.Number(listOfItems.Count() - 1));
        }
    }
}
