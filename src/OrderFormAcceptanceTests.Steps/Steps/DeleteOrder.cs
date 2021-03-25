namespace OrderFormAcceptanceTests.Steps.Steps
{
    using System.Linq;
    using System.Threading.Tasks;
    using FluentAssertions;
    using OrderFormAcceptanceTests.Domain;
    using OrderFormAcceptanceTests.Steps.Utils;
    using OrderFormAcceptanceTests.TestData;
    using TechTalk.SpecFlow;

    [Binding]
    public sealed class DeleteOrder : TestBase
    {
        public DeleteOrder(UITest test, ScenarioContext context)
            : base(test, context)
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
        public async Task GivenTheConfirmDeletePageIsDisplayed()
        {
            var commonSteps = new CommonSteps(Test, Context);
            await commonSteps.GivenAnIncompleteOrderExists();
            commonSteps.WhenTheOrderFormForTheExistingOrderIsPresented();

            var order = Context.Get<Order>(ContextKeys.CreatedOrder);
            order.SetLastUpdatedBy(order.LastUpdatedBy, order.LastUpdatedByName);

            WhenTheUserChoosesToDeleteTheOrder();
            ThenTheUserIsAskedToConfirmTheChoiceToDelete();
        }

        [Given(@"the Order deleted page is presented")]
        public async Task GivenTheOrderDeletedPageIsPresented()
        {
            await GivenTheConfirmDeletePageIsDisplayed();
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
    }
}
