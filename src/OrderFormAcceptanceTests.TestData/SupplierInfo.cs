namespace OrderFormAcceptanceTests.TestData
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using OrderFormAcceptanceTests.Domain;
    using OrderFormAcceptanceTests.TestData.Extensions;
    using OrderFormAcceptanceTests.TestData.Models;
    using OrderFormAcceptanceTests.TestData.Utils;

    public static class SupplierInfo
    {
        public static async Task<IEnumerable<SupplierDetails>> SuppliersWithCatalogueSolution(string connectionString, ProvisioningType provisioningType)
        {
            return await SupplierLookup(connectionString, CatalogueItemType.Solution, provisioningType);
        }

        public static async Task<IEnumerable<SupplierDetails>> SuppliersWithout(string connectionString, CatalogueItemType catalogueItemType)
        {
            var query = @"SELECT DISTINCT ci.SupplierId, su.[Name]
                            FROM CatalogueItem AS ci
                            INNER JOIN Supplier AS su On ci.SupplierId=su.Id
                            WHERE SupplierId NOT IN(
	                            SELECT DISTINCT SupplierId
	                            FROM CatalogueItem    
	                            WHERE CatalogueItemTypeId = @catalogueItemType
                            )";

            return await SqlExecutor.ExecuteAsync<SupplierDetails>(connectionString, query, new { catalogueItemType = (int)catalogueItemType });
        }

        public static async Task<string> GetSupplierWithMultipleAssociatedServices(string connectionString)
        {
            var query = @"SELECT TOP (1000) s.Id
                          FROM dbo.AssociatedService AS a
                          INNER JOIN CatalogueItem ci ON ci.CatalogueItemId = a.AssociatedServiceId
                          INNER JOIN Supplier AS s ON s.Id = ci.SupplierId
                          WHERE ci.PublishedStatusId = 3
                          GROUP BY s.Id
                          ORDER BY COUNT(*) DESC;";

            return (await SqlExecutor.ExecuteAsync<string>(connectionString, query, null)).First();
        }

        public static async Task<string> GetSolutionWithMultipleAdditionalServices(string connectionString)
        {
            var query = @"SELECT SolutionId
                        FROM dbo.AdditionalService
                        GROUP BY SolutionId
                        ORDER BY COUNT(*) DESC";

            return (await SqlExecutor.ExecuteAsync<string>(connectionString, query, null)).First();
        }

        public static async Task<IEnumerable<CatalogueItemModel>> GetPublishedCatalogueItemsNoTieredAsync(string connectionString, string supplierId, CatalogueItemType itemType)
        {
            var query = $@"SELECT *,
                            CatalogueItemTypeId AS 'CatalogueItemType'
                            FROM dbo.CatalogueItem AS ci
                            INNER JOIN CataloguePrice AS cp ON cp.CatalogueItemId = ci.CatalogueItemId
                            WHERE ci.SupplierId = @supplierId
                            AND ci.PublishedStatusId = 3
                            AND ci.CatalogueItemTypeId = @itemType
                            AND ci.CatalogueItemId NOT LIKE 'Auto%'
                            AND cp.CataloguePriceTypeId = 1;";

            return await SqlExecutor.ExecuteAsync<CatalogueItemModel>(connectionString, query, new { supplierId, itemType });
        }

        public static async Task<IEnumerable<CatalogueItemModel>> GetAllPublishedItemsOfTypeAsync(string connectionString, string supplierId, CatalogueItemType itemType)
        {
            var query = $@"SELECT *,
                            CatalogueItemTypeId AS 'CatalogueItemType'
                            FROM dbo.CatalogueItem
                            WHERE SupplierId = @supplierId
                            AND PublishedStatusId = 3
                            AND CatalogueItemTypeId = @itemType
                            AND CatalogueItemId NOT LIKE 'Auto%';";

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

            return result is not null;
        }

        public static async Task<Supplier> GetSupplierWithCatalogueItemWithMoreThan1Price(CatalogueItemType catalogueItemType, string bapiConnectionString)
        {
            var query = @"SELECT 
                            ci.SupplierId
                            FROM CatalogueItem AS ci
                            INNER JOIN CataloguePrice AS cp ON ci.CatalogueItemId = cp.CatalogueItemId
                            WHERE ci.CatalogueItemTypeId = @catalogueItemType
                            GROUP BY ci.CatalogueItemId, ci.SupplierId
                            HAVING
                            COUNT(cp.PricingUnitId) > 1;";

            var supplierId = (await SqlExecutor.ExecuteAsync<string>(bapiConnectionString, query, new { catalogueItemType })).Single();

            return (await GetSupplierWithId(supplierId, bapiConnectionString)).ToDomain();
        }

        private static async Task<IEnumerable<SupplierDetails>> SupplierLookup(string connectionString, CatalogueItemType catalogueItemType, ProvisioningType provisioningType)
        {
            var query = @"SELECT ci.SupplierId, su.[Name], su.Address    
                            FROM dbo.CatalogueItem AS ci
                            INNER JOIN CataloguePrice AS pr ON ci.CatalogueItemId=pr.CatalogueItemId
                            INNER JOIN Supplier AS su On ci.SupplierId=su.Id
                            WHERE ci.CatalogueItemTypeId=@catalogueItemType
                            AND pr.ProvisioningTypeId=@provisioningType";

            return await SqlExecutor.ExecuteAsync<SupplierDetails>(connectionString, query, new
            {
                catalogueItemType = (int)catalogueItemType,
                provisioningType = (int)provisioningType,
            });
        }
    }
}
