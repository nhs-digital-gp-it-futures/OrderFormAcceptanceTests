namespace OrderFormAcceptanceTests.Actions.Pages
{
    using System;
    using OpenQA.Selenium;
    using OrderFormAcceptanceTests.Actions.Utils;

    public class CompleteOrder : PageAction
    {
        public CompleteOrder(IWebDriver driver)
            : base(driver)
        {
        }

        public bool FundingSourceYesContentIsDisplayed()
        {
            const string content = "payment will be centrally processed";
            try
            {
                Wait.Until(d => d.FindElement(Objects.Pages.CompleteOrder.FundingSourceContent).Text.Contains(content, StringComparison.OrdinalIgnoreCase));
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool FundingSourceYesContentOnCompletedScreenIsDisplayed()
        {
            const string content = "payment will be centrally processed";
            try
            {
                Wait.Until(d => d.FindElement(Objects.Pages.CompleteOrder.CompletedPageDescription).Text.Contains(content, StringComparison.OrdinalIgnoreCase));
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool FundingSourceNoContentIsDisplayed()
        {
            const string content = "As you're not paying for this order in full using your GP IT Futures centrally held funding allocation";
            try
            {
                Wait.Until(d => d.FindElement(Objects.Pages.CompleteOrder.FundingSourceContent).Text.Contains(content, StringComparison.OrdinalIgnoreCase));
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool FundingSourceNoContentOnCompletedScreenIsDisplayed()
        {
            const string content = "As your centrally held funding allocation is not the only source of funding";
            try
            {
                Wait.Until(d => d.FindElement(Objects.Pages.CompleteOrder.CompletedPageDescription).Text.Contains(content, StringComparison.OrdinalIgnoreCase));
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
                Wait.Until(d => d.FindElements(Objects.Pages.OrderForm.CompleteOrderButton).Count == 1);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void ClickCompleteOrderButton()
        {
            Driver.FindElement(Objects.Pages.CompleteOrder.CompleteOrderButton).Click();
        }

        public bool ContinueEditingOrderButtonIsDisplayed()
        {
            try
            {
                Wait.Until(d => d.FindElements(Objects.Pages.CompleteOrder.ContinueEditingButton).Count == 1);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void ClickContinueEditingOrderButton()
        {
            Driver.FindElement(Objects.Pages.CompleteOrder.ContinueEditingButton).Click();
        }

        public bool DownloadPDFControlIsDisplayed()
        {
            try
            {
                Wait.Until(d => d.FindElements(Objects.Pages.CompleteOrder.GetOrderSummaryLink).Count == 1);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void ClickGetOrderSummaryLink()
        {
            Driver.FindElement(Objects.Pages.CompleteOrder.GetOrderSummaryLink).Click();
        }
    }
}
