using System.Collections;
using System.Collections.Generic;
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
            test.Driver?.Quit();

            if (_context.ContainsKey(ContextKeys.CreatedOrder))
            {
                var order = ((Order)_context[ContextKeys.CreatedOrder]).Retrieve(test.OrdapiConnectionString);

                new OrderItem().DeleteAllOrderItemsForOrderId(test.OrdapiConnectionString, order.OrderId);

                new ServiceRecipient().DeleteAllServiceRecipientsForOrderId(test.OrdapiConnectionString, order.OrderId);

                order.Delete(test.OrdapiConnectionString);

                if (order.OrganisationContactId.HasValue || order.SupplierContactId.HasValue)
                {
                    var contacts = order.GetContactIdsForOrder(test.OrdapiConnectionString);
                    foreach (var contact in contacts)
                    {
                        new Contact { ContactId = contact }.Delete(test.OrdapiConnectionString);
                    }
                }

                if(order.OrganisationAddressId.HasValue)
                {
                    new Address { AddressId = order.OrganisationAddressId.Value }.Delete(test.OrdapiConnectionString);
                }
                if (order.SupplierAddressId.HasValue)
                {
                    new Address { AddressId = order.SupplierAddressId.Value }.Delete(test.OrdapiConnectionString);
                }
            }
            if (_context.ContainsKey(ContextKeys.User))
            {
                ((User)_context[ContextKeys.User]).Delete(test.IsapiConnectionString);
            }
        }
    }
}
