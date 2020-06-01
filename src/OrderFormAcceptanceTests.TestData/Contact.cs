using Bogus;
using FluentAssertions;
using OrderFormAcceptanceTests.TestData.Utils;
using System.Linq;

namespace OrderFormAcceptanceTests.TestData
{
    public sealed class Contact
    {
        public int? ContactId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public void Equals(Contact contact)
        {
            this.FirstName.Should().BeEquivalentTo(contact.FirstName);
            this.LastName.Should().BeEquivalentTo(contact.LastName);
            this.Email.Should().BeEquivalentTo(contact.Email);
            this.Phone.Should().BeEquivalentTo(contact.Phone);
        }

        public Contact Generate()
        {
            Faker faker = new Faker();
            return new Contact()
            {
                FirstName = faker.Name.FirstName(),
                LastName = faker.Name.LastName(),
                Email = faker.Internet.Email(),
                Phone = faker.Phone.PhoneNumber()
            };
        }

        public int? Create(string connectionString)
        {

            var query = @"INSERT INTO [dbo].[Contact]
                                (FirstName,
                                 LastName,
                                 Email,
                                 Phone
                                 )
                                VALUES
                                (@FirstName,
                                 @LastName,
                                 @Email,
                                 @Phone
                        );
    
                        SELECT ContactId = SCOPE_IDENTITY()";
            this.ContactId = SqlExecutor.Execute<int>(connectionString, query, this).Single();
            return this.ContactId;
        }

        public Contact Retrieve(string connectionString)
        {
            var query = "SELECT * from [dbo].[Contact] WHERE ContactId=@ContactId";
            return SqlExecutor.Execute<Contact>(connectionString, query, this).Single();
        }

        public void Update(string connectionString)
        {
            var query = @"UPDATE [dbo].[Contact] 
                        SET FirstName=@FirstName,
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
