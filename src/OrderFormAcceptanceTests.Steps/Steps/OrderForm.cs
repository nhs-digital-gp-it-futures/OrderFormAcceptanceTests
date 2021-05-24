namespace OrderFormAcceptanceTests.Steps.Steps
{
    using System.Linq;
    using System.Threading.Tasks;
    using FluentAssertions;
    using Microsoft.EntityFrameworkCore;
    using OpenQA.Selenium;
    using OrderFormAcceptanceTests.Domain;
    using OrderFormAcceptanceTests.Steps.Utils;
    using OrderFormAcceptanceTests.TestData.Information;
    using TechTalk.SpecFlow;

    [Binding]
    internal sealed class OrderForm : TestBase
    {
        public OrderForm(UITest test, ScenarioContext context)
            : base(test, context)
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

        [Then(@"there is no Service Recipient section")]
        public void ThenThereIsNoServiceRecipientSection()
        {
            var taskListSections = Test.Driver.FindElements(By.CssSelector(".bc-c-task-list__task-name"));
            taskListSections.Where(e => e.Text.Contains("service recipients", System.StringComparison.OrdinalIgnoreCase)).Should().BeEmpty();
        }

        [Then(@"there is the Catalogue Solutions section")]
        public void ThenThereIsTheCatalogueSolutionsSection()
        {
            Test.Pages.OrderForm.EditCatalogueSolutionsSectionDisplayed().Should().BeTrue();
        }

        [Then(@"there is the Additional Service section")]
        public void ThenThereIsTheAdditionalServiceSection()
        {
            Test.Pages.OrderForm.EditAdditionalServicesSectionDisplayed().Should().BeTrue();
        }

        [Then(@"they are presented with the Select Associated Service form")]
        [Then(@"there is the Associated Services section")]
        public void ThenThereIsTheAssociatedServicesSection()
        {
            Test.Pages.OrderForm.EditAssociatedServicesSectionDisplayed().Should().BeTrue();
        }

        [Then(@"there is the Funding Source section")]
        public void ThenThereIsTheFundingSourceSection()
        {
            Test.Pages.OrderForm.EditFundingSourceSectionDisplayed().Should().BeTrue();
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

        [StepDefinition(@"the User chooses to save")]
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
            Context.Add(ContextKeys.ExpectedUrl, url);
        }

        [Then(@"they will be navigated to the relevant part of the page")]
        public void ThenTheyWillBeNavigatedToTheRelevantPartOfThePage()
        {
            var expectedUrl = (string)Context[ContextKeys.ExpectedUrl];
            Test.Driver.Url.Should().Contain(expectedUrl);
        }

        [Given(@"the user has entered a valid description for the order")]
        public void GivenTheUserHasEnteredAValidDescriptionForTheOrder()
        {
            var randomText = RandomInformation.RandomString(99);
            Test.Pages.OrderForm.EnterTextIntoTextArea(randomText);
        }

        [Then(@"they are presented with the Order dashboard")]
        [Then(@"the Order is saved")]
        [Then(@"the Order dashboard is presented")]
        [Then(@"the Order tasklist is presented")]
        public void ThenTheOrderIsSaved()
        {
            Test.Pages.OrderForm.TaskListDisplayed().Should().BeTrue();
            var (success, callOffId) = CallOffId.Parse(Test.Pages.OrderForm.GetCallOffId());
            var order = DbContext.Order.Single(o => o.Id == callOffId.Id);

            Context.TryAdd(ContextKeys.CreatedOrder, order);
        }

        [Then(@"the content validation status of the (.*) section is complete")]
        public void ThenTheContentValidationStatusOfTheSectionIsComplete(string sectionName)
        {
            Test.Pages.OrderForm.TaskListDisplayed();
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
        }

        [Then(@"the Order Description section is saved in the DB")]
        public async Task ThenTheOrderDescriptionSectionIsSavedInTheDB()
        {
            var orderId = Context.Get<Order>(ContextKeys.CreatedOrder).Id;

            (await DbContext.Order.FindAsync(orderId)).Description.Should().NotBeNullOrEmpty();
        }

        [Given(@"the Catalogue Solution section is complete")]
        public async Task GivenTheCatalogueSolutionSectionIsComplete()
        {
            await new CommonSteps(Test, Context).SetOrderCatalogueSectionToComplete();
        }

        [Given(@"the Additional Services section is complete")]
        public async Task GivenTheAdditionalServicesSectionIsComplete()
        {
            await new CommonSteps(Test, Context).SetOrderAdditionalServicesSectionToComplete();
        }

        [Given(@"the Additional Services section is not complete")]
        public async Task GivenTheAdditionalServicesSectionIsNotCompleteAndNoServicesAreAdded()
        {
            var commonSteps = new CommonSteps(Test, Context);

            await commonSteps.SetOrderCatalogueSectionToComplete();
        }

        [Given(@"the Associated Services section is not complete")]
        public async Task GivenTheAssociatedServicesSectionIsNotCompleteAndNoServicesAreAdded()
        {
            var commonSteps = new CommonSteps(Test, Context);

            await commonSteps.SetOrderCatalogueSectionToComplete();
            await commonSteps.SetOrderAdditionalServicesSectionToComplete();
        }

        [Given(@"the Funding Source section is complete with 'no' selected")]
        [Given(@"the Funding Source section is complete")]
        public async Task GivenTheFundingSourceSectionIsCompleteWithNoSelected()
        {
            var order = (Order)Context[ContextKeys.CreatedOrder];
            order.FundingSourceOnlyGms = false;
            DbContext.Update(order);
            await DbContext.SaveChangesAsync();
        }

        [Given(@"the Funding Source section is complete with 'yes' selected")]
        public async Task GivenTheFundingSourceSectionIsCompleteWithYesSelected()
        {
            var order = (Order)Context[ContextKeys.CreatedOrder];
            order.FundingSourceOnlyGms = true;
            DbContext.Update(order);
            await DbContext.SaveChangesAsync();
        }

        [When(@"the User navigates back to the Organisation's Orders dashboard")]
        public void WhenTheUserNavigatesBackToTheOrganisationSOrdersDashboard()
        {
            Test.Pages.OrderForm.ClickBackLink();
            Test.Pages.OrganisationsOrdersDashboard.WaitForDashboardToBeDisplayed();
        }

        [Then(@"the Order description section is enabled")]
        public void ThenTheOrderDescriptionSectionIsEnabled()
        {
            Test.Pages.OrderForm.OrderDescriptionEnabled().Should().BeTrue();
        }

        [Then(@"the Call-off Ordering Party section is enabled")]
        public void ThenTheCall_OffOrderingPartySectionIsEnabled()
        {
            Test.Pages.OrderForm.CallOffOrderingPartyEnabled().Should().BeTrue();
        }

        [Then(@"the Supplier section is enabled")]
        public void ThenTheSupplierSectionIsEnabled()
        {
            Test.Pages.OrderForm.SupplierInformationEnabled().Should().BeTrue();
        }

        [Then(@"the Commencement date section is enabled")]
        public void ThenTheCommencementDateSectionIsEnabled()
        {
            Test.Pages.OrderForm.CommencementDateEnabled().Should().BeTrue();
        }

        [Then(@"the Catalogue Solution section is enabled")]
        public void ThenTheCatalogueSolutionSectionIsEnabled()
        {
            Test.Pages.OrderForm.EditCatalogueSolutionsSectionIsEnabled().Should().BeTrue();
        }

        [Then(@"the Catalogue Solution section is not enabled")]
        public void ThenTheCatalogueSolutionSectionIsNotEnabled()
        {
            Test.Pages.OrderForm.EditCatalogueSolutionsSectionIsEnabled().Should().BeFalse();
        }

        [Then(@"the Additional Service section is enabled")]
        public void ThenTheAdditionalServiceSectionIsEnabled()
        {
            Test.Pages.OrderForm.EditAdditionalServicesSectionIsEnabled().Should().BeTrue();
        }

        [Then(@"the Additional Service section is not enabled")]
        public void ThenTheAdditionalServiceSectionIsNotEnabled()
        {
            Test.Pages.OrderForm.EditAdditionalServicesSectionIsEnabled().Should().BeFalse();
        }

        [Then(@"the Associated Service section is enabled")]
        public void ThenTheAssociatedServiceSectionIsEnabled()
        {
            Test.Pages.OrderForm.EditAssociatedServicesSectionIsEnabled().Should().BeTrue();
        }

        [Then(@"the Associated Service section is not enabled")]
        public void ThenTheAssociatedServiceSectionIsNotEnabled()
        {
            Test.Pages.OrderForm.EditAssociatedServicesSectionIsEnabled().Should().BeFalse();
        }

        [Then(@"the Funding Source section is enabled")]
        public void ThenTheFundingSourceSectionIsEnabled()
        {
            Test.Pages.OrderForm.EditFundingSourceSectionIsEnabled().Should().BeTrue();
        }

        [Then(@"the Funding Source section is not enabled")]
        public void ThenTheFundingSourceSectionIsNotEnabled()
        {
            Test.Pages.OrderForm.EditFundingSourceSectionIsEnabled().Should().BeFalse();
        }
    }
}
