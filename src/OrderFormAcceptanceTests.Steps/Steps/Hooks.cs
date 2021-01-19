using BoDi;
using Microsoft.Extensions.Configuration;
using OrderFormAcceptanceTests.Steps.Utils;
using OrderFormAcceptanceTests.TestData;
using TechTalk.SpecFlow;

namespace OrderFormAcceptanceTests.Steps.Steps
{
    [Binding]
    public sealed class Hooks
    {
        private readonly ScenarioContext _context;
        private readonly IObjectContainer _objectContainer;

        public Hooks(ScenarioContext context, IObjectContainer objectContainer)
        {
            _context = context;
            _objectContainer = objectContainer;
        }

        [BeforeScenario(Order = 0)]
        public void BeforeScenario()
        {
            var configurationBuilder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();

            _objectContainer.RegisterInstanceAs<IConfiguration>(configurationBuilder);
            var test = _objectContainer.Resolve<UITest>();
            test.GoToUrl();
            new CommonSteps(test, _context).GivenThatABuyerUserHasLoggedIn();
        }

        [AfterScenario]
        public void AfterScenario()
        {
            var test = _objectContainer.Resolve<UITest>();
            test.Driver?.Quit();

            if (_context.ContainsKey(ContextKeys.CreatedOrder))
            {
                var order = ((Order)_context[ContextKeys.CreatedOrder]).Retrieve(test.OrdapiConnectionString);

                OrderItem.DeleteAllOrderItemsForOrderId(test.OrdapiConnectionString, order.OrderId);

                ServiceRecipient.DeleteAllServiceRecipientsForOrderId(test.OrdapiConnectionString, order.OrderId);

                order.Delete(test.OrdapiConnectionString);

                new Address() { AddressId = order.SupplierAddressId }.Delete(test.OrdapiConnectionString);
                new Address() { AddressId = order.OrganisationAddressId }.Delete(test.OrdapiConnectionString);
                new Address() { AddressId = order.OrganisationBillingAddressId }.Delete(test.OrdapiConnectionString);
                new Contact() { ContactId = order.OrganisationContactId }.Delete(test.OrdapiConnectionString);
                new Contact() { ContactId = order.SupplierContactId }.Delete(test.OrdapiConnectionString);
            }

            if (_context.ContainsKey(ContextKeys.User))
            {
                ((User)_context[ContextKeys.User]).Delete(test.IsapiConnectionString);
            }
        }
    }
}
