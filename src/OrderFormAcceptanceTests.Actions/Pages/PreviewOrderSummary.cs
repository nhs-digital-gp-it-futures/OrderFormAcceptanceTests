using OpenQA.Selenium;
using OrderFormAcceptanceTests.Actions.Utils;
using System;

namespace OrderFormAcceptanceTests.Actions.Pages
{
    public class PreviewOrderSummary : PageAction
    {
        public PreviewOrderSummary(IWebDriver driver) : base(driver)
        {
        }

        public void ClickGetPreviewOrderSummary()
        {
            Wait.Until(d => d.FindElements(Pages.PreviewOrderSummary.TopGetOrderSummaryLink).Count == 1);

            Driver.FindElement(Pages.PreviewOrderSummary.TopGetOrderSummaryLink)
                .Click();
        }


        public bool TopGetOrderSummaryIsDisplayed()
        {
            try
            {
                Wait.Until(d => d.FindElements(Pages.PreviewOrderSummary.TopGetOrderSummaryLink).Count == 1);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool BottomGetOrderSummaryIsDisplayed()
        {
            try
            {
                Wait.Until(d => d.FindElements(Pages.PreviewOrderSummary.BottomGetOrderSummaryLink).Count == 1);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
