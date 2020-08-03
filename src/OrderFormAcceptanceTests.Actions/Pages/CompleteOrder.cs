using OpenQA.Selenium;
using OrderFormAcceptanceTests.Actions.Utils;

namespace OrderFormAcceptanceTests.Actions.Pages
{
    public class CompleteOrder : PageAction
    {
        public CompleteOrder(IWebDriver driver) : base(driver)
        {
        }

        public bool FundingSourceYesContentIsDisplayed()
        {
            try
            {
                Wait.Until(d => d.FindElements(Pages.CompleteOrder.FundingSourceYesContent).Count == 1);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool CompleteOrderButtonIsDisplayed()
        {
            try
            {
                Wait.Until(d => d.FindElements(Pages.OrderForm.SubmitOrderButton).Count == 1);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
