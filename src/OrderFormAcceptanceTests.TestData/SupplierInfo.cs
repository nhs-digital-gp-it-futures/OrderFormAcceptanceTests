using OrderFormAcceptanceTests.TestData.Utils;
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
	}
}
