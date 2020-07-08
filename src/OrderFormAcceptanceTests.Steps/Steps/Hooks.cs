﻿using OrderFormAcceptanceTests.Steps.Utils;
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
            if (Context.ContainsKey("CreatedOrder"))
            {
                var order = (Order)Context["CreatedOrder"];
                new OrderItem().DeleteAllOrderItemsForOrderId(Test.ConnectionString, order.OrderId);

                new ServiceRecipient().DeleteAllServiceRecipientsForOrderId(Test.ConnectionString, order.OrderId);

                ((Order)Context["CreatedOrder"]).Delete(Test.ConnectionString);
            }
            if (Context.ContainsKey("CreatedContact"))
            {
                ((Contact)Context["CreatedContact"]).Delete(Test.ConnectionString);
            }
            if (Context.ContainsKey("CreatedSupplierContact"))
            {
                ((Contact)Context["CreatedSupplierContact"]).Delete(Test.ConnectionString);
            }
            if (Context.ContainsKey("CreatedAddress"))
            {
                ((Address)Context["CreatedAddress"]).Delete(Test.ConnectionString);
            }
            if (Context.ContainsKey("CreatedSupplierAddress"))
            {
                ((Address)Context["CreatedSupplierAddress"]).Delete(Test.ConnectionString);
            }
            

            Test.Driver?.Quit();
        }
    }
}
