namespace OrderFormAcceptanceTests.Objects.Pages
{
    using OpenQA.Selenium;
    using OrderFormAcceptanceTests.Objects.Utils;

    public static class Homepage
    {
        public static By Title => By.CssSelector(".nhsuk-hero__wrapper h1");

        public static By LoginLogoutLink => CustomBy.DataTestId("login-logout-component", "a");

        public static By OrderTile => CustomBy.DataTestId("order-form-promo", "a");
    }
}
