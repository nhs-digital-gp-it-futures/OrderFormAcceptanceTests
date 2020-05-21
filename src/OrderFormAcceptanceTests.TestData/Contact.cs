using Bogus;
using OrderFormAcceptanceTests.TestData.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OrderFormAcceptanceTests.TestData
{
    public sealed class Contact
    {
        public int ContactId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public Contact Generate()
        {
            Faker faker = new Faker();
            return new Contact()
            {
                ContactId = faker.Random.Number(),
                FirstName = faker.Name.FirstName(),
                LastName = faker.Name.LastName(),
                Email = faker.Internet.Email(),
                Phone = faker.Phone.PhoneNumber()
            };
        }

        public void Create(string connectionString)
        {
            var query = @"INSERT INTO [dbo].[Contact]
                                (ContactId,
                                 FirstName,
                                 LastName,
                                 Email,
                                 Phone
                                 )
                                VALUES
                                (@ContactId,
                                 @FirstName,
                                 @LastName,
                                 @Email,
                                 @Phone
                        )";
            SqlExecutor.Execute<Contact>(connectionString, query, this);
        }

        public Contact Retrieve(string connectionString)
        {
            var query = "SELECT * from [dbo].[Contact] WHERE ContactId=@ContactId";
            return SqlExecutor.Execute<Contact>(connectionString, query, this).Single();
        }

        public void Update(string connectionString)
        {
            var query = @"UPDATE [dbo].[Contact] 
                        SET 
                            ContactId=@ContactId,
                            FirstName=@FirstName,
                            LastName=@LastName,
                            Email=@Email,
                            Phone=@Phone
                        WHERE ContactId=@ContactId";
            SqlExecutor.Execute<Contact>(connectionString, query, this);
        }

        public void Delete(string connectionString)
        {
            var query = @"DELETE FROM [dbo].[Contact] WHERE ContactId=@ContactId";
            SqlExecutor.Execute<Contact>(connectionString, query, this);
        }
    }
}
