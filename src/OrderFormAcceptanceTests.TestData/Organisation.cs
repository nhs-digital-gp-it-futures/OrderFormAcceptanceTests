﻿using System;
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
            var query = @"SELECT *
                          FROM dbo.Organisations AS o
                          WHERE NOT EXISTS (
                               SELECT *
                               FROM dbo.AspNetUsers AS u
                               WHERE u.PrimaryOrganisationId = o.OrganisationId);";
            var listOfItems = SqlExecutor.Execute<Organisation>(connectionString, query, this);
            return listOfItems.ElementAt(new Faker().Random.Number(listOfItems.Count() - 1));
        }
    }
}
