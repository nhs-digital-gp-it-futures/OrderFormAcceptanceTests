namespace OrderFormAcceptanceTests.TestData.Helpers
{
    using System.Threading.Tasks;
    using OrderFormAcceptanceTests.Domain;
    using OrderFormAcceptanceTests.Persistence.Data;

    public static class OrderProgressHelper
    {
        public static async Task SetProgress(
            OrderingDbContext context,
            Order order,
            bool catalogueSolutionsViewed,
            bool additionalServicesViewed,
            bool associatedServicesViewed)
        {
            order.Progress.CatalogueSolutionsViewed = catalogueSolutionsViewed;
            order.Progress.AdditionalServicesViewed = additionalServicesViewed;
            order.Progress.AssociatedServicesViewed = associatedServicesViewed;

            context.Update(order);
            await context.SaveChangesAsync();
        }
    }
}
