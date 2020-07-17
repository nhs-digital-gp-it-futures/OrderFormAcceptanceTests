using FluentAssertions;
using OrderFormAcceptanceTests.Steps.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using TechTalk.SpecFlow;

namespace OrderFormAcceptanceTests.Steps.Steps
{
    [Binding]
    class AssociatedServices : TestBase
    {
        public AssociatedServices(UITest test, ScenarioContext context) : base(test, context)
        {

        }

        [Then(@"the User is able to manage the Associated Services section")]
        public void ThenTheUserIsAbleToManageTheAssociatedServicesSection()
        {
            Test.Pages.OrderForm.ClickEditAssociatedServices();
            Test.Pages.OrderForm.EditNamedSectionPageDisplayed("Associated Services").Should().BeTrue();
        }

        [Given(@"an Associated Service is added to the order")]
        public void GivenAnAssociatedServiceIsAddedToTheOrder()
        {
            //dev work to update data model needs completing
            Context.Pending();
        }

    }
}
