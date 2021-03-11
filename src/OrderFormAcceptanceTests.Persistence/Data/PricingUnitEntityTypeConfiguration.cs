using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderFormAcceptanceTests.Domain;

namespace OrderFormAcceptanceTests.Persistence.Data
{
    internal sealed class PricingUnitEntityTypeConfiguration : IEntityTypeConfiguration<PricingUnit>
    {
        public void Configure(EntityTypeBuilder<PricingUnit> builder)
        {
            builder.ToTable("PricingUnit");
            builder.HasKey(u => u.Name);
            builder.Property(u => u.Name).HasMaxLength(20);
            builder.Property(u => u.Description)
                .IsRequired()
                .HasMaxLength(40);
        }
    }
}
