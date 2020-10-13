using Bogus;
using OrderFormAcceptanceTests.TestData.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OrderFormAcceptanceTests.TestData
{
    public sealed class Order
    {
        public string OrderId { get; set; }

        public string Description { get; set; }

        public Guid OrganisationId { get; set; }

        public string OrganisationName { get; set; }

        public string OrganisationOdsCode { get; set; }

        public int? OrganisationAddressId { get; set; }

        public int? OrganisationBillingAddressId { get; set; }

        public int? OrganisationContactId { get; set; }

        public int OrderStatusId { get; set; }

        public int ServiceRecipientsViewed { get; set; }

        public int? SupplierId { get; set; }

        public string SupplierName { get; set; }

        public int? SupplierAddressId { get; set; }

        public int? SupplierContactId { get; set; }

        public int CatalogueSolutionsViewed { get; set; }

        public int AdditionalServicesViewed { get; set; }

        public int AssociatedServicesViewed { get; set; }

        public int? FundingSourceOnlyGMS { get; set; }

        public DateTime? CommencementDate { get; set; }

        public DateTime? DateCompleted { get; set; }

        public DateTime Created { get; set; }

        public DateTime LastUpdated { get; set; }

        public Guid LastUpdatedBy { get; set; }

        public string LastUpdatedByName { get; set; }

        public int IsDeleted { get; set; }

        public Order Retrieve(string connectionString)
        {
            var query = "SELECT * from [dbo].[Order] WHERE OrderId=@orderId";

            return SqlExecutor.Execute<Order>(connectionString, query, this).Single();
        }

        public IEnumerable<int> GetContactIdsForOrder(string connectionString)
        {
            var query = "(SELECT OrganisationContactId FROM dbo.[Order] WHERE OrderId = @orderId) " +
                "UNION (SELECT SupplierContactId FROM dbo.[Order] WHERE OrderId = @orderId);";
            return SqlExecutor.Execute<int>(connectionString, query, this);
        }

        public Order Generate(Organisation organisation = null)
        {
            var faker = new Faker();
            return new Order
            {
                OrderId = GenerateRandomCallOffId(),
                Description = faker.Lorem.Sentence(6),
                OrganisationId = organisation?.OrganisationId ?? Guid.Parse("6F6F7D0D-01E9-488F-B7CD-C2E889C4080B"),
                OrganisationName = organisation?.Name ?? "NHS Darlington CCG",
                OrganisationOdsCode = organisation?.OdsCode ?? "00C",
                ServiceRecipientsViewed = 0,
                OrderStatusId = 2,
                CatalogueSolutionsViewed = 0,
                AdditionalServicesViewed = 0,
                AssociatedServicesViewed = 0,
                Created = DateTime.Now,
                DateCompleted = DateTime.Now,
                LastUpdated = DateTime.Now,
                LastUpdatedBy = Guid.Parse("BC0A6D7B-B44B-436D-8916-1E64EBCAAE64"),
                LastUpdatedByName = "Alice Smith",
                IsDeleted = 0
            };
        }

        public void Create(string connectionString)
        {
            var query = @"INSERT INTO [dbo].[Order]
                                (OrderId,
                                 Description,
                                 OrganisationId,
                                 OrganisationName
                                ,OrganisationOdsCode
                                ,OrganisationAddressId
                                ,OrganisationBillingAddressId
                                ,OrganisationContactId,
                                 OrderStatusId,
                                 ServiceRecipientsViewed,
                                 SupplierId
                                ,SupplierName
                                ,SupplierAddressId
                                ,SupplierContactId,
                                 CatalogueSolutionsViewed,
                                 AdditionalServicesViewed,
                                 AssociatedServicesViewed,
                                 FundingSourceOnlyGMS,
                                 CommencementDate,
                                 Created,
                                 Completed,
                                 LastUpdated,
                                 LastUpdatedBy,
                                 LastUpdatedByName,
                                 IsDeleted
                                 )
                                VALUES
                                (@OrderId,
                                 @Description,
                                 @OrganisationId,
                                 @OrganisationName
                                ,@OrganisationOdsCode
                                ,@OrganisationAddressId
                                ,@OrganisationBillingAddressId
                                ,@OrganisationContactId,
                                 @OrderStatusId,
                                 @ServiceRecipientsViewed,
                                 @SupplierId
                                ,@SupplierName
                                ,@SupplierAddressId
                                ,@SupplierContactId,
                                 @CatalogueSolutionsViewed,
                                 @AdditionalServicesViewed,
                                 @AssociatedServicesViewed,
                                 @FundingSourceOnlyGMS,
                                 @CommencementDate,
                                 @Created,
                                 @DateCompleted,
                                 @LastUpdated,
                                 @LastUpdatedBy,
                                 @LastUpdatedByName,
                                 @IsDeleted
                        )";
            SqlExecutor.Execute<Order>(connectionString, query, this);
        }

        public void Update(string connectionString)
        {
            const string query = @"UPDATE [dbo].[Order]
                        SET
                            OrderId=@orderId,
                            Description=@description,
                            OrganisationId=@organisationId,
                            OrganisationName=@OrganisationName
                           ,OrganisationOdsCode=@OrganisationOdsCode
                           ,OrganisationAddressId=@OrganisationAddressId
                           ,OrganisationBillingAddressId=@OrganisationBillingAddressId
                           ,OrganisationContactId=@OrganisationContactId,
                            OrderStatusId=@orderStatusId,
                            ServiceRecipientsViewed=@ServiceRecipientsViewed,
                            SupplierId=@SupplierId
                           ,SupplierName=@SupplierName
                           ,SupplierAddressId=@SupplierAddressId
                           ,SupplierContactId=@SupplierContactId,
                            CatalogueSolutionsViewed=@CatalogueSolutionsViewed,
                            AdditionalServicesViewed=@AdditionalServicesViewed,
                            AssociatedServicesViewed=@AssociatedServicesViewed,
                            FundingSourceOnlyGMS=@FundingSourceOnlyGMS,
                            CommencementDate=@CommencementDate,
                            Completed=@DateCompleted,
                            Created=@created,
                            LastUpdated=@lastUpdated,
                            LastUpdatedBy=@lastUpdatedBy,
                            LastUpdatedByName=@lastUpdatedByName,
                            IsDeleted=@IsDeleted
                        WHERE OrderId=@orderId";
            SqlExecutor.Execute<Order>(connectionString, query, this);
        }

        public void Delete(string connectionString)
        {
            var query = @"DELETE FROM [dbo].[Order] WHERE OrderId=@orderId";
            SqlExecutor.Execute<Order>(connectionString, query, this);
        }

        private static string GenerateRandomCallOffId()
        {
            var randomNum = new Faker().Random.Number(max: 99999).ToString("D5");
            return string.Format("C9{0}-01", randomNum);
        }
    }
}
