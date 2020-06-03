using FluentAssertions;
using OrderFormAcceptanceTests.Steps.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using TechTalk.SpecFlow;

namespace OrderFormAcceptanceTests.Steps.Steps
{
    [Binding]
    public sealed class ServiceRecipents : TestBase
    {
        public ServiceRecipents(UITest test, ScenarioContext context) : base(test, context)
        {
        }

        [Then(@"the user is able to manage the Service Recipients section")]
        public void ThenTheUserIsAbleToManageTheServiceRecipientsSection()
        {
            Test.Pages.OrderForm.ClickEditServiceRecipients();
            Test.Pages.OrderForm.TaskListDisplayed().Should().BeTrue();
            //TODO: enable below
            //Test.Pages.OrderForm.EditNamedSectionPageDisplayed("service recipients").Should().BeTrue();
        }

    }
}
