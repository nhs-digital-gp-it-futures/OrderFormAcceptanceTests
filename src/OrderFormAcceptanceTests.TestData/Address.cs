using Bogus;
using OrderFormAcceptanceTests.TestData.Utils;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OrderFormAcceptanceTests.TestData
{
    public sealed class Address
    {
          public int AddressId { get; set; }
          public string Line1 { get; set; }
          public string Line2 { get; set; }
          public string Line3 { get; set; }
          public string Line4 { get; set; }
          public string Line5 { get; set; }
          public string Town { get; set; }
          public string County { get; set; }
          public string Postcode { get; set; }
          public string Country { get; set; }

        public Address Generate()
        {
            Faker faker = new Faker();
            var randomAddress = faker.Address;
            return new Address()
            {
                AddressId = faker.Random.Number(),
                Line1 = randomAddress.StreetAddress(),
                Town = randomAddress.City(),
                County = randomAddress.County(),
                Postcode = randomAddress.ZipCode(),
                Country = randomAddress.Country()
            };
        }

        public void Create(string connectionString)
        {
            var query = @"INSERT INTO [dbo].[Address]
                                (AddressId,
                                 Line1,
                                 Line2,
                                 Line3,
                                 Line4,
                                 Line5,
                                 Town,
                                 County,
                                 Postcode,
                                 Country
                                 )
                                VALUES
                                (@AddressId,
                                 @Line1,
                                 @Line2,
                                 @Line3,
                                 @Line4,
                                 @Line5,
                                 @Town,
                                 @County,
                                 @Postcode,
                                 @Country
                        )";
            SqlExecutor.Execute<Address>(connectionString, query, this);
        }

        public Address Retrieve(string connectionString)
        {
            var query = "SELECT * from [dbo].[Address] WHERE AddressId=@AddressId";
            return SqlExecutor.Execute<Address>(connectionString, query, this).Single();
        }

        public void Update(string connectionString)
        {
            var query = @"UPDATE [dbo].[Address] 
                        SET 
                            AddressId=@AddressId,
                            Line1=@Line1,
                            Line2=@Line2,
                            Line3=@Line3,
                            Line4=@Line4,
                            Line5=@Line5,
                            Town=@Town,
                            County=@County,
                            Postcode=@Postcode,
                            Country=@Country
                        WHERE AddressId=@AddressId";
            SqlExecutor.Execute<Address>(connectionString, query, this);
        }

        public void Delete(string connectionString)
        {
            var query = @"DELETE FROM [dbo].[Address] WHERE AddressId=@AddressId";
            SqlExecutor.Execute<Address>(connectionString, query, this);
        }
    }
}
