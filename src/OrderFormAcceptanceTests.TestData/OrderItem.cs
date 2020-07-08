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
        public int CatalogueItemTypeId { get; set; }
        public string CatalogueItemName { get; set; }
        public string OdsCode { get; set; }
        public int ProvisioningTypeId { get; set; }
        public int CataloguePriceTypeId { get; set; }
        public string PricingUnitTierName { get; set; }
        public string PricingUnitName { get; set; }
        public string PricingUnitDescription { get; set; }
        public int? TimeUnitId { get; set; }
        public string CurrencyCode { get; set; }
        public int Quantity { get; set; }
        public int? EstimationPeriodId { get; set; }
        public DateTime DeliveryDate { get; set; }
        public decimal Price { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastUpdated { get; set; }

        public OrderItem GenerateOrderItemWithFlatPricedVariableOnDemand(Order order)
        {   
            return new OrderItem
            {
                OrderId = order.OrderId,
                CatalogueItemId = "100000-001",
                CatalogueItemTypeId = 1,
                CatalogueItemName = "Write on Time",
                OdsCode = order.OrganisationOdsCode,
                ProvisioningTypeId = 3,
                CataloguePriceTypeId = 1,
                PricingUnitTierName = "consultations",
                PricingUnitName = "consultation",
                PricingUnitDescription = "per consultation",
                TimeUnitId = null,
                CurrencyCode = "GBP",
                Quantity = 1111,
                EstimationPeriodId = 2,
                DeliveryDate = DateTime.Now.AddYears(1),
                Price = 1001.010M,
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

        public IEnumerable<OrderItem> RetrieveByOrderId(string connectionString, string OrderId)
        {
            var query = "SELECT * from [dbo].[OrderItem] WHERE OrderId=@orderId";
            return SqlExecutor.Execute<OrderItem>(connectionString, query, new { OrderId });
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
    }
}
