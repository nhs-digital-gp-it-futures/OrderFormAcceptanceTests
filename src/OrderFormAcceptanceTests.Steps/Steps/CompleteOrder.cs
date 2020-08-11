using Bogus.Extensions;
using FluentAssertions;
using OpenQA.Selenium;
using OrderFormAcceptanceTests.Steps.Utils;
using OrderFormAcceptanceTests.TestData;
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

        [StepDefinition(@"the User chooses to complete the Order")]
        public void WhenTheUserChoosesToCompleteTheOrder()
        {
            Test.Pages.OrderForm.ClickCompleteOrderLink();
        }

        [StepDefinition(@"the User confirms to complete the Order")]
        public void WhenTheUserConfirmsToCompleteTheOrder()
        {
            Test.Pages.CompleteOrder.ClickCompleteOrderButton();
        }

        [When(@"the User chooses to download a PDF of their Order Summary")]
        public void WhenTheUserChoosesToDownloadAPdfOfOrderSummary()
        {
            Test.Pages.CompleteOrder.ClickGetOrderSummaryLink();
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

        [Then(@"there is specific content related to the User answering 'yes' on the Funding Source question on the completed screen")]
        public void ThenThereIsSpecificContentRelatedToTheUserAnsweringYesOnTheFundingSourceQuestionOnTheCompletedScreen()
        {
            Test.Pages.CompleteOrder.FundingSourceYesContentOnCompletedScreenIsDisplayed().Should().BeTrue();
        }

        [Then(@"there is specific content related to the User answering 'no' on the Funding Source question on the completed screen")]
        public void ThenThereIsSpecificContentRelatedToTheUserAnsweringNoOnTheFundingSourceQuestionOnTheCompletedScreen()
        {
            Test.Pages.CompleteOrder.FundingSourceNoContentOnCompletedScreenIsDisplayed().Should().BeTrue();
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

        [Given(@"the order is complete enough so that the Complete order button is enabled with Funding Source option '(.*)' selected")]
        public void GivenTheOrderIsCompleteEnoughSoThatTheCompleteOrderButtonIsEnabled(string fsValue)
        {
            new CommonSteps(Test, Context).GivenAnUnsubmittedOrderExists();
            new CatalogueSolutions(Test, Context).GivenThereAreNoServiceRecipientsInTheOrder();
            new AssociatedServices(Test, Context).GivenAnAssociatedServiceWithAFlatPriceDeclarativeOrderTypeIsSavedToTheOrder();
            if (fsValue.Equals("yes"))
            {
                new OrderForm(Test, Context).GivenTheFundingSourceSectionIsCompleteWithYesSelected();
            }
            else
            {
                new OrderForm(Test, Context).GivenTheFundingSourceSectionIsCompleteWithNoSelected();
            }
        }

        [Given(@"that the User is on the confirm complete order screen with Funding Source option '(.*)' selected")]
        public void GivenThatTheUserIsOnTheConfirmCompleteOrderScreen(string fsValue)
        {
            GivenTheOrderIsCompleteEnoughSoThatTheCompleteOrderButtonIsEnabled(fsValue);
            new CommonSteps(Test, Context).WhenTheOrderFormForTheExistingOrderIsPresented();
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
            GivenThatTheUserIsOnTheConfirmCompleteOrderScreen("no");
            WhenTheUserConfirmsToCompleteTheOrder();
            ThenTheOrderCompletedScreenIsDisplayed();
        }

        [Given(@"a User has completed an Order")]
        public void GivenAUserHasCompletedAnOrder()
        {
            new CommonSteps(Test, Context).GivenACompletedOrderExists();
        }

        [When(@"they choose to view the Completed Order from their Organisation's Orders Dashboard")]
        public void WhenTheyChooseToViewTheCompletedOrderFromTheirOrganisationSOrdersDashboard()
        {
            new OrganisationsOrdersDashboard(Test, Context).WhenTheUserIsPresentedWithTheOrganisationSOrdersDashboard();
            Test.Pages.OrganisationsOrdersDashboard.SelectExistingOrder(((Order)Context["CreatedOrder"]).OrderId);
        }

        [Then(@"the Completed version of the Order Summary is presented")]
        public void ThenTheCompletedVersionOfTheOrderSummaryIsPresented()
        {
            new PreviewOrderSummary(Test, Context).ThenTheOrderSummaryIsPresented();
            ThenThereIsAControlThatAllowsTheUserToDownloadA_PDFVersionOfTheOrderSummary();
        }

        [Then(@"the completed order summary has specific content related to the order being completed")]
        public void ThenTheCompletedOrderSummaryHasSpecificContentRelatedToTheOrderBeingCompleted()
        {
            Test.Driver.FindElement(By.TagName("h2")).Text.Should().ContainEquivalentOf("This order is complete");
        }

        [Then(@"the completed order summary contains the date the Order was completed")]
        public void ThenTheCompletedOrderSummaryContainsTheDateTheOrderWasCompleted()
        {
            var order = (Order)Context["CreatedOrder"];
            var date = Test.Pages.OrderForm.GetDateOrderCompletedValue();
            date.Should().NotBeNullOrEmpty();
            //var expectedDate = order.DateCompleted.ToString("d MMMM yyyy");
            //date.Should().EndWithEquivalent(expectedDate);
        }
    }
}
