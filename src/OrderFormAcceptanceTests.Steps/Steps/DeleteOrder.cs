using Bogus.Extensions;
using FluentAssertions;
using OrderFormAcceptanceTests.Steps.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using TechTalk.SpecFlow;

namespace OrderFormAcceptanceTests.Steps.Steps
{
    [Binding]
    class DeleteOrder : TestBase
    {
        public DeleteOrder(UITest test, ScenarioContext context) : base(test, context)
        {

        }

        [When(@"the User chooses to delete the order")]
        public void WhenTheUserChoosesToDeleteTheOrder()
        {
            Test.Pages.OrderForm.ClickDeleteButton();
        }

        [Then(@"the User is asked to confirm the choice to delete")]
        public void ThenTheUserIsAskedToConfirmTheChoiceToDelete()
        {
            Test.Pages.OrderForm.EditNamedSectionPageDisplayed("Delete order").Should().BeTrue();
        }

        [Given(@"the confirm delete page is displayed")]
        public void GivenTheConfirmDeletePageIsDisplayed()
        {
            var commonSteps = new CommonSteps(Test, Context);
            commonSteps.GivenAnUnsubmittedOrderExists();
            commonSteps.WhenTheOrderFormForTheExistingOrderIsPresented();
        }

    }
}
