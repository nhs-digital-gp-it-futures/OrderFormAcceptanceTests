namespace OrderFormAcceptanceTests.Objects.Pages
{
    using OpenQA.Selenium;
    using OrderFormAcceptanceTests.Objects.Utils;

    public static class AdditionalServices
    {
        public static By AddAdditionalServices => CustomBy.DataTestId("add-orderItem-button");

        public static By NoAddedOrderItemsMessage => CustomBy.DataTestId("no-added-orderItems");

        public static By PricePageTitle => CustomBy.DataTestId("additional-service-price-page-title");

        public static By ServiceRecipientsTitle => CustomBy.DataTestId("additional-service-recipient-page-title");

        public static By PriceInput => By.Id("price");

        public static By PriceUnit => CustomBy.DataTestId("unit-of-order");

        public static By QuantityInput => By.Id("quantity");
    }
}
