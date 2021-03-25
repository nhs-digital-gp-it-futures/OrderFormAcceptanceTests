namespace OrderFormAcceptanceTests.TestData.Builders
{
    using System;
    using System.Collections.Generic;
    using OrderFormAcceptanceTests.Domain;

    public sealed class OrderItemBuilder
    {
        private readonly OrderItem orderItem;

        public OrderItemBuilder(int orderId)
        {
            orderItem = new OrderItem
            {
                OrderId = orderId,
            };
        }

        public OrderItemBuilder WithDefaultDeliveryDate(DateTime date)
        {
            orderItem.DefaultDeliveryDate = date;
            return this;
        }

        public OrderItemBuilder WithProvisioningType(ProvisioningType provisioningType)
        {
            orderItem.ProvisioningType = provisioningType;
            return this;
        }

        public OrderItemBuilder WithCataloguePriceType(CataloguePriceType cataloguePriceType)
        {
            orderItem.CataloguePriceType = cataloguePriceType;
            return this;
        }

        public OrderItemBuilder WithCatalogueItem(CatalogueItem catalogueItem)
        {
            orderItem.CatalogueItem = catalogueItem;
            return this;
        }

        public OrderItemBuilder WithPrice(decimal price)
        {
            orderItem.Price = price;
            return this;
        }

        public OrderItemBuilder WithCurrencyCode(string code = "GBP")
        {
            orderItem.CurrencyCode = code;
            return this;
        }

        public OrderItemBuilder WithPricingUnit(PricingUnit unit)
        {
            orderItem.PricingUnit = unit;
            return this;
        }

        public OrderItemBuilder WithPricingTimeUnit(TimeUnit unit)
        {
            orderItem.PriceTimeUnit = unit;
            return this;
        }

        public OrderItemBuilder WithEstimationPeriod(TimeUnit unit)
        {
            orderItem.EstimationPeriod = unit;
            return this;
        }

        public OrderItemBuilder WithRecipients(IEnumerable<OrderItemRecipient> recipients)
        {
            orderItem.SetRecipients(recipients);
            return this;
        }

        public OrderItem Build()
        {
            return orderItem;
        }
    }
}
