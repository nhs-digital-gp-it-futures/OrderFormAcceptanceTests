﻿namespace OrderFormAcceptanceTests.TestData
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using OrderFormAcceptanceTests.Domain;

    public sealed class OrderItemList
    {
        private readonly Dictionary<int, Domain.OrderItem> cache = new();

        public OrderItemList(params Domain.OrderItem[] orderItems)
        {
            if (orderItems is null)
            {
                throw new ArgumentNullException(nameof(orderItems));
            }

            foreach (var orderItem in orderItems)
            {
                Add(orderItem);
            }
        }

        public IEnumerable<Domain.OrderItem> GetAll() =>
            cache.Values;

        public void Add(Domain.OrderItem orderItem)
        {
            if (orderItem is null)
            {
                throw new ArgumentNullException(nameof(orderItem));
            }

            cache.Add(cache.Count, orderItem);
        }

        public decimal GetTotalOneOffCost() => GetTotalAnnualCost();

        public decimal GetTotalAnnualCost() => GetAll().Sum(orderItem => orderItem.Price.Value);

        public decimal GetTotalMonthlyCost() => GetAll().Sum(orderItem => orderItem.Price.Value) / 12m;
    }
}
