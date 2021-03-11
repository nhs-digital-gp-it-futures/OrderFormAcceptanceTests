namespace OrderFormAcceptanceTests.Steps.Steps
{
    using FluentAssertions;
    using OrderFormAcceptanceTests.Steps.Utils;
    using OrderFormAcceptanceTests.TestData;
    using TechTalk.SpecFlow;

    [Binding]
    public sealed class ServiceRecipients : TestBase
    {
        public ServiceRecipients(UITest test, ScenarioContext context)
            : base(test, context)
        {
        }

        [Then(@"they are presented with select Service Recipient form")]
        public void ThenTheyArePresentedWithSelectServiceRecipientForm()
        {
            Test.Pages.OrderForm.EditNamedSectionPageDisplayed("service recipients").Should().BeTrue();
        }

        [Then(@"the Call Off Ordering Party's Name \(organisation name\) and ODS code are presented as a Service Recipient")]
        public void ThenTheCallOffOrderingPartySNameOrganisationNameAndODSCodeArePresentedAsAServiceRecipient()
        {
            Test.Pages.OrderForm.ServiceRecipientsNameAndOdsDisplayed().Should().BeTrue();
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
            // this is an action to select the service recipient, one about is an assertion that the recipient is selected
            Test.Pages.OrderForm.ClickCheckbox();
        }
    }
}
