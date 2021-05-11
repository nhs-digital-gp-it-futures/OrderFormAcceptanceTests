namespace OrderFormAcceptanceTests.TestData.Models
{
    using System;

    public sealed class RelatedOrganisation
    {
        public Guid OrganisationId { get; set; }

        public Guid RelatedOrganisationId { get; set; }
    }
}
