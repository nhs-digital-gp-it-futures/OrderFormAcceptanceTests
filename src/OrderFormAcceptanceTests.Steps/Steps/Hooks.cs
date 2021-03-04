namespace OrderFormAcceptanceTests.Steps.Steps
{
    using System.Threading.Tasks;
    using BoDi;
    using Microsoft.Extensions.Configuration;
    using OrderFormAcceptanceTests.Steps.Utils;
    using OrderFormAcceptanceTests.TestData;
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
            test.GoToUrl();
            await new CommonSteps(test, context).GivenThatABuyerUserHasLoggedIn();
        }

        [AfterScenario]
        public void AfterScenario()
        {
            var test = objectContainer.Resolve<UITest>();
            test.Driver?.Quit();

            if (context.ContainsKey(ContextKeys.CreatedOrder))
            {
                var order = ((Order)context[ContextKeys.CreatedOrder]).Retrieve(test.OrdapiConnectionString);

                OrderItem.DeleteAllOrderItemsForOrderId(test.OrdapiConnectionString, order.OrderId);

                ServiceRecipient.DeleteAllServiceRecipientsForOrderId(test.OrdapiConnectionString, order.OrderId);

                order.Delete(test.OrdapiConnectionString);

                new Address() { AddressId = order.SupplierAddressId }.Delete(test.OrdapiConnectionString);
                new Address() { AddressId = order.OrganisationAddressId }.Delete(test.OrdapiConnectionString);
                new Contact() { ContactId = order.OrganisationContactId }.Delete(test.OrdapiConnectionString);
                new Contact() { ContactId = order.SupplierContactId }.Delete(test.OrdapiConnectionString);
            }

            if (context.ContainsKey(ContextKeys.User))
            {
                ((User)context[ContextKeys.User]).Delete(test.IsapiConnectionString);
            }
        }
    }
}
