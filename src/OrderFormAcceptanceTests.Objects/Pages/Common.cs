namespace OrderFormAcceptanceTests.Objects.Pages
{
    using System;
    using OpenQA.Selenium;
    using OrderFormAcceptanceTests.Objects.Utils;

    public static class Common
    {
        public static By ErrorTitle => CustomBy.DataTestId("error-title");

        public static By ErrorSummary => CustomBy.DataTestId("error-summary");

        public static By ErrorMessages => By.CssSelector("ul.nhsuk-list.nhsuk-error-summary__list li a");

        public static By BackLink => By.ClassName("nhsuk-back-link__link");

        public static By LoggedInDisplayName => CustomBy.DataTestId("logged-in-text");

        public static By SaveButton => CustomBy.DataTestId("save-button", "button");

        public static By ContinueButton => CustomBy.DataTestId("continue-button", "button");

        public static By DeleteButton => CustomBy.DataTestId("delete-order-button", "a");

        public static By DeleteCatalogueItemButton => CustomBy.DataTestId("delete-button");

        public static By TextArea => By.TagName("textarea");

        public static By TextField => By.ClassName("nhsuk-input");

        public static By Checkbox => By.ClassName("nhsuk-checkboxes__input");

        public static By RadioButton => By.ClassName("nhsuk-radios__input");

        public static By RadioButtonLabel => By.CssSelector("div.nhsuk-radios__item label.nhsuk-radios__label");

        public static By Footer => By.Id("nhsuk-footer");

        public static By Header => CustomBy.DataTestId("header-banner");

        public static By BetaBanner => CustomBy.DataTestId("beta-banner");

        public static By TableRows => By.CssSelector("[data-test-id^=table-row-]");

        public static Func<int, By> TableRowX => (index) => By.CssSelector(string.Format("[data-test-id=table-row-{0}]", index.ToString()));

        public static By DeleteConfirmationOrderDescription => CustomBy.DataTestId("catalogue-description");

        public static By ChangeOrgLink => CustomBy.DataTestId("dashboard-page-proxy-on-behalf");

        public static By SelectPage => By.ClassName("nhsuk-hint");
    }
}
