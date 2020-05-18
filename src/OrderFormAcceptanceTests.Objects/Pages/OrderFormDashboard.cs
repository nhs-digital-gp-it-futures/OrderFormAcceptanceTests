using OpenQA.Selenium;
using OrderFormAcceptanceTests.Objects.Utils;
using System;

namespace OrderFormAcceptanceTests.Objects.Pages
{
	public class OrderFormDashboard
	{
		public By PageTitle => CustomBy.DataTestId("dashboard-page-title");
		public By CreateOrderButton => CustomBy.DataTestId("new-order-button", "a");
		public Func<string, By> ExistingOrder => (CallOffAgreementId) => CustomBy.PartialDataTestId("{0}-id", CallOffAgreementId);
	}
}
