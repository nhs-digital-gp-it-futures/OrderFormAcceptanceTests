using OpenQA.Selenium;
using OrderFormAcceptanceTests.Objects.Utils;
using System;

namespace OrderFormAcceptanceTests.Objects.Pages
{
	public class OrderFormDashboard
	{
		public By PageTitle => CustomBy.DataTestId("dashboard-page-title");
		public By CreateOrderButton => CustomBy.DataTestId("new-order-button", "a");
		public Func<string, By> SpecificExistingOrder => (CallOffAgreementId) => CustomBy.PartialDataTestId("{0}-id", CallOffAgreementId);
		public By Orders => By.CssSelector("div[data-test-id='table'] > div + div");
		public By GenericExistingOrder => By.CssSelector("[data-test-id$='-id']");
		public By GenericExistingOrderDescription => By.CssSelector("[data-test-id='table'] > div > [data-test-id$='-description']");
		public By GenericExistingOrderLastUpdatedBy => By.CssSelector("[data-test-id$='-lastUpdatedBy']");
		public By GenericExistingOrderLastUpdatedDate => By.CssSelector("[data-test-id$='-lastUpdated']");
		public By GenericExistingOrderCreatedDate => By.CssSelector("[data-test-id$='-dateCreated']");
		public By UnsubmittedOrdersTable => CustomBy.DataTestId("unsubmitted-orders-table-title");
		public By SubmittedOrdersTable => CustomBy.DataTestId("submitted-orders-table-title");
		public By NominateProxy => CustomBy.DataTestId("proxy-link", "a");
	}
}
