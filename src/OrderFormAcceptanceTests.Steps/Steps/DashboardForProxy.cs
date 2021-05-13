namespace OrderFormAcceptanceTests.Steps.Steps
{
    using FluentAssertions;
    using OrderFormAcceptanceTests.Steps.Utils;
    using TechTalk.SpecFlow;

    [Binding]
    internal sealed class DashboardForProxy : TestBase
    {
        public DashboardForProxy(UITest test, ScenarioContext context)
            : base(test, context)
        {
        }

        [Given(@"the user is logged in as a proxy buyer")]
        public void GivenTheUserIsLoggedInAsAProxyBuyer()
        {
            Test.Driver.Url.Should().Contain("/order/");
        }

        [When(@"the user chooses to create and manager orders")]
        public void WhenTheUserChoosesToCreateAndManagerOrders()
        {
            Test.Pages.OrganisationsOrdersDashboard.CreateNewOrderButtonDisplayed();
        }

        [Then(@"they are presented with the selected organisation's orders dashboard")]
        [Then(@"the organisation's order dashboard is presented")]
        public void ThenTheOrganisationsOrderDashboardIsPresented()
        {
            Test.Pages.OrganisationsOrdersDashboard.WaitForDashboardToBeDisplayed();
        }

        [Then(@"the selected organisation's name is displayed in the current organisation section")]
        public void ThenTheSelectedOrganisationSNameIsDisplayedInTheCurrentOrganisationSection()
        {
            Test.Pages.OrganisationsOrdersDashboard.WaitForDashboardToBeDisplayed();
        }

        [Then(@"there is no change to the organisation")]
        [Then(@"the current organisation section is presented")]
        public void ThenTheCurrentOrganisationSectionIsPresented()
        {
            Test.Pages.OrderForm.LoggedInDisplayNameIsDisplayed().Should().BeTrue();
        }

        [Then(@"there is a control to change organisation")]
        public void ThenThereIsAControlToChangeOrganisation()
        {
            Test.Pages.OrderForm.ChangeOrgLinkDisplayed();
        }

        [Given(@"the user is on the organisation's order dashboard")]
        public void GivenTheUserIsOnTheOrganisationsOrderDashboard()
        {
            GivenTheUserIsLoggedInAsAProxyBuyer();
            WhenTheUserChoosesToCreateAndManagerOrders();
            ThenTheOrganisationsOrderDashboardIsPresented();
        }

        [When(@"the user chooses to change organisation")]
        public void WhenTheUserChoosesToChangeOrganisation()
        {
            Test.Pages.OrderForm.ClickChangeOrgLink();
        }

        [Then(@"the user is presented with a list of all organisations they can create orders for")]
        public void ThenTheUserIsPresentedWithAListOfAllOrganisationsTheyCanCreateOrdersFor()
        {
            Test.Pages.OrderForm.NumberOfRadioButtonsDisplayed().Should().BeGreaterThan(0);
        }

        [Then(@"no organisation is pre-selected")]
        public void ThenNoOrganisationIsPre_Selected()
        {
            Test.Pages.OrderForm.IsRadioButtonSelected().Should().BeTrue();
        }

        [Given(@"the user selects an organisation")]
        public void GivenTheUserSelectsAnOrganisation()
        {
            Test.Pages.OrderForm.ClickRadioButton(0);
        }

        [When(@"the user chooses to continue without selecting an organisation")]
        [When(@"the user chooses to continue")]
        public void WhenTheUserChoosesToContinue()
        {
            Test.Pages.OrderForm.ClickContinueButton();
        }

        [Given(@"the user is on the change organisation page")]
        public void GivenTheUserIsOnTheChangeOrganisationPage()
        {
            GivenTheUserIsLoggedInAsAProxyBuyer();
            WhenTheUserChoosesToChangeOrganisation();
            ThenTheUserIsPresentedWithAListOfAllOrganisationsTheyCanCreateOrdersFor();
            Test.Pages.OrderForm.SelectRelatedOrgPageDisplayed()
            .Should().ContainEquivalentOf("select the organisation");
        }

        [When(@"the user chooses to go back")]
        public void WhenTheUserChoosesToGoBack()
        {
            Test.Pages.OrderForm.ClickBackLink();
        }

        [Then(@"an error message is presented")]
        public void ThenAnErrorMessageIsPresented()
        {
            Test.Pages.OrderForm.ErrorMessagesDisplayed();
        }

        [Given(@"the list of organisations the user can create orders for is displayed")]
        public void GivenTheListOfOrganisationsTheUserCanCreateOrdersForIsDisplayed()
        {
            GivenTheUserIsLoggedInAsAProxyBuyer();
            WhenTheUserChoosesToCreateAndManagerOrders();
            WhenTheUserChoosesToChangeOrganisation();
            ThenTheUserIsPresentedWithAListOfAllOrganisationsTheyCanCreateOrdersFor();
        }
    }
}
