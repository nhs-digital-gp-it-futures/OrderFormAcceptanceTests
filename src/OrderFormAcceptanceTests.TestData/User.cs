using System;
using Bogus;
using Microsoft.AspNetCore.Identity;
using OrderFormAcceptanceTests.TestData.Utils;

namespace OrderFormAcceptanceTests.TestData
{
    public class User
    {
        public Guid Id { get; set; } = new Guid();
        public string UserName { get; set; }
        public string NormalizedUserName { get; set; }
        public string Email { get; set; }
        public string NormalizedEmail { get; set; }
        public int EmailConfirmed { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public Guid ConcurrencyStamp { get; set; } = new Guid();
        public string PhoneNumber { get; set; }
        public int PhoneNumberConfirmed { get; set; }
        public int TwoFactorEnabled { get; set; }
        public string LockoutEnd { get; set; }
        public int LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }
        public Guid PrimaryOrganisationId { get; set; }
        public string OrganisationFunction { get; set; }
        public int Disabled { get; set; }
        public int CatalogueAgreementSigned { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public UserType UserType { get; set; }

        public static string GenericTestPassword()
        {
            return "BuyingC@t4logue";
        }

        public User GenerateRandomUser(Guid primaryOrganisationId)
        {
            Faker faker = new Faker();
            var generatedEmail = faker.Internet.Email();
            return new User
            {
                Id = faker.Random.Guid(),
                Email = generatedEmail,
                UserName = generatedEmail,
                NormalizedUserName = generatedEmail.ToUpper(),
                NormalizedEmail = generatedEmail.ToUpper(),
                EmailConfirmed = 1,
                PasswordHash = new PasswordHasher<User>().HashPassword(this, GenericTestPassword()),
                SecurityStamp = faker.Random.Hash(),
                ConcurrencyStamp = faker.Random.Guid(),
                PhoneNumber = faker.Phone.PhoneNumber(),
                PhoneNumberConfirmed = 0,
                TwoFactorEnabled = 0,
                LockoutEnd = null,
                LockoutEnabled = 1,
                AccessFailedCount = 0,
                PrimaryOrganisationId = primaryOrganisationId,
                OrganisationFunction = UserType.ToString(),
                Disabled = 0,
                CatalogueAgreementSigned = 0,
                FirstName = faker.Name.FirstName(),
                LastName = faker.Name.LastName()
            };
        }

        public void Create(string connectionString)
        {
            var query = @"INSERT INTO dbo.AspNetUsers (
                            Id,
                            UserName,
                            NormalizedUserName,
                            Email,
                            NormalizedEmail,
                            EmailConfirmed,
                            PasswordHash,
                            SecurityStamp,
                            ConcurrencyStamp,
                            PhoneNumber,
                            PhoneNumberConfirmed,
                            TwoFactorEnabled,
                            LockoutEnd,
                            LockoutEnabled,
                            AccessFailedCount,
                            PrimaryOrganisationId,
                            OrganisationFunction,
                            Disabled,
                            CatalogueAgreementSigned,
                            FirstName,
                            LastName
                        )
                        VALUES (
                            @id,
                            @userName,
                            @normalizedUserName,
                            @email,
                            @normalizedEmail,
                            @emailConfirmed,
                            @passwordHash,
                            @securityStamp,
                            @concurrencyStamp,
                            @phoneNumber,
                            @phoneNumberConfirmed,
                            @twoFactorEnabled,
                            @lockoutEnd,
                            @lockoutEnabled,
                            @accessFailedCount,
                            @primaryOrganisationId,
                            @organisationFunction,
                            @disabled,
                            @catalogueAgreementSigned,
                            @firstName,
                            @lastName
                        );";

            SqlExecutor.Execute<User>(connectionString, query, this);
        }
        public void Delete(string connectionString)
        {
            var query = @"DELETE FROM dbo.AspNetUsers WHERE UserName = @userName;";

            SqlExecutor.Execute<User>(connectionString, query, this);
        }
    }
}
