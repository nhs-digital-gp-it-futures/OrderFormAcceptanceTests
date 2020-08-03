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
                OrganisationsOrdersDashboard = new OrganisationsOrdersDashboard(driver),
                OrderForm = new OrderForm(driver),
                CommencementDate = new CommencementDate(driver),
                AdditionalServices = new AdditionalServices(driver),
                CompleteOrder = new CompleteOrder(driver),
                DeleteOrder = new DeleteOrder(driver)
            };
        }

        public PageActionCollection PageActionCollection { get; set; }
    }
}
