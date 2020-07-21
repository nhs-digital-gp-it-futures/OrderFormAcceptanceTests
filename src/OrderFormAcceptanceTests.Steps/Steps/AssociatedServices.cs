using Bogus;
using FluentAssertions;
using OrderFormAcceptanceTests.Steps.Utils;
using OrderFormAcceptanceTests.TestData.Utils;
using System.Linq;
using TechTalk.SpecFlow;

namespace OrderFormAcceptanceTests.Steps.Steps
{
    [Binding]
    class AssociatedServices : TestBase
    {
        public AssociatedServices(UITest test, ScenarioContext context) : base(test, context)
        {

        }

        [Then(@"the User is able to manage the Associated Services section")]
        public void ThenTheUserIsAbleToManageTheAssociatedServicesSection()
        {
            Test.Pages.OrderForm.ClickEditAssociatedServices();
            ThenTheAssociatedServicesDashboardIsPresented();
        }

        [Given(@"an Associated Service is added to the order")]
        public void GivenAnAssociatedServiceIsAddedToTheOrder()
        {
            //dev work to update data model needs completing
            Context.Pending();
        }

        [StepDefinition(@"the User has chosen to manage the Associated Service section")]
        public void WhenTheUserHasChosenToManageTheAssociatedServiceSection()
        {
            new CommonSteps(Test, Context).WhenTheOrderFormForTheExistingOrderIsPresented();
            ThenTheUserIsAbleToManageTheAssociatedServicesSection();
        }

        [Then(@"the Associated Services dashboard is presented")]
        public void ThenTheAssociatedServicesDashboardIsPresented()
        {
            Test.Pages.OrderForm.EditNamedSectionPageDisplayed("Associated Services").Should().BeTrue();
        }

        [Then(@"there is a control to add an Associated Service")]
        public void ThenThereIsAControlToAddAnAssociatedService()
        {
            Test.Pages.OrderForm.AddAssociatedServiceButtonDisplayed().Should().BeTrue();
        }

        [Then(@"they are presented with the Associated Services available from their chosen Supplier")]
        public void ThenTheyArePresentedWithTheAssociatedServicesAvailableFromTheirChosenSupplier()
        {
            Test.Pages.OrderForm.EditNamedSectionPageDisplayed("Add Associated Service").Should().BeTrue();
        }

        [Given(@"the User is presented with Associated Services available from their chosen Supplier")]
        public void GivenTheUserIsPresentedWithAssociatedServicesAvailableFromTheirChosenSupplier()
        {
            WhenTheUserHasChosenToManageTheAssociatedServiceSection();
            new CommonSteps(Test, Context).WhenTheyChooseToContinue();
            ThenTheyArePresentedWithTheAssociatedServicesAvailableFromTheirChosenSupplier();
        }

        [Given(@"the User is presented with the prices for the selected Associated Service")]
        public void GivenTheUserIsPresentedWithThePricesForTheSelectedAssociatedService()
        {
            GivenTheUserIsPresentedWithAssociatedServicesAvailableFromTheirChosenSupplier();
            Test.Pages.OrderForm.ClickRadioButton(0);
            new CommonSteps(Test, Context).ContinueAndWaitForRadioButtons();
        }

        [Then(@"the User is informed they have to select an Associated Service")]
        public void ThenTheUserIsInformedTheyHaveToSelectAnAssociatedService()
        {
            Test.Pages.OrderForm.ErrorSummaryDisplayed().Should().BeTrue();
            Test.Pages.OrderForm.ErrorMessagesDisplayed().Should().BeTrue();
            Test.Pages.OrderForm.ClickOnErrorLink().Should().ContainEquivalentOf("selectAssociatedService");
        }

        [Then(@"the User is informed they have to select a Associated Service price")]
        public void ThenTheUserIsInformedTheyHaveToSelectAAssociatedServicePrice()
        {
            Test.Pages.OrderForm.ErrorSummaryDisplayed().Should().BeTrue();
            Test.Pages.OrderForm.ErrorMessagesDisplayed().Should().BeTrue();
            Test.Pages.OrderForm.ClickOnErrorLink().Should().ContainEquivalentOf("selectAssociatedServicePrice");
        }

        [Given(@"the User selects an Associated Service to add")]
        public void GivenTheUserSelectsAnAssociatedServiceToAdd()
        {
            var itemId = Test.Pages.OrderForm.ClickRadioButton();
            Context.Add("ChosenItemId", itemId);
        }

        [Given(@"the User selects the flat variable price type")]
        public void GivenTheUserSelectsTheFlatVariablePriceType()
        {
            Test.Pages.OrderForm.ClickRadioButton(1);
        }

        [Given(@"the User selects the flat declarative price type")]
        public void GivenTheUserSelectsTheFlatDeclarativePriceType()
        {
            Test.Pages.OrderForm.ClickRadioButton(0);
        }

        [Then(@"all the available prices for that Associated Service are presented")]
        public void ThenAllTheAvailablePricesForThatAssociatedServiceArePresented()
        {
            Test.Pages.OrderForm.EditNamedSectionPageDisplayed("List price").Should().BeTrue();
            var itemId = (string)Context["ChosenItemId"];
            var query = "Select count(*) FROM [dbo].[CataloguePrice] where CatalogueItemId=@itemId";
            var expectedNumberOfPrices = SqlExecutor.Execute<int>(Test.BapiConnectionString, query, new { itemId }).Single();
            Test.Pages.OrderForm.NumberOfRadioButtonsDisplayed().Should().Be(expectedNumberOfPrices);
        }

        [Then(@"the name of the selected Associated Service is displayed on the Associated Service edit form")]
        public void ThenTheNameOfTheSelectedAssociatedServiceIsDisplayedOnTheAssociatedServiceEditForm()
        {
            var itemId = (string)Context["ChosenItemId"];
            var query = "Select Name FROM [dbo].[CatalogueItem] where CatalogueItemId=@itemId";
            var expectedSolutionName = SqlExecutor.Execute<string>(Test.BapiConnectionString, query, new { itemId }).Single();
            Test.Pages.OrderForm.TextDisplayedInPageTitle(expectedSolutionName).Should().BeTrue();
        }

        [Given(@"the User is presented with the Associated Service edit form for a variable flat price")]
        public void GivenTheUserIsPresentedWithTheAssociatedServiceEditFormForAVariableFlatPrice()
        {
            GivenTheUserIsPresentedWithThePricesForTheSelectedAssociatedService();
            GivenTheUserSelectsTheFlatVariablePriceType();
            new CommonSteps(Test, Context).WhenTheyChooseToContinue();
            new CatalogueSolutions(Test, Context).ThenTheyArePresentedWithTheOrderItemPriceEditForm();
        }

        [Given(@"the User is presented with the Associated Service edit form for a declarative flat price")]
        public void GivenTheUserIsPresentedWithTheAssociatedServiceEditFormForADeclarativeFlatPrice()
        {
            GivenTheUserIsPresentedWithThePricesForTheSelectedAssociatedService();
            GivenTheUserSelectsTheFlatVariablePriceType();
            new CommonSteps(Test, Context).WhenTheyChooseToContinue();
            new CatalogueSolutions(Test, Context).ThenTheyArePresentedWithTheOrderItemPriceEditForm();
        }

        [Given(@"fills in the Associated Service edit form with valid data")]
        public void GivenFillsInTheAssociatedServiceEditFormWithValidData()
        {
            if (Test.Pages.OrderForm.NumberOfRadioButtonsDisplayed() == 2)
            {
                Test.Pages.OrderForm.ClickRadioButton(1);
            }

            var f = new Faker();
            Test.Pages.OrderForm.EnterQuantity(f.Random.Number(min: 1).ToString());
            Test.Pages.OrderForm.EnterPriceInputValue(f.Finance.Amount().ToString());
        }

    }
}
