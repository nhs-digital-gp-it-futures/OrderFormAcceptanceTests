namespace OrderFormAcceptanceTests.Steps.Steps
{
    using System;
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

            var dbContext = GetDbContext(test.OrdapiConnectionString);

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
                var orderCallOffId = context.Get<Order>(ContextKeys.CreatedOrder).CallOffId;

                var dbContext = GetDbContext(test.OrdapiConnectionString);

                if (await dbContext.Order.SingleOrDefaultAsync(o => o.CallOffId == orderCallOffId) is not null)
                {
                    dbContext.Remove(await dbContext.Order.SingleAsync(s => s.CallOffId == orderCallOffId));
                    await dbContext.SaveChangesAsync();
                }
            }

            if (context.ContainsKey(ContextKeys.User))
            {
                var user = (User)context[ContextKeys.User];
                await UsersHelper.Delete(test.IsapiConnectionString, user);
            }
        }

        private static OrderingDbContext GetDbContext(string connectionString)
        {
            DbContextOptions<OrderingDbContext> options = new DbContextOptionsBuilder<OrderingDbContext>()
                .UseSqlServer(connectionString, s =>
                {
                    s.EnableRetryOnFailure(5);
                })
                .Options;

            OrderingDbContext dbContext = new(options);

            return dbContext;
        }
    }
}
