using FluentAssertions;
using OrderFormAcceptanceTests.Steps.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;

namespace OrderFormAcceptanceTests.Steps.Steps
{
    [Binding]
    public sealed class SupplierInformation : TestBase
    {
        public SupplierInformation(UITest test, ScenarioContext context) : base(test, context)
        {
        }

        [Then(@"the user is able to manage the Supplier section")]
        public void ThenTheUserIsAbleToManageTheSupplierSection()
        {
            Test.Pages.OrderForm.ClickEditSupplier();
            Test.Pages.OrderForm.EditNamedSectionPageDisplayed("supplier information").Should().BeTrue();
        }


    }
}
