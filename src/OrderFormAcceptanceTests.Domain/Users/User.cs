namespace OrderFormAcceptanceTests.Domain.Users
{
    using System;

    public class User
    {
        public Guid Id { get; set; } = default;

        public string UserName { get; set; }

        public string NormalizedUserName { get; set; }

        public string Email { get; set; }

        public string NormalizedEmail { get; set; }

        public int EmailConfirmed { get; set; }

        public string PasswordHash { get; set; }

        public string SecurityStamp { get; set; }

        public Guid ConcurrencyStamp { get; set; } = default;

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
    }
}
