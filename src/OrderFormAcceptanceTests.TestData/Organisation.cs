using System;
using System.Linq;
using Bogus;
using OrderFormAcceptanceTests.TestData.Utils;

namespace OrderFormAcceptanceTests.TestData
{
    public sealed class Organisation
    {
        public Guid OrganisationId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string OdsCode { get; set; }
        public string PrimaryRoleId { get; set; }
        public int CatalogueAgreementSigned { get; set; } = 0;
        public DateTime LastUpdated { get; set; }        

        public Organisation RetrieveRandomOrganisationWithNoUsers(string connectionString)
        {
            var query = @"SELECT [dbo].[Organisations].* FROM [dbo].[Organisations]
                          LEFT JOIN AspNetUsers on AspNetUsers.PrimaryOrganisationId=[Organisations].OrganisationId
                          WHERE AspNetUsers.Id IS NULL;";
            var listOfItems = SqlExecutor.Execute<Organisation>(connectionString, query, this);
            return listOfItems.ElementAt(new Faker().Random.Number(listOfItems.Count() - 1));
        }
    }
}
