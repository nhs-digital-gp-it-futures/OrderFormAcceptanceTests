using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;
using OrderFormAcceptanceTests.Objects.Utils;

namespace OrderFormAcceptanceTests.Objects.Pages
{
    public class PreviewOrderSummary
    {
        public By TopGetOrderSummaryLink => CustomBy.DataTestId("summary-page-orderSummaryButton-top","a");
        public By BottomGetOrderSummaryLink => CustomBy.DataTestId("summary-page-orderSummaryButton-bottom", "a");

        public By DateOrderSummaryCreated => CustomBy.DataTestId("date-summary-created");
        public By DateOrderCompleted => CustomBy.DataTestId("date-completed");

        public By CallOffOrderingPartyPreview => CustomBy.DataTestId("call-off-party");
        public By SupplierPreview => CustomBy.DataTestId("supplier");
        public By CommencementDate => CustomBy.DataTestId("commencement-date");
        public By OneOffCostsTable => CustomBy.DataTestId("one-off-cost-table");
        public By RecurringCostsTable => CustomBy.DataTestId("recurring-cost-table");        
        public By ItemRecipientName = CustomBy.DataTestId("recipient-name");
        public By ItemId => CustomBy.DataTestId("item-id");
        public By ItemName => CustomBy.DataTestId("item-name");
        public By ItemPrice => CustomBy.DataTestId("price-unit");
        public By ItemQuantity => CustomBy.DataTestId("quantity");
        public By ItemPlannedDate => CustomBy.DataTestId("planned-date");
        public By ItemCost => CustomBy.DataTestId("item-cost");
        public By TotalOneOffCost => CustomBy.DataTestId("total-cost-value");
        public By TotalAnnualCost => CustomBy.DataTestId("total-year-cost-value");
        public By TotalMonthlyCost => CustomBy.DataTestId("total-monthly-cost-value");
        public By TotalOwnershipCost => CustomBy.DataTestId("total-ownership-cost-value");
        public By ServiceInstanceIdColumn => CustomBy.DataTestId("column-heading-3", "div");
    }
}
