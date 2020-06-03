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
                CommencementDate = new CommencementDate()
            };
        }
        public PageCollection Pages { get; set; }
    }
}
