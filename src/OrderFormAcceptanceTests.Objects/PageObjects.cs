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
                Common = new Common()
            };
        }
        public PageCollection Pages { get; set; }
    }
}
