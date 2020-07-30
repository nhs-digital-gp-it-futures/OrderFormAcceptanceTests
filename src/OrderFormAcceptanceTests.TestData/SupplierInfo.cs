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
              FROM[buyingcatalogue].[dbo].[SupplierContact]
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

        public static IEnumerable<string> SuppliersWithAdditionalServices(string connectionString, ProvisioningType provisioningType)
        {
            return SupplierLookup(connectionString, CatalogueItemType.AdditionalService, provisioningType);
        }

        public static IEnumerable<string> SuppliersWithAssociatedServices(string connectionString, ProvisioningType provisioningType)
        {
            return SupplierLookup(connectionString, CatalogueItemType.AssociatedService, provisioningType);
        }

        private static IEnumerable<string> SupplierLookup(string connectionString, CatalogueItemType catalogueItemType, ProvisioningType provisioningType)
        {
            var query = @"SELECT ci.[CatalogueItemId]      
                            FROM [buyingcatalogue].[dbo].[CatalogueItem] as ci
                            INNER JOIN CataloguePrice as pr ON ci.CatalogueItemId=pr.CatalogueItemId
                            WHERE ci.CatalogueItemTypeId=@catalogueItemType
                            AND pr.ProvisioningTypeId=@provisioningType";
            
            return SqlExecutor.Execute<string>(connectionString, query, new { catalogueItemType = (int)catalogueItemType, provisioningType = (int)provisioningType });
        }
    }
}
