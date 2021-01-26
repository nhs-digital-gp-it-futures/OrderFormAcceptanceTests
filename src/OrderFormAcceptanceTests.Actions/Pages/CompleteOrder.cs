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
            try
            {
                Wait.Until(d => d.FindElement(Objects.Pages.CompleteOrder.FundingSourceContent).Text.Contains("As GMS is your only source", StringComparison.OrdinalIgnoreCase));
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool FundingSourceYesContentOnCompletedScreenIsDisplayed()
        {
            try
            {
                Wait.Until(d => d.FindElement(Objects.Pages.CompleteOrder.CompletedPageDescription).Text.Contains("It'll be automatically processed", StringComparison.OrdinalIgnoreCase));
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
                Wait.Until(d => d.FindElement(Objects.Pages.CompleteOrder.FundingSourceContent).Text.Contains("As GMS is not your only source", StringComparison.OrdinalIgnoreCase));
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool FundingSourceNoContentOnCompletedScreenIsDisplayed()
        {
            try
            {
                Wait.Until(d => d.FindElement(Objects.Pages.CompleteOrder.CompletedPageDescription).Text.Contains("not be automatically processed", StringComparison.OrdinalIgnoreCase));
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
