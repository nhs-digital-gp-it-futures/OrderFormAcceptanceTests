namespace OrderFormAcceptanceTests.Objects.Pages
{
    using System;
    using OpenQA.Selenium;
    using OrderFormAcceptanceTests.Objects.Utils;

    public static class OrganisationsOrdersDashboard
    {
        public static By PageTitle => CustomBy.DataTestId("dashboard-page-title");

        public static By CreateOrderButton => CustomBy.DataTestId("new-order-button", "a");

        public static Func<string, By> SpecificExistingOrder => callOffAgreementId => CustomBy.PartialDataTestId("{0}-id", callOffAgreementId);

        public static By Orders => By.CssSelector("table[data-test-id='table'] > tbody > tr");

        public static By GenericExistingOrder => By.CssSelector("a[data-test-id$='-id']");

        public static By GenericExistingOrderDescription => By.CssSelector("td > [data-test-id$='-description']");

        public static By GenericExistingOrderLastUpdatedBy => By.CssSelector("[data-test-id$='-lastUpdatedBy']");

        public static By GenericExistingOrderLastUpdatedDate => By.CssSelector("[data-test-id$='-lastUpdated']");

        public static By GenericExistingOrderCreatedDate => By.CssSelector("[data-test-id$='-dateCreated']");

        public static By GenericExistingOrderCompletedDate => By.CssSelector("[data-test-id$='-dateCompleted']");

        public static By IncompleteOrdersBeforeCompletedOrders => By.CssSelector(".nhsuk-grid-column-full > [data-test-id='incomplete-orders-table'] + [data-test-id='complete-orders-table']");

        public static By GenericColumnHeadingData => By.CssSelector("[data-test-id$='-data']");

        public static By IncompleteOrdersTable => CustomBy.DataTestId("incomplete-orders-table");

        public static By CompletedOrdersTable => CustomBy.DataTestId("complete-orders-table");

        public static By GenericExistingOrderAutomaticallyProcessed => By.CssSelector("[data-test-id$='-automaticallyProcessed']");
    }
}
