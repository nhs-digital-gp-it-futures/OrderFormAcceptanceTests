using OpenQA.Selenium;
using OrderFormAcceptanceTests.Actions.Collections;

namespace OrderFormAcceptanceTests.Actions
{
    public sealed class PageActions
    {
        public PageActions(IWebDriver driver)
        {
            PageActionCollection = new PageActionCollection
            {
            };
        }

        public PageActionCollection PageActionCollection { get; set; }
    }
}
