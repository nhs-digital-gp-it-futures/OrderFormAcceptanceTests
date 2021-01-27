namespace OrderFormAcceptanceTests.TestData
{
    using System.Linq;
    using Bogus;
    using FluentAssertions;
    using OrderFormAcceptanceTests.TestData.Utils;

    public sealed class Address
    {
        public int? AddressId { get; set; }

        public string Line1 { get; set; }

        public string Line2 { get; set; }

        public string Line3 { get; set; }

        public string Line4 { get; set; }

        public string Town { get; set; }

        public string County { get; set; }

        public string Postcode { get; set; }

        public string Country { get; set; }

        public static Address Generate()
        {
            Faker faker = new Faker();
            var randomAddress = faker.Address;
            return new Address()
            {
                Line1 = randomAddress.StreetAddress(),
                Town = randomAddress.City(),
                County = randomAddress.County(),
                Postcode = randomAddress.ZipCode(),
                Country = randomAddress.Country(),
            };
        }

        public static void DeleteOrphanedAddresses(string connectionString)
        {
            var query = @"DELETE FROM dbo.[Address] 
                            WHERE AddressId NOT IN(
                            (SELECT OrganisationAddressId FROM dbo.[Order])
                            UNION
                            (SELECT SupplierAddressId FROM dbo.[Order])
                            );";
            SqlExecutor.Execute<Address>(connectionString, query, null);
        }

        public void Equals(Address address)
        {
            Line1 = Line1 == string.Empty ? null : Line1;
            Line2 = Line2 == string.Empty ? null : Line2;
            Line3 = Line3 == string.Empty ? null : Line3;
            Line4 = Line4 == string.Empty ? null : Line4;
            Town = Town == string.Empty ? null : Town;
            County = County == string.Empty ? null : County;
            Postcode = Postcode == string.Empty ? null : Postcode;
            Country = Country == string.Empty ? null : Country;

            address.Line1 = address.Line1 == string.Empty ? null : address.Line1;
            address.Line2 = address.Line2 == string.Empty ? null : address.Line2;
            address.Line3 = address.Line3 == string.Empty ? null : address.Line3;
            address.Line4 = address.Line4 == string.Empty ? null : address.Line4;
            address.Town = address.Town == string.Empty ? null : address.Town;
            address.County = address.County == string.Empty ? null : address.County;
            address.Postcode = address.Postcode == string.Empty ? null : address.Postcode;
            address.Country = address.Country == string.Empty ? null : address.Country;

            Line1.Should().BeEquivalentTo(address.Line1);
            Line2.Should().BeEquivalentTo(address.Line2);
            Line3.Should().BeEquivalentTo(address.Line3);
            Line4.Should().BeEquivalentTo(address.Line4);
            Town.Should().BeEquivalentTo(address.Town);
            County.Should().BeEquivalentTo(address.County);
            Postcode.Should().BeEquivalentTo(address.Postcode);
            Country.Should().BeEquivalentTo(address.Country);
        }

        public int? Create(string connectionString)
        {
            var query = @"INSERT INTO dbo.[Address]
                                (Line1,
                                 Line2,
                                 Line3,
                                 Line4,
                                 Town,
                                 County,
                                 Postcode,
                                 Country
                                 )
                                VALUES
                                (@Line1,
                                 @Line2,
                                 @Line3,
                                 @Line4,
                                 @Town,
                                 @County,
                                 @Postcode,
                                 @Country
                        );

                        SELECT AddressId FROM dbo.[Address] Where AddressId = SCOPE_IDENTITY();";
            this.AddressId = SqlExecutor.Execute<int>(connectionString, query, this).Single();
            return this.AddressId;
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
                            
                            Line1=@Line1,
                            Line2=@Line2,
                            Line3=@Line3,
                            Line4=@Line4,
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
