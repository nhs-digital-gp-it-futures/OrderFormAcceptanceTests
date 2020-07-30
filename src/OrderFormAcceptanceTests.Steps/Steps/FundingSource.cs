using FluentAssertions;
using OrderFormAcceptanceTests.Steps.Utils;
using OrderFormAcceptanceTests.TestData;
using TechTalk.SpecFlow;

namespace OrderFormAcceptanceTests.Steps.Steps
{
    [Binding]
    class FundingSource : TestBase
    {
        public FundingSource(UITest test, ScenarioContext context) : base(test, context)
        {
        }

        [Then(@"the User is able to manage the Funding Source section")]
        public void ThenTheUserIsAbleToManageTheFundingSourceSection()
        {
            Test.Pages.OrderForm.ClickEditFundingSource();
            ThenTheFundingSourceScreenIsPresented();
        }

        [StepDefinition(@"the Edit Funding Source Page is presented")]
        public void ThenTheFundingSourceScreenIsPresented()
        {
            Test.Pages.OrderForm.EditNamedSectionPageDisplayed("Funding source for").Should().BeTrue();
        }

        [Then(@"they are informed they have to select a Funding Source option")]
        public void ThenTheyAreInformedTheyHaveToSelectAFundingSourceOption()
        {
            Test.Pages.OrderForm.ErrorSummaryDisplayed().Should().BeTrue();
            Test.Pages.OrderForm.ErrorMessagesDisplayed().Should().BeTrue();
            Test.Pages.OrderForm.ClickOnErrorLink().Should().ContainEquivalentOf("selectFundingSource");
        }

        [Given(@"the minimum data needed to enable the Funding Source section exists")]
        public void GivenTheMinimumDataNeededToEnableTheFundingSourceSectionExists()
        {
            new CommonSteps(Test, Context).GivenAnUnsubmittedOrderExists();
            new CatalogueSolutions(Test, Context).GivenThereAreNoServiceRecipientsInTheOrder();
            new AssociatedServices(Test, Context).GivenAnAssociatedServiceWithAFlatPriceDeclarativeOrderTypeIsSavedToTheOrder();
        }

        [Given(@"the User is presented with the edit Funding Source page")]
        public void GivenTheUserIsPresentedWithTheEditFundingSourcePage()
        {
            new CommonSteps(Test, Context).WhenTheOrderFormForTheExistingOrderIsPresented();
            ThenTheUserIsAbleToManageTheFundingSourceSection();
        }

        [When(@"the User chooses a Funding Source option")]
        public void WhenTheUserChoosesAFundingSourceOption()
        {
            var option = Test.Pages.OrderForm.ClickRadioButton();
            Context.Add("ChosenOption", option);
        }

        [Then(@"the Funding Source section is complete")]
        public void ThenTheFundingSourceSectionIsComplete()
        {
            var ChosenOption = (string)Context["ChosenOption"];
            var expectedValue = ChosenOption == "true" ? 1 : 0;
            var order = (Order)Context["CreatedOrder"];
            order = order.Retrieve(Test.ConnectionString);
            order.FundingSourceOnlyGMS.Should().Be(expectedValue);
        }
    }
}
