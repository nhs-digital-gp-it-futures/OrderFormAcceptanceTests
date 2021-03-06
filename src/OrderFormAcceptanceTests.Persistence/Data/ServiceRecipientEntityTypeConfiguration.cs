﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderFormAcceptanceTests.Domain;

namespace OrderFormAcceptanceTests.Persistence.Data
{
    internal sealed class ServiceRecipientEntityTypeConfiguration : IEntityTypeConfiguration<ServiceRecipient>
    {
        public void Configure(EntityTypeBuilder<ServiceRecipient> builder)
        {
            builder.ToTable("ServiceRecipient");
            builder.HasKey(e => e.OdsCode);
            builder.Property(e => e.OdsCode).HasMaxLength(8);
            builder.Property(e => e.Name).HasMaxLength(256);
        }
    }
}
