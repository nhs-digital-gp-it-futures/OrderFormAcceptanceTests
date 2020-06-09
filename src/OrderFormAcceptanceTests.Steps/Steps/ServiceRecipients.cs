using FluentAssertions;
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
        [Given(@"the User chooses to edit the Service Recipient section")]
        public void ThenTheUserIsAbleToManageTheServiceRecipientsSection()
        {
            new CommonSteps(Test, Context).WhenTheOrderFormForTheExistingOrderIsPresented();
            Test.Pages.OrderForm.ClickEditServiceRecipients();
            Test.Pages.OrderForm.TaskListDisplayed().Should().BeFalse();
            Test.Pages.OrderForm.EditNamedSectionPageDisplayed("service recipients").Should().BeTrue();
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
            order.ServiceRecipientsViewed.Should().Be(1);
        }

        [Then(@"the Service Recipient is saved in the DB")]
        public void ThenTheServiceRecipientIsSavedInTheDB()
        {
            var order = (Order)Context["CreatedOrder"];
            var serviceRecipientInDB = new ServiceRecipient().RetrieveByOrderId(Test.ConnectionString, order.OrderId);
            serviceRecipientInDB.Should().NotBeNull();
        }

    }
}
