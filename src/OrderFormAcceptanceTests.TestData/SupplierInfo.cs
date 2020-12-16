using OrderFormAcceptanceTests.TestData.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OrderFormAcceptanceTests.TestData
{
    public static class SupplierInfo
    {
        public static bool SupplierHasContactInfo(string connectionString, string supplierName)
        {
            var query = @"SELECT TOP (1000) [Id]
                  ,[SupplierId]
                  ,[FirstName]
                  ,[LastName]
                  ,[Email]
                  ,[PhoneNumber]
                  ,[LastUpdated]
                  ,[LastUpdatedBy]
              FROM [dbo].[SupplierContact]
              Where SupplierId = (Select Id FROM Supplier s where s.Name=@supplierName)";

            try
            {
                Contact contact = SqlExecutor.Execute<Contact>(connectionString, query, new { supplierName }).First();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static IEnumerable<SupplierDetails> SuppliersWithCatalogueSolution(string connectionString, ProvisioningType provisioningType)
        {
            return SupplierLookup(connectionString, CatalogueItemType.Solution, provisioningType);
        }

        public static IEnumerable<SupplierDetails> SuppliersWithAdditionalServices(string connectionString, ProvisioningType provisioningType)
        {
            return SupplierLookup(connectionString, CatalogueItemType.AdditionalService, provisioningType);
        }

        public static IEnumerable<SupplierDetails> SuppliersWithAssociatedServices(string connectionString, ProvisioningType provisioningType)
        {
            return SupplierLookup(connectionString, CatalogueItemType.AssociatedService, provisioningType);
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

        public static int SupplierWithSolutionWithOnePrice(string connectionString)
        {
            var query =
                $@"SELECT SupplierId, COUNT(*) FROM CatalogueItem LEFT JOIN CataloguePrice ON CataloguePrice.CatalogueItemId = CatalogueItem.CatalogueItemId WHERE CatalogueItemTypeId = {(int)CatalogueItemType.Solution} GROUP BY SupplierId ORDER BY 2 ASC";

            return SqlExecutor.Execute<int>(connectionString, query, null).FirstOrDefault();
        }

        public static string SupplierName(string connectionString, int supplierId)
        {
            var query =
                $@"SELECT Name FROM Supplier WHERE Id = @supplierId";

            return SqlExecutor.Execute<string>(connectionString, query, new { supplierId }).FirstOrDefault();
        }

        private static IEnumerable<SupplierDetails> SupplierLookup(string connectionString, CatalogueItemType catalogueItemType, ProvisioningType provisioningType)
        {
            var query = @"SELECT ci.[SupplierId], su.[Name]      
                            FROM [dbo].[CatalogueItem] ci
                            INNER JOIN CataloguePrice pr ON ci.CatalogueItemId=pr.CatalogueItemId
                            INNER JOIN Supplier su On ci.SupplierId=su.Id
                            WHERE ci.CatalogueItemTypeId=@catalogueItemType
                            AND pr.ProvisioningTypeId=@provisioningType";

            return SqlExecutor.Execute<SupplierDetails>(connectionString, query, new { catalogueItemType = (int)catalogueItemType, provisioningType = (int)provisioningType });
        }

        public static int SupplierWithMoreThanOneSolution(string connectionString)
        {
            var query =
                $@"SELECT SupplierId, COUNT(*) FROM CatalogueItem WHERE CatalogueItemTypeId = {(int)CatalogueItemType.Solution} GROUP BY SupplierId ORDER BY 2 DESC";

            return SqlExecutor.Execute<int>(connectionString, query, null).FirstOrDefault();
        }

        public static string GetSupplierSolutionNameWithPrice(string connectionString, string supplierId)
        {
            var query =
                $@"SELECT ci.[Name] FROM dbo.CatalogueItem AS ci INNER JOIN CataloguePrice AS cp ON cp.CatalogueItemId = ci.CatalogueItemId WHERE ci.CatalogueItemTypeId = 1 AND ci.PublishedStatusId = 3 AND ci.SupplierId = @supplierId;";

            return SqlExecutor.Execute<string>(connectionString, query, new { supplierId }).FirstOrDefault();
        }

        public static IEnumerable<string> GetPublishedCatalogueItems(string connectionString, string supplierId, CatalogueItemType itemType)
        {
            var query = $@"SELECT Name FROM dbo.CatalogueItem WHERE SupplierId = @supplierId AND PublishedStatusId = 3 AND CatalogueItemTypeId = @itemType;";

            return SqlExecutor.Execute<string>(connectionString, query, new { supplierId, itemType });
        }
    }
}
