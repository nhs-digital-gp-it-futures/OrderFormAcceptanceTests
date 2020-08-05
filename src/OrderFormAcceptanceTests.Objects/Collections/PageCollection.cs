using OrderFormAcceptanceTests.Objects.Pages;

namespace OrderFormAcceptanceTests.Objects.Collections
{
    public sealed class PageCollection
    {
        public Login Login { get; set; }
        public Homepage Homepage { get; set; }
        public Common Common { get; set; }
        public OrganisationsOrdersDashboard OrganisationsOrdersDashboard { get; set; }
        public OrderForm OrderForm { get; set; }
        public CommencementDate CommencementDate { get; set; }
        public AdditionalServices AdditionalServices { get; set; }
        public CompleteOrder CompleteOrder { get; set; }
        public DeleteOrder DeleteOrder { get; set; }
        public PrintOrderSummary PrintOrderSummary { get; set; }
    }
}
