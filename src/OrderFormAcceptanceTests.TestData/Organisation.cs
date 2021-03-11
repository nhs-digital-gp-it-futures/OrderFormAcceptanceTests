namespace OrderFormAcceptanceTests.TestData
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
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

        public static async Task<Organisation> GetOdsCode(string connectionString, Guid organisationId)
        {
            var query = "SELECT * FROM Organisations WHERE OrganisationId = @organisationId;";
            var result = await SqlExecutor.ExecuteAsync<Organisation>(connectionString, query, new { organisationId });
            return result.Single();
        }
    }
}
