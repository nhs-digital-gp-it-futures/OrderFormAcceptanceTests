using OpenQA.Selenium;
using OrderFormAcceptanceTests.Objects.Utils;

namespace OrderFormAcceptanceTests.Objects.Pages
{
	public class Common
	{
		public By ErrorTitle => CustomBy.DataTestId("error-title");
		public By ErrorMessages => By.CssSelector("ul.nhsuk-list.nhsuk-error-summary__list li a");
		public By BackLink => By.ClassName("nhsuk-back-link__link");
		public By LoggedInDisplayName => CustomBy.DataTestId("logged-in-text");
		public By SaveButton => CustomBy.DataTestId("save-button", "button");
	}
}
