using System;
using System.Collections.Generic;
using System.Linq;

namespace OrderFormAcceptanceTests.TestData
{
    public sealed class OrderItemList
    {
        private readonly Dictionary<int, OrderItem> _cache = new Dictionary<int, OrderItem>();

        public OrderItemList(params OrderItem[] orderItems)
        {
            if (orderItems is null)
                throw new ArgumentNullException(nameof(orderItems));

            foreach (var orderItem in orderItems)
            {
                Add(orderItem);
            }
        }

        public IEnumerable<OrderItem> GetAll() =>
            _cache.Values;

        public void Add(OrderItem orderItem)
        {
            if (orderItem is null)
                throw new ArgumentNullException(nameof(orderItem));

            _cache.Add(orderItem.OrderItemId, orderItem);
        }

        public decimal GetTotalOneOffCost() => GetTotalAnnualCost();

        public decimal GetTotalAnnualCost() => GetAll().Sum(orderItem => orderItem.CalculateItemCost());

        public decimal GetTotalMonthlyCost() => GetAll().Sum(orderItem => orderItem.CalculateItemCost()) / 12m;
    }
}
