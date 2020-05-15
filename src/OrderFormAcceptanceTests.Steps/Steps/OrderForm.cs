using FluentAssertions;
using OrderFormAcceptanceTests.Steps.Utils;
using OrderFormAcceptanceTests.TestData;
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

        [Given(@"the user has entered a valid description for the order")]
        public void GivenTheUserHasEnteredAValidDescriptionForTheOrder()
        {
            var randomText = RandomInformation.RandomString(99);
            Test.Pages.OrderForm.EnterTextIntoTextArea(randomText);
            Context.Add("ExpectedDescriptionValue", randomText);
        }

        [Then(@"the Order is saved")]
        public void ThenTheOrderIsSaved()
        {
            Test.Pages.OrderForm.TaskListDisplayed().Should().BeTrue();
        }

        [Then(@"the content validation status of the (.*) section is (.*)")]
        public void ThenTheContentValidationStatusOfTheSectionIsComplete(string sectionName, string sectionStatus)
        {
            Test.Pages.OrderForm.SectionStatusTextMatchesExpected(sectionName, sectionStatus);
        }

        [Then(@"the Call Off Agreement ID is generated")]
        public void ThenTheCallOffAgreementIDIsGenerated()
        {
            var id = Test.Pages.OrderForm.GetCallOffId();
            id.Should().NotBeNullOrEmpty();
        }

        [Then(@"the Order Description section is saved in the DB")]
        public void ThenTheOrderDescriptionSectionIsSavedInTheDB()
        {
            var expectedDescriptionValue = (string)Context["ExpectedDescriptionValue"];
            var id = Test.Pages.OrderForm.GetCallOffId();
            var order = new Order { OrderId = id }.Retrieve(Test.ConnectionString);
            order.Description.Should().BeEquivalentTo(expectedDescriptionValue);

        }

    }
}
