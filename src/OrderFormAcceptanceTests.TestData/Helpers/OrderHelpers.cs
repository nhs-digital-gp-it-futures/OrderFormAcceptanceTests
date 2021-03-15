namespace OrderFormAcceptanceTests.TestData.Helpers
{
    using System.Linq;
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
                    .AsNoTracking()
                    .SingleOrDefaultAsync();

            return fullOrder;
        }

        public static async Task<Order> CreateOrderAsync(CreateOrderModel model, OrderingDbContext dbContext, User user, string bapiConnectionString, string isapiConnectionString)
        {
            var organisationDetails = await Organisation.GetOdsCode(isapiConnectionString, user.PrimaryOrganisationId);

            var orderingParty = await dbContext.OrderingParty.SingleOrDefaultAsync(o => o.OdsCode == organisationDetails.OdsCode)
                ?? new OrderingParty
                {
                    Id = model.OrganisationId,
                    Address = AddressHelper.Generate(),
                    OdsCode = organisationDetails.OdsCode,
                    Name = organisationDetails.Name,
                };

            var order = new OrderBuilder(model.Description, user, orderingParty)
                .Build();

            dbContext.Add(order);
            await dbContext.SaveChangesAsync();

            return order;
        }
    }
}
