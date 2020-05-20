using FluentAssertions;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
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

        [Then(@"there is the Call-off Ordering Party section")]
        public void ThenThereIsTheCall_OffOrderingPartySection()
        {
            Test.Pages.OrderForm.EditCallOffOrderingPartySectionDisplayed().Should().BeTrue();
        }

        [Then(@"the user is able to manage the Order Description section")]
        public void ThenTheUserIsAbleToManageTheOrderDescriptionSection()
        {
            Test.Pages.OrderForm.ClickEditDescription();
            Test.Pages.OrderForm.EditNamedSectionPageDisplayed("Order description").Should().BeTrue();
        }

        [Then(@"the user is able to manage the Call-off Ordering Party section")]
        public void ThenTheUserIsAbleToManageTheCall_OffOrderingPartySection()
        {
            Test.Pages.OrderForm.ClickEditCallOffOrderingParty();
            // TODO: At present it's accepted that an error page is displayed because the page does not exist yet
            //Test.Pages.OrderForm.EditNamedSectionPageDisplayed("call-off-ordering-party").Should().BeTrue();
            Test.Pages.OrderForm.ErrorTitle().Should().ContainEquivalentOf("call-off-ordering-party");
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
            Test.Pages.OrderForm.ErrorSummaryDisplayed().Should().BeTrue();
            Test.Pages.OrderForm.ErrorMessagesDisplayed().Should().BeTrue();
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

        [Given(@"the Order is saved for the first time")]
        public void GivenTheOrderIsSavedForTheFirstTime()
        {
            GivenTheUserIsManagingTheOrderDescriptionSection();
            GivenTheUserHasEnteredAValidDescriptionForTheOrder();
            WhenTheUserChoosesToSave();
            ThenTheOrderIsSaved();
            ThenTheCallOffAgreementIDIsGenerated();
            var id = (string)Context["CallOffAgreementId"];
            var order = new Order { OrderId = id }.Retrieve(Test.ConnectionString);
            Context.Add("CreatedOrder", order);
        }

        [Then(@"the content validation status of the (.*) section is (.*)")]
        public void ThenTheContentValidationStatusOfTheSectionIsComplete(string sectionName, string sectionStatus)
        {
            Test.Pages.OrderForm.SectionStatusTextMatchesExpected(sectionName, sectionStatus);
        }

        [Then(@"the Call Off Agreement ID is generated")]
        [Then(@"the Call Off Agreement ID is displayed")]
        public void ThenTheCallOffAgreementIDIsGenerated()
        {
            var id = Test.Pages.OrderForm.GetCallOffId();
            id.Should().NotBeNullOrEmpty();
            Context.Add("CallOffAgreementId", id);
        }

        [Then(@"the Order Description section is saved in the DB")]
        public void ThenTheOrderDescriptionSectionIsSavedInTheDB()
        {
            var expectedDescriptionValue = (string)Context["ExpectedDescriptionValue"];
            var id = Test.Pages.OrderForm.GetCallOffId();
            var order = new Order { OrderId = id }.Retrieve(Test.ConnectionString);
            order.Description.Should().BeEquivalentTo(expectedDescriptionValue);
            Context.Add("CreatedOrder", order);
        }

        [Given(@"the Call Off Ordering Party section is not complete")]
        public void GivenTheCallOffOrderingPartySectionIsNotComplete()
        {
            var order = (Order)Context["CreatedOrder"];
            // TODO: When datamodel has changed, we need to update Order class to have the call-off ordering party property, and set it to null here 
        }

        [When(@"the User navigates back to the Organisation's Orders dashboard")]
        public void WhenTheUserNavigatesBackToTheOrganisationSOrdersDashboard()
        {
            Test.Pages.OrderForm.ClickBackLink();
            Test.Pages.OrganisationsOrdersDashboard.WaitForDashboardToBeDisplayed();
        }

    }
}
