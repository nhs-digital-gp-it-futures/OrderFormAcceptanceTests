using FluentAssertions;
using OrderFormAcceptanceTests.Steps.Utils;
using OrderFormAcceptanceTests.TestData;
using System.Linq;
using TechTalk.SpecFlow;

namespace OrderFormAcceptanceTests.Steps.Steps
{
    [Binding]
    public sealed class DeleteOrder : TestBase
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
            commonSteps.GivenAnIncompleteOrderExists();
            commonSteps.WhenTheOrderFormForTheExistingOrderIsPresented();
            WhenTheUserChoosesToDeleteTheOrder();
            ThenTheUserIsAskedToConfirmTheChoiceToDelete();
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
            var order = (Order)Context[ContextKeys.CreatedOrder];
            order = order.Retrieve(Test.OrdapiConnectionString);
            order.IsDeleted.Should().Be(1);
        }

        [Then(@"the status of the Order does not change to deleted")]
        public void ThenTheStatusOfTheOrderDoesNotChangeToDeleted()
        {
            var order = (Order)Context[ContextKeys.CreatedOrder];
            order.Retrieve(Test.OrdapiConnectionString);
            order.IsDeleted.Should().Be(0);
        }

        [Then(@"the Order is not on the Organisation's Orders Dashboard")]
        public void ThenTheOrderIsNotOnTheOrganisationSOrdersDashboard()
        {
            var deletedOrder = (Order)Context[ContextKeys.CreatedOrder];
            Test.Pages.OrderForm.ClickBackLink();
            new OrganisationsOrdersDashboard(Test, Context).ThenThePageDisplaysWhoIsLoggedInAndThePrimaryOrganisationName();
            var listOfOrders = Test.Pages.OrganisationsOrdersDashboard.GetListOfIncompleteOrders();
            listOfOrders.Where(o => o.OrderId == deletedOrder.OrderId).Count().Should().Be(0);
        }
    }
}
