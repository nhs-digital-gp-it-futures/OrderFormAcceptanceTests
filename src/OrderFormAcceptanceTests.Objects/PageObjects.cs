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
                OrderFormDashboard = new OrderFormDashboard(),
                OrderForm = new OrderForm()
            };
        }
        public PageCollection Pages { get; set; }
    }
}
