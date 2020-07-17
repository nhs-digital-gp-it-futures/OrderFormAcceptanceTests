using FluentAssertions;
using OrderFormAcceptanceTests.Actions.Utils;
using OrderFormAcceptanceTests.Steps.Utils;
using OrderFormAcceptanceTests.TestData;
using System;
using System.Collections.Generic;
using System.Text;
using TechTalk.SpecFlow;

namespace OrderFormAcceptanceTests.Steps.Steps
{
    [Binding]
    public sealed class ServiceRecipients : TestBase
    {
        public ServiceRecipients(UITest test, ScenarioContext context) : base(test, context)
        {
        }

        [Then(@"the user is able to manage the Service Recipients section")]
        public void ThenTheUserIsAbleToManageTheServiceRecipientsSection()
        {
            Test.Pages.OrderForm.ClickEditServiceRecipients();
            Test.Pages.OrderForm.TaskListDisplayed().Should().BeFalse();
            Test.Pages.OrderForm.EditNamedSectionPageDisplayed("service recipients").Should().BeTrue();
        }

        [Given(@"the User chooses to edit the Service Recipient section")]
        public void GivenTheUserChoosesToManageTheServiceRecipientsSection()
        {
            new CommonSteps(Test, Context).WhenTheOrderFormForTheExistingOrderIsPresented();
            ThenTheUserIsAbleToManageTheServiceRecipientsSection();
        }

        [Then(@"the Call Off Ordering Party's Name \(organisation name\) and ODS code are presented as a Service Recipient")]
        public void ThenTheCallOffOrderingPartySNameOrganisationNameAndODSCodeArePresentedAsAServiceRecipient()
        {
            Test.Pages.OrderForm.ServiceRecipientsNameAndOdsDisplayed().Should().BeTrue();
        }

        [Then(@"the User is able to select the Call Off Ordering Party")]
        public void ThenTheUserIsAbleToSelectTheCallOffOrderingParty()
        {
            Test.Pages.OrderForm.NumberOfCheckboxesDisplayed().Should().BeGreaterThan(0);
        }

        [When(@"the User chooses to select all")]
        [When(@"the User chooses to deselect all")]
        public void WhenTheUserChoosesToSelectAll()
        {
            Test.Pages.OrderForm.ClickSelectDeselectAll();
        }

        [Then(@"the Select all button changes to Deselect all")]
        public void ThenTheSelectAllButtonChangesToDeselectAll()
        {
            Test.Pages.OrderForm.GetSelectDeselectAllText().Should().BeEquivalentTo("Deselect all");
        }

        [Then(@"the Deselect all button changes to Select all")]
        public void ThenTheDeselectAllButtonChangesToSelectAll()
        {
            Test.Pages.OrderForm.GetSelectDeselectAllText().Should().BeEquivalentTo("Select all");
        }

        [Then(@"the Call Off Ordering Party is selected")]
        public void ThenTheCallOffOrderingPartyIsSelected()
        {
            Test.Pages.OrderForm.IsCheckboxChecked().Should().BeTrue();
        }

        [Then(@"the selected Call Off Ordering Party presented is deselected")]
        public void ThenTheSelectedCallOffOrderingPartyPresentedIsDeselected()
        {
            Test.Pages.OrderForm.IsCheckboxChecked().Should().BeFalse();
        }

        [Given(@"the Call Off Ordering Party is selected")]
        public void GivenTheCallOffOrderingPartyIsSelected()
        {
            //this is an action to select the service recipient, one about is an assertion that the recipient is selected
            Test.Pages.OrderForm.ClickCheckbox();
        }

        [Then(@"the Service Recipient section is saved in the DB")]
        public void ThenTheServiceRecipientSectionIsSavedInTheDB()
        {
            var order = (Order)Context["CreatedOrder"];
            order.Retrieve(Test.ConnectionString).ServiceRecipientsViewed.Should().Be(1);
        }

        [Then(@"the Service Recipient is saved in the DB")]
        public void ThenTheServiceRecipientIsSavedInTheDB()
        {
            var order = (Order)Context["CreatedOrder"];
            var serviceRecipientInDB = new ServiceRecipient().RetrieveByOrderId(Test.ConnectionString, order.OrderId);
            Context.Add("CreatedServiceRecipient", serviceRecipientInDB);
            serviceRecipientInDB.Should().NotBeNull();            
        }

        [Then(@"the Service Recipient is deleted from the Order")]
        [Then(@"all the Service Recipients are deleted from the Order")]
        public void ThenTheServiceRecipientIsDeletedFromTheOrder()
        {
            var order = (Order)Context["CreatedOrder"];
            var serviceRecipientInDB = new ServiceRecipient().RetrieveByOrderId(Test.ConnectionString, order.OrderId);
            serviceRecipientInDB.Should().BeNullOrEmpty();
            serviceRecipientInDB = ((ServiceRecipient)Context["CreatedServiceRecipient"]).Retrieve(Test.ConnectionString);
            serviceRecipientInDB.Should().BeNullOrEmpty();
        }

        [When(@"the User deselects a Service Recipient that have been previously saved in the Order")]
        [StepDefinition(@"the User deselects all Service Recipients that have been previously saved in the Order")]
        public void WhenTheUserDeselectsAServiceRecipientThatHaveBeenPreviouslySavedInTheOrder()
        {
            GivenTheUserChoosesToManageTheServiceRecipientsSection();
            WhenTheUserChoosesToSelectAll();
            ThenTheSelectAllButtonChangesToDeselectAll();
            WhenTheUserChoosesToSelectAll();
            ThenTheSelectedCallOffOrderingPartyPresentedIsDeselected();
            new CommonSteps(Test, Context).WhenTheyChooseToContinue();
            new OrderForm(Test, Context).ThenTheOrderIsSaved();
        }

        [When(@"the User adds a Service Recipient to the Service Recipient section")]
        public void WhenTheUserAddsAServiceRecipientToTheServiceRecipientSection()
        {
            ThenTheUserIsAbleToManageTheServiceRecipientsSection();
            GivenTheCallOffOrderingPartyIsSelected();
            new CommonSteps(Test, Context).WhenTheyChooseToContinue();
            new OrderForm(Test, Context).ThenTheOrderIsSaved();
        }
    }
}
