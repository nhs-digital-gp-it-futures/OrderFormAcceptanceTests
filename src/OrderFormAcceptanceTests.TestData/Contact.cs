namespace OrderFormAcceptanceTests.TestData
{
    using System.Linq;
    using Bogus;
    using FluentAssertions;
    using OrderFormAcceptanceTests.TestData.Utils;

    public sealed class Contact
    {
        public int? ContactId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public static Contact Generate()
        {
            Faker faker = new();
            return new Contact()
            {
                FirstName = faker.Name.FirstName(),
                LastName = faker.Name.LastName(),
                Email = faker.Internet.Email(),
                Phone = faker.Phone.PhoneNumber(),
            };
        }

        public static void DeleteOrphanedContacts(string connectionString)
        {
            var query = @"DELETE FROM dbo.[Contact] 
                        WHERE ContactId NOT IN (
	                        (SELECT SupplierContactId FROM dbo.[Order]) 
	                        UNION 
	                        (SELECT OrganisationContactId FROM dbo.[Order])
                        );";
            SqlExecutor.Execute<Contact>(connectionString, query, null);
        }

        public void Equals(Contact contact)
        {
            FirstName.Should().BeEquivalentTo(contact.FirstName);
            LastName.Should().BeEquivalentTo(contact.LastName);
            Email.Should().BeEquivalentTo(contact.Email);
            Phone.Should().BeEquivalentTo(contact.Phone);
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
            ContactId = SqlExecutor.Execute<int>(connectionString, query, this).Single();
            return ContactId;
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
