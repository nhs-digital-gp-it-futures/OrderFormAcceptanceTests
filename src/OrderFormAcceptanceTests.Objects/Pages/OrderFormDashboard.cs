using OpenQA.Selenium;
using OrderFormAcceptanceTests.Objects.Utils;

namespace OrderFormAcceptanceTests.Objects.Pages
{
	public class OrderFormDashboard
	{
		public By CreateOrderButton => CustomBy.DataTestId("new-order-button", "a");
	}
}
