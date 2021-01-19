using OpenQA.Selenium;
using OrderFormAcceptanceTests.Objects.Utils;

namespace OrderFormAcceptanceTests.Objects.Pages
{
    public static class PrintOrderSummary
    {
        public static By PrintableOrderSummary => CustomBy.DataTestId("summary-page-title");
        public static By PrintPreview => By.TagName("print-preview-app");
    }
}
