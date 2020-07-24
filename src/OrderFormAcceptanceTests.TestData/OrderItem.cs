using OrderFormAcceptanceTests.TestData.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OrderFormAcceptanceTests.TestData
{
    public sealed class OrderItem
    {
        public int OrderItemId { get; set; }
        public string OrderId { get; set; }
        public string CatalogueItemId { get; set; }
        public CatalogueItemType CatalogueItemTypeId { get; set; }
        public string CatalogueItemName { get; set; }
        public string ParentCatalogueItemId { get; set; }
        public string OdsCode { get; set; }
        public ProvisioningType ProvisioningTypeId { get; set; }
        public int CataloguePriceTypeId { get; set; }
        public string PricingUnitTierName { get; set; }
        public string PricingUnitName { get; set; }
        public string PricingUnitDescription { get; set; }
        public int? TimeUnitId { get; set; }
        public string CurrencyCode { get; set; }
        public int Quantity { get; set; }
        public TimeUnit? EstimationPeriodId { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public decimal Price { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastUpdated { get; set; }

        public OrderItem GenerateOrderItemWithFlatPricedVariableOnDemand(Order order)
        {
            return new OrderItem
            {
                OrderId = order.OrderId,
                CatalogueItemId = "100000-001",
                CatalogueItemTypeId = CatalogueItemType.Solution,
                CatalogueItemName = "Write on Time",
                OdsCode = order.OrganisationOdsCode,
                ProvisioningTypeId = ProvisioningType.OnDemand,
                CataloguePriceTypeId = 1,
                PricingUnitTierName = null,
                PricingUnitName = "consultation",
                PricingUnitDescription = "per consultation",
                TimeUnitId = null,
                CurrencyCode = "GBP",
                Quantity = 1111,
                EstimationPeriodId = TimeUnit.Year,
                DeliveryDate = DateTime.Now.AddYears(1),
                Price = 1001.010M,
                Created = DateTime.Now,
                LastUpdated = DateTime.Now
            };
        }

        public OrderItem GenerateOrderItemWithFlatPricedVariablePerPatient(Order order)
        {
            return new OrderItem
            {
                OrderId = order.OrderId,
                CatalogueItemId = "100000-001",
                CatalogueItemTypeId = CatalogueItemType.Solution,
                CatalogueItemName = "Write on Time",
                OdsCode = order.OrganisationOdsCode,
                ProvisioningTypeId = ProvisioningType.Patient,
                CataloguePriceTypeId = 1,
                PricingUnitTierName = null,
                PricingUnitName = "patient",
                PricingUnitDescription = "per patient",
                TimeUnitId = 2,
                CurrencyCode = "GBP",
                Quantity = 1111,
                EstimationPeriodId = TimeUnit.Month,
                DeliveryDate = DateTime.Now.AddYears(1),
                Price = 99.99M,
                Created = DateTime.Now,
                LastUpdated = DateTime.Now
            };
        }

        public OrderItem GenerateOrderItemWithFlatPricedVariableDeclarative(Order order)
        {
            return new OrderItem
            {
                OrderId = order.OrderId,
                CatalogueItemId = "100003-001",
                CatalogueItemTypeId = CatalogueItemType.Solution,
                CatalogueItemName = "Intellidoc Comms",
                OdsCode = order.OrganisationOdsCode,
                ProvisioningTypeId = ProvisioningType.Declarative,
                CataloguePriceTypeId = 1,
                PricingUnitTierName = null,
                PricingUnitName = "bed",
                PricingUnitDescription = "per bed",
                TimeUnitId = 1,
                CurrencyCode = "GBP",
                Quantity = 1111,
                EstimationPeriodId = TimeUnit.Year,
                DeliveryDate = DateTime.Now.AddYears(1),
                Price = 7.00M,
                Created = DateTime.Now,
                LastUpdated = DateTime.Now
            };
        }

        public OrderItem GenerateAdditionalServiceOrderItemWithFlatPricedPerPatient(Order order)
        {
            return new OrderItem
            {
                OrderId = order.OrderId,
                CatalogueItemId = "100000-001-A01",
                CatalogueItemTypeId = CatalogueItemType.AdditionalService,
                CatalogueItemName = "Write on Time additional service",
                OdsCode = order.OrganisationOdsCode,
                ProvisioningTypeId = ProvisioningType.Patient,
                CataloguePriceTypeId = 1,
                PricingUnitTierName = null,
                PricingUnitName = "patient",
                PricingUnitDescription = "per patient",
                TimeUnitId = 1,
                CurrencyCode = "GBP",
                Quantity = 305,
                EstimationPeriodId = TimeUnit.Month,
                DeliveryDate = DateTime.Now.AddYears(1),
                Price = 199.990M,
                Created = DateTime.Now,
                LastUpdated = DateTime.Now
            };
        }

        public OrderItem GenerateAdditionalServiceWithFlatPricedVariableOnDemand(Order order)
        {
            return new OrderItem
            {
                OrderId = order.OrderId,
                CatalogueItemId = "100000-S-001",
                CatalogueItemTypeId = CatalogueItemType.AdditionalService,
                CatalogueItemName = "Really Kool additional service",
                OdsCode = order.OrganisationOdsCode,
                ProvisioningTypeId = ProvisioningType.OnDemand,
                CataloguePriceTypeId = 1,
                PricingUnitTierName = "half days",
                PricingUnitName = "halfDay",
                PricingUnitDescription = "per half day",
                TimeUnitId = null,
                CurrencyCode = "GBP",
                Quantity = 9,
                EstimationPeriodId = TimeUnit.Year,
                DeliveryDate = DateTime.Now.AddYears(1),
                Price = 150.030M,
                Created = DateTime.Now,
                LastUpdated = DateTime.Now
            };
        }

        public OrderItem GenerateAdditionalServiceOrderItemWithVariablePricedPerPatient(Order order)
        {
            return new OrderItem
            {
                OrderId = order.OrderId,
                CatalogueItemId = "100000-S-001",
                CatalogueItemTypeId = CatalogueItemType.AdditionalService,
                CatalogueItemName = "Write on Time patient additional service",
                OdsCode = order.OrganisationOdsCode,
                ProvisioningTypeId = ProvisioningType.Patient,
                CataloguePriceTypeId = 1,
                PricingUnitTierName = "patients",
                PricingUnitName = "patient",
                PricingUnitDescription = "per patient",
                TimeUnitId = 2,
                CurrencyCode = "GBP",
                Quantity = 1111,
                EstimationPeriodId = TimeUnit.Month,
                DeliveryDate = DateTime.Now.AddYears(1),
                Price = 99.99M,
                Created = DateTime.Now,
                LastUpdated = DateTime.Now
            };
        }

        public OrderItem GenerateAdditionalServiceOrderItemWithDeclarative(Order order)
        {
            return new OrderItem
            {
                OrderId = order.OrderId,
                CatalogueItemId = "100000-S-001",
                CatalogueItemTypeId = CatalogueItemType.AdditionalService,
                CatalogueItemName = "Really Kool additional service",
                OdsCode = order.OrganisationOdsCode,
                ProvisioningTypeId = ProvisioningType.Declarative,
                CataloguePriceTypeId = 1,
                PricingUnitTierName = "appointments",
                PricingUnitName = "appointments",
                PricingUnitDescription = "per appointment",
                TimeUnitId = 1,
                CurrencyCode = "GBP",
                Quantity = 9,
                EstimationPeriodId = TimeUnit.Year,
                DeliveryDate = DateTime.Now.AddYears(1),
                Price = 150.100M,
                Created = DateTime.Now,
                LastUpdated = DateTime.Now
            };
        }

        public OrderItem GenerateAssociatedServiceWithFlatPricedVariableOnDemand(Order order)
        {
            return new OrderItem
            {
                OrderId = order.OrderId,
                CatalogueItemId = "100000-S-001",
                CatalogueItemTypeId = CatalogueItemType.AssociatedService,
                CatalogueItemName = "Really Kool associated service",
                OdsCode = order.OrganisationOdsCode,
                ProvisioningTypeId = ProvisioningType.OnDemand,
                CataloguePriceTypeId = 1,
                PricingUnitTierName = null,
                PricingUnitName = "halfDay",
                PricingUnitDescription = "per half day",
                TimeUnitId = null,
                CurrencyCode = "GBP",
                Quantity = 9,
                EstimationPeriodId = TimeUnit.Year,
                DeliveryDate = DateTime.Now.AddYears(1),
                Price = 150.000M,
                Created = DateTime.Now,
                LastUpdated = DateTime.Now
            };
        }

        public OrderItem GenerateAssociatedServiceWithFlatPricedDeclarative(Order order)
        {
            return new OrderItem
            {
                OrderId = order.OrderId,
                CatalogueItemId = "100000-S-001",
                CatalogueItemTypeId = CatalogueItemType.AssociatedService,
                CatalogueItemName = "Really Kool associated service",
                OdsCode = order.OrganisationOdsCode,
                ProvisioningTypeId = ProvisioningType.Declarative,
                CataloguePriceTypeId = 1,
                PricingUnitTierName = null,
                PricingUnitName = "course",
                PricingUnitDescription = "per course",
                TimeUnitId = null,
                CurrencyCode = "GBP",
                Quantity = 9,
                EstimationPeriodId = TimeUnit.Year,
                DeliveryDate = null,
                Price = 150.000M,
                Created = DateTime.Now,
                LastUpdated = DateTime.Now
            };
        }

        public int Create(string connectionString)
        {
            var query = @"INSERT INTO [dbo].[OrderItem](
                         [OrderId]
                        ,[CatalogueItemId]
                        ,[CatalogueItemTypeId]
                        ,[CatalogueItemName]
                        ,[ParentCatalogueItemId]
                        ,[OdsCode]
                        ,[ProvisioningTypeId]
                        ,[CataloguePriceTypeId]
                        ,[PricingUnitTierName]
                        ,[PricingUnitName]
                        ,[PricingUnitDescription]
                        ,[TimeUnitId]
                        ,[CurrencyCode]
                        ,[Quantity]
                        ,[EstimationPeriodId]
                        ,[DeliveryDate]
                        ,[Price]
                        ,[Created]
                        ,[LastUpdated]
                        )VALUES(
                         @OrderId
                        ,@CatalogueItemId
                        ,@CatalogueItemTypeId
                        ,@CatalogueItemName
                        ,@ParentCatalogueItemId
                        ,@OdsCode
                        ,@ProvisioningTypeId
                        ,@CataloguePriceTypeId
                        ,@PricingUnitTierName
                        ,@PricingUnitName
                        ,@PricingUnitDescription
                        ,@TimeUnitId
                        ,@CurrencyCode
                        ,@Quantity
                        ,@EstimationPeriodId
                        ,@DeliveryDate
                        ,@Price
                        ,@Created
                        ,@LastUpdated
                        );

                        SELECT OrderItemId = SCOPE_IDENTITY()";
            this.OrderItemId = SqlExecutor.Execute<int>(connectionString, query, this).Single();
            return this.OrderItemId;
        }

        public IEnumerable<OrderItem> RetrieveByOrderId(string connectionString, string OrderId, int CatalogueItemTypeId = 1)
        {
            var query = "SELECT * from [dbo].[OrderItem] WHERE OrderId=@orderId AND CatalogueItemTypeId=@catalogueItemTypeId";
            return SqlExecutor.Execute<OrderItem>(connectionString, query, new { OrderId, CatalogueItemTypeId });
        }

        public void Update(string connectionString)
        {
            var query = @"UPDATE [dbo].[OrderItem] 
                        SET 
                         OrderItemId=@OrderItemId
                        ,OrderId=@OrderId
                        ,CatalogueItemId=@CatalogueItemId
                        ,CatalogueItemTypeId=@CatalogueItemTypeId
                        ,CatalogueItemName=@CatalogueItemName
                        ,ParentCatalogueItem=@ParentCatalogueItemId
                        ,OdsCode=@OdsCode
                        ,ProvisioningTypeId=@ProvisioningTypeId
                        ,CataloguePriceTypeId=@CataloguePriceTypeId
                        ,PricingUnitTierName=@PricingUnitTierName
                        ,PricingUnitName=@PricingUnitName
                        ,PricingUnitDescription=@PricingUnitDescription
                        ,TimeUnitId=@TimeUnitId
                        ,CurrencyCode=@CurrencyCode
                        ,Quantity=@Quantity
                        ,EstimationPeriodId=@EstimationPeriodId
                        ,DeliveryDate=@DeliveryDate
                        ,Price=@Price
                        ,Created=@Created
                        ,=LastUpdated@LastUpdated
                        WHERE OrderItemId=@OrderItemId";

            SqlExecutor.Execute<OrderItem>(connectionString, query, this);
        }

        public void Delete(string connectionString)
        {
            var query = @"DELETE FROM [dbo].[OrderItem] WHERE OrderItemId=@OrderItemId";
            SqlExecutor.Execute<OrderItem>(connectionString, query, this);
        }

        public void DeleteAllOrderItemsForOrderId(string connectionString, string orderId)
        {
            var query = @"DELETE FROM [dbo].[OrderItem] WHERE OrderId=@orderId";
            SqlExecutor.Execute<Order>(connectionString, query, new { orderId });
        }

        public string GetEstimationPeriod(string connectionString)
        {
            const string query = @"Select [Description] FROM [dbo].[TimeUnit] WHERE TimeUnitId=@EstimationPeriodId";
            return SqlExecutor.Execute<string>(connectionString, query, this).FirstOrDefault();
        }

        public string GetTimeUnitPeriod(string connectionString)
        {
            const string query = @"Select [Description] FROM [dbo].[TimeUnit] WHERE TimeUnitId=@TimeUnitId";
            return SqlExecutor.Execute<string>(connectionString, query, this).FirstOrDefault();
        }

        public decimal CalculateItemCost()
        {
            return Price * Quantity * GetItemCostOffset();
        }

        private decimal GetItemCostOffset()
        {
            if (ProvisioningTypeId == ProvisioningType.OnDemand
                && EstimationPeriodId == TimeUnit.Month ||
                (ProvisioningTypeId == ProvisioningType.Declarative
                 && (CatalogueItemTypeId == CatalogueItemType.Solution 
                     || CatalogueItemTypeId == CatalogueItemType.AdditionalService)))
            {
                return 12m;
            }

            return 1m;
        }
    }
}
