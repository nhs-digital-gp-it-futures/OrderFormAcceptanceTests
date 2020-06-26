using FluentAssertions;
using OrderFormAcceptanceTests.Steps.Utils;
using OrderFormAcceptanceTests.TestData;
using OrderFormAcceptanceTests.TestData.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;

namespace OrderFormAcceptanceTests.Steps.Steps
{
    [Binding]
    public sealed class CatalogueSolutions : TestBase
    {

        public CatalogueSolutions(UITest test, ScenarioContext context) : base(test, context)
        {

        }

        [Given(@"there are one or more Service Recipients in the order")]
        public void GivenThereAreOneOrMoreServiceRecipientsInTheOrder()
        {
            Context.ContainsKey("CreatedServiceRecipient").Should().BeTrue();
        }

        [Given(@"there are no Service Recipients in the order")]
        public void GivenThereAreNoServiceRecipientsInTheOrder()
        {
            var order = (Order)Context["CreatedOrder"];
            order.ServiceRecipientsViewed.Should().Be(1);

            var serviceRecipient = (ServiceRecipient)Context["CreatedServiceRecipient"];
            serviceRecipient.Delete(Test.ConnectionString);
            Context.Remove("CreatedServiceRecipient");
        }

        [Then(@"the User is able to manage the Catalogue Solutions section")]
        public void ThenTheUserIsAbleToManageTheCatalogueSolutionsSection()
        {
            Test.Pages.OrderForm.ClickEditCatalogueSolutions();
            ThenTheCatalogueSolutionDashboardIsPresented();
        }

        [Then(@"the User is not able to manage the Catalogue Solutions section")]
        [Then(@"the Catalogue Solution section is disabled")]
        public void ThenTheUserIsNotAbleToManageTheCatalogueSolutionsSection()
        {
            Test.Pages.OrderForm.EditCatalogueSolutionsSectionIsEnabled().Should().BeFalse();
        }

        [Then(@"the Catalogue Solution section is not complete")]
        public void ThenTheCatalogueSolutionSectionIsNotComplete()
        {
            Test.Pages.OrderForm.AssertThatEditCatalogueSolutionsSectionIsNotComplete();
        }

        [StepDefinition(@"the User has chosen to manage the Catalogue Solution section")]
        public void WhenTheUserHasChosenToManageTheCatalogueSolutionSection()
        {
            new CommonSteps(Test, Context).WhenTheOrderFormForTheExistingOrderIsPresented();
            ThenTheUserIsAbleToManageTheCatalogueSolutionsSection();
        }

        [Then(@"the Catalogue Solution dashboard is presented")]
        public void ThenTheCatalogueSolutionDashboardIsPresented()
        {
            Test.Pages.OrderForm.EditNamedSectionPageDisplayed("Catalogue Solution").Should().BeTrue();
        }

        [Then(@"there is a control to add a Catalogue Solution")]
        public void ThenThereIsAControlToAddACatalogueSolution()
        {
            Test.Pages.OrderForm.AddSolutionButtonDisplayed().Should().BeTrue();
        }

        [Then(@"there is a control to continue")]
        public void ThenThereIsAControlToContinue()
        {
            Test.Pages.OrderForm.ContinueButtonDisplayed().Should().BeTrue();
        }

        [Then(@"there is content indicating there is no Catalogue Solution added")]
        public void ThenThereIsContentIndicatingThereIsNoCatalogueSolutionAdded()
        {
            Test.Pages.OrderForm.NoSolutionsAddedDisplayed().Should().BeTrue();
        }

        [When(@"the User chooses to add a single Catalogue Solution")]
        public void WhenTheUserChoosesToAddASingleCatalogueSolution()
        {
            Test.Pages.OrderForm.ClickAddSolutionButton();
        }

        [Then(@"they are presented with the Catalogue Solutions available from their chosen Supplier")]
        public void ThenTheyArePresentedWithTheCatalogueSolutionsAvailableFromTheirChosenSupplier()
        {
            Test.Pages.OrderForm.EditNamedSectionPageDisplayed("Add Catalogue Solution").Should().BeTrue();
        }

        [Then(@"they can select one Catalogue Solution to add")]
        [Then(@"they can select a price for the Catalogue Solution")]
        [Then(@"they are presented with the Service Recipients saved in the Order")]
        public void ThenTheyCanSelectOneRadioButton()
        {
            Test.Pages.OrderForm.NumberOfRadioButtonsDisplayed().Should().BeGreaterThan(0);
        }

        [Given(@"the User is presented with Catalogue Solutions available from their chosen Supplier")]
        public void GivenTheUserIsPresentedWithCatalogueSolutionsAvailableFromTheirChosenSupplier()
        {
            WhenTheUserHasChosenToManageTheCatalogueSolutionSection();
            WhenTheUserChoosesToAddASingleCatalogueSolution();
            ThenTheyArePresentedWithTheCatalogueSolutionsAvailableFromTheirChosenSupplier();
        }

        [Then(@"the User is informed they have to select a Catalogue Solution")]
        public void ThenTheUserIsInformedTheyHaveToSelectACatalogueSolution()
        {
            Test.Pages.OrderForm.ErrorSummaryDisplayed().Should().BeTrue();
            Test.Pages.OrderForm.ErrorMessagesDisplayed().Should().BeTrue();
            Test.Pages.OrderForm.ClickOnErrorLink().Should().ContainEquivalentOf("selectSolution");
        }

        [Then(@"the User is informed they have to select a Catalogue Solution price")]
        public void ThenTheUserIsInformedTheyHaveToSelectACatalogueSolutionPrice()
        {
            Test.Pages.OrderForm.ErrorSummaryDisplayed().Should().BeTrue();
            Test.Pages.OrderForm.ErrorMessagesDisplayed().Should().BeTrue();
            Test.Pages.OrderForm.ClickOnErrorLink().Should().ContainEquivalentOf("selectSolutionPrice");
        }

        [Given(@"the User selects a catalogue solution to add")]
        public void GivenTheUserSelectsACatalogueSolutionToAdd()
        {
            var solutionId = Test.Pages.OrderForm.ClickRadioButton();
            Context.Add("ChosenSolutionId", solutionId);
        }

        [Then(@"all the available prices for that Catalogue Solution are presented")]
        public void ThenAllTheAvailablePricesForThatCatalogueSolutionArePresented()
        {
            Test.Pages.OrderForm.EditNamedSectionPageDisplayed("List price").Should().BeTrue();
            var SolutionId = (string)Context["ChosenSolutionId"];
            var query = "Select count(*) FROM [dbo].[CataloguePrice] where CatalogueItemId=@SolutionId";
            var expectedNumberOfPrices = SqlExecutor.Execute<int>(Test.BapiConnectionString, query, new { SolutionId }).Single();
            Test.Pages.OrderForm.NumberOfRadioButtonsDisplayed().Should().Be(expectedNumberOfPrices);
        }

        [Given(@"the User is presented with the prices for the selected Catalogue Solution")]
        public void GivenTheUserIsPresentedWithThePricesForTheSelectedCatalogueSolution()
        {
            GivenTheUserIsPresentedWithCatalogueSolutionsAvailableFromTheirChosenSupplier();
            GivenTheUserSelectsACatalogueSolutionToAdd();
            new CommonSteps(Test, Context).WhenTheyChooseToContinue();
            ThenTheyCanSelectOneRadioButton();
        }

        [Given(@"the User selects a price")]
        public void GivenTheUserSelectsAPrice()
        {
            Test.Pages.OrderForm.ClickRadioButton();
        }

        [Given(@"the User is presented with the Service Recipients saved in the Order")]
        public void GivenTheUserIsPresentedWithTheServiceRecipientsSavedInTheOrder()
        {
            GivenTheUserIsPresentedWithThePricesForTheSelectedCatalogueSolution();
            GivenTheUserSelectsAPrice();
            new CommonSteps(Test, Context).WhenTheyChooseToContinue();
            ThenTheyCanSelectOneRadioButton();
        }

        [Then(@"the User is informed they have to select a Service Recipient")]
        public void ThenTheUserIsInformedTheyHaveToSelectAServiceRecipient()
        {
            Test.Pages.OrderForm.ErrorSummaryDisplayed().Should().BeTrue();
            Test.Pages.OrderForm.ErrorMessagesDisplayed().Should().BeTrue();
            Test.Pages.OrderForm.ClickOnErrorLink().Should().ContainEquivalentOf("selectRecipient");
        }
    }
}
