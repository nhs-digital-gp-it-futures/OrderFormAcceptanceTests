using OrderFormAcceptanceTests.TestData.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OrderFormAcceptanceTests.TestData
{
    public sealed class Order
    {
        public string OrderId { get; set; }
        public string Description { get; set; }
        public Guid OrganisationId { get; set; }
        public int OrderStatusId { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastUpdated { get; set; }
        public Guid LastUpdatedBy { get; set; }

        public Order Retrieve(string connectionString)
        {
            var query = "SELECT * from [dbo].[Order] WHERE Order.OrderId=@orderId";

            return SqlExecutor.Execute<Order>(connectionString, query, this).Single();
        }

        public void Create(string connectionString)
        {
            var query = @"INSERT INTO [dbo].[Order]
                                (OrderId,
                                 Description,
                                 OrganisationId,
                                 OrderStatusId,
                                 Created,
                                 LastUpdated,
                                 LastUpdatedBy
                                 )
                                VALUES
                                (@OrderId,
                                 @Description,
                                 @OrganisationId,
                                 @OrderStatusId,
                                 @Created,
                                 @LastUpdated,
                                 @LastUpdatedBy
                        )";
            SqlExecutor.Execute<Order>(connectionString, query, this);
        }

        public void Update(string connectionString)
        {
            var query = @"UPDATE Order 
                        SET 
                            OrderId=@orderId,
                            Description=@description,
                            OrganisationId=@organisationId,
                            OrderStatusId=@orderStatusId,
                            Created=@created,
                            LastUpdated=@lastUpdated,
                            LastUpdatedBy=@lastUpdatedBy
                        WHERE OrderId=@orderId";
            SqlExecutor.Execute<Order>(connectionString, query, this);
        }

        public void Delete(string connectionString)
        {
            var query = @"DELETE FROM Order WHERE OrderId=@orderId";
            SqlExecutor.Execute<Order>(connectionString, query, this);
        }
    }
}
