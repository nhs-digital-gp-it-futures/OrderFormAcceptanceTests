namespace OrderFormAcceptanceTests.Steps.Steps
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using FluentAssertions;
    using OpenQA.Selenium;
    using OrderFormAcceptanceTests.Steps.Utils;
    using OrderFormAcceptanceTests.TestData;
    using TechTalk.SpecFlow;

    [Binding]
    internal sealed class CompleteOrder : TestBase
    {
        public CompleteOrder(UITest test, ScenarioContext context)
            : base(test, context)
        {
        }

        [StepDefinition(@"the User chooses to complete the Order")]
        public void WhenTheUserChoosesToCompleteTheOrder()
        {
            Test.Pages.OrderForm.ClickCompleteOrderLink();
        }

        [StepDefinition(@"the User confirms to complete the Order")]
        public async Task WhenTheUserConfirmsToCompleteTheOrderAsync()
        {
            var precount = await Test.EmailServerDriver.GetEmailCountAsync();
            Context.Add(ContextKeys.EmailCount, precount);
            Test.Pages.CompleteOrder.ClickCompleteOrderButton();
        }

        [When(@"the User chooses to continue editing order")]
        public void WhenTheUserChoosesToContinueEditingOrder()
        {
            Test.Pages.CompleteOrder.ClickContinueEditingOrderButton();
        }

        [When(@"the User chooses to download a PDF of their Order Summary")]
        public void WhenTheUserChoosesToDownloadAPdfOfOrderSummary()
        {
            Test.Pages.CompleteOrder.ClickGetOrderSummaryLink();
        }

        [StepDefinition(@"the User chooses to get the Preview Order Summary")]
        public void WhentheUserChoosesToGetThePreviewOrderSummary()
        {
            Test.Pages.OrderForm.ClickPreviewOrderButton();
        }

        [StepDefinition(@"the User chooses to get the Order Summary")]
        [When(@"the User chooses to print the Preview Order Summary")]
        public void WhenTheUserChoosesToPrintThePreviewOrderSummary()
        {
            Test.Pages.PreviewOrderSummary.ClickGetPreviewOrderSummary();
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

        [Then(@"there is a control to continue editing order")]
        public void ThenThereIsAControlToContinueEditingOrder()
        {
            Test.Pages.CompleteOrder.ContinueEditingOrderButtonIsDisplayed().Should().BeTrue();
        }

        [Then(@"there is a control that allows the User to download a \.PDF version of the Order Summary")]
        public void ThenThereIsAControlThatAllowsTheUserToDownloadA_PDFVersionOfTheOrderSummary()
        {
            Test.Pages.CompleteOrder.DownloadPDFControlIsDisplayed().Should().BeTrue();
        }

        [Then(@"there is a button to get the Order Summary at the top and bottom of it")]
        [Then(@"there is a button to get the Preview Order Summary at the top and bottom of it")]
        public void ThenThereIsAControlThatAllowsTheUserToGetAPreviewOrderSummary()
        {
            Test.Pages.PreviewOrderSummary.TopGetOrderSummaryIsDisplayed().Should().BeTrue();
            Test.Pages.PreviewOrderSummary.BottomGetOrderSummaryIsDisplayed().Should().BeTrue();
        }

        [Given(@"the order is complete enough so that the Complete order button is enabled with Funding Source option '(.*)' selected")]
        public void GivenTheOrderIsCompleteEnoughSoThatTheCompleteOrderButtonIsEnabled(string fsValue)
        {
            new CommonSteps(Test, Context).GivenAnIncompleteOrderExists();
            new CatalogueSolutions(Test, Context).GivenThereAreNoServiceRecipientsInTheOrder();
            new AssociatedServices(Test, Context).GivenAnAssociatedServiceWithAFlatPriceDeclarativeOrderTypeIsSavedToTheOrder();
            if (fsValue.ToLower().Equals("yes"))
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

        [Given("the User chooses to preview the Order Summary")]
        [Given("that the User is on the Order Summary")]
        public void GivenThatTheUserIsOnTheOrderSummary()
        {
            GivenTheOrderIsCompleteEnoughSoThatTheCompleteOrderButtonIsEnabled("yes");
            new CommonSteps(Test, Context).WhenTheOrderFormForTheExistingOrderIsPresented();
        }

        [StepDefinition(@"the Order completed screen is displayed")]
        public void ThenTheOrderCompletedScreenIsDisplayed()
        {
            Test.Pages.OrderForm.EditNamedSectionPageDisplayed("completed").Should().BeTrue();
        }

        [Given(@"that the User has completed their Order")]
        public async Task GivenThatTheUserHasCompletedTheirOrderAsync()
        {
            GivenThatTheUserIsOnTheConfirmCompleteOrderScreen("no");
            await WhenTheUserConfirmsToCompleteTheOrderAsync();
            ThenTheOrderCompletedScreenIsDisplayed();
        }

        [When(@"they choose to view the Completed Order from their Organisation's Orders Dashboard")]
        public void WhenTheyChooseToViewTheCompletedOrderFromTheirOrganisationSOrdersDashboard()
        {
            new OrganisationsOrdersDashboard(Test, Context).WhenTheUserIsPresentedWithTheOrganisationSOrdersDashboard();
            Test.Pages.OrganisationsOrdersDashboard.SelectCompletedOrder(Context.Get<IList<Order>>(ContextKeys.CreatedCompletedOrders)[0].OrderId);
        }

        [Then(@"the Completed version of the Order Summary is presented")]
        public void ThenTheCompletedVersionOfTheOrderSummaryIsPresented()
        {
            new PreviewOrderSummary(Test, Context).ThenTheOrderSummaryIsPresented();
            Test.Pages.PreviewOrderSummary.TopGetOrderSummaryIsDisplayed().Should().BeTrue();
        }

        [Then(@"the completed order summary has specific content related to the order being completed")]
        public void ThenTheCompletedOrderSummaryHasSpecificContentRelatedToTheOrderBeingCompleted()
        {
            Test.Driver.FindElement(By.TagName("h2")).Text.Should().ContainEquivalentOf("This order is complete");
        }

        [Then(@"the completed order summary contains the date the Order was completed")]
        public void ThenTheCompletedOrderSummaryContainsTheDateTheOrderWasCompleted()
        {
            var order = Context.Get<IList<Order>>(ContextKeys.CreatedCompletedOrders).First();
            var date = Test.Pages.PreviewOrderSummary.GetDateOrderCompletedValue();
            date.Should().NotBeNullOrEmpty();
            var expectedDate = order.DateCompleted.Value.ToString("d MMMM yyyy");
            date.Should().EndWithEquivalent(expectedDate);
        }

        [StepDefinition(@"the Completed Order Summary is displayed")]
        public void WhenTheCompletedOrderSummaryIsDisplayed()
        {
            WhenTheyChooseToViewTheCompletedOrderFromTheirOrganisationSOrdersDashboard();
            ThenTheCompletedVersionOfTheOrderSummaryIsPresented();
        }

        [Then(@"the Order is not completed")]
        public void ThenTheOrderIsNotCompleted()
        {
            var order = (Order)Context[ContextKeys.CreatedOrder];
            order = order.Retrieve(Test.OrdapiConnectionString);
            order.OrderStatusId.Should().Be(2);
            order.DateCompleted.Should().BeNull();
        }
    }
}
