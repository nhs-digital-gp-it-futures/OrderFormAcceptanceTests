namespace OrderFormAcceptanceTests.TestData.Helpers
{
    using System;
    using System.Threading.Tasks;
    using Bogus;
    using Microsoft.AspNetCore.Identity;
    using OrderFormAcceptanceTests.Domain.Users;
    using OrderFormAcceptanceTests.TestData.Utils;

    public class UsersHelper
    {
        public static string GenericTestPassword()
        {
            return "BuyingC@t4logue";
        }

        public static User GenerateRandomUser(Guid primaryOrganisationId, User user)
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
                PasswordHash = new PasswordHasher<User>().HashPassword(user, GenericTestPassword()),
                SecurityStamp = faker.Random.Hash(),
                ConcurrencyStamp = faker.Random.Guid(),
                PhoneNumber = faker.Phone.PhoneNumber(),
                PhoneNumberConfirmed = 0,
                TwoFactorEnabled = 0,
                LockoutEnd = null,
                LockoutEnabled = 1,
                AccessFailedCount = 0,
                PrimaryOrganisationId = primaryOrganisationId,
                OrganisationFunction = user.UserType.ToString(),
                Disabled = 0,
                CatalogueAgreementSigned = 0,
                FirstName = faker.Name.FirstName(),
                LastName = faker.Name.LastName(),
            };
        }

        public static async Task Create(string connectionString, User user)
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

            await SqlExecutor.ExecuteAsync<User>(connectionString, query, user);
        }

        public static async Task Delete(string connectionString, User user)
        {
            var query = @"DELETE FROM dbo.AspNetUsers WHERE UserName = @userName;";

            await SqlExecutor.ExecuteAsync<User>(connectionString, query, user);
        }
    }
}
