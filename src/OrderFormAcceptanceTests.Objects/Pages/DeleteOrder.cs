namespace OrderFormAcceptanceTests.Objects.Pages
{
    using OpenQA.Selenium;
    using OrderFormAcceptanceTests.Objects.Utils;

    public static class DeleteOrder
    {
        public static By DeleteButtonYes => CustomBy.DataTestId("yes-button", "button");

        public static By DeleteButtonNo => CustomBy.DataTestId("no-button", "a");
    }
}
