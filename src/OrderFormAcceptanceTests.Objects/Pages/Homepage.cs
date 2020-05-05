using OpenQA.Selenium;
using OrderFormAcceptanceTests.Objects.Utils;

namespace OrderFormAcceptanceTests.Objects.Pages
{
	public sealed class Homepage
	{
		public By Title => By.CssSelector(".nhsuk-hero__wrapper h1");
		public By LoginLogoutLink => CustomBy.DataTestId("login-logout-component", "a");

		public By OrderTile => CustomBy.DataTestId("order-form-promo", "a");
	}
}
