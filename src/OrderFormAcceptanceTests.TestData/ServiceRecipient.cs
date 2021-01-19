using OrderFormAcceptanceTests.TestData.Utils;
using System.Collections.Generic;

namespace OrderFormAcceptanceTests.TestData
{
    public sealed class ServiceRecipient
    {
        public string OrderId { get; set; }
        public string Name { get; set; }
        public string OdsCode { get; set; }


        public static ServiceRecipient Generate(string orderId, string odsCode = "00C", string name = "NHS Darlington CCG")
        {
            return new ServiceRecipient
            {
                OrderId = orderId,
                Name = name,
                OdsCode = odsCode
            };
        }

        public void Create(string connectionString)
        {
            var query = @"INSERT INTO [dbo].[ServiceRecipient]
                        (
                            [OrderId]
                            ,[Name]
                            ,[OdsCode]
                        )VALUES
                        (
                            @OrderId
                            ,@Name
                            ,@OdsCode
                        )";
            SqlExecutor.Execute<ServiceRecipient>(connectionString, query, this);
        }

        public IEnumerable<ServiceRecipient> Retrieve(string connectionString)
        {
            var query = "SELECT * from [dbo].[ServiceRecipient] WHERE OrderId=@OrderId";

            return SqlExecutor.Execute<ServiceRecipient>(connectionString, query, this);
        }

        public static IEnumerable<ServiceRecipient> RetrieveByOrderId(string connectionString, string orderId)
        {
            var query = "SELECT * from [dbo].[ServiceRecipient] WHERE OrderId=@orderId";

            return SqlExecutor.Execute<ServiceRecipient>(connectionString, query, new { orderId });
        }

        public void Update(string connectionString)
        {
            var query = @"UPDATE [dbo].[ServiceRecipient]
                        SET
                            [OrderId]=@OrderId
                            ,[Name]=@Name 
                            ,[OdsCode]=@OdsCode
                        WHERE OrderId=@OrderId";
            SqlExecutor.Execute<ServiceRecipient>(connectionString, query, this);
        }

        public void Delete(string connectionString)
        {
            var query = @"DELETE FROM [dbo].[ServiceRecipient] WHERE OrderId=@OrderId";
            SqlExecutor.Execute<Order>(connectionString, query, this);
        }

        public static void DeleteAllServiceRecipientsForOrderId(string connectionString, string orderId)
        {
            var query = @"DELETE FROM [dbo].[ServiceRecipient] WHERE OrderId=@orderId";
            SqlExecutor.Execute<Order>(connectionString, query, new { orderId });
        }
    }
}
