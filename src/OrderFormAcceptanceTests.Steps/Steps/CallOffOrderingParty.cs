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
            Test.Pages.OrderForm.AddressDisplayedAndNotEditable().Should().BeTrue();
        }

        [Then(@"the Call Off Agreement ID is displayed in the page title")]
        public void ThenTheCallOffAgreementIDIsDisplayedInThePageTitle()
        {
            Test.Pages.OrderForm.CallOffIdDisplayedInPageTitle(((Order)Context["CreatedOrder"]).OrderId).Should().BeTrue();
        }

        [Given(@"the user is managing the Call Off Ordering Party section")]
        public void GivenTheUserIsManagingTheCallOffOrderingPartySection()
        {
            new CommonSteps(Test, Context).GivenAnUnsubmittedOrderExists();
            new OrderForm(Test, Context).GivenTheCallOffOrderingPartySectionIsNotComplete();
            WhenTheUserChoosesToEditTheCallOffOrderingPartyInformation();
        }

        [Given(@"the user has entered a valid Call Off Ordering Party contact for the order")]
        [Given(@"the user has entered a valid supplier contact for the order")]
        public void GivenTheUserHasEnteredAValidCallOffOrderingPartyContactForTheOrder()
        {
            var contact = new Contact().Generate();
            Test.Pages.OrderForm.EnterContact(contact);
            Context.Add("ExpectedContact", contact);
        }

        [Given(@"makes a note of the autopopulated Ordering Party details")]
        public void GivenMakesANoteOfTheAutopopulatedOrderingPartyDetails()
        {
            var order = (Order)Context["CreatedOrder"];
            order.OrganisationOdsCode = Test.Pages.OrderForm.GetOdsCode();
            order.OrganisationName = Test.Pages.OrderForm.GetOrganisationName();
            var address = Test.Pages.OrderForm.GetAddress();
            Context.Add("ExpectedAddress", address);
        }

        [Then(@"the Call Off Ordering Party section is saved in the DB")]
        public void ThenTheCallOffOrderingPartySectionIsSavedInTheDB()
        {
            var id = Test.Pages.OrderForm.GetCallOffId();
            var order = new Order { OrderId = id }.Retrieve(Test.ConnectionString);
            var dbContact = new Contact { ContactId = order.OrganisationContactId }.Retrieve(Test.ConnectionString);
            Context.Remove("CreatedContact");
            Context.Add("CreatedContact", dbContact);

            var dbAddress = new Address { AddressId = order.OrganisationAddressId }.Retrieve(Test.ConnectionString);
            Context.Remove("CreatedAddress");
            Context.Add("CreatedAddress", dbAddress);

            var expectedContact = (Contact)Context["ExpectedContact"];
            dbContact.Equals(expectedContact);

            var expectedAddress = (Address)Context["ExpectedAddress"];
            dbAddress.Equals(expectedAddress);
        }

    }
}
