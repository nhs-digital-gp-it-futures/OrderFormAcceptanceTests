using OrderFormAcceptanceTests.TestData.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        public OrderItem RetrieveByOrderId(string connectionString, string OrderId)
        {
            var query = "SELECT * from [dbo].[OrderItem] WHERE OrderId=@orderId";
            return SqlExecutor.Execute<OrderItem>(connectionString, query, new { OrderId }).Single();
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
