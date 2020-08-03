using OpenQA.Selenium;
using OrderFormAcceptanceTests.Actions.Utils;
using System;

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
                Wait.Until(d => d.FindElement(Pages.CompleteOrder.FundingSourceContent).Text.Contains("As GMS is your only source", StringComparison.OrdinalIgnoreCase));
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool FundingSourceNoContentIsDisplayed()
        {
            try
            {
                Wait.Until(d => d.FindElement(Pages.CompleteOrder.FundingSourceContent).Text.Contains("As GMS is not your only source", StringComparison.OrdinalIgnoreCase));
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

        public bool DownloadPDFControlIsDisplayed()
        {
            try
            {
                Wait.Until(d => d.FindElements(Pages.CompleteOrder.DownloadPDF).Count == 1);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
