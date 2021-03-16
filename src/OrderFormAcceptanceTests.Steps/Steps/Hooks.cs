namespace OrderFormAcceptanceTests.Steps.Steps
{
    using System.Threading.Tasks;
    using BoDi;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using OrderFormAcceptanceTests.Domain;
    using OrderFormAcceptanceTests.Domain.Users;
    using OrderFormAcceptanceTests.Persistence.Data;
    using OrderFormAcceptanceTests.Steps.Utils;
    using OrderFormAcceptanceTests.TestData.Helpers;
    using TechTalk.SpecFlow;

    [Binding]
    public sealed class Hooks
    {
        private readonly ScenarioContext context;
        private readonly IObjectContainer objectContainer;

        public Hooks(ScenarioContext context, IObjectContainer objectContainer)
        {
            this.context = context;
            this.objectContainer = objectContainer;
        }

        [BeforeScenario(Order = 0)]
        public async Task BeforeScenario()
        {
            var configurationBuilder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();

            objectContainer.RegisterInstanceAs<IConfiguration>(configurationBuilder);
            var test = objectContainer.Resolve<UITest>();

            DbContextOptions<OrderingDbContext> options = new DbContextOptionsBuilder<OrderingDbContext>()
                .UseSqlServer(test.OrdapiConnectionString, s =>
                {
                    s.EnableRetryOnFailure(5);
                })
                .Options;

            OrderingDbContext dbContext = new(options);

            context.Add(ContextKeys.DbContext, dbContext);

            test.GoToUrl();
            await new CommonSteps(test, context).GivenThatABuyerUserHasLoggedIn();
        }

        [AfterScenario]
        public async Task AfterScenario()
        {
            var test = objectContainer.Resolve<UITest>();
            test.Driver?.Quit();

            if (context.ContainsKey(ContextKeys.CreatedOrder))
            {
                var order = (Order)context[ContextKeys.CreatedOrder];

                var dbContext = (OrderingDbContext)context[ContextKeys.DbContext];

                var orderFromDb = dbContext.Find<Order>(order.Id);

                dbContext.Order.Remove(orderFromDb);
                await dbContext.SaveChangesAsync();
            }

            if (context.ContainsKey(ContextKeys.User))
            {
                var user = (User)context[ContextKeys.User];
                await UsersHelper.Delete(test.IsapiConnectionString, user);
            }
        }
    }
}
