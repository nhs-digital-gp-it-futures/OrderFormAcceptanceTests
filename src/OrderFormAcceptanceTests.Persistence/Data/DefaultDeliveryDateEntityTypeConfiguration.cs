using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderFormAcceptanceTests.Domain;

namespace OrderFormAcceptanceTests.Persistence.Data
{
    internal sealed class DefaultDeliveryDateEntityTypeConfiguration : IEntityTypeConfiguration<DefaultDeliveryDate>
    {
        public void Configure(EntityTypeBuilder<DefaultDeliveryDate> builder)
        {
            builder.ToTable("DefaultDeliveryDate");
            builder.HasKey(d => new { d.OrderId, d.CatalogueItemId });
            builder
                .Property(d => d.CatalogueItemId)
                .HasMaxLength(14)
                .HasConversion(id => id.ToString(), id => CatalogueItemId.ParseExact(id));

            builder.Property(d => d.DeliveryDate).HasColumnType("date");

            builder.HasOne<Order>()
                .WithMany(o => o.DefaultDeliveryDates)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK_DefaultDeliveryDate_OrderId");
        }
    }
}
