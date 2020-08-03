using Bogus.Extensions;
using FluentAssertions;
using OrderFormAcceptanceTests.Steps.Utils;
using OrderFormAcceptanceTests.TestData;
using System;
using System.Collections.Generic;
using System.Linq;
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

        [Given(@"the Order deleted page is presented")]
        public void GivenTheOrderDeletedPageIsPresented()
        {
            GivenTheConfirmDeletePageIsDisplayed();
            ConfirmDeleteOrder();
            ThenTheUserIsInformedThatTheOrderHasBeenDeleted();
        }

        [When(@"the User chooses to delete")]
        public void ConfirmDeleteOrder()
        {
            Test.Pages.DeleteOrder.ClickDeleteButtonYes();
        }

        [When(@"the User chooses not to delete the Order")]
        public void WhenTheUserChoosesNotToDeleteTheOrder()
        {
            Test.Pages.DeleteOrder.ClickDeleteButtonNo();
        }

        [Then(@"the User is informed that the Order has been deleted")]
        public void ThenTheUserIsInformedThatTheOrderHasBeenDeleted()
        {
            Test.Pages.OrderForm.EditNamedSectionPageDisplayed("deleted").Should().BeTrue();
        }

        [Then(@"the Order has a Deleted status")]
        public void ThenTheOrderHasADeletedStatus()
        {
            var order = (Order)Context["CreatedOrder"];
            order.Retrieve(Test.ConnectionString);
            order.IsDeleted.Should().Be(1);
        }

        [Then(@"the status of the Order does not change to deleted")]
        public void ThenTheStatusOfTheOrderDoesNotChangeToDeleted()
        {
            var order = (Order)Context["CreatedOrder"];
            order.Retrieve(Test.ConnectionString);
            order.IsDeleted.Should().Be(0);
        }

        [Then(@"the Order is not on the Organisation's Orders Dashboard")]
        public void ThenTheOrderIsNotOnTheOrganisationSOrdersDashboard()
        {
            var deletedOrder = (Order)Context["CreatedOrder"];
            Test.Pages.OrderForm.ClickBackLink();
            new OrganisationsOrdersDashboard(Test, Context).ThenThePageDisplaysWhoIsLoggedInAndThePrimaryOrganisationName();
            var listOfOrders = Test.Pages.OrganisationsOrdersDashboard.GetListOfUnsubmittedOrders();
            listOfOrders.Where(o => o.OrderId == deletedOrder.OrderId).Count().Should().Be(0);
        }
    }
}
