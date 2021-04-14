namespace OrderFormAcceptanceTests.TestData.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using OrderFormAcceptanceTests.Domain;
    using OrderFormAcceptanceTests.Persistence.Data;
    using OrderFormAcceptanceTests.TestData.Builders;
    using OrderFormAcceptanceTests.TestData.Extensions;
    using OrderFormAcceptanceTests.TestData.Information;
    using OrderFormAcceptanceTests.TestData.Utils;

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
            var solution = await SupplierInfo.GetPublishedCatalogueItemsNoTieredAsync(connectionString, order.Supplier.Id, catalogueItemType);

            var selectedItem = RandomInformation.GetRandomItem(solution);

            var catalogueItem = await context.FindAsync<CatalogueItem>(CatalogueItemId.ParseExact(selectedItem.CatalogueItemId))
                ?? selectedItem.ToDomain();

            var catalogueItemPricingUnit = await GetPricingUnitAsync(provisioningType, connectionString);

            var pricingUnit = await context.FindAsync<PricingUnit>(catalogueItemPricingUnit.Name) ?? catalogueItemPricingUnit;

            var orderItem = new OrderItemBuilder(order.Id)
                .WithCatalogueItem(catalogueItem)
                .WithCataloguePriceType(cataloguePriceType)
                .WithCurrencyCode()
                .WithDefaultDeliveryDate(DateTime.Today)
                .WithPrice(0.01M)
                .WithPricingTimeUnit(timeUnit)
                .WithProvisioningType(provisioningType)
                .WithPricingUnit(pricingUnit)
                .WithEstimationPeriod(timeUnit);

            if (provisioningType == ProvisioningType.OnDemand)
            {
                orderItem.WithEstimationPeriod(timeUnit);
            }

            return orderItem.Build();
        }

        public static async Task<OrderItem> GetOrderItemWithId(Order order, string solutionId, string connectionString, OrderingDbContext context)
        {
            var newPricingUnit = await GetPricingUnitAsync(ProvisioningType.Declarative, connectionString);

            var pricingUnit = await context.FindAsync<PricingUnit>(newPricingUnit.Name) ?? newPricingUnit;

            var selectedItem = (await SupplierInfo.GetPublishedCatalogueItemsNoTieredAsync(connectionString, order.Supplier.Id, CatalogueItemType.Solution))
                .Single(s => s.CatalogueItemId.ToString().Equals(solutionId));

            var catalogueItem = await context.FindAsync<CatalogueItem>(CatalogueItemId.ParseExact(solutionId))
                ?? selectedItem.ToDomain();

            var orderItem = new OrderItemBuilder(order.Id)
                .WithCatalogueItem(catalogueItem)
                .WithCataloguePriceType(CataloguePriceType.Flat)
                .WithCurrencyCode()
                .WithDefaultDeliveryDate(DateTime.Today)
                .WithPrice(0.01M)
                .WithPricingTimeUnit(TimeUnit.PerYear)
                .WithProvisioningType(ProvisioningType.Declarative)
                .WithPricingUnit(pricingUnit)
                .WithEstimationPeriod(TimeUnit.PerMonth);

            return orderItem.Build();
        }

        public static async Task<OrderItem> AddRecipientToOrderItem(OrderItem orderItem, IEnumerable<OrderItemRecipient> recipients, OrderingDbContext context)
        {
            List<OrderItemRecipient> validatedRecipients = new();
            foreach (var recipient in recipients)
            {
                var existing = await context.ServiceRecipient.FindAsync(recipient.Recipient.OdsCode) ?? recipient.Recipient;
                recipient.Recipient = existing;

                validatedRecipients.Add(recipient);
            }

            orderItem.SetRecipients(validatedRecipients);
            return orderItem;
        }

        public static async Task<int> GetNumberOfPricingUnitsForItemAsync(string itemId, string connectionString)
        {
            var query = @"SELECT COUNT(*)
                          FROM dbo.CataloguePrice
                          WHERE CatalogueItemId = @itemId;";

            var result = await SqlExecutor.ExecuteScalarAsync(connectionString, query, new { itemId });

            return result;
        }

        public static async Task<ProvisioningType> GetProvisioningTypeForItemAsync(string itemId, string connectionString)
        {
            var query = @"SELECT ProvisioningTypeId
                            FROM dbo.CataloguePrice
                            WHERE CataloguePriceId = @itemId;";
            var result = (await SqlExecutor.ExecuteAsync<ProvisioningType>(connectionString, query, new { itemId })).Single();
            return result;
        }

        public static async Task<string> GetCatalogueItemIdForNameAsync(string itemName, string bapiConnectionString)
        {
            var query = @"SELECT CatalogueItemId
                        FROM dbo.CatalogueItem
                        WHERE [Name] = @itemName;";

            var result = (await SqlExecutor.ExecuteAsync<string>(bapiConnectionString, query, new { itemName })).Single();

            return result;
        }

        private static async Task<PricingUnit> GetPricingUnitAsync(ProvisioningType provisioningType, string bapiConnectionString)
        {
            var query = @"SELECT pu.[Name],
                          pu.Description
                          FROM PricingUnit AS pu
                          INNER JOIN CataloguePrice AS cp ON cp.PricingUnitId = pu.PricingUnitId
                          WHERE cp.ProvisioningTypeId = @provisioningType
                          AND CataloguePriceTypeId = '1';";

            var result = (await SqlExecutor.ExecuteAsync<PricingUnit>(bapiConnectionString, query, new { provisioningType })).FirstOrDefault();

            return result;
        }
    }
}
