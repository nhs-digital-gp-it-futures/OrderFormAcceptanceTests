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
    class CallOffOrderingParty : TestBase
    {
        public CallOffOrderingParty(UITest test, ScenarioContext context) : base(test, context)
        {

        }

        [Then(@"the user is able to manage the Call-off Ordering Party section")]
        public void ThenTheUserIsAbleToManageTheCall_OffOrderingPartySection()
        {
            Test.Pages.OrderForm.ClickEditCallOffOrderingParty();
            Test.Pages.OrderForm.EditNamedSectionPageDisplayed("Call-off Ordering Party").Should().BeTrue();
        }

        [StepDefinition(@"the User chooses to edit the Call Off Ordering Party information")]
        public void WhenTheUserChoosesToEditTheCallOffOrderingPartyInformation()
        {
            new CommonSteps(Test, Context).WhenTheOrderFormForTheExistingOrderIsPresented();
            ThenTheUserIsAbleToManageTheCall_OffOrderingPartySection();
        }

        [Then(@"the Call Off Ordering Party ODS code is autopopulated from the User's organisation")]
        [Then(@"the User is unable to edit the ODS code")]
        public void ThenTheCallOffOrderingPartyODSCodeIsAutopopulatedFromTheUserSOrganisation()
        {
            Test.Pages.OrderForm.OdsCodeDisplayedAndNotEditable().Should().BeTrue();
        }

        [Then(@"the Call Off Ordering Party Organisation Name is autopopulated from the User's organisation")]
        [Then(@"the User is unable to edit the Organisation Name")]
        public void ThenTheCallOffOrderingPartyOrganisationNameIsAutopopulatedFromTheUserSOrganisation()
        {
            Test.Pages.OrderForm.OrganisationNameDisplayedAndNotEditable().Should().BeTrue();
        }

        [Then(@"the Call Off Ordering Party Organisation Address is autopopulated from the User's organisation")]
        [Then(@"the User is unable to edit Address")]
        public void ThenTheCallOffOrderingPartyOrganisationAddressIsAutopopulatedFromTheUserSOrganisation()
        {
            Test.Pages.OrderForm.OrganisationAddressDisplayedAndNotEditable().Should().BeTrue();
        }

        [Then(@"the Call Off Agreement ID is displayed in the page title")]
        public void ThenTheCallOffAgreementIDIsDisplayedInThePageTitle()
        {
            Test.Pages.OrderForm.CallOffIdDisplayedInPageTitle(((Order)Context["CreatedOrder"]).OrderId).Should().BeTrue();
        }


    }
}
