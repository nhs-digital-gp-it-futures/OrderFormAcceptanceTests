using OrderFormAcceptanceTests.TestData.Utils;
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
            const string query = @"SELECT SupplierId, COUNT(*) FROM CatalogueItem WHERE CatalogueItemTypeId = 1 GROUP BY SupplierId ORDER BY 2 DESC";

            return SqlExecutor.Execute<int>(connectionString, query, null).FirstOrDefault();
        } 
    }
}
