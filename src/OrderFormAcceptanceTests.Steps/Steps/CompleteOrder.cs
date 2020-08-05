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
    class CompleteOrder : TestBase
    {
        public CompleteOrder(UITest test, ScenarioContext context) : base(test, context)
        {

        }

        [Given(@"the User chooses to complete the Order")]
        [When(@"the User chooses to complete the Order")]
        public void WhenTheUserChoosesToCompleteTheOrder()
        {
            Test.Pages.OrderForm.ClickCompleteOrderButton();
        }

        [When(@"the User chooses to download a PDF of their Order Summary")]
        public void WhenTheUserChoosesToDownloadAPdfOfOrderSummary()
        {
            Test.Pages.CompleteOrder.ClickDownloadPDFButton();
        }

        [Then(@"the confirm complete order screen is displayed")]
        public void ThenTheConfirmCompleteOrderScreenIsDisplayed()
        {
            Test.Pages.OrderForm.EditNamedSectionPageDisplayed("Complete order").Should().BeTrue();
        }

        [Then(@"there is specific content related to the User answering 'yes' on the Funding Source question")]
        public void ThenThereIsSpecificContentRelatedToTheUserAnsweringYesOnTheFundingSourceQuestion()
        {
            Test.Pages.CompleteOrder.FundingSourceYesContentIsDisplayed().Should().BeTrue();
        }

        [Then(@"there is specific content related to the User answering 'no' on the Funding Source question")]
        public void ThenThereIsSpecificContentRelatedToTheUserAnsweringNoOnTheFundingSourceQuestion()
        {
            Test.Pages.CompleteOrder.FundingSourceNoContentIsDisplayed().Should().BeTrue();
        }

        [Then(@"there is a control to complete order")]
        public void ThenThereIsAControlToCompleteOrder()
        {
            Test.Pages.CompleteOrder.CompleteOrderButtonIsDisplayed().Should().BeTrue();
        }

        [Then(@"there is a control that allows the User to download a \.PDF version of the Order Summary")]
        public void ThenThereIsAControlThatAllowsTheUserToDownloadA_PDFVersionOfTheOrderSummary()
        {
            Test.Pages.CompleteOrder.DownloadPDFControlIsDisplayed().Should().BeTrue();
        }

        [Given(@"that the User is on the confirm complete order screen")]
        public void GivenThatTheUserIsOnTheConfirmCompleteOrderScreen()
        {
            var commonSteps = new CommonSteps(Test, Context);
            commonSteps.GivenAnUnsubmittedOrderExists();
            new CatalogueSolutions(Test, Context).GivenThereAreNoServiceRecipientsInTheOrder();
            commonSteps.WhenTheOrderFormForTheExistingOrderIsPresented();
            WhenTheUserChoosesToCompleteTheOrder();
            ThenTheConfirmCompleteOrderScreenIsDisplayed();
        }

        [StepDefinition(@"the Order completed screen is displayed")]
        public void ThenTheOrderCompletedScreenIsDisplayed()
        {
            Test.Pages.OrderForm.EditNamedSectionPageDisplayed("completed").Should().BeTrue();            
        }

        [Given(@"that the User has completed their Order")]
        public void GivenThatTheUserHasCompletedTheirOrder()
        {
            GivenThatTheUserIsOnTheConfirmCompleteOrderScreen();
            WhenTheUserChoosesToCompleteTheOrder();
            ThenTheOrderCompletedScreenIsDisplayed();
        }

    }
}
