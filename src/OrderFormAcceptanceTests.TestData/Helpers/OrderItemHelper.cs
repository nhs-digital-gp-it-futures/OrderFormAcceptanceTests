namespace OrderFormAcceptanceTests.TestData.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using OrderFormAcceptanceTests.Domain;
    using OrderFormAcceptanceTests.Persistence.Data;
    using OrderFormAcceptanceTests.TestData.Builders;
    using OrderFormAcceptanceTests.TestData.Extensions;
    using OrderFormAcceptanceTests.TestData.Information;

    public static class OrderItemHelper
    {
        public static async Task<OrderItem> CreateOrderItem(
            Order order,
            CatalogueItemType catalogueItemType,
            CataloguePriceType cataloguePriceType,
            ProvisioningType provisioningType,
            OrderingDbContext context,
            string connectionString,
            TimeUnit timeUnit = TimeUnit.PerYear)
        {
            var solution = await SupplierInfo.GetPublishedCatalogueItems(connectionString, order.Supplier.Id, catalogueItemType);

            var selectedItem = RandomInformation.GetRandomItem(solution);

            var catalogueItem = await context.FindAsync<CatalogueItem>(CatalogueItemId.ParseExact(selectedItem.CatalogueItemId))
                ?? selectedItem.ToDomain();

            var pricingUnitName = "per banana";

            var pricingUnit = await context.FindAsync<PricingUnit>(pricingUnitName) ?? new PricingUnit
            {
                Name = pricingUnitName,
            };

            pricingUnit.Description = pricingUnitName;

            var orderItem = new OrderItemBuilder(order.Id)
                .WithCatalogueItem(catalogueItem)
                .WithCataloguePriceType(cataloguePriceType)
                .WithCurrencyCode()
                .WithDefaultDeliveryDate(DateTime.Today)
                .WithPrice(0.01M)
                .WithPricingTimeUnit(timeUnit)
                .WithProvisioningType(provisioningType)
                .WithPricingUnit(pricingUnit);

            if (provisioningType == ProvisioningType.OnDemand)
            {
                orderItem.WithEstimationPeriod(timeUnit);
            }

            return orderItem.Build();
        }

        public static OrderItem AddRecipientToOrderItem(OrderItem orderItem, IEnumerable<OrderItemRecipient> recipients, OrderingDbContext context)
        {
            List<OrderItemRecipient> validatedRecipients = new();
            foreach (var recipient in recipients)
            {
                var existing = context.ServiceRecipient.Find(recipient.Recipient.OdsCode) ?? recipient.Recipient;
                recipient.Recipient = existing;

                validatedRecipients.Add(recipient);
            }

            orderItem.SetRecipients(validatedRecipients);
            return orderItem;
        }
    }
}
