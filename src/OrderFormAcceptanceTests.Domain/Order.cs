﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace OrderFormAcceptanceTests.Domain
{
    public sealed class Order : IAudited, IEquatable<Order>
    {
        private readonly List<DefaultDeliveryDate> defaultDeliveryDates = new();
        private readonly List<OrderItem> orderItems = new();
        private readonly List<ServiceInstanceItem> serviceInstanceItems = new();

        private DateTime? completed;
        private DateTime lastUpdated;
        private Guid lastUpdatedBy;
        private string lastUpdatedByName;

#pragma warning disable 0649 // Set by EF Core

        [SuppressMessage("Style", "IDE0044:Add read-only modifier", Justification = "Set by EF Core")]
        private int id;

#pragma warning restore 0649
        public int Id => id;

        public byte Revision { get; init; } = 1;

        public CallOffId CallOffId { get; init; }

        public string Description { get; set; }

        public OrderingParty OrderingParty { get; init; }

        public Contact OrderingPartyContact { get; set; }

        public Supplier Supplier { get; set; }

        public Contact SupplierContact { get; set; }

        public DateTime? CommencementDate { get; set; }

        public bool? FundingSourceOnlyGms { get; set; }

        public DateTime Created { get; } = DateTime.UtcNow;

        // Backing field required so that EF Core can set value
        // ReSharper disable once ConvertToAutoPropertyWithPrivateSetter
        public DateTime LastUpdated => lastUpdated;

        // Backing field required so that EF Core can set value
        // ReSharper disable once ConvertToAutoPropertyWithPrivateSetter
        public Guid LastUpdatedBy => lastUpdatedBy;

        // Backing field required so that EF Core can set value
        // ReSharper disable once ConvertToAutoPropertyWithPrivateSetter
        public string LastUpdatedByName => lastUpdatedByName;

        // Backing field is initialized by EF Core (allowing property to be read-only)
        // ReSharper disable once ConvertToAutoPropertyWithPrivateSetter
        public DateTime? Completed => completed;

        public OrderProgress Progress { get; init; } = new();

        public OrderStatus OrderStatus { get; set; } = OrderStatus.Incomplete;

        public bool IsDeleted { get; set; }

        public IReadOnlyList<OrderItem> OrderItems => orderItems.AsReadOnly();

        public IReadOnlyList<DefaultDeliveryDate> DefaultDeliveryDates => defaultDeliveryDates.AsReadOnly();

        public IReadOnlyList<ServiceInstanceItem> ServiceInstanceItems => serviceInstanceItems.AsReadOnly();

        public decimal CalculateCostPerYear(CostType costType)
        {
            return orderItems.Where(i => i.CostType == costType).Sum(i => i.CalculateTotalCostPerYear());
        }

        public decimal CalculateTotalOwnershipCost()
        {
            const int defaultContractLength = 3;

            return CalculateCostPerYear(CostType.OneOff) + (defaultContractLength * CalculateCostPerYear(CostType.Recurring));
        }

        public DeliveryDateResult SetDefaultDeliveryDate(CatalogueItemId catalogueItemId, DateTime date)
        {
            var result = DeliveryDateResult.Updated;

            var existingDate = DefaultDeliveryDates.SingleOrDefault(d => d.CatalogueItemId == catalogueItemId);
            if (existingDate is null)
            {
                existingDate = new DefaultDeliveryDate
                {
                    CatalogueItemId = catalogueItemId,
                    OrderId = Id,
                };

                defaultDeliveryDates.Add(existingDate);
                result = DeliveryDateResult.Added;
            }

            existingDate.DeliveryDate = date;
            return result;
        }

        public void SetLastUpdatedBy(Guid userId, string userName)
        {
            lastUpdatedBy = userId;
            lastUpdatedByName = userName ?? throw new ArgumentNullException(nameof(userName));
            lastUpdated = DateTime.UtcNow;
        }

        public OrderItem AddOrUpdateOrderItem(OrderItem orderItem)
        {
            if (orderItem is null)
                throw new ArgumentNullException(nameof(orderItem));

            var existingItem = orderItems.SingleOrDefault(o => o.Equals(orderItem));
            if (existingItem is null)
            {
                orderItems.Add(orderItem);
                orderItem.MarkOrderSectionAsViewed(this);

                return orderItem;
            }

            existingItem.EstimationPeriod = orderItem.EstimationPeriod;
            existingItem.Price = orderItem.Price;

            return existingItem;
        }

        public bool CanComplete()
        {
            if (!FundingSourceOnlyGms.HasValue)
                return false;

            int catalogueSolutionsCount = OrderItems.Count(o => o.CatalogueItem.CatalogueItemType.Equals(CatalogueItemType.Solution));
            int associatedServicesCount = OrderItems.Count(o => o.CatalogueItem.CatalogueItemType.Equals(CatalogueItemType.AssociatedService));

            var solutionAndAssociatedServices = catalogueSolutionsCount > 0
                && associatedServicesCount > 0;

            var solutionAndNoAssociatedServices = catalogueSolutionsCount > 0
                && associatedServicesCount == 0
                && Progress.AssociatedServicesViewed;

            var noSolutionsAndAssociatedServices = catalogueSolutionsCount == 0
                && Progress.CatalogueSolutionsViewed
                && associatedServicesCount > 0;

            return solutionAndNoAssociatedServices
                || solutionAndAssociatedServices
                || noSolutionsAndAssociatedServices;
        }

        public bool Complete()
        {
            if (!CanComplete())
                return false;

            OrderStatus = OrderStatus.Complete;
            completed = DateTime.UtcNow;

            return true;
        }

        public bool Equals(Order other)
        {
            if (ReferenceEquals(null, other))
                return false;

            return ReferenceEquals(this, other) || Id == other.Id;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Order);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
