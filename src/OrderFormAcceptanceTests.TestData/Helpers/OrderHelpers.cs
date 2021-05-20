namespace OrderFormAcceptanceTests.TestData.Helpers
{
    using System;
    using System.Linq;
    using System.Text.Json;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using OrderFormAcceptanceTests.Domain;
    using OrderFormAcceptanceTests.Domain.Users;
    using OrderFormAcceptanceTests.Persistence.Data;
    using OrderFormAcceptanceTests.TestData.Builders;
    using OrderFormAcceptanceTests.TestData.Models;

    public static class OrderHelpers
    {
        public static async Task<Order> GetFullOrderAsync(CallOffId callOffId, OrderingDbContext context)
        {
            var fullOrder = await context.Order
                .Where(o => o.Id == callOffId.Id)
                    .Include(o => o.OrderingParty).ThenInclude(p => p.Address)
                    .Include(o => o.OrderingPartyContact)
                    .Include(o => o.Supplier).ThenInclude(s => s.Address)
                    .Include(o => o.SupplierContact)
                    .Include(o => o.OrderItems).ThenInclude(i => i.CatalogueItem)
                    .Include(o => o.OrderItems).ThenInclude(i => i.OrderItemRecipients).ThenInclude(r => r.Recipient)
                    .Include(o => o.OrderItems).ThenInclude(i => i.PricingUnit)
                    .Include(o => o.Progress)
                    .AsNoTracking()
                    .SingleOrDefaultAsync();

            return fullOrder;
        }

        public static async Task<Order> CreateOrderAsync(CreateOrderModel model, OrderingDbContext dbContext, User user, string isapiConnectionString)
        {
            var organisationDetails = await Organisation.GetOrganisationById(isapiConnectionString, model.OrganisationId);
            var orderingParty = await dbContext.OrderingParty.FindAsync(model.OrganisationId)
                ?? new OrderingParty
                {
                    Id = model.OrganisationId,
                    OdsCode = organisationDetails.OdsCode,
                    Address = AddressHelper.Generate(),
                };

            var order = new OrderBuilder(model.Description, user, orderingParty)
                .Build();

            dbContext.Add(order);
            await dbContext.SaveChangesAsync();

            return order;
        }

        public static async Task<Order> GetFullOrderTrackedAsync(CallOffId callOffId, OrderingDbContext context)
        {
            var fullOrder = await context.Order
                .Where(o => o.Id == callOffId.Id)
                    .Include(o => o.OrderingParty).ThenInclude(p => p.Address)
                    .Include(o => o.OrderingPartyContact)
                    .Include(o => o.Supplier).ThenInclude(s => s.Address)
                    .Include(o => o.SupplierContact)
                    .Include(o => o.OrderItems).ThenInclude(i => i.CatalogueItem)
                    .Include(o => o.OrderItems).ThenInclude(i => i.OrderItemRecipients).ThenInclude(r => r.Recipient)
                    .Include(o => o.OrderItems).ThenInclude(i => i.PricingUnit)
                    .Include(o => o.Progress)
                    .SingleOrDefaultAsync();

            return fullOrder;
        }
    }
}
