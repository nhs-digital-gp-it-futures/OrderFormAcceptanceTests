using Bogus;
using FluentAssertions;
using OrderFormAcceptanceTests.TestData.Utils;
using System.Linq;


namespace OrderFormAcceptanceTests.TestData
{
    public sealed class Address
    {
          public int? AddressId { get; set; }
          public string Line1 { get; set; }
          public string Line2 { get; set; }
          public string Line3 { get; set; }
          public string Line4 { get; set; }
          public string Line5 { get; set; }
          public string Town { get; set; }
          public string County { get; set; }
          public string Postcode { get; set; }
          public string Country { get; set; }

        public void Equals(Address address)
        {
            this.Line1 = this.Line1 == "" ? null : this.Line1;
            this.Line2 = this.Line2 == "" ? null : this.Line2;
            this.Line3 = this.Line3 == "" ? null : this.Line3;
            this.Line4 = this.Line4 == "" ? null : this.Line4;
            this.Town = this.Town == "" ? null : this.Town;
            this.County = this.County == "" ? null : this.County;
            this.Postcode = this.Postcode == "" ? null : this.Postcode;
            this.Country = this.Country == "" ? null : this.Country;

            address.Line1 = address.Line1 == "" ? null : address.Line1;
            address.Line2 = address.Line2 == "" ? null : address.Line2;
            address.Line3 = address.Line3 == "" ? null : address.Line3;
            address.Line4 = address.Line4 == "" ? null : address.Line4;
            address.Town = address.Town == "" ? null : address.Town;
            address.County = address.County == "" ? null : address.County;
            address.Postcode = address.Postcode == "" ? null : address.Postcode;
            address.Country = address.Country == "" ? null : address.Country;

            this.Line1.Should().BeEquivalentTo(address.Line1);
            this.Line2.Should().BeEquivalentTo(address.Line2);
            this.Line3.Should().BeEquivalentTo(address.Line3);
            this.Line4.Should().BeEquivalentTo(address.Line4);
            this.Town.Should().BeEquivalentTo(address.Town);
            this.County.Should().BeEquivalentTo(address.County);
            this.Postcode.Should().BeEquivalentTo(address.Postcode);
            this.Country.Should().BeEquivalentTo(address.Country);
        }

        public Address Generate()
        {
            Faker faker = new Faker();
            var randomAddress = faker.Address;
            return new Address()
            {
                Line1 = randomAddress.StreetAddress(),
                Town = randomAddress.City(),
                County = randomAddress.County(),
                Postcode = randomAddress.ZipCode(),
                Country = randomAddress.Country()
            };
        }

        public int? Create(string connectionString)
        {
            var query = @"INSERT INTO [dbo].[Address]
                                (Line1,
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
                                (@Line1,
                                 @Line2,
                                 @Line3,
                                 @Line4,
                                 @Line5,
                                 @Town,
                                 @County,
                                 @Postcode,
                                 @Country
                        );

                        SELECT AddressId = SCOPE_IDENTITY()";
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
