using OpenQA.Selenium;
using OrderFormAcceptanceTests.Objects.Utils;

namespace OrderFormAcceptanceTests.Objects.Pages
{
    public static class CompleteOrder
    {
        public static By CompleteOrderButton => CustomBy.DataTestId("complete-order-button", "button");
        public static By ContinueEditingButton => CustomBy.DataTestId("continue-editing-order-button", "a");
        public static By FundingSourceContent => CustomBy.DataTestId("complete-order-page-description");
        public static By CompletedPageDescription => CustomBy.DataTestId("order-confirmation-page-description");
        public static By GetOrderSummaryLink => CustomBy.DataTestId("order-confirmation-page-orderSummaryButton","a");
    }
}
