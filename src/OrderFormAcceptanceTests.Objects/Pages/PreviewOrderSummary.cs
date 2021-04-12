namespace OrderFormAcceptanceTests.Objects.Pages
{
    using OpenQA.Selenium;
    using OrderFormAcceptanceTests.Objects.Utils;

    public static class PreviewOrderSummary
    {
        public static By TopGetOrderSummaryLink => CustomBy.DataTestId("summary-page-orderSummaryButton-top", "a");

        public static By BottomGetOrderSummaryLink => CustomBy.DataTestId("summary-page-orderSummaryButton-bottom", "a");

        public static By DateOrderSummaryCreated => CustomBy.DataTestId("date-summary-created");

        public static By DateOrderCompleted => CustomBy.DataTestId("date-completed");

        public static By CallOffOrderingPartyPreview => CustomBy.DataTestId("call-off-party");

        public static By SupplierPreview => CustomBy.DataTestId("supplier");

        public static By CommencementDate => CustomBy.DataTestId("commencement-date");

        public static By OneOffCostsTable => CustomBy.DataTestId("one-off-cost-table");

        public static By RecurringCostsTable => CustomBy.DataTestId("recurring-cost-table");

        public static By ItemRecipientName => CustomBy.DataTestId("recipient-name");

        public static By ItemId => CustomBy.DataTestId("item-id");

        public static By ItemName => CustomBy.DataTestId("item-name");

        public static By ItemPrice => CustomBy.DataTestId("price-unit");

        public static By ItemQuantity => CustomBy.DataTestId("quantity");

        public static By ItemPlannedDate => CustomBy.DataTestId("planned-date");

        public static By ItemCost => CustomBy.DataTestId("item-cost");

        public static By CostPerYear => CustomBy.DataTestId("costPerYear");

        public static By TotalOneOffCost => CustomBy.DataTestId("total-cost-value");

        public static By TotalAnnualCost => CustomBy.DataTestId("total-year-cost-value");

        public static By TotalMonthlyCost => CustomBy.DataTestId("total-monthly-cost-value");

        public static By TotalOwnershipCost => CustomBy.DataTestId("total-ownership-cost-value");

        public static By ServiceInstanceIdColumn => CustomBy.DataTestId("column-heading-3", "div");

        public static By SummaryDescription => CustomBy.DataTestId("summary-page-description");
    }
}
