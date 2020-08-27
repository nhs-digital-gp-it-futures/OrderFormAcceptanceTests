using OpenQA.Selenium;
using OrderFormAcceptanceTests.Actions.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public string GetDateOrderSummaryCreatedValue()
        {
            return Driver.FindElement(Pages.PreviewOrderSummary.DateOrderSummaryCreated).Text;
        }

        public string GetDateOrderCompletedValue()
        {
            return Driver.FindElement(Pages.PreviewOrderSummary.DateOrderCompleted).Text;
        }

        public string GetCallOffOrderingPartyPreviewValue()
        {
            return Driver.FindElement(Pages.PreviewOrderSummary.CallOffOrderingPartyPreview).Text;
        }

        public string GetSupplierPreviewValue()
        {
            return Driver.FindElement(Pages.PreviewOrderSummary.SupplierPreview).Text;
        }

        public string GetCommencementDateValue()
        {
            return Driver.FindElement(Pages.PreviewOrderSummary.CommencementDate).Text;
        }

        public bool OneOffCostsTableIsDisplayed()
        {
            return Driver.FindElements(Pages.PreviewOrderSummary.OneOffCostsTable).Count == 1;
        }

        public bool OneOffCostsTableIsPopulated()
        {
            return Driver.FindElement(Pages.PreviewOrderSummary.OneOffCostsTable)
                .FindElements(Pages.Common.TableRowX(0))
                .Count > 0;
        }

        public bool RecurringCostsTableIsDisplayed()
        {
            return Driver.FindElements(Pages.PreviewOrderSummary.RecurringCostsTable).Count == 1;
        }

        public bool RecurringCostsTableIsPopulated()
        {
            return Driver.FindElement(Pages.PreviewOrderSummary.RecurringCostsTable)
                .FindElements(Pages.Common.TableRowX(0))
                .Count > 0;
        }

        public string GetItemRecipientName()
        {
            return Driver.FindElement(Pages.PreviewOrderSummary.ItemRecipientName).Text;
        }

        public string GetItemId()
        {
            return Driver.FindElement(Pages.PreviewOrderSummary.ItemId).Text;
        }
        public string GetItemName()
        {
            return Driver.FindElement(Pages.PreviewOrderSummary.ItemName).Text;
        }

        public IEnumerable<string> GetItemNames()
        {
            return Driver.FindElements(Pages.PreviewOrderSummary.ItemName).Select(x => x.Text);
        }

        public IEnumerable<string> GetItemRecipientNames()
        {
            return Driver.FindElements(Pages.PreviewOrderSummary.ItemRecipientName).Select(x => x.Text);
        }

        public string GetItemPrice()
        {
            return Driver.FindElement(Pages.PreviewOrderSummary.ItemPrice).Text;
        }
        public string GetItemQuantity()
        {
            return Driver.FindElement(Pages.PreviewOrderSummary.ItemQuantity).Text;
        }

        public string GetItemPlannedDate()
        {
            return Driver.FindElement(Pages.PreviewOrderSummary.ItemPlannedDate).Text;
        }

        public string GetItemCost()
        {
            return Driver.FindElement(Pages.PreviewOrderSummary.ItemCost).Text;
        }

        public string GetTotalOneOffCost()
        {
            return Driver.FindElement(Pages.PreviewOrderSummary.TotalOneOffCost).Text;
        }

        public string GetTotalAnnualCost()
        {
            return Driver.FindElement(Pages.PreviewOrderSummary.TotalAnnualCost).Text;
        }

        public string GetTotalMonthlyCost()
        {
            return Driver.FindElement(Pages.PreviewOrderSummary.TotalMonthlyCost).Text;
        }

        public string GetTotalOwnershipCost()
        {
            return Driver.FindElement(Pages.PreviewOrderSummary.TotalOwnershipCost).Text;
        }
    }
}
