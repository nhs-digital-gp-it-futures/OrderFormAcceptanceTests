namespace OrderFormAcceptanceTests.TestData
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using OrderFormAcceptanceTests.Domain;
    using OrderFormAcceptanceTests.TestData.Models;
    using OrderFormAcceptanceTests.TestData.Utils;

    public static class SupplierInfo
    {
        public static IEnumerable<SupplierDetails> SuppliersWithCatalogueSolution(string connectionString, Domain.ProvisioningType provisioningType)
        {
            return SupplierLookup(connectionString, CatalogueItemType.Solution, provisioningType);
        }

        public static IEnumerable<SupplierDetails> SuppliersWithout(string connectionString, CatalogueItemType catalogueItemType)
        {
            var query = @"SELECT DISTINCT ci.[SupplierId], su.[Name]
                            FROM CatalogueItem ci
                            INNER JOIN Supplier su On ci.SupplierId=su.Id
                            WHERE SupplierId NOT IN(
	                            SELECT DISTINCT SupplierId
	                            FROM CatalogueItem    
	                            WHERE CatalogueItemTypeId = @catalogueItemType
                            )";

            return SqlExecutor.Execute<SupplierDetails>(connectionString, query, new { catalogueItemType = (int)catalogueItemType });
        }

        public static async Task<IEnumerable<CatalogueItemModel>> GetPublishedCatalogueItems(string connectionString, string supplierId, CatalogueItemType itemType)
        {
            var query = $@"SELECT *, CatalogueItemTypeId AS 'CatalogueItemType' FROM dbo.CatalogueItem WHERE SupplierId = @supplierId AND PublishedStatusId = 3 AND CatalogueItemTypeId = @itemType;";

            return await SqlExecutor.ExecuteAsync<CatalogueItemModel>(connectionString, query, new { supplierId, itemType });
        }

        public static async Task<SupplierDetails> GetSupplierWithId(string supplierId, string connectionString)
        {
            var query = "SELECT [Id] as 'SupplierId', [Name], [Address] FROM Supplier WHERE Id = @supplierId";

            var result = await SqlExecutor.ExecuteAsync<SupplierDetails>(connectionString, query, new { supplierId });

            return result.Single();
        }

        public static async Task<bool> GetSupplierWithContactDetails(string connectionString, string supplierName)
        {
            var query = @"SELECT Id
                            FROM SupplierContact
                            WHERE SupplierId = (SELECT Id FROM Supplier AS s WHERE s.[Name] = @supplierName);";

            var result = (await SqlExecutor.ExecuteAsync<Guid?>(connectionString, query, new { supplierName })).SingleOrDefault();

            if (result is null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private static IEnumerable<SupplierDetails> SupplierLookup(string connectionString, CatalogueItemType catalogueItemType, Domain.ProvisioningType provisioningType)
        {
            var query = @"SELECT ci.[SupplierId], su.[Name], su.Address    
                            FROM [dbo].[CatalogueItem] ci
                            INNER JOIN CataloguePrice pr ON ci.CatalogueItemId=pr.CatalogueItemId
                            INNER JOIN Supplier su On ci.SupplierId=su.Id
                            WHERE ci.CatalogueItemTypeId=@catalogueItemType
                            AND pr.ProvisioningTypeId=@provisioningType";

            return SqlExecutor.Execute<SupplierDetails>(connectionString, query, new { catalogueItemType = (int)catalogueItemType, provisioningType = (int)provisioningType });
        }
    }
}
