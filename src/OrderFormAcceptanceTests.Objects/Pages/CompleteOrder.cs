using System;
using OpenQA.Selenium;
using OrderFormAcceptanceTests.Objects.Utils;

namespace OrderFormAcceptanceTests.Objects.Pages
{
    public sealed class CompleteOrder
    {
        public By CompleteOrderButton => CustomBy.DataTestId("complete-order-button", "button");
        public By FundingSourceContent => CustomBy.DataTestId("complete-order-page-description");
        public By CompletedPageDescription => CustomBy.DataTestId("order-confirmation-page-description");
        public By GetOrderSummaryLink => CustomBy.DataTestId("order-confirmation-page-orderSummaryButton","a");
    }
}
