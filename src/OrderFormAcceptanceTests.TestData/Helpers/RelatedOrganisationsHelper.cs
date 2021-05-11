namespace OrderFormAcceptanceTests.TestData.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using OrderFormAcceptanceTests.TestData.Information;
    using OrderFormAcceptanceTests.TestData.Models;
    using OrderFormAcceptanceTests.TestData.Utils;

    public static class RelatedOrganisationsHelper
    {
        public static RelatedOrganisation GenerateRelatedOrganisation(IEnumerable<Guid> allOrganisations, IEnumerable<RelatedOrganisation> relatedOrganisations)
        {
            var primaryOrganisation = relatedOrganisations.First().OrganisationId;

            var relatedOrganisationIds = relatedOrganisations.Select(s => s.RelatedOrganisationId);

            var orgsWithoutRelationship = allOrganisations
                .Where(s => !s.Equals(primaryOrganisation))
                .Except(relatedOrganisationIds);

            RelatedOrganisation relatedOrganisation = new()
            {
                OrganisationId = primaryOrganisation,
                RelatedOrganisationId = RandomInformation.GetRandomItem(orgsWithoutRelationship),
            };

            return relatedOrganisation;
        }

        public static async Task<IEnumerable<RelatedOrganisation>> GetAllRelatedOrganisationsForOrganisation(string connectionString, Guid organisationId)
        {
            var query = "SELECT * FROM dbo.RelatedOrganisation WHERE OrganisationId = @organisationId;";

            return await SqlExecutor.ExecuteAsync<RelatedOrganisation>(connectionString, query, new { organisationId });
        }

        public static async Task AddRelatedOrganisation(string connectionString, RelatedOrganisation relatedOrganisation)
        {
            var query = "INSERT INTO dbo.RelatedOrganisation (OrganisationId, RelatedOrganisationId) VALUES (@organisationId, @relatedOrganisationId);";

            await SqlExecutor.ExecuteAsync<RelatedOrganisation>(connectionString, query, new { organisationId = relatedOrganisation.OrganisationId, relatedOrganisationId = relatedOrganisation.RelatedOrganisationId });
        }

        public static async Task DeleteRelatedOrganisation(string connectionString, RelatedOrganisation relatedOrganisation)
        {
            var query = "DELETE FROM dbo.RelatedOrganisation WHERE OrganisationId = @organisationId AND RelatedOrganisationId = @relatedOrganisationId;";

            await SqlExecutor.ExecuteAsync<RelatedOrganisation>(connectionString, query, new { organisationId = relatedOrganisation.OrganisationId, relatedOrganisationId = relatedOrganisation.RelatedOrganisationId });
        }

        public static async Task<IEnumerable<Guid>> GetAllOrganisations(string connectionString)
        {
            var query = "SELECT OrganisationId FROM dbo.Organisations;";

            return await SqlExecutor.ExecuteAsync<Guid>(connectionString, query, null);
        }
    }
}
