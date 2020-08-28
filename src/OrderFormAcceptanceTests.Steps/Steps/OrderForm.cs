using FluentAssertions;
using OpenQA.Selenium;
using OrderFormAcceptanceTests.Steps.Utils;
using OrderFormAcceptanceTests.TestData;
using OrderFormAcceptanceTests.TestData.Information;
using System.Collections.Generic;
using System.Linq;
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
            Context.Add(ContextKeys.ExpectedDescriptionValue, randomText);
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
            var id = (string)Context[ContextKeys.CallOffAgreementId];
            var order = new Order { OrderId = id }.Retrieve(Test.ConnectionString);
            Context.Add(ContextKeys.CreatedOrder, order);
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
            Context.Add(ContextKeys.CallOffAgreementId, id);
        }

        [Then(@"the Order Description section is saved in the DB")]
        public void ThenTheOrderDescriptionSectionIsSavedInTheDB()
        {
            var expectedDescriptionValue = (string)Context[ContextKeys.ExpectedDescriptionValue];
            var id = Test.Pages.OrderForm.GetCallOffId();
            var order = new Order { OrderId = id }.Retrieve(Test.ConnectionString);
            order.Description.Should().BeEquivalentTo(expectedDescriptionValue);
            Context.Add(ContextKeys.CreatedOrder, order);
        }

        [Given(@"the Call Off Ordering Party section is not complete")]
        public void GivenTheCallOffOrderingPartySectionIsNotComplete()
        {
            var order = (Order)Context[ContextKeys.CreatedOrder];
            order.OrganisationAddressId = null;
            order.OrganisationBillingAddressId = null;
            order.OrganisationContactId = null;
            order.Update(Test.ConnectionString);
        }

        [Given(@"the Supplier section is not complete")]
        public void GivenTheSupplierSectionIsNotComplete()
        {
            var order = (Order)Context[ContextKeys.CreatedOrder];
            order.SupplierAddressId = null;
            order.SupplierContactId = null;
            order.SupplierId = null;
            order.SupplierName = null;
            order.Update(Test.ConnectionString);
        }

        [Given(@"the Commencement Date section is not complete")]
        public void GivenTheCommencementDateSectionIsNotComplete()
        {
            var order = (Order)Context[ContextKeys.CreatedOrder];
            order.CommencementDate = null;
            order.Update(Test.ConnectionString);
        }

        [Given(@"the Service Recipients section is not complete")]
        public void GivenTheServiceRecipientsSectionIsNotComplete()
        {
            var order = (Order)Context[ContextKeys.CreatedOrder];
            order.ServiceRecipientsViewed = 0;
            order.Update(Test.ConnectionString);

            var serviceRecipient = (ServiceRecipient)Context[ContextKeys.CreatedServiceRecipient];
            serviceRecipient.Delete(Test.ConnectionString);
            Context.Remove(ContextKeys.CreatedServiceRecipient);
        }

        [Given(@"the Service Recipients section is complete")]
        public void GivenTheServiceRecipientsSectionIsComplete()
        {
            var order = (Order)Context[ContextKeys.CreatedOrder];
            order.ServiceRecipientsViewed = 1;
            order.Update(Test.ConnectionString);

            var serviceRecipient = new ServiceRecipient().Generate(order.OrderId, order.OrganisationOdsCode);
            serviceRecipient.Create(Test.ConnectionString);
            Context.Add(ContextKeys.CreatedServiceRecipient, serviceRecipient);
        }

        [Given(@"the Catalogue Solutions section is not complete")]
        public void GivenTheCatalogueSolutionsSectionIsNotComplete()
        {
            var order = (Order)Context[ContextKeys.CreatedOrder];
            order.CatalogueSolutionsViewed = 0;
            order.Update(Test.ConnectionString);
        }

        [Given(@"the Catalogue Solution section is complete")]
        public void GivenTheCatalogueSolutionSectionIsComplete()
        {
            var order = (Order)Context[ContextKeys.CreatedOrder];
            order.CatalogueSolutionsViewed = 1;
            order.Update(Test.ConnectionString);
        }

        [Given(@"the Additional Services section is complete")]
        public void GivenTheAdditionalServicesSectionIsComplete()
        {
            var order = (Order)Context[ContextKeys.CreatedOrder];
            order.AdditionalServicesViewed = 1;
            order.Update(Test.ConnectionString);
        }

        [Given(@"the Additional Services section is not complete")]
        public void GivenTheAdditionalServicesSectionIsNotCompleteAndNoServicesAreAdded()
        {
            var order = (Order)Context[ContextKeys.CreatedOrder];
            order.AdditionalServicesViewed = 0;
            IEnumerable<OrderItem> items = new OrderItem().RetrieveByOrderId(Test.ConnectionString, order.OrderId, 2);
            foreach (var item in items)
            {
                item.Delete(Test.ConnectionString);
            }
            order.Update(Test.ConnectionString);
        }

        [Given(@"the Associated Services section is not complete")]
        public void GivenTheAssociatedServicesSectionIsNotCompleteAndNoServicesAreAdded()
        {
            var order = (Order)Context[ContextKeys.CreatedOrder];
            order.AssociatedServicesViewed = 0;
            IEnumerable<OrderItem> items = new OrderItem().RetrieveByOrderId(Test.ConnectionString, order.OrderId, 3);
            foreach (var item in items)
            {
                item.Delete(Test.ConnectionString);
            }
            order.Update(Test.ConnectionString);
        }

        [Given(@"the Funding Source section is not complete")]
        public void GivenTheFundingSourceSectionIsNotComplete()
        {
            var order = (Order)Context[ContextKeys.CreatedOrder];
            order.FundingSourceOnlyGMS = null;
            order.Update(Test.ConnectionString);
        }

        [Given(@"the Funding Source section is complete with 'no' selected")]
        [Given(@"the Funding Source section is complete")]
        public void GivenTheFundingSourceSectionIsCompleteWithNoSelected()
        {
            var order = (Order)Context[ContextKeys.CreatedOrder];
            order.FundingSourceOnlyGMS = 0;
            order.Update(Test.ConnectionString);
        }

        [Given(@"the Funding Source section is complete with 'yes' selected")]
        public void GivenTheFundingSourceSectionIsCompleteWithYesSelected()
        {
            var order = (Order)Context[ContextKeys.CreatedOrder];
            order.FundingSourceOnlyGMS = 1;
            order.Update(Test.ConnectionString);
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
