﻿namespace OrderFormAcceptanceTests.Steps.Steps
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using FluentAssertions;
    using OrderFormAcceptanceTests.Domain;
    using OrderFormAcceptanceTests.Steps.Utils;
    using OrderFormAcceptanceTests.TestData;
    using OrderFormAcceptanceTests.TestData.Helpers;
    using OrderFormAcceptanceTests.TestData.Models;
    using OrderFormAcceptanceTests.TestData.Utils;
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

        [StepDefinition(@"the primary organisation is presented as the default organisation")]
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

        [Then(@"the correct selected organisation's name is displayed in the current organisation section")]
        public void ThenTheSelectedOrganisationSNameIsDisplayedInTheCurrentOrganisationSection()
        {
            var organisation = Context.Get<string>(ContextKeys.RelatedOrganisationName);
            Test.Pages.OrderForm.TextDisplayedInPageTitle(organisation).Should().BeTrue();
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
        public async Task GivenTheUserSelectsAnOrganisationAsync()
        {
            var orgId = Test.Pages.OrderForm.ClickRadioButton(1);
            var organisation = await Organisation.GetOrganisationById(Test.IsapiConnectionString, new Guid(orgId));

            Context.Remove(ContextKeys.RelatedOrganisationName);
            Context.Add(ContextKeys.RelatedOrganisationName, organisation.Name);
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

        [Given(@"the user has previously selected an organisation")]
        public async Task GivenTheUserHasPreviouslySelectedAnOrganisationAsync()
        {
            GivenTheUserIsLoggedInAsAProxyBuyer();
            WhenTheUserChoosesToChangeOrganisation();

            var orgId = Test.Pages.OrderForm.ClickRadioButton(1);
            var organisation = await Organisation.GetOrganisationById(Test.IsapiConnectionString, new Guid(orgId));

            Context.Remove(ContextKeys.RelatedOrganisationName);
            Context.Add(ContextKeys.RelatedOrganisationName, organisation.Name);

            WhenTheUserChoosesToContinue();
            Test.Pages.OrganisationsOrdersDashboard.WaitForDashboardToBeDisplayed();
        }

        [When(@"the user chooses to change organization")]
        public void WhenTheUserChoosesToChangeOrganization()
        {
            Test.Driver.Navigate().Refresh();
            Test.Pages.OrganisationsOrdersDashboard.CreateNewOrderButtonDisplayed();
            Test.Pages.OrderForm.ClickChangeOrgLink();
        }

        [Given(@"the user has selected a customer Organisation to act on behalf of")]
        public async Task GivenTheUserHasSelectedACustomerOrganisationToActOnBehalfOfAsync()
        {
            var orgId = Test.Pages.OrderForm.ClickRadioButton(1);
            var organisation = await Organisation.GetOrganisationById(Test.IsapiConnectionString, new Guid(orgId));
            Context.Remove(ContextKeys.ExpectedUrl);
            Context.Add(ContextKeys.ExpectedUrl, organisation.OdsCode);
            Test.Pages.OrderForm.ClickContinueButton();
        }

        [Given(@"the user chooses to manage orders for the organisation")]
        public void GivenTheUserChoosesToManageOrdersForTheOrganisation()
        {
            Test.Pages.OrganisationsOrdersDashboard.CreateNewOrderButtonDisplayed();
            Test.Pages.OrganisationsOrdersDashboard.CreateNewOrder();
        }

        [Then(@"the user is taken back to a page with the correct Organisation ID in the URL")]
        public void ThenTheUserIsTakenBackToAPageWithTheCorrectOrganisationIDInTheURLAsync()
        {
            // using ODS Code rather than OrgId
            var expectedOdsCode = Context.Get<string>(ContextKeys.ExpectedUrl);
            Test.Driver.Url.Split('/').Last().Equals(expectedOdsCode);
        }
    }
}
