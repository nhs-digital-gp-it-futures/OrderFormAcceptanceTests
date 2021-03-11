using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderFormAcceptanceTests.Domain;

namespace OrderFormAcceptanceTests.Persistence.Data
{
    internal sealed class SupplierEntityTypeConfiguration : IEntityTypeConfiguration<Supplier>
    {
        public void Configure(EntityTypeBuilder<Supplier> builder)
        {
            builder.ToTable("Supplier");
            builder.Property(s => s.Id).HasMaxLength(6);
            builder.Property(s => s.Name)
                .IsRequired()
                .HasMaxLength(256);

            builder.HasOne(s => s.Address)
                .WithMany()
                .HasForeignKey(d => d.AddressId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Supplier_Address");
        }
    }
}
