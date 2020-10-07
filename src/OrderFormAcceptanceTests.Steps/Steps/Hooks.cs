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
        }

        [AfterScenario]
        public void AfterScenario()
        {
            var test = _objectContainer.Resolve<UITest>();
            if (_context.ContainsKey(ContextKeys.CreatedOrder))
            {
                var order = (Order)_context[ContextKeys.CreatedOrder];
                new OrderItem().DeleteAllOrderItemsForOrderId(test.OrdapiConnectionString, order.OrderId);

                new ServiceRecipient().DeleteAllServiceRecipientsForOrderId(test.OrdapiConnectionString, order.OrderId);

                ((Order)_context[ContextKeys.CreatedOrder]).Delete(test.OrdapiConnectionString);
            }
            if (_context.ContainsKey(ContextKeys.CreatedContact))
            {
                ((Contact)_context[ContextKeys.CreatedContact]).Delete(test.OrdapiConnectionString);
            }
            if (_context.ContainsKey(ContextKeys.CreatedSupplierContact))
            {
                ((Contact)_context[ContextKeys.CreatedSupplierContact]).Delete(test.OrdapiConnectionString);
            }
            if (_context.ContainsKey(ContextKeys.CreatedAddress))
            {
                ((Address)_context[ContextKeys.CreatedAddress]).Delete(test.OrdapiConnectionString);
            }
            if (_context.ContainsKey(ContextKeys.CreatedSupplierAddress))
            {
                ((Address)_context[ContextKeys.CreatedSupplierAddress]).Delete(test.OrdapiConnectionString);
            }
            if (_context.ContainsKey(ContextKeys.User))
            {
                ((User)_context[ContextKeys.User]).Delete(test.IsapiConnectionString);
            }

            test.Driver?.Quit();
        }
    }
}
