namespace OrderFormAcceptanceTests.Objects.Pages
{
    using OpenQA.Selenium;
    using OrderFormAcceptanceTests.Objects.Utils;

    public static class PrintOrderSummary
    {
        public static By PrintableOrderSummary => CustomBy.DataTestId("summary-page-title");

        public static By PrintPreview => By.TagName("print-preview-app");
    }
}
