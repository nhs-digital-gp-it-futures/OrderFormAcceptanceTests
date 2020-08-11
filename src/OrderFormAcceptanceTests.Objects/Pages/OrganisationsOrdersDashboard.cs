using OpenQA.Selenium;
using OrderFormAcceptanceTests.Objects.Utils;
using System;

namespace OrderFormAcceptanceTests.Objects.Pages
{
    public class OrganisationsOrdersDashboard
    {
        public By PageTitle => CustomBy.DataTestId("dashboard-page-title");
        public By CreateOrderButton => CustomBy.DataTestId("new-order-button", "a");
        public Func<string, By> SpecificExistingOrder => (CallOffAgreementId) => CustomBy.PartialDataTestId("{0}-id", CallOffAgreementId);
        public By Orders => By.CssSelector("table[data-test-id='table'] > tbody > tr");
        public By GenericExistingOrder => By.CssSelector("a[data-test-id$='-id']");
        public By GenericExistingOrderDescription => By.CssSelector("td > [data-test-id$='-description']");
        public By GenericExistingOrderLastUpdatedBy => By.CssSelector("[data-test-id$='-lastUpdatedBy']");
        public By GenericExistingOrderLastUpdatedDate => By.CssSelector("[data-test-id$='-lastUpdated']");
        public By GenericExistingOrderCreatedDate => By.CssSelector("[data-test-id$='-dateCreated']");
        public By UnsubmittedOrdersTable => CustomBy.DataTestId("incomplete-orders-table");
        public By SubmittedOrdersTable => CustomBy.DataTestId("complete-orders-table");
        public By GenericExistingOrderAutomaticallyProcessed => By.CssSelector("[data-test-id$='-automaticallyProcessed']");
    }
}
