using FluentAssertions;
using OrderFormAcceptanceTests.Steps.Utils;
using OrderFormAcceptanceTests.TestData.Information;
using TechTalk.SpecFlow;

namespace OrderFormAcceptanceTests.Steps.Steps
{
    [Binding]
    class OrderForm : TestBase
    {
        public OrderForm(UITest test, ScenarioContext context) : base(test, context)
        {

        }

        [Then(@"there is the Order description section")]
        public void ThenThereIsTheOrderDescriptionSection()
        {
            Test.Pages.OrderForm.EditDescriptionSectionDisplayed().Should().BeTrue();
        }

        [Then(@"the user is able to manage the Order Description section")]
        public void ThenTheUserIsAbleToManageTheOrderDescriptionSection()
        {
            Test.Pages.OrderForm.ClickEditDescription();
            Test.Pages.OrderForm.EditNamedSectionPageDisplayed("Order description").Should().BeTrue();
        }

        [Given(@"the user is managing the Order Description section")]
        public void GivenTheUserIsManagingTheOrderDescriptionSection()
        {
            var common = new CommonSteps(Test, Context);
            common.GivenTheUserHasChosenToManageANewOrderForm();
            common.ThenTheNewOrderIsPresented();
            ThenTheUserIsAbleToManageTheOrderDescriptionSection();
        }

        [When(@"the User chooses to save")]
        [Given(@"the validation has been triggered")]
        public void WhenTheUserChoosesToSave()
        {
            Test.Pages.OrderForm.ClickSaveButton();
        }

        [Then(@"the reason is displayed")]
        public void ThenTheReasonIsDisplayed()
        {
            Test.Pages.OrderForm.ErrorTitle().Should().NotBeNullOrEmpty();
        }

        [Given(@"the User has entered data into a field that exceeds the maximum length of (.*) characters")]
        public void GivenTheUserHasEnteredDataIntoAFieldThatExceedsTheMaximumLength(int maxLength)
        {
            var randomText = RandomInformation.RandomString(maxLength + 1);
            Test.Pages.OrderForm.EnterTextIntoTextArea(randomText);
        }

        [When(@"the user selects an error link in the Error Summary")]
        public void WhenTheUserSelectsAnErrorLinkInTheErrorSummary()
        {
            var url = Test.Pages.OrderForm.ClickOnErrorLink();
            Context.Add("ExpectedUrl", url);
        }

        [Then(@"they will be navigated to the relevant part of the page")]
        public void ThenTheyWillBeNavigatedToTheRelevantPartOfThePage()
        {
            var expectedUrl = (string)Context["ExpectedUrl"];
            Test.Driver.Url.Should().Contain(expectedUrl);
        }

    }
}
