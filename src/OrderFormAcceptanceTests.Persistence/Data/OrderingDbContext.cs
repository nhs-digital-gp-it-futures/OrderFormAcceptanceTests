using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NHSD.BuyingCatalogue.Ordering.Persistence.Data;
using OrderFormAcceptanceTests.Domain;

namespace OrderFormAcceptanceTests.Persistence.Data
{
    public sealed class OrderingDbContext : DbContext
    {

        public OrderingDbContext(
            DbContextOptions<OrderingDbContext> options)
            : base(options)
        {
        }

        public DbSet<Order> Order { get; set; }

        public DbSet<OrderingParty> OrderingParty { get; set; }

        public DbSet<ServiceRecipient> ServiceRecipient { get; set; }

        public DbSet<DefaultDeliveryDate> DefaultDeliveryDate { get; set; }

        public DbSet<Supplier> Supplier { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (modelBuilder is null)
            {
                throw new ArgumentNullException(nameof(modelBuilder));
            }

            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new AddressEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new CatalogueItemEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ContactEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new DefaultDeliveryDateEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new OrderEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new OrderingPartyEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new OrderItemEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new OrderItemRecipientEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new OrderProgressEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new PricingUnitEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ServiceInstanceItemEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new SelectedServiceRecipientEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ServiceRecipientEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new SupplierEntityTypeConfiguration());
        }
    }
}
