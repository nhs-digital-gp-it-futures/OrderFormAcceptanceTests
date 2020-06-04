using Bogus;
using OrderFormAcceptanceTests.TestData.Utils;
using System.Linq;

namespace OrderFormAcceptanceTests.TestData
{
    public sealed class ServiceRecipient
    {
        public int ServiceRecipientId { get; set; }
        public string OrderId { get; set; }
        public string Name { get; set; }
        public string OdsCode { get; set; }


        public ServiceRecipient Generate(string orderId, string odsCode = "00C")
        {
            var faker = new Faker();
            return new ServiceRecipient
            {
                OrderId = orderId,
                Name = faker.Name.FullName(),
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
                            @[OrderId]
                            ,@[Name]
                            ,@[OdsCode]
                        )";
            SqlExecutor.Execute<ServiceRecipient>(connectionString, query, this);
        }

        public ServiceRecipient Retrieve(string connectionString)
        {
            var query = "SELECT * from [dbo].[ServiceRecipient] WHERE ServiceRecipientId=@ServiceRecipientId";

            return SqlExecutor.Execute<ServiceRecipient>(connectionString, query, this).Single();
        }

        public ServiceRecipient RetrieveByOrderId(string connectionString, string orderId)
        {
            var query = "SELECT * from [dbo].[ServiceRecipient] WHERE ServiceRecipientId=@orderId";

            return SqlExecutor.Execute<ServiceRecipient>(connectionString, query, new { orderId }).Single();
        }

        public void Update(string connectionString)
        {
            var query = @"UPDATE [dbo].[ServiceRecipient]
                        SET
                            [OrderId]=@OrderId
                            ,[Name]=@Name 
                            ,[OdsCode]=@OdsCode
                        WHERE ServiceRecipientId=@ServiceRecipientId";
            SqlExecutor.Execute<ServiceRecipient>(connectionString, query, this);
        }

        public void Delete(string connectionString)
        {
            var query = @"DELETE FROM [dbo].[ServiceRecipient] WHERE ServiceRecipientId=@ServiceRecipientId";
            SqlExecutor.Execute<Order>(connectionString, query, this);
        }
    }
}
