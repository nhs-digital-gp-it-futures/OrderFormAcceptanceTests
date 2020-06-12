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

        [Then(@"there is the Supplier section")]
        public void ThenThereIsTheSupplierSection()
        {
            Test.Pages.OrderForm.EditSupplierSectionDisplayed().Should().BeTrue();
        }

        [Then(@"there is the Commencement date section")]
        public void ThenThereIsTheCommencementDateSection()
        {
            Test.Pages.OrderForm.EditCommencementDateSectionDisplayed().Should().BeTrue();
        }

        [Then(@"there is the Service Recipients section")]
        public void ThenThereIsTheServiceRecipientsSection()
        {
            Test.Pages.OrderForm.EditServiceRecipientsSectionDisplayed().Should().BeTrue();
        }

        [Then(@"there is the Catalogue Solutions section")]
        public void ThenThereIsTheCatalogueSolutionsSection()
        {
            Test.Pages.OrderForm.EditCatalogueSolutionsSectionDisplayed().Should().BeTrue();
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
            Test.Pages.OrderForm.ErrorSummaryDisplayed().Should().BeTrue();
            Test.Pages.OrderForm.ErrorMessagesDisplayed().Should().BeTrue();
        }

        [Given(@"the User has entered data into a field that exceeds the maximum length of (.*) characters")]
        public void GivenTheUserHasEnteredDataIntoAFieldThatExceedsTheMaximumLength(int maxLength)
        {
            var randomText = RandomInformation.RandomString(maxLength + 1);
            if (Test.Driver.FindElements(By.TagName("textarea")).Count > 0)
            {
                Test.Pages.OrderForm.EnterTextIntoTextArea(randomText);
            }
            else
            {
                Test.Pages.OrderForm.EnterTextIntoTextField(randomText);
            }
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
        [Then(@"the Order dashboard is presented")]
        [Then(@"the Order tasklist is presented")]
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

        [Then(@"the content validation status of the (.*) section is complete")]
        public void ThenTheContentValidationStatusOfTheSectionIsComplete(string sectionName)
        {
            Test.Pages.OrderForm.SectionComplete(sectionName).Should().BeTrue();
        }

        [Then(@"the content validation status of the (.*) section is incomplete")]
        public void ThenTheContentValidationStatusOfTheSupplierSectionIsIncomplete(string sectionName)
        {
            Test.Pages.OrderForm.SectionComplete(sectionName).Should().BeFalse();
        }

        [Then(@"the Order description is displayed")]
        public void ThenTheOrderDescriptionIsDisplayed()
        {
            Test.Pages.OrderForm.GetOrderDescription().Should().NotBeNullOrEmpty();
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
            order.OrganisationAddressId = null;
            order.OrganisationBillingAddressId = null;
            order.OrganisationContactId = null;
            order.Update(Test.ConnectionString);
        }

        [Given(@"the Supplier section is not complete")]
        public void GivenTheSupplierSectionIsNotComplete()
        {
            var order = (Order)Context["CreatedOrder"];
            order.SupplierAddressId = null;
            order.SupplierContactId = null;
            order.SupplierId = null;
            order.SupplierName = null;
            order.Update(Test.ConnectionString);
        }

        [Given(@"the Commencement Date section is not complete")]
        public void GivenTheCommencementDateSectionIsNotComplete()
        {
            var order = (Order)Context["CreatedOrder"];
            order.CommencementDate = null;
            order.Update(Test.ConnectionString);
        }

        [Given(@"the Service Recipients section is not complete")]
        public void GivenTheServiceRecipientsSectionIsNotComplete()
        {
            var order = (Order)Context["CreatedOrder"];
            order.ServiceRecipientsViewed = 0;
            order.Update(Test.ConnectionString);

            var serviceRecipient = (ServiceRecipient)Context["CreatedServiceRecipient"];
            serviceRecipient.Delete(Test.ConnectionString);
            Context.Remove("CreatedServiceRecipient");
        }

        [Given(@"the Service Recipients section is complete")]
        public void GivenTheServiceRecipientsSectionIsComplete()
        {
            var order = (Order)Context["CreatedOrder"];
            order.ServiceRecipientsViewed = 1;
            order.Update(Test.ConnectionString);

            var serviceRecipient = new ServiceRecipient().Generate(order.OrderId, order.OrganisationOdsCode);
            serviceRecipient.Create(Test.ConnectionString);
            Context.Add("CreatedServiceRecipient", serviceRecipient);
        }

        [Given(@"the Catalogue Solutions section is not complete")]
        public void GivenTheCatalogueSolutionsSectionIsNotComplete()
        {
            var order = (Order)Context["CreatedOrder"];
            order.CatalogueSolutionsViewed = 0;
            order.Update(Test.ConnectionString);
        }

        [Given(@"the Catalogue Solution section is complete")]
        public void GivenTheCatalogueSolutionSectionIsComplete()
        {
            var order = (Order)Context["CreatedOrder"];
            order.CatalogueSolutionsViewed = 1;
            order.Update(Test.ConnectionString);
        }

        [When(@"the User navigates back to the Organisation's Orders dashboard")]
        public void WhenTheUserNavigatesBackToTheOrganisationSOrdersDashboard()
        {
            Test.Pages.OrderForm.ClickBackLink();
            Test.Pages.OrganisationsOrdersDashboard.WaitForDashboardToBeDisplayed();
        }

    }
}
