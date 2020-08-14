using OrderFormAcceptanceTests.Objects.Collections;
using OrderFormAcceptanceTests.Objects.Pages;

namespace OrderFormAcceptanceTests.Objects
{
    public sealed class PageObjects
    {
        public PageObjects()
        {
            Pages = new PageCollection
            {
                Login = new Login(),
                Homepage = new Homepage(),
                Common = new Common(),
                OrganisationsOrdersDashboard = new OrganisationsOrdersDashboard(),
                OrderForm = new OrderForm(),
                CommencementDate = new CommencementDate(),
                AdditionalServices = new AdditionalServices(),
                CompleteOrder = new CompleteOrder(),
                DeleteOrder = new DeleteOrder(),
                PrintOrderSummary = new PrintOrderSummary(),
                PreviewOrderSummary = new PreviewOrderSummary()
            };
        }
        public PageCollection Pages { get; set; }
    }
}
