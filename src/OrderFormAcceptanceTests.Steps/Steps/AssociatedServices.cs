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
            ThenTheAssociatedServicesDashboardIsPresented();
        }

        [Given(@"an Associated Service is added to the order")]
        public void GivenAnAssociatedServiceIsAddedToTheOrder()
        {
            //dev work to update data model needs completing
            Context.Pending();
        }

        [StepDefinition(@"the User has chosen to manage the Associated Service section")]
        public void WhenTheUserHasChosenToManageTheAssociatedServiceSection()
        {
            new CommonSteps(Test, Context).WhenTheOrderFormForTheExistingOrderIsPresented();
            ThenTheUserIsAbleToManageTheAssociatedServicesSection();
        }

        [Then(@"the Associated Services dashboard is presented")]
        public void ThenTheAssociatedServicesDashboardIsPresented()
        {
            Test.Pages.OrderForm.EditNamedSectionPageDisplayed("Associated Services").Should().BeTrue();
        }

        [Then(@"there is a control to add an Associated Service")]
        public void ThenThereIsAControlToAddAnAssociatedService()
        {
            Test.Pages.OrderForm.AddAssociatedServiceButtonDisplayed().Should().BeTrue();
        }

        [Then(@"they are presented with the Associated Services available from their chosen Supplier")]
        public void ThenTheyArePresentedWithTheAssociatedServicesAvailableFromTheirChosenSupplier()
        {
            Test.Pages.OrderForm.EditNamedSectionPageDisplayed("Add Associated Services").Should().BeTrue();
        }

        [Given(@"the User is presented with Associated Services available from their chosen Supplier")]
        public void GivenTheUserIsPresentedWithAssociatedServicesAvailableFromTheirChosenSupplier()
        {
            WhenTheUserHasChosenToManageTheAssociatedServiceSection();
            new CommonSteps(Test, Context).WhenTheyChooseToContinue();
            ThenTheyArePresentedWithTheAssociatedServicesAvailableFromTheirChosenSupplier();
        }

        [Then(@"the User is informed they have to select an Associated Service")]
        public void ThenTheUserIsInformedTheyHaveToSelectAnAssociatedService()
        {
            Test.Pages.OrderForm.ErrorSummaryDisplayed().Should().BeTrue();
            Test.Pages.OrderForm.ErrorMessagesDisplayed().Should().BeTrue();
            Test.Pages.OrderForm.ClickOnErrorLink().Should().ContainEquivalentOf("selectAssociatedService");
        }

    }
}
