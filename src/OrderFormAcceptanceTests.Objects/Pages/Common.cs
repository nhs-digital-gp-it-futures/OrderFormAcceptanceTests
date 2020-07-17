using OpenQA.Selenium;
using OrderFormAcceptanceTests.Objects.Utils;
using System.Runtime.CompilerServices;

namespace OrderFormAcceptanceTests.Objects.Pages
{
	public class Common
	{
		public By ErrorTitle => CustomBy.DataTestId("error-title");
		public By ErrorSummary => CustomBy.DataTestId("error-summary");
		public By ErrorMessages => By.CssSelector("ul.nhsuk-list.nhsuk-error-summary__list li a");
		public By BackLink => By.ClassName("nhsuk-back-link__link");
		public By LoggedInDisplayName => CustomBy.DataTestId("logged-in-text");
		public By SaveButton => CustomBy.DataTestId("save-button", "button");
		public By ContinueButton => CustomBy.DataTestId("continue-button", "button");
		public By DeleteButton => CustomBy.DataTestId("delete-button", "button");
		public By TextArea => By.TagName("textarea");
		public By TextField => By.ClassName("nhsuk-input");
		public By Checkbox => By.ClassName("nhsuk-checkboxes__input");
		public By RadioButton => By.ClassName("nhsuk-radios__input");
		public By RadioButtonLabel => By.CssSelector("div.nhsuk-radios__item label.nhsuk-radios__label");
		public By Footer => By.Id("nhsuk-footer");
		public By Header => CustomBy.DataTestId("header-banner");
		public By BetaBanner => CustomBy.DataTestId("beta-banner");
	}
}
