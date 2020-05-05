using OpenQA.Selenium;
using OrderFormAcceptanceTests.Actions.Collections;
using OrderFormAcceptanceTests.Actions.Pages;

namespace OrderFormAcceptanceTests.Actions
{
    public sealed class PageActions
    {
        public PageActions(IWebDriver driver)
        {
            PageActionCollection = new PageActionCollection
            {
                Authentication = new Authentication(driver),
                Homepage = new Homepage(driver),
                OrderForm = new OrderForm(driver)
            };
        }

        public PageActionCollection PageActionCollection { get; set; }
    }
}
