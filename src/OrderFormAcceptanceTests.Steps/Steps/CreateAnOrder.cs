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

        [Given(@"the User is presented with the Organisation's Orders dashboard")]
        public void GivenTheUserIsPresentedWithTheOrganisationsOrdersDashboard()
        {
            Test.Pages.Homepage.ClickOrderTile();            
        }
        
        [Given(@"they choose to create a new Order")]
        public void GivenTheyChooseToCreateANewOrder()
        {
            Test.Pages.OrderForm.CreateNewOrder();
        }
        
        [Then(@"the new Order is presented")]
        public void ThenTheNewOrderIsPresented()
        {
            Test.Pages.OrderForm.NewOrderFormDisplayed().Should().BeTrue();
        }
        
        [Then(@"the User is presented with a control to return to the Organisation's Orders dashboard")]
        public void ThenTheUserIsPresentedWithAControlToReturnToTheOrganisationSOrdersDashboard()
        {
            Test.Pages.OrderForm.BackLinkDisplayed();
        }
        
        [Then(@"the User is unable to delete the order")]
        public void ThenTheUserIsUnableToDeleteTheOrder()
        {
            Test.Pages.OrderForm.DeleteOrderButtonIsDisabled();
        }
        
        [Then(@"the User is unable to preview the order summary")]
        public void ThenTheUserIsUnableToPreviewTheOrderSummary()
        {
            Test.Pages.OrderForm.PreviewOrderButtonIsDisabled();
        }
        
        [Then(@"the User is unable to submit the order")]
        public void ThenTheUserIsUnableToSubmitTheOrder()
        {
            Test.Pages.OrderForm.SubmitOrderButtonIsDisabled();
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
        
        [Then(@"there is alt text content on the disabled Submit button")]
        public void ThenThereIsAltTextContentOnTheDisabledSubmitButton()
        {
            Test.Pages.OrderForm.SubmitOrderButtonHasAltTest();
        }
    }
}
