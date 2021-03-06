﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderFormAcceptanceTests.Domain;

namespace OrderFormAcceptanceTests.Persistence.Data
{
    public sealed class ServiceInstanceItemEntityTypeConfiguration : IEntityTypeConfiguration<ServiceInstanceItem>
    {
        public void Configure(EntityTypeBuilder<ServiceInstanceItem> builder)
        {
            if (builder is null)
                throw new ArgumentNullException(nameof(builder));

            builder.ToView(nameof(ServiceInstanceItem));
            builder.HasKey(orderItem => orderItem.OrderItemId);
        }
    }
}
