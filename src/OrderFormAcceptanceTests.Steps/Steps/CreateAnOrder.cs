namespace OrderFormAcceptanceTests.Steps.Steps
{
    using FluentAssertions;
    using OrderFormAcceptanceTests.Steps.Utils;
    using TechTalk.SpecFlow;

    [Binding]
    public class CreateAnOrder : TestBase
    {
        public CreateAnOrder(UITest test, ScenarioContext context)
            : base(test, context)
        {
        }

        [When(@"the user chooses to create and manager orders")]
        [When(@"they choose to create a new Order")]
        public void WhenTheyChooseToCreateANewOrder()
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

        [Then(@"no organisation is pre-selected")]
        public void ThenNoOrganisationIsPre_Selected()
        {
            Test.Pages.OrderForm.IsRadioButtonSelected().Should().BeFalse();
        }
    }
}
