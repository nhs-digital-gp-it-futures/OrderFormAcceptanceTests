﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace OrderFormAcceptanceTests.Domain
{
    public sealed class OrderItem : IEquatable<OrderItem>
    {
        private readonly List<OrderItemRecipient> recipients = new();

#pragma warning disable IDE0044 // Add read-only modifier

        private DateTime created = DateTime.UtcNow;

        private DateTime lastUpdated = DateTime.UtcNow;

#pragma warning restore IDE0044 // Add read-only modifier

        public int OrderId { get; set; }

        public CatalogueItem CatalogueItem { get; set; }

        public CataloguePriceType CataloguePriceType { get; set; }

        public string CurrencyCode { get; set; }

        public DateTime? DefaultDeliveryDate { get; set; }

        public TimeUnit? EstimationPeriod { get; set; }

        public IReadOnlyList<OrderItemRecipient> OrderItemRecipients => recipients;

        public int? PriceId { get; set; } = 1;

        public decimal? Price { get; set; }

        public PricingUnit PricingUnit { get; set; }

        public TimeUnit? PriceTimeUnit { get; set; }

        public ProvisioningType ProvisioningType { get; set; }

        // Backing field is set by EF Core (allowing property to be read-only)
        // ReSharper disable once ConvertToAutoPropertyWithPrivateSetter
        public DateTime LastUpdated => lastUpdated;

        // Backing field is set by EF Core (allowing property to be read-only)
        // ReSharper disable once ConvertToAutoProperty
        public DateTime Created => created;

        public CostType CostType =>
            CatalogueItem.CatalogueItemType.Equals(CatalogueItemType.AssociatedService) &&
            ProvisioningType.Equals(ProvisioningType.Declarative)
                ? CostType.OneOff
                : CostType.Recurring;

        public void SetRecipients(IEnumerable<OrderItemRecipient> itemRecipients)
        {
            recipients.Clear();
            recipients.AddRange(itemRecipients);
            Updated();
        }

        public decimal CalculateTotalCostPerYear()
        {
            return OrderItemRecipients.Sum(r => r.CalculateTotalCostPerYear(
                Price.GetValueOrDefault(),
                PriceTimeUnit ?? EstimationPeriod));
        }

        public bool Equals(OrderItem other)
        {
            if (other is null)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            return CatalogueItem.Equals(other.CatalogueItem) && OrderId == other.OrderId;
        }

        public override bool Equals(object obj) => Equals(obj as OrderItem);

        public override int GetHashCode()
        {
            return HashCode.Combine(OrderId, CatalogueItem);
        }

        internal void MarkOrderSectionAsViewed(Order order)
        {
            if (order is null)
                throw new ArgumentNullException(nameof(order));

            CatalogueItem.CatalogueItemType.MarkOrderSectionAsViewed(order);
        }

        private void Updated() => lastUpdated = DateTime.UtcNow;
    }
}
