using OrderFormAcceptanceTests.Steps.Utils;
using OrderFormAcceptanceTests.TestData;
using TechTalk.SpecFlow;

namespace OrderFormAcceptanceTests.Steps.Steps
{
    [Binding]
    public sealed class Hooks : TestBase
    {
        public Hooks(UITest test, ScenarioContext context) : base(test, context)
        {
        }

        [AfterScenario]
        public void AfterScenario()
        {
            if (Context.ContainsKey(ContextKeys.CreatedOrder))
            {
                var order = (Order)Context[ContextKeys.CreatedOrder];
                new OrderItem().DeleteAllOrderItemsForOrderId(Test.ConnectionString, order.OrderId);

                new ServiceRecipient().DeleteAllServiceRecipientsForOrderId(Test.ConnectionString, order.OrderId);

                ((Order)Context[ContextKeys.CreatedOrder]).Delete(Test.ConnectionString);
            }
            if (Context.ContainsKey(ContextKeys.CreatedContact))
            {
                ((Contact)Context[ContextKeys.CreatedContact]).Delete(Test.ConnectionString);
            }
            if (Context.ContainsKey(ContextKeys.CreatedSupplierContact))
            {
                ((Contact)Context[ContextKeys.CreatedSupplierContact]).Delete(Test.ConnectionString);
            }
            if (Context.ContainsKey(ContextKeys.CreatedAddress))
            {
                ((Address)Context[ContextKeys.CreatedAddress]).Delete(Test.ConnectionString);
            }
            if (Context.ContainsKey(ContextKeys.CreatedSupplierAddress))
            {
                ((Address)Context[ContextKeys.CreatedSupplierAddress]).Delete(Test.ConnectionString);
            }

            Test.Driver?.Quit();
        }
    }
}
