namespace OrderFormAcceptanceTests.Steps.Steps
{
    using System.Threading.Tasks;
    using FluentAssertions;
    using OrderFormAcceptanceTests.Domain;
    using OrderFormAcceptanceTests.Steps.Utils;
    using OrderFormAcceptanceTests.TestData.Helpers;
    using TechTalk.SpecFlow;

    [Binding]
    internal sealed class CallOffOrderingParty : TestBase
    {
        public CallOffOrderingParty(UITest test, ScenarioContext context)
            : base(test, context)
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
        public void ThenTheCallOffOrderingPartyOdsCodeIsAutoPopulatedFromTheUsersOrganisation()
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
        public void ThenTheCallOffAgreementIdIsDisplayedInThePageTitle()
        {
            Test.Pages.OrderForm.TextDisplayedInPageTitle(((Order)Context[ContextKeys.CreatedOrder]).CallOffId.ToString()).Should().BeTrue();
        }

        [Given(@"the user is managing the Call Off Ordering Party section")]
        public async Task GivenTheUserIsManagingTheCallOffOrderingPartySection()
        {
            await new CommonSteps(Test, Context).GivenAnIncompleteOrderExists();
            WhenTheUserChoosesToEditTheCallOffOrderingPartyInformation();
        }

        [Given(@"the user has entered a valid Call Off Ordering Party contact for the order")]
        [Given(@"the user has entered a valid supplier contact for the order")]
        public void GivenTheUserHasEnteredAValidCallOffOrderingPartyContactForTheOrder()
        {
            var contact = ContactHelper.Generate();
            Test.Pages.OrderForm.EnterContact(contact);
        }

        [Then(@"the Call Off Ordering Party section is saved in the DB")]
        public async Task ThenTheCallOffOrderingPartySectionIsSavedInTheDb()
        {
            var orderId = Context.Get<Order>(ContextKeys.CreatedOrder).Id;

            var orderingPartyInDb = (await DbContext.Order.FindAsync(orderId)).OrderingParty;
            var orderingPartyContactInDb = (await DbContext.Order.FindAsync(orderId)).OrderingPartyContact;

            orderingPartyInDb.Should().NotBeNull();
            orderingPartyContactInDb.Should().NotBeNull();
        }
    }
}
