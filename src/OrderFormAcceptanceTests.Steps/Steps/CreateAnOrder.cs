using FluentAssertions;
using OrderFormAcceptanceTests.Steps.Utils;
using TechTalk.SpecFlow;

namespace OrderFormAcceptanceTests.Steps.Steps
{
    [Binding]
    public class CreateAnOrder : TestBase
    {
        public CreateAnOrder(UITest test, ScenarioContext context) : base(test, context)
        {
        }

        [When(@"they choose to create a new Order")]
        public void GivenTheyChooseToCreateANewOrder()
        {
            Test.Pages.OrganisationsOrdersDashboard.CreateNewOrder();
        }

        [Then(@"the User is presented with a control to return to the Organisation's Orders dashboard")]
        public void ThenTheUserIsPresentedWithAControlToReturnToTheOrganisationSOrdersDashboard()
        {
            Test.Pages.OrderForm.BackLinkDisplayed();
        }

        [Then(@"the User is unable to delete the order")]
        public void ThenTheUserIsUnableToDeleteTheOrder()
        {
            Test.Pages.OrderForm.DeleteOrderButtonIsDisabled().Should().BeTrue();
        }

        [Then(@"the Delete order button is enabled")]
        public void ThenTheDeleteOrderButtonIsEnabled()
        {
            Test.Pages.OrderForm.DeleteOrderButtonIsDisabled().Should().BeFalse();
        }

        [Then(@"the User is unable to preview the order summary")]
        public void ThenTheUserIsUnableToPreviewTheOrderSummary()
        {
            Test.Pages.OrderForm.PreviewOrderButtonIsDisabled().Should().BeTrue();
        }

        [Then(@"the Preview order summary button is enabled")]
        public void ThenThePreviewOrderSummaryButtonIsEnabled()
        {
            Test.Pages.OrderForm.PreviewOrderButtonIsDisabled().Should().BeFalse();
        }

        [Then(@"the User is unable to complete the order")]
        [Then(@"the User is unable to submit the order")]
        [Then(@"the Complete order button is disabled")]
        public void ThenTheUserIsUnableToCompleteTheOrder()
        {
            Test.Pages.OrderForm.CompleteOrderButtonIsDisabled().Should().BeTrue();
        }

        [Then(@"the Complete order button is enabled")]
        public void ThenTheCompleteOrderButtonIsDisabled()
        {
            Test.Pages.OrderForm.CompleteOrderButtonIsDisabled().Should().BeFalse();
        }

        [Then(@"there is alt text content on the disabled Delete order button")]
        public void ThenThereIsAltTextContentOnTheDisabledDeleteOrderButton()
        {
            Test.Pages.OrderForm.DeleteOrderButtonHasAltTest();
        }

        [Then(@"there is alt text content on the disabled Preview Order Summary button")]
        public void ThenThereIsAltTextContentOnTheDisabledPreviewOrderSummaryButton()
        {
            Test.Pages.OrderForm.PreviewOrderButtonHasAltTest();
        }

        [Then(@"there is alt text content on the disabled Complete button")]
        public void ThenThereIsAltTextContentOnTheDisabledCompleteButton()
        {
            Test.Pages.OrderForm.CompleteOrderButtonHasAltTest();
        }
    }
}
