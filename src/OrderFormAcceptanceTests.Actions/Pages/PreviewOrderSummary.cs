namespace OrderFormAcceptanceTests.Actions.Pages
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using OpenQA.Selenium;
    using OrderFormAcceptanceTests.Actions.Utils;

    public class PreviewOrderSummary : PageAction
    {
        public PreviewOrderSummary(IWebDriver driver)
            : base(driver)
        {
        }

        public void ClickGetPreviewOrderSummary()
        {
            Wait.Until(d => d.FindElements(Objects.Pages.PreviewOrderSummary.TopGetOrderSummaryLink).Count == 1);

            Driver.FindElement(Objects.Pages.PreviewOrderSummary.TopGetOrderSummaryLink)
                .Click();
        }

        public bool TopGetOrderSummaryIsDisplayed()
        {
            try
            {
                Wait.Until(d => d.FindElements(Objects.Pages.PreviewOrderSummary.TopGetOrderSummaryLink).Count == 1);
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
                Wait.Until(d => d.FindElements(Objects.Pages.PreviewOrderSummary.BottomGetOrderSummaryLink).Count == 1);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public string GetDateOrderSummaryCreatedValue()
        {
            return Driver.FindElement(Objects.Pages.PreviewOrderSummary.DateOrderSummaryCreated).Text;
        }

        public string GetDateOrderCompletedValue()
        {
            return Driver.FindElement(Objects.Pages.PreviewOrderSummary.DateOrderCompleted).Text;
        }

        public string GetCallOffOrderingPartyPreviewValue()
        {
            return Driver.FindElement(Objects.Pages.PreviewOrderSummary.CallOffOrderingPartyPreview).Text;
        }

        public string GetSupplierPreviewValue()
        {
            return Driver.FindElement(Objects.Pages.PreviewOrderSummary.SupplierPreview).Text;
        }

        public string GetCommencementDateValue()
        {
            return Driver.FindElement(Objects.Pages.PreviewOrderSummary.CommencementDate).Text;
        }

        public bool OneOffCostsTableIsDisplayed()
        {
            return Driver.FindElements(Objects.Pages.PreviewOrderSummary.OneOffCostsTable).Count == 1;
        }

        public bool OneOffCostsTableIsPopulated()
        {
            return Driver.FindElement(Objects.Pages.PreviewOrderSummary.OneOffCostsTable)
                .FindElements(Objects.Pages.Common.TableRowX(0))
                .Count > 0;
        }

        public bool RecurringCostsTableIsDisplayed()
        {
            return Driver.FindElements(Objects.Pages.PreviewOrderSummary.RecurringCostsTable).Count == 1;
        }

        public bool RecurringCostsTableIsPopulated()
        {
            return Driver.FindElement(Objects.Pages.PreviewOrderSummary.RecurringCostsTable)
                .FindElements(Objects.Pages.Common.TableRowX(0))
                .Count > 0;
        }

        public string GetItemRecipientName()
        {
            return Driver.FindElement(Objects.Pages.PreviewOrderSummary.ItemRecipientName).Text;
        }

        public string GetItemId()
        {
            return Driver.FindElement(Objects.Pages.PreviewOrderSummary.ItemId).Text;
        }

        public string GetItemName()
        {
            return Driver.FindElement(Objects.Pages.PreviewOrderSummary.ItemName).Text;
        }

        public IEnumerable<string> GetItemNames()
        {
            return Driver.FindElements(Objects.Pages.PreviewOrderSummary.ItemName).Select(x => x.Text);
        }

        public IEnumerable<string> GetItemRecipientNames()
        {
            return Driver.FindElements(Objects.Pages.PreviewOrderSummary.ItemRecipientName).Select(x => x.Text);
        }

        public string GetItemPrice()
        {
            return Driver.FindElement(Objects.Pages.PreviewOrderSummary.ItemPrice).Text;
        }

        public string GetItemQuantity()
        {
            return Driver.FindElement(Objects.Pages.PreviewOrderSummary.ItemQuantity).Text;
        }

        public string GetItemPlannedDate()
        {
            return Driver.FindElement(Objects.Pages.PreviewOrderSummary.ItemPlannedDate).Text;
        }

        public string GetItemCost()
        {
            return Driver.FindElement(Objects.Pages.PreviewOrderSummary.ItemCost).Text;
        }

        public string GetTotalOneOffCost()
        {
            return Driver.FindElement(Objects.Pages.PreviewOrderSummary.TotalOneOffCost).Text;
        }

        public string GetTotalAnnualCost()
        {
            return Driver.FindElement(Objects.Pages.PreviewOrderSummary.TotalAnnualCost).Text;
        }

        public string GetTotalMonthlyCost()
        {
            return Driver.FindElement(Objects.Pages.PreviewOrderSummary.TotalMonthlyCost).Text;
        }

        public string GetTotalOwnershipCost()
        {
            return Driver.FindElement(Objects.Pages.PreviewOrderSummary.TotalOwnershipCost).Text;
        }

        public bool ServiceInstanceIdColumn()
        {
            return Driver.FindElement(Objects.Pages.PreviewOrderSummary.RecurringCostsTable)
                .FindElement(Objects.Pages.PreviewOrderSummary.ServiceInstanceIdColumn).Text.Equals("Service Instance ID", StringComparison.Ordinal);
        }
    }
}
